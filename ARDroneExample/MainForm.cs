using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AviationInstruments;

using System.Runtime.InteropServices;

namespace TestDLL
{
    public partial class MainForm : Form
    {
        private bool isFlying = false;
        private bool connected = false;
       
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            buttonShutdown.Enabled = false;
        }


        private void Connect()
        {
            if (connected) { return; }

            int connectResult = arDroneControl.Connect();

            if (connectResult == 0)
            {
                textboxOutput.AppendText("Connected to Drone\r\n");

                timerBattery.Start();

                buttonConnect.Enabled = false;
                buttonShutdown.Enabled = true;

                connected = true;
            }
            else
            {
                textboxOutput.AppendText("InitDrone() returned " + connectResult.ToString() + "\r\n");
            }
        }

        private void Disconnect()
        {
            if (!connected) { return; }

            if (arDroneControl.Shutdown())
            {
                textboxOutput.AppendText("Shutdown Drone\r\n");
                buttonConnect.Enabled = true;
                buttonShutdown.Enabled = false;

                connected = false;
            }
            else
            {
                textboxOutput.AppendText("Error shutting down Drone\r\n");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void buttonShutdown_Click(object sender, EventArgs e)
        {
            Disconnect();
        }        


        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (!isFlying)
            {
                arDroneControl.Takeoff();
            }
            else
            {
                arDroneControl.Land();
            }

            isFlying = !isFlying;
        }

        private void buttonEmergency_Click(object sender, EventArgs e)
        {
            arDroneControl.Emergency();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            arDroneControl.FlatTrim();
        }

        private void buttonChangeCamera_Click(object sender, EventArgs e)
        {
            
        }

        private void timerBattery_Tick(object sender, EventArgs e)
        {
            int batteryLevel = arDroneControl.BatteryLevel;
            String batteryLevelText = batteryLevel + "%";

            labelBattery.Text = batteryLevelText;
        }
    }
}
