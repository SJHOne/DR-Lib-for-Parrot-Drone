////////////////////////////////////////////////////////////////////////////////////////
//
// ARDrone.Net control 
//
// Created by Stephen Hobley - October 2010
// http://www.stephenhobley.com
//
// Edited by Thomas Endres
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
using System.Diagnostics;

using System.Threading;

namespace ARDroneFormsControl
{
    public partial class ARDroneControl : UserControl
    {
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

            public DroneNavEventArgs(string droneState, int batteryLevel, double theta, double phi, double psi, int altitude, double vx, double vy, double vz)
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

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int InitDrone(IntPtr vidHandle);

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern bool UpdateDrone();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern bool ShutdownDrone();

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

        public delegate void DroneNavDelegate(DroneNavEventArgs a);
        public event DroneNavDelegate DroneEvent;

        public ARDroneControl()
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

        public int SendCommand(bool bhovering, float roll, float pitch, float gaz, float yaw)
        {
            return SetProgressCmd(bhovering, roll, pitch, gaz, yaw); 
        }


        
        // Threading

        delegate void StringParameterDelegate(string value);
        readonly object stateLock = new object();
        
        bool isThreadRunning = false;
        bool shouldThreadBeTerminated = false;

        void StartThread()
        {
            lock (stateLock)
            {
                shouldThreadBeTerminated = false;
            }

            Thread thread = new Thread(new ThreadStart(ThreadJob));
            thread.IsBackground = true;
            thread.Start();

            isThreadRunning = true;
        }

        void StopThread()
        {
            lock (stateLock)
            {
                shouldThreadBeTerminated = true;
            }
       
        }

        void ThreadJob()
        {
            bool localStop = false;
            lock (stateLock)
            {
                localStop = shouldThreadBeTerminated;
            }

            SendEvent("Starting loop");
            
            // Keep calling update till we are told to stop
            while (!localStop && UpdateDrone())
            {
                lock (stateLock)
                {
                    localStop = shouldThreadBeTerminated;
                }

                Thread.Sleep(100);
            }

            SendEvent("Loop finished");

            if (ShutdownDrone() == false)
            {
                SendEvent("Error shutting down");
            }

            isThreadRunning = false;
        }

        private void SendEvent(string val)
        {
            if (DroneEvent != null)
            {
                DroneEvent(new DroneNavEventArgs(GetDroneState(), GetBatteryLevel(), GetTheta(), GetPhi(), GetPsi(), GetAltitude(), GetVX(), GetVY(), GetVZ()));
            }
        }

        public bool IsRunning
        {
            get {
                return isThreadRunning;
            }
        }

        public int BatteryLevel
        {
            get
            {
                return GetBatteryLevel();
            }
        }
    }
}
