
using Mallaca;
using Newtonsoft.Json.Linq;
using RH_APP.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Mallaca.Network;
using System.Threading;
using Mallaca.Network.Packet.Request;

namespace RH_APP.Controller
{
// ReSharper disable once InconsistentNaming
    class RH_Controller
    {
        private const int _readOffsetMillis = 500;
        private readonly IBike _bike;
        private readonly BackgroundWorker _bw = new BackgroundWorker();
        private readonly List<Measurement> _data = new List<Measurement>();
        private readonly Queue<String> _queue = new Queue<string>();
        private int _pulse;
        private bool ignoreNext = false;

        private DateTime _lastSuccesfullRead;
        public Measurement LatestMeasurement
        {
            get
            {
                var m = new Measurement();
                try
                {
                    return _data.Last();
                }
                catch (InvalidOperationException) { }
                return m;
            }
        }

        public void Stop()
        {
            _bw.CancelAsync();
            UpdatedList -= SendToServer;
        }

        public RH_Controller(IBike b, bool sendToServer = false)
        {
            SendCommand(String.Format("CM"));
            _lastSuccesfullRead = DateTime.Now;
            //bike = new Classes.COM_Bike("COM3");
            //bike = new Classes.STUB_Bike();
            _bike = b;
            if(sendToServer)
                UpdatedList += SendToServer;
            InitializeBackgroundWorker();
            _bw.RunWorkerAsync();
            
        }

        public void SetPower(int power)
        {
            //SendCommand(String.Format("CM"));
            SendCommand(String.Format("PW {0}", power));
        }

        public void SetPulse(int pulse)
        {
            _pulse = pulse;
        }

        public void Reset()
        {
            SendCommand(String.Format("RS"));
        }

        public void SendCommand(string cmd)
        {
            _queue.Enqueue(cmd);
            Console.WriteLine("Send following command to the bike: " + cmd);
        }
        
        public event EventHandler UpdatedList;

        private void OnUpdatedList(MeasurementEventArgs e)
        {
            var handler = UpdatedList;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void WriteAllDataToFile()
        {
            _bw.CancelAsync();
            var filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (var writer = new StreamWriter(filepath + "\\RH_DATA.txt", append: false))
            {
                foreach (var measurement in _data)
                {
                    writer.WriteLine(measurement.toProtocolString());
                }
                writer.Flush();
            }

        }

        public List<Measurement> GetList()
        {
            return _data;
        }

        private void SendToServer(object sender, EventArgs args)
        {


            var jsonObject = new PushPacket<Measurement>(PushPacket<Measurement>.DataType.Measurements,
                new List<Measurement>() { LatestMeasurement },
                Settings.GetInstance().authToken
                );

            TCPController.Send(jsonObject.ToString());
        }

        public void FormClosing()
        {
            var jsonObject = new JObject(new JProperty("CMD", "dc"));
            var json = jsonObject.ToString();
            
            TCPController.Send(json);
        }

        private void InitializeBackgroundWorker()
        {
            _bw.WorkerSupportsCancellation = true;
            // Attach event handlers to the BackgroundWorker object.
            _bw.DoWork += BackgroundWorker_DoWork;
            _bw.RunWorkerCompleted +=  BackgroundWorker_RunWorkerCompleted;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //System.Threading.Thread.Sleep(500);
            while (_queue.Count > 0)
            {
                Thread.Sleep(200);
                var cmd = _queue.Dequeue();
                _bike.SendData(cmd);
                
            }

                var m = _bike.RecieveData();
                e.Result = m;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Access the result through the Result property. 
            // But first be sure that e.Result is a Measurement instance.
            if (e.Result != null)
            {
                var result = e.Result as Measurement;
                if (result != null)
                {
                    var timeSpan = DateTime.Now.Subtract(_lastSuccesfullRead);
                    if (timeSpan.Milliseconds > _readOffsetMillis)
                    {
                        if (result.PULSE == 0)
                        {
                            result.PULSE = _pulse;
                        }
                        _lastSuccesfullRead = DateTime.Now;
                        _data.Add(result);
                        OnUpdatedList(new MeasurementEventArgs(result));
                    }
                }
            }

            if (!((BackgroundWorker)sender).CancellationPending)
            {
                _bw.RunWorkerAsync();
            }
        }

    }
}
