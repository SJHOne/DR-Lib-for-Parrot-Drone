////////////////////////////////////////////////////////////////////////////////////////
//
// ARDrone.Net control 
//
// Created by Stephen Hobley - October 2010
// http://www.stephenhobley.com
//
// <add your names here>
// 
//
////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Diagnostics; // TODO Move the output strings to a window?

using System.Threading;

namespace ARDroneFormsCtl
{
    public partial class ARDroneCtl : UserControl
    {
        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int InitDrone(IntPtr vidHandle);

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern bool UpdateDrone();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern bool ShutdownDrone();

         // double vX, vY, vZ;

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern string GetDroneState();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int GetBatteryLevel();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetTheta();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetPhi();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetPsi();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int GetAltitude();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetVX();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetVY();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetVZ();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int SendFlatTrim();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int SendEmergency();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int SendTakeoff();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int SendLand();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int SetProgressCmd(bool bhovering, float roll, float pitch, float gaz, float yaw);


        /*
        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetPitch();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetRoll();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetYaw();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern double GetGaz();
        */

        private TextBox tbOutput = null;

        /// <summary>
        /// Set a textbox to receive updates
        /// </summary>
        public TextBox StatusOutput
        {
            set { tbOutput = value; }
        }
        
        /// <summary>
        /// Returns true if the drone is connected and running
        /// </summary>
        public bool IsRunning
        {
            get { return bThreadRunning; }
        }

        public ARDroneCtl()
        {
            InitializeComponent();
        }

        public int Connect()
        {
            int i = InitDrone(this.Handle);
            
            if (i == 0)
            {
                StartThread();
            }
            return i;
        }

        public bool Shutdown()
        {
            StopThread();
            return true;
        }
 
        private void ARDroneCtl_Load(object sender, EventArgs e)
        {

        }

        public int FlatTrim()
        {
            return SendFlatTrim();
        }

        public int Emergency()
        {
            return SendEmergency();
        }

        public int Takeoff()
        {
            return SendTakeoff();        
        }

        public int Land()
        {
            return SendLand();
        }

        public int ProgressCmd(bool bhovering, float roll, float pitch, float gaz, float yaw)
        {
            return SetProgressCmd(bhovering, roll, pitch, gaz, yaw); 
        }
        
        // THREADING -------------------------------------------------------------

        delegate void StringParameterDelegate(string value);
        readonly object stateLock = new object();
        
        bool bThreadRunning = false;

        bool bTerminate = false;

        void StartThread()
        {
            lock (stateLock)
            {
                bTerminate = false;
            }

            Thread t = new Thread(new ThreadStart(ThreadJob));
            t.IsBackground = true;
            t.Start();

            bThreadRunning = true;
        }

        void StopThread()
        {
            lock (stateLock)
            {
                bTerminate = true;
            }
       
        }

        void ThreadJob()
        {
            bool localStop = false;

            lock (stateLock)
            {
                localStop = bTerminate;
            }

            UpdateStatus("Starting loop");
            
            // Keep calling update till we are told to stop
            while (!localStop && UpdateDrone())
            {
                lock (stateLock)
                {
                    localStop = bTerminate;
                }

                Thread.Sleep(100);
            }
            
            UpdateStatus("Loop finished");

            if (ShutdownDrone() == false)
                UpdateStatus("Error shutting down");

            bThreadRunning = false;
        }

        void UpdateStatus(string value)
        {
            if (tbOutput != null)
            {
                if (InvokeRequired)
                {
                    // We're not in the UI thread, so we need to call BeginInvoke
                    BeginInvoke(new StringParameterDelegate(UpdateStatus), new object[] { value });
                    return;
                }

                // Must be on the UI thread if we've got this far
                tbOutput.AppendText(value + "\r\n");
            }
        }
        
        void NavEvent(string val)
        {
            if (tbOutput != null)
            {
                if (InvokeRequired)
                {
                    // We're not in the UI thread, so we need to call BeginInvoke
                    BeginInvoke(new StringParameterDelegate(NavEvent), new object[] { val });
                    return;
                }

                // Must be on the UI thread if we've got this far
                OnDroneNavEvent(new DroneNavEventArgs(GetDroneState(), GetBatteryLevel(), GetTheta(), GetPhi(), GetPsi(), GetAltitude(), GetVX(), GetVY(), GetVZ()));
            }
        }

        public event DroneNavDelegate OnDroneNavEvent;

    }

    //--------------------------------------------------------------------------------------------------
    public delegate void DroneNavDelegate(DroneNavEventArgs a);
    
    public class DroneNavEventArgs : EventArgs
    {
        public string DroneState { get; private set; }
        public int BatteryLevel { get; private set; }
        public double Theta { get; private set; }
        public double Phi { get; private set; }
        public double Psi { get; private set; }
        public int Altitude { get; private set; }
        public double vX { get; private set; }
        public double vY { get; private set; }
        public double vZ { get; private set; }

        // DroneNavEventArgs constructor
        public DroneNavEventArgs(string droneState, int batteryLevel, double theta, double phi,
                                 double psi, int altitude, double vx, double vy, double vz)
        {
            DroneState = droneState;
            BatteryLevel = batteryLevel;
            Theta = theta;
            Phi = phi;
            Psi = psi;
            Altitude = altitude;
            vX = vx;
            vY = vy;
            vZ = vz;
        }
    }


}
