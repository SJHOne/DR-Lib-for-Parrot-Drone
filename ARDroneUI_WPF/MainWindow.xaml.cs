using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ARDroneUI;
using InputLibrary;

namespace ARDroneUI
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timerStatusUpdate;
        private DispatcherTimer timerInputUpdate;
        private DispatcherTimer timerVideoUpdate;

        InputLibrary.InputManager input = null;
        private ARDroneControl arDroneControl = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimers();

            System.Windows.Interop.WindowInteropHelper helper = new System.Windows.Interop.WindowInteropHelper(this);
            input = new InputLibrary.InputManager(helper.Handle);

            arDroneControl = new ARDroneControl();
        }

        public void InitializeTimers()
        {
            timerStatusUpdate = new DispatcherTimer();
            timerStatusUpdate.Interval = new TimeSpan(0, 0, 1);
            timerStatusUpdate.Tick += new EventHandler(timerStatusUpdate_Tick);

            timerInputUpdate = new DispatcherTimer();
            timerInputUpdate.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timerInputUpdate.Tick += new EventHandler(timerInputUpdate_Tick);

            timerVideoUpdate = new DispatcherTimer();
            timerVideoUpdate.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timerVideoUpdate.Tick += new EventHandler(timerVideoUpdate_Tick);
        }

        public void Init()
        {
            timerInputUpdate.Start();
            timerStatusUpdate.Start();

            UpdateStatus();
            UpdateUI();
        }

        private void Connect()
        {
            if (!arDroneControl.CanConnect) { return; }

            if (arDroneControl.Connect())
            {
                AddOutput("Connected to Drone");
            }
            else
            {
                AddOutput("Error initializing drone");
            }

            timerVideoUpdate.Start();
            UpdateUI();
        }

        private void Disconnect()
        {
            if (!arDroneControl.CanDisconnect) { return; }

            timerVideoUpdate.Stop();

            if (arDroneControl.Shutdown())
            {
                AddOutput("Shutdown Drone");
            }
            else
            {
                AddOutput("Error shutting down Drone");
            }

            UpdateUI();
        }

        private void ChangeCamera()
        {
            if (!arDroneControl.CanChangeCamera) { return; }

            arDroneControl.ChangeCamera();
            AddOutput("Changing camera");

            UpdateUI();
        }

        private void Takeoff()
        {
            if (!arDroneControl.CanTakeoff) { return; }

            arDroneControl.Takeoff();
            AddOutput("Taking off");

            UpdateUI();
        }

        private void Land()
        {
            if (!arDroneControl.CanLand) { return; }

            arDroneControl.Land();
            AddOutput("Landing");

            UpdateUI();
        }

        private void Emergency()
        {
            if (!arDroneControl.CanCallEmergency) { return; }

            arDroneControl.Emergency();
            AddOutput("Emergency button hit");

            UpdateUI();
        }

        private void FlatTrim()
        {
            if (!arDroneControl.CanSendFlatTrim) { return; }

            arDroneControl.FlatTrim();
            AddOutput("Sending flat trim");

            UpdateUI();
        }

        private void EnterHoverMode()
        {
            if (!arDroneControl.CanEnterHoverMode) { return; }

            arDroneControl.EnterHoverMode();
            AddOutput("Entering hover mode");

            UpdateUI();
        }

        private void LeaverHoverMode()
        {
            if (!arDroneControl.CanLeaveHoverMode) { return; }

            arDroneControl.LeaveHoverMode();
            AddOutput("Leaving hover mode");

            UpdateUI();
        }

        private void Navigate(float roll, float pitch, float yaw, float gaz)
        {
            if (!arDroneControl.CanFlyFreely) { return; }

             arDroneControl.SetFlightData(roll, pitch, gaz, yaw);
        }

        private void AddOutput(String output)
        {
            textBoxOutput.AppendText(output + "\r\n");
            scrollViewerOutput.ScrollToBottom();
        }

        private void UpdateUI()
        {
            input.SetFlags(arDroneControl.IsConnected, arDroneControl.IsEmergency, arDroneControl.IsFlying, arDroneControl.IsHovering);

            if (arDroneControl.CanConnect) { buttonConnect.IsEnabled = true; } else { buttonConnect.IsEnabled = false; }
            if (arDroneControl.CanDisconnect) { buttonShutdown.IsEnabled = true; } else { buttonShutdown.IsEnabled = false; }

            if (arDroneControl.CanTakeoff || arDroneControl.CanLand) { buttonCommandTakeoff.IsEnabled = true; } else { buttonCommandTakeoff.IsEnabled = false; }
            if (arDroneControl.CanEnterHoverMode || arDroneControl.CanLeaveHoverMode) { buttonCommandHover.IsEnabled = true; } else { buttonCommandHover.IsEnabled = false; }
            if (arDroneControl.CanCallEmergency) { buttonCommandEmergency.IsEnabled = true; } else { buttonCommandEmergency.IsEnabled = false; }
            if (arDroneControl.CanSendFlatTrim) { buttonCommandFlatTrim.IsEnabled = true; } else { buttonCommandFlatTrim.IsEnabled = false; }
            if (arDroneControl.CanChangeCamera) { buttonCommandChangeCamera.IsEnabled = true; } else { buttonCommandChangeCamera.IsEnabled = false; }

            if (!arDroneControl.IsFlying) { buttonCommandTakeoff.Content = "Take off"; } else { buttonCommandTakeoff.Content = "Land"; }
            if (!arDroneControl.IsHovering) { buttonCommandHover.Content = "Start hover"; } else { buttonCommandHover.Content = "Stop hover"; }
        }

        private void UpdateStatus()
        {
            if (!arDroneControl.IsConnected)
            {
                labelCamera.Content = "No picture";
                labelStatusCamera.Content = "None";

                labelStatusBattery.Content = "N/A";
                labelStatusAltitude.Content = "N/A";
            }
            else
            {
                ARDroneControl.DroneData data  = new ARDroneControl.DroneData();
                data = arDroneControl.GetCurrentDroneData();

                if (arDroneControl.CurrentCameraType == ARDroneControl.CameraType.FrontCamera)
                {
                    labelCamera.Content = "Front camera";
                    labelStatusCamera.Content = "Front";
                }
                else
                {
                    labelCamera.Content = "Bottom camera";
                    labelStatusCamera.Content = "Bottom";
                }

                labelStatusBattery.Content = data.BatteryLevel.ToString() + "%";
                labelStatusAltitude.Content = data.Altitude.ToString();
            }

            labelStatusConnected.Content = arDroneControl.IsConnected.ToString();
            labelStatusFlying.Content = arDroneControl.IsFlying.ToString();
            labelStatusHovering.Content = arDroneControl.IsHovering.ToString();
        }

        private void SetNewVideoImage()
        {
            if (arDroneControl.IsConnected)
            {
                System.Drawing.Image newImage = arDroneControl.GetDisplayedImage();
                if (newImage != null)
                {
                    BitmapImage newBitmapImage = Utility.CreateBitmapImageFromImage(newImage);
                    imageVideo.Source = newBitmapImage;
                }
            }
        }

        // Event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Disconnect();
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        private void buttonShutdown_Click(object sender, RoutedEventArgs e)
        {
            Disconnect();
        }

        private void buttonCommandChangeCamera_Click(object sender, RoutedEventArgs e)
        {
            ChangeCamera();
        }

        private void buttonCommandTakeoff_Click(object sender, RoutedEventArgs e)
        {
            if (!arDroneControl.IsFlying)
            {
                Takeoff();
            }
            else
            {
                Land();
            }
        }

        private void buttonCommandHover_Click(object sender, RoutedEventArgs e)
        {
            if (!arDroneControl.IsHovering)
            {
                EnterHoverMode();
            }
            else
            {
                LeaverHoverMode();
            }
        }

        private void buttonCommandEmergency_Click(object sender, RoutedEventArgs e)
        {
            Emergency();
        }

        private void buttonCommandFlatTrim_Click(object sender, RoutedEventArgs e)
        {
            FlatTrim();
        }

        private void timerStatusUpdate_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void timerInputUpdate_Tick(object sender, EventArgs e)
        {
            InputState inputState = input.GetCurrentState();

            labelInputRoll.Content = String.Format("{0:+0.000;-0.000;0.000}", inputState.Roll);
            labelInputPitch.Content = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Pitch);
            labelInputYaw.Content = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Yaw);
            labelInputGaz.Content = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Gaz);

            checkBoxInputTakeoff.IsChecked = inputState.TakeOff;
            checkBoxInputLand.IsChecked = inputState.Land;
            checkBoxInputHover.IsChecked = inputState.Hover;
            checkBoxInputEmergency.IsChecked = inputState.Emergency;
            checkBoxInputFlatTrim.IsChecked = inputState.FlatTrim;
            checkBoxInputChangeCamera.IsChecked = inputState.CameraSwap;

            if (inputState.CameraSwap)
            {
                ChangeCamera();
            }

            if (inputState.TakeOff && arDroneControl.CanTakeoff)
            {
                Takeoff();
            }
            else if (inputState.Land && arDroneControl.CanLand)
            {
                Land();
            }

            if (inputState.Hover && arDroneControl.CanEnterHoverMode)
            {
                EnterHoverMode();
            }
            else if (inputState.Hover && arDroneControl.CanLeaveHoverMode)
            {
                LeaverHoverMode();
            }

            if (inputState.Emergency)
            {
                Emergency();
            }
            else if (inputState.FlatTrim)
            {
                FlatTrim();
            }

            float roll = inputState.Roll / 1.0f;
            float pitch = inputState.Pitch / 1.0f;
            float yaw = inputState.Yaw / 2.0f;
            float gaz = inputState.Gaz / 2.0f;

            Navigate(roll, pitch, yaw, gaz);
        }

        private void timerVideoUpdate_Tick(object sender, EventArgs e)
        {
            SetNewVideoImage();
        }
    }
}