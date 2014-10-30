using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        // timeToIncreasePower[x] = y
        // x = minuut
        // y = true / false 
        // y geeft aan of de power al is toegenomen in minuut x.
        private readonly bool[] timetoIncreasePower = new bool[900];

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



        public enum TestPhases
        {
            WarmingUp, WarmingPulse, SteadyState, Training, CoolingDown, EndTraining, End
        }

        public TestPhases state;
        public TestPhases? oldState;

        public ÄstrandTestController(RH_Controller rh, User s)
        {
            client = s;
            controller = rh;

            for (int i = 0; i < timetoIncreasePower.Length; i++)
                timetoIncreasePower[i] = false;
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

                    case TestPhases.Training:

                        if(time.Minutes == 8)
                            state = TestPhases.CoolingDown;
                        else if (!timetoIncreasePower[time.Minutes])
                        {
                            timetoIncreasePower[time.Minutes] = true;
                            power += client.Gender == "f" ? 10 : 15;
                            controller.SetPower(power);
                            Console.WriteLine("Increased power to " + power);
                        }

                    
                    break;

                    case TestPhases.CoolingDown:
                    if (power != 25)
                    {
                        power = 25;
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
