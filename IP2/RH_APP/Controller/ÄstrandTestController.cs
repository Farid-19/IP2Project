﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        // timeToIncreasePower[x] = y
        // x = minuut
        // y = true / false 
        // y geeft aan of de power al is toegenomen in minuut x.
        private readonly bool[] timetoIncreasePower = new bool[900];

        public delegate void TrainingFinished();

        public TrainingFinished OnTrainingFinished;

        public enum TestPhases
        {
            WarmingUp, Training, CoolingDown, EndTraining, End
        }

        public TestPhases state;

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
            TimeSpan time = TimeSpan.ParseExact(m.TIME, "g", CultureInfo.CurrentCulture);

            switch (state)
            {
                case TestPhases.WarmingUp:
                    if (time.Minutes > 2)
                        state = TestPhases.Training;

                    break;

                    case TestPhases.Training:
                    if (time.Seconds == 0)
                    {
                        if(time.Minutes == 8)
                            state = TestPhases.CoolingDown;
                        else if (!timetoIncreasePower[time.Minutes])
                        {
                            timetoIncreasePower[time.Minutes] = true;
                            power += client.Gender == "f" ? 10 : 15;
                            controller.SetPower(power);
                        }

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
            
            
        }

    }
}
