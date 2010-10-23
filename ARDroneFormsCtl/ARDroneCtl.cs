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
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern int InitDrone(IntPtr vidHandle);

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern bool UpdateDrone();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern bool ShutdownDrone();

         // double vX, vY, vZ;

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern string GetDroneState();
        
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern int GetBatteryLevel();
        
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetTheta();
        
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetPhi();
        
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetPsi();
        
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern int GetAltitude();
        
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetVX();
        
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetVY();
        
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetVZ();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern int SendFlatTrim();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern int SendEmergency();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern int SendTakeoff();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern int SendLand();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern int SetProgressCmd(bool bhovering, float roll, float pitch, float gaz, float yaw);


        /*
        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetPitch();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetRoll();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
        static extern double GetYaw();

        [DllImport(@"D:\ARDrone_SDK\ARDrone_SDK\Examples\Win32\VCProjects\ARDrone\Debug\ARDroneDLL.dll")]
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
