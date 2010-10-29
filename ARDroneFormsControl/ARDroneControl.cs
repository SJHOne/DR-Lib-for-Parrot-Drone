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
        public enum CameraType
        {
            FrontCamera,
            BottomCamera
        }

        public class DroneData : EventArgs
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

            public DroneData()
            {
                DroneState = "";
                BatteryLevel = 0;
                Theta = 0.0f;
                Phi = 0.0f;
                Psi = 0.0f;
                Altitude = 0;
                vX = 0.0;
                vY = 0.0;
                vZ = 0.0;
            }

            public DroneData(string droneState, int batteryLevel, double theta, double phi, double psi, int altitude, double vx, double vy, double vz)
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

            public override String ToString()
            {
                return "Drone state: " + DroneState + " , Theta: " + Theta + " , Phi: " + Phi + " , Psi: " + Psi + " , Altitude: " + Altitude + " , vX: " + vX + " , vY: " + vY + " , vZ: " + vZ;
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

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int ChangeToFrontCamera();

        [DllImport(@"..\ARDroneDLL\ARDroneDLL.dll")]
        static extern int ChangeToBottomCamera();

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

        public delegate void DroneNavDelegate(DroneData data);
        public event DroneNavDelegate DroneEvent;

        private CameraType currentCameraType = CameraType.FrontCamera;

        private float currentRoll = 0.0f;
        private float currentPitch = 0.0f;
        private float currentGaz = 0.0f;
        private float currentYaw = 0.0f;

        private float thresholdBetweenSettingCommands = 0.03f;

        public ARDroneControl()
        {
            InitializeComponent();
        }

        public int Connect()
        {
            int resultValue = InitDrone(this.Handle);

            if (resultValue == 0)
            {
                StartThread();
            }

            ChangeToFrontCamera();
            return resultValue;
        }

        public bool Shutdown()
        {
            StopThread();
            return true;
        }

        public int SwapCamera()
        {
            int resultValue = 0;

            if (currentCameraType == CameraType.FrontCamera)
            {
                resultValue = ChangeToBottomCamera();
                currentCameraType = CameraType.BottomCamera;
            }
            else
            {
                resultValue = ChangeToFrontCamera();
                currentCameraType = CameraType.FrontCamera;
            }

            return resultValue;
        }

        public DroneData GetCurrentDroneData()
        {
            return new DroneData(GetDroneState(), GetBatteryLevel(), GetTheta(), GetPhi(), GetPsi(), GetAltitude(), GetVX(), GetVY(), GetVZ());
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

        public int SendCommand(bool hovering, float roll, float pitch, float gaz, float yaw)
        {
            if (Math.Abs(roll - currentRoll) + Math.Abs(pitch - currentPitch) + Math.Abs(yaw - currentYaw) + Math.Abs(gaz - currentGaz) > thresholdBetweenSettingCommands ||
                ((currentRoll != 0.0f && roll == 0.0f) && (currentPitch != 0.0f && pitch == 0.0f) && (currentYaw != 0.0f && yaw == 0.0f) && (currentGaz != 0.0f && gaz == 0.0f)))
            {
                currentRoll = roll;
                currentPitch = pitch;
                currentYaw = yaw;
                currentGaz = gaz;
                Console.WriteLine("Hovering: " + hovering.ToString() + ", Roll: " + roll.ToString() + ", Roll: " + roll.ToString() + ", Pitch: " + pitch.ToString() + ", Gaz: " + gaz.ToString() + ", Yaw: " + yaw.ToString());

                return SetProgressCmd(hovering, roll, pitch, gaz, yaw);
            }

            return 0;
        }

        public bool IsRunning
        {
            get
            {
                return isThreadRunning;
            }
        }

        public CameraType CurrentCameraType
        {
            get
            {
                return currentCameraType;
            }
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
                DroneEvent(new DroneData(GetDroneState(), GetBatteryLevel(), GetTheta(), GetPhi(), GetPsi(), GetAltitude(), GetVX(), GetVY(), GetVZ()));
            }
        }
    }
}
