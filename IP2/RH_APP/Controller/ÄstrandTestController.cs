using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Mallaca;
using Mallaca.Usertypes;

namespace RH_APP.Controller
{
    class ÄstrandTestController
    {
        private readonly RH_Controller controller;
        private readonly User client;
        private int power;
        private bool started = false;

        private int maxPulse;
        private bool rpmOk = false;
        private bool rpmToLow = true;
        private bool rpmtoHigh = false;

        // timeToIncreasePower[x][h] = y
        // x = minuut
        // h = seconden / 10
        // y = true / false 
        // y geeft aan of de power al is toegenomen in minuut x:seconden/10 h .
        private readonly bool[][] timetoIncreasePower = new bool[900][];

        public delegate void TrainingFinished();

        public delegate void TrainingStateChanged(string state);

        public delegate void RPMtoLow();

        public delegate void RPMToHigh();

        public delegate void RPMIsOK();

        public event RPMToHigh OnRPMToHigh;

        public event RPMtoLow OnRPMToLow;

        public event RPMIsOK OnRPMIsOK;

        public event TrainingStateChanged OnTrainingStateChanged;

        public event TrainingFinished OnTrainingFinished;


        private int beatrateHigherThan130;

        public enum TestPhases
        {
            WarmingUp, WarmingPulse, SteadyState, Training, CoolingDown, EndTraining, End
        }

        public TestPhases state;
        public TestPhases? oldState;

        //steady state vars
        private TimeSpan steadyStateEndTime;
        private List<int> steadyStateHartRateList = new List<int>();
        private double steadyStateHartRateAverage = -1;

        public ÄstrandTestController(RH_Controller rh, User s)
        {
            client = s;
            controller = rh;

            for (int i = 0; i < timetoIncreasePower.Length; i++)
            {
                timetoIncreasePower[i] = new bool[9];
                for (int j = 0; j < timetoIncreasePower.Length; j++)
                    timetoIncreasePower[i][j] = false;
            }


        }

        public void Start()
        {
            started = true;
            controller.Reset();
            state = TestPhases.WarmingUp;
            power = client.Gender == "f" ? 25 : 50;

            controller.SetPower(power);


            if (client.Gender == "f")
            {
                maxPulse = 200 - getAge();
            }
            else
            {
                maxPulse = 220 - getAge();
            }
        }

        public void End()
        {
            controller.Reset();
        }

        public void CheckConditions(Measurement m = null)
        {
            if (started == false)
                return;

            if(m == null)
                m = controller.LatestMeasurement;
            //TimeSpan time = TimeSpan.ParseExact(m.TIME, "g", CultureInfo.CurrentCulture);
            TimeSpan time = TimeSpan.ParseExact(m.TIME, @"mm\:ss", CultureInfo.CurrentCulture);

            switch (state)
            {
                case TestPhases.WarmingUp:
                    //if (time.Minutes > 2)
                    if (time.Seconds > 10)
                        state = TestPhases.Training;

                    break;

                    case TestPhases.WarmingPulse:

                        if (m.PULSE <= 130 &&
                            !timetoIncreasePower[time.Minutes][time.Seconds / 10]) 
                        {
                            timetoIncreasePower[time.Minutes][time.Seconds / 10] = true;
                            power += 10;
                            controller.SetPower(power);
                            Console.WriteLine("Increased power to " + power);
                        }
                        else if (m.PULSE >= 130)
                        {
                            beatrateHigherThan130 = time.Add(new TimeSpan(0, 0, 0, 10)).Seconds;

                            if (time.Seconds == beatrateHigherThan130)
                            {
                                //TODO measure if hartrate is consistent
                                steadyStateEndTime = time.Add(new TimeSpan(0,0,2,0));
                                state = TestPhases.SteadyState;
                            }

                        }

                    
                    break;

                case TestPhases.SteadyState:

                    steadyStateHartRateList.Add(m.PULSE);

                    if (steadyStateEndTime.Seconds == time.Seconds &&
                        steadyStateEndTime.Minutes == time.Minutes)
                    {
                        double total = 0;
                        steadyStateHartRateList.ForEach(x => total += x);
                        steadyStateHartRateAverage = total / steadyStateHartRateList.Count;
                        state = TestPhases.CoolingDown;
                    }

                    break;

                    case TestPhases.CoolingDown:
                    if (power != 25)
                    {
                        power -= 1;
                        controller.SetPower(power);
                    }

                    break;

                case TestPhases.EndTraining:
                    controller.Reset();
                    if (OnTrainingFinished != null)
                        OnTrainingFinished();

                    state = TestPhases.End;
                    break;

            }


            if (oldState == null || oldState != state)
            {
                oldState = state;
                OnTrainingStateChanged(state.ToString());
            }

            if (!rpmToLow && m.RPM < 60)
            {
                OnRPMToLow();
                rpmToLow = true;
                rpmOk = false;
            }
            else if (rpmtoHigh && m.RPM > 70)
            {
                OnRPMToHigh();
                rpmtoHigh = true;
                rpmOk = false;
            }
            else if(!rpmOk)
            {
                OnRPMIsOK();
                rpmOk = true;
            }
        }


        public int getAge()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - client.DateOfBirth.Year;

            if (client.DateOfBirth > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
