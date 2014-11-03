﻿using System;
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

        public int HeartRate { get; set; }


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
            {
                timetoIncreasePower[i] = false;
            }


        }

        public void Start()
        {
            started = true;
            controller.Reset();
            state = TestPhases.WarmingUp;
            power = client.Gender == "f" ? 25 : 50;

            controller.SendCommand("CM");
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

            if (m.PULSE == 0)
            {
                m.PULSE = HeartRate;
            }
            //TimeSpan time = TimeSpan.ParseExact(m.TIME, "g", CultureInfo.CurrentCulture);
            TimeSpan time = TimeSpan.ParseExact(m.TIME, @"mm\:ss", CultureInfo.CurrentCulture);

            switch (state)
            {
                case TestPhases.WarmingUp:
                    WarmingUp(time);
                    break;

                case TestPhases.WarmingPulse:
                    WarmingPulse(m, time);
                    break;

                case TestPhases.SteadyState:
                    SteadyState(m, time);
                    break;

                case TestPhases.CoolingDown:
                    CoolingDown(time);
                    break;

                case TestPhases.EndTraining:
                    EndTraining();
                    break;
            }


            if (oldState == null || oldState != state)
            {
                oldState = state;
                OnTrainingStateChanged(state.ToString());
            }

            if (m.RPM < 60)
            {
                OnRPMToLow();
            }
            else if (m.RPM > 70)
            {
                OnRPMToHigh();
            }
            else
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


        private void WarmingUp(TimeSpan time)
        {
            if (time.Minutes >= 2)
            //if (time.Seconds > 10)
            {
                state = TestPhases.WarmingPulse;
            }
        }

        // timeToIncreasePower[x] = y
        // x = minuut
        // y = true / false 
        // y geeft aan of de power al is toegenomen in minuut x.
        private readonly bool[] timetoIncreasePower = new bool[900];
        private int beatrateHigherThan150 = -1;
        private const int minBPM = 120;
        private void WarmingPulse(Measurement m, TimeSpan time)
        {
            bool isNewMinute = time.Seconds == 0;
            bool bpmEndSecIsSet = beatrateHigherThan150 != -1;

            if (m.PULSE < minBPM && bpmEndSecIsSet)
            {
                beatrateHigherThan150 = -1;
            }
            if (m.PULSE < minBPM && !timetoIncreasePower[time.Minutes])//&& isNewMinute)
            {
                timetoIncreasePower[time.Minutes] = true;
                power += 25;
                controller.SetPower(power);
                Console.WriteLine("Increased power to " + power);

            }
            else if (m.PULSE >= minBPM && !bpmEndSecIsSet)
            {
                //current time +10 seconds
                beatrateHigherThan150 = time.Add(new TimeSpan(0, 0, 0, 10)).Seconds;
            }
            else if (m.PULSE >= minBPM && time.Seconds == beatrateHigherThan150)
            {
                //current time +2 minutes
                steadyStateEndTime = time.Add(new TimeSpan(0, 0, 2, 0));
                state = TestPhases.SteadyState;
                Console.WriteLine("pulse was higher than " + minBPM + " for 10 seconds. Switching state to SteadyState.");
            }

            
        }
    

        //steady state vars
        private TimeSpan steadyStateEndTime;
        private List<int> steadyStateHartRateList = new List<int>();
        public double steadyStateHartRateAverage { get; private set; }

        private void SteadyState(Measurement m, TimeSpan time)
        {
            steadyStateHartRateList.Add(m.PULSE);

            if (steadyStateEndTime.Seconds == time.Seconds &&
                steadyStateEndTime.Minutes == time.Minutes)
            {
                double total = 0;
                steadyStateHartRateList.ForEach(x => total += x);
                steadyStateHartRateAverage = total / steadyStateHartRateList.Count;
                state = TestPhases.CoolingDown;
                coolingDownTime = time.Add(new TimeSpan(0, 0, 1, 0));
                Console.WriteLine("Ending steady state.");
                Console.WriteLine("Average steady bpm: {0}.", steadyStateHartRateAverage);
                Console.WriteLine("Switching state to end (cooling down) state.");
            }
        }

        private TimeSpan coolingDownTime;
        private void CoolingDown(TimeSpan time)
        {
            if (power != 25)
            {
                power -= 1;
                controller.SetPower(power);
            }

            if(coolingDownTime.Equals(time))
                state = TestPhases.EndTraining;
        }

        private void EndTraining()
        {
            controller.Reset();
            if (OnTrainingFinished != null)
                OnTrainingFinished();

            state = TestPhases.End;
        }

    }
}
