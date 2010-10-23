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
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            joystickControl2.bgColor = Color.LemonChiffon;
            joystickControl2.gridColor = Color.DarkKhaki;
            arDroneCtl1.StatusOutput = tbOutput;
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            int i = arDroneCtl1.Connect();

            if (i == 0)
            {
                tbOutput.AppendText("Connected to Drone\r\n");
                
                bConnect.Enabled = false;
                button1.Enabled = true;
            }
            else
            {
                tbOutput.AppendText("InitDrone() returned " + i.ToString() + "\r\n");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (arDroneCtl1.Shutdown())
            {
                tbOutput.AppendText("Shutdown Drone\r\n");
                bConnect.Enabled = true;
                button1.Enabled = false;

            }
            else
            {
                tbOutput.AppendText("Error shutting down Drone\r\n");
            }
        }
        
        private bool Fly = false;

        private void button4_Click(object sender, EventArgs e)
        {
            if (!Fly)
                arDroneCtl1.Takeoff();
            else
                arDroneCtl1.Land();
   
            Fly = !Fly;
        }
    }
}
