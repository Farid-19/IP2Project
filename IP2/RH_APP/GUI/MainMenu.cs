﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mallaca.Usertypes;
using RH_APP.Classes;

namespace RH_APP.GUI
{
    public partial class MainMenu : Form
    {
        private bool IsSpecialist;
        public MainMenu(bool isSpecialist)
        {
            IsSpecialist = isSpecialist;
            InitializeComponent();
            if (!isSpecialist)
            {
                createUserButton.Enabled = false;
                createConnectionBbutton.Enabled = false;
            }
            else
            {
                startButton.Enabled = false;
                startButton.Enabled = false;
            }
            User s = Settings.GetInstance().CurrentUser;
            welcomeLabel.Text = String.Format("Welcome {0}", s.Fullname);
        }

        private void createConnectionBbutton_Click(object sender, EventArgs e)
        {
            CreateConnectionScreen c = new CreateConnectionScreen();
            c.Show();
        }

        private void createUserButton_Click(object sender, EventArgs e)
        {
            CreateScreen c = new CreateScreen();
            c.Show();
        }

        private void displayDataButton_Click(object sender, EventArgs e)
        {
            SelectMeasurementScreen s = new SelectMeasurementScreen(IsSpecialist);
            s.Show();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            TrainingScreen s = new TrainingScreen(false);
            s.Show();
        }
    }
}
