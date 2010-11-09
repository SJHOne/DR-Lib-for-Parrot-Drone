using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ARDrone.Control;
using ARDrone.Capture;
using ARDrone.Input;

namespace ARDrone.UI
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timerStatusUpdate;
        private DispatcherTimer timerInputUpdate;
        private DispatcherTimer timerVideoUpdate;

        private VideoRecorder videoRecorder = null;
        private SnapshotRecorder snapshotRecorder = null;

        ARDrone.Input.InputManager inputManager = null;
        private ARDroneControl arDroneControl = null;

        int frameCountSinceLastCapture = 0;
        DateTime lastFrameRateCaptureTime;
        int averageFrameRate = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimers();

            System.Windows.Interop.WindowInteropHelper helper = new System.Windows.Interop.WindowInteropHelper(this);

            inputManager = new ARDrone.Input.InputManager(helper.Handle);
            arDroneControl = new ARDroneControl();

            videoRecorder = new VideoRecorder();
            snapshotRecorder = new SnapshotRecorder();

            videoRecorder.CompressionComplete += new EventHandler(videoRecorder_CompressionComplete);
            videoRecorder.CompressionError += new ErrorEventHandler(videoRecorder_CompressionError);
        }

        public void Dispose()
        {
            videoRecorder.Dispose();
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
            lastFrameRateCaptureTime = DateTime.Now;

            UpdateUI();
        }

        private void Disconnect()
        {
            if (!arDroneControl.CanDisconnect) { return; }

            timerVideoUpdate.Stop();
            if (videoRecorder.IsVideoCaptureRunning)
            {
                videoRecorder.EndVideo();
            }

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
            if (!arDroneControl.CanChangeCamera && !videoRecorder.IsVideoCaptureRunning) { return; }

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

        private void LeaveHoverMode()
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
            inputManager.SetFlags(arDroneControl.IsConnected, arDroneControl.IsEmergency, arDroneControl.IsFlying, arDroneControl.IsHovering);

            if (arDroneControl.CanConnect) { buttonConnect.IsEnabled = true; } else { buttonConnect.IsEnabled = false; }
            if (arDroneControl.CanDisconnect) { buttonShutdown.IsEnabled = true; } else { buttonShutdown.IsEnabled = false; }

            if (arDroneControl.CanTakeoff || arDroneControl.CanLand) { buttonCommandTakeoff.IsEnabled = true; } else { buttonCommandTakeoff.IsEnabled = false; }
            if (arDroneControl.CanEnterHoverMode || arDroneControl.CanLeaveHoverMode) { buttonCommandHover.IsEnabled = true; } else { buttonCommandHover.IsEnabled = false; }
            if (arDroneControl.CanCallEmergency) { buttonCommandEmergency.IsEnabled = true; } else { buttonCommandEmergency.IsEnabled = false; }
            if (arDroneControl.CanSendFlatTrim) { buttonCommandFlatTrim.IsEnabled = true; } else { buttonCommandFlatTrim.IsEnabled = false; }
            if (arDroneControl.CanChangeCamera && !videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonCommandChangeCamera.IsEnabled = true; } else { buttonCommandChangeCamera.IsEnabled = false; }


            if (!arDroneControl.IsFlying) { buttonCommandTakeoff.Content = "Take off"; } else { buttonCommandTakeoff.Content = "Land"; }
            if (!arDroneControl.IsHovering) { buttonCommandHover.Content = "Start hover"; } else { buttonCommandHover.Content = "Stop hover"; }

            if (arDroneControl.IsConnected) { buttonSnapshot.IsEnabled = true; } else { buttonSnapshot.IsEnabled = false; }
            if (!arDroneControl.IsConnected || videoRecorder.IsVideoCaptureRunning || videoRecorder.IsCompressionRunning) { checkBoxVideoCompress.IsEnabled = false; } else { checkBoxVideoCompress.IsEnabled = true; }
            if (CanCaptureVideo && !videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonVideoStart.IsEnabled = true; } else { buttonVideoStart.IsEnabled = false; }
            if (CanCaptureVideo && videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonVideoEnd.IsEnabled = true; } else { buttonVideoEnd.IsEnabled = false; }

            
            if      (videoRecorder.IsCompressionRunning)  { labelVideoStatus.Content = "Compressing"; }
            else if (videoRecorder.IsVideoCaptureRunning) { labelVideoStatus.Content = "Recording"; }
            else    { labelVideoStatus.Content = "Idling ..."; }
        }

        private void UpdateStatus()
        {
            if (!arDroneControl.IsConnected)
            {
                labelCamera.Content = "No picture";
                labelStatusCamera.Content = "None";

                labelStatusBattery.Content = "N/A";
                labelStatusAltitude.Content = "N/A";

                labelStatusFrameRate.Content = "No video";
            }
            else
            {
                ARDroneControl.DroneData data  = new ARDroneControl.DroneData();
                data = arDroneControl.GetCurrentDroneData();
                int frameRate = GetCurrentFrameRate();

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

                labelStatusFrameRate.Content = frameRate.ToString();
            }


            labelStatusConnected.Content = arDroneControl.IsConnected.ToString();
            labelStatusFlying.Content = arDroneControl.IsFlying.ToString();
            labelStatusHovering.Content = arDroneControl.IsHovering.ToString();
        }

        private void UpdateInput()
        {
            InputState inputState = inputManager.GetCurrentState();

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
                LeaveHoverMode();
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

        private void SetNewVideoImage()
        {
            if (arDroneControl.IsConnected)
            {
                System.Drawing.Image newImage = arDroneControl.GetDisplayedImage();

                if (newImage != null)
                {
                    frameCountSinceLastCapture++;

                    if (videoRecorder.IsVideoCaptureRunning)
                    {
                        videoRecorder.AddFrame((System.Drawing.Bitmap)newImage.Clone());
                    }

                    BitmapImage newBitmapImage = Utility.CreateBitmapImageFromImage(newImage);
                    imageVideo.Source = newBitmapImage;
                }
            }
        }

        private void TakeSnapshot()
        {
            String snapshotFilePath = ShowFileDialog(".png", "PNG files (.png)|*.png");
            if (snapshotFilePath == null) { return; }

            System.Drawing.Bitmap currentImage = (System.Drawing.Bitmap)arDroneControl.GetDisplayedImage();
            snapshotRecorder.SaveSnapshot(currentImage, snapshotFilePath);
        }

        private void StartVideoCapture()
        {
            if (!CanCaptureVideo || videoRecorder.IsVideoCaptureRunning) { return; }

            String videoFilePath = ShowFileDialog(".avi", "Video files (.avi)|*.avi");
            if (videoFilePath == null) { return; }

            System.Drawing.Size size;
            if (arDroneControl.CurrentCameraType == ARDroneControl.CameraType.FrontCamera)
            {
                size = arDroneControl.FrontCameraPictureSize;
            }
            else
            {
                size = arDroneControl.BottomCameraPictureSize;
            }

            videoRecorder.StartVideo(videoFilePath, averageFrameRate, size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb, 4, checkBoxVideoCompress.IsChecked == true ? true : false);
            UpdateUI();
        }

        private void EndVideoCapture()
        {
            if (!videoRecorder.IsVideoCaptureRunning)
            {
                return;
            }

            videoRecorder.EndVideo();

            UpdateUI();
        }

        private String ShowFileDialog(String extension, String filter)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog();
            fileDialog.FileName = "ARDroneOut";
            fileDialog.DefaultExt = extension;
            fileDialog.Filter = filter;

            bool? result = fileDialog.ShowDialog();
            
            String fileName = null;
            if (result == true)
            {
                fileName = fileDialog.FileName;
            }

            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(null, "The file could not be deleted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                fileName = null;
            }

            return fileName;
        }

        private int GetCurrentFrameRate()
        {
            int timePassed = (int)(DateTime.Now - lastFrameRateCaptureTime).TotalMilliseconds;
            int frameRate = frameCountSinceLastCapture * 1000 / timePassed;
            averageFrameRate = (averageFrameRate + frameRate) / 2;

            lastFrameRateCaptureTime = DateTime.Now;
            frameCountSinceLastCapture = 0;

            return averageFrameRate;
        }


        private bool CanCaptureVideo
        {
            get
            {
                return arDroneControl.CanChangeCamera;
            }
        }

        // Event handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            videoRecorder.Dispose();
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
                LeaveHoverMode();
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

        private void buttonSnapshot_Click(object sender, RoutedEventArgs e)
        {
            TakeSnapshot();
        }

        private void buttonVideoStart_Click(object sender, RoutedEventArgs e)
        {
            StartVideoCapture();
        }

        private void buttonVideoEnd_Click(object sender, RoutedEventArgs e)
        {
            EndVideoCapture();
        }

        private void timerStatusUpdate_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void timerInputUpdate_Tick(object sender, EventArgs e)
        {
            UpdateInput();
        }

        private void timerVideoUpdate_Tick(object sender, EventArgs e)
        {
            SetNewVideoImage();
        }

        private void videoRecoderSync_CompressionComplete(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Successfully compressed video!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            UpdateUI();
        }

        private void videoRecorder_CompressionComplete(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new EventHandler(videoRecoderSync_CompressionComplete), this, e);
        }

        private void videoRecoderSync_CompressionError(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(this, e.GetException().Message, "Success", MessageBoxButton.OK, MessageBoxImage.Error);
            UpdateUI();
        }

        private void videoRecorder_CompressionError(object sender, ErrorEventArgs e)
        {
            Dispatcher.BeginInvoke(new ErrorEventHandler(videoRecoderSync_CompressionError), this, e);
        }
    }
}