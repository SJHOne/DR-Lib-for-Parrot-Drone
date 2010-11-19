using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ARDrone.Control;
using ARDrone.Capture;
using ARDrone.Input;
using AviationInstruments;

namespace ARDrone.UI
{
    public partial class MainForm : Form
    {
        private VideoRecorder videoRecorder = null;
        private SnapshotRecorder snapshotRecorder = null;
        private InstrumentsManager instrumentsManager;

        ARDrone.Input.InputManager inputManager = null;
        private ARDroneControl arDroneControl = null;

        int frameCountSinceLastCapture = 0;
        DateTime lastFrameRateCaptureTime;
        int averageFrameRate = 0;

        public MainForm()
        {
            InitializeComponent();

            inputManager = new ARDrone.Input.InputManager(this.Handle);
            arDroneControl = new ARDroneControl();

            videoRecorder = new VideoRecorder();
            snapshotRecorder = new SnapshotRecorder();

            videoRecorder.CompressionComplete += new EventHandler(videoRecorder_CompressionComplete);
            videoRecorder.CompressionError += new ErrorEventHandler(videoRecorder_CompressionError);

            timerStatusUpdate.Start();
            timerInputUpdate.Start();
            UpdateUI();

            this.instrumentsManager = new InstrumentsManager(arDroneControl);
            this.instrumentsManager.addInstrument(this.attitudeControl);
            this.instrumentsManager.addInstrument(this.altimeterControl);
            this.instrumentsManager.addInstrument(this.headingControl);
            this.instrumentsManager.startManage();
        }

        public void DisposeControl()
        {
            videoRecorder.Dispose();
            instrumentsManager.stopManage();
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
        }

        private void UpdateUI()
        {
            inputManager.SetFlags(arDroneControl.IsConnected, arDroneControl.IsEmergency, arDroneControl.IsFlying, arDroneControl.IsHovering);

            if (arDroneControl.CanConnect) { buttonConnect.Enabled = true; } else { buttonConnect.Enabled = false; }
            if (arDroneControl.CanDisconnect) { buttonShutdown.Enabled = true; } else { buttonShutdown.Enabled = false; }

            if (arDroneControl.CanTakeoff || arDroneControl.CanLand) { buttonCommandTakeoff.Enabled = true; } else { buttonCommandTakeoff.Enabled = false; }
            if (arDroneControl.CanEnterHoverMode || arDroneControl.CanLeaveHoverMode) { buttonCommandHover.Enabled = true; } else { buttonCommandHover.Enabled = false; }
            if (arDroneControl.CanCallEmergency) { buttonCommandEmergency.Enabled = true; } else { buttonCommandEmergency.Enabled = false; }
            if (arDroneControl.CanSendFlatTrim) { buttonCommandFlatTrim.Enabled = true; } else { buttonCommandFlatTrim.Enabled = false; }
            if (arDroneControl.CanChangeCamera && !videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonCommandChangeCamera.Enabled = true; } else { buttonCommandChangeCamera.Enabled = false; }


            if (!arDroneControl.IsFlying) { buttonCommandTakeoff.Text = "Take off"; } else { buttonCommandTakeoff.Text = "Land"; }
            if (!arDroneControl.IsHovering) { buttonCommandHover.Text = "Start hover"; } else { buttonCommandHover.Text = "Stop hover"; }

            if (arDroneControl.IsConnected) { buttonSnapshot.Enabled = true; } else { buttonSnapshot.Enabled = false; }
            if (!arDroneControl.IsConnected || videoRecorder.IsVideoCaptureRunning || videoRecorder.IsCompressionRunning) { checkBoxVideoCompress.Enabled = false; } else { checkBoxVideoCompress.Enabled = true; }
            if (CanCaptureVideo && !videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonVideoStart.Enabled = true; } else { buttonVideoStart.Enabled = false; }
            if (CanCaptureVideo && videoRecorder.IsVideoCaptureRunning && !videoRecorder.IsCompressionRunning) { buttonVideoEnd.Enabled = true; } else { buttonVideoEnd.Enabled = false; }

            
            if      (videoRecorder.IsCompressionRunning)  { labelVideoStatus.Text = "Compressing"; }
            else if (videoRecorder.IsVideoCaptureRunning) { labelVideoStatus.Text = "Recording"; }
            else    { labelVideoStatus.Text = "Idling ..."; }
        }

        private void UpdateStatus()
        {
            if (!arDroneControl.IsConnected)
            {
                labelCamera.Text = "No picture";
                labelStatusCamera.Text = "None";

                labelStatusBattery.Text = "N/A";
                labelStatusAltitude.Text = "N/A";

                labelStatusFrameRate.Text = "No video";
            }
            else
            {
                ARDroneControl.DroneData data  = new ARDroneControl.DroneData();
                data = arDroneControl.GetCurrentDroneData();
                int frameRate = GetCurrentFrameRate();

                if (arDroneControl.CurrentCameraType == ARDroneControl.CameraType.FrontCamera)
                {
                    labelCamera.Text = "Front camera";
                    labelStatusCamera.Text = "Front";
                }
                else
                {
                    labelCamera.Text = "Bottom camera";
                    labelStatusCamera.Text = "Bottom";
                }

                labelStatusBattery.Text = data.BatteryLevel.ToString() + "%";
                labelStatusAltitude.Text = data.Altitude.ToString();

                labelStatusFrameRate.Text = frameRate.ToString();
            }


            labelStatusConnected.Text = arDroneControl.IsConnected.ToString();
            labelStatusFlying.Text = arDroneControl.IsFlying.ToString();
            labelStatusHovering.Text = arDroneControl.IsHovering.ToString();
            labelStatusEmergency.Text = arDroneControl.IsEmergency.ToString();
        }

        private void UpdateInput()
        {
            InputState inputState = null; // inputManager.GetCurrentState();

            labelInputRoll.Text = String.Format("{0:+0.000;-0.000;0.000}", inputState.Roll);
            labelInputPitch.Text = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Pitch);
            labelInputYaw.Text = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Yaw);
            labelInputGaz.Text = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Gaz);

            checkBoxInputTakeoff.Checked = inputState.TakeOff;
            checkBoxInputLand.Checked = inputState.Land;
            checkBoxInputHover.Checked = inputState.Hover;
            checkBoxInputEmergency.Checked = inputState.Emergency;
            checkBoxInputFlatTrim.Checked = inputState.FlatTrim;
            checkBoxInputChangeCamera.Checked = inputState.CameraSwap;

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

                    pictureBoxVideo.Image = newImage;
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

            videoRecorder.StartVideo(videoFilePath, averageFrameRate, size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppRgb, 4, checkBoxVideoCompress.Checked == true ? true : false);
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
            fileDialog.FileName = "ARDroneOut";
            fileDialog.DefaultExt = extension;
            fileDialog.Filter = filter;

            DialogResult result = fileDialog.ShowDialog();
            
            String fileName = null;
            if (result == DialogResult.OK)
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
                MessageBox.Show(null, "The file could not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void Window_Loaded(object sender, EventArgs e)
        {
            Init();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisposeControl();
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

        private void buttonCommandChangeCamera_Click(object sender, EventArgs e)
        {
            ChangeCamera();
        }

        private void buttonCommandTakeoff_Click(object sender, EventArgs e)
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

        private void buttonCommandHover_Click(object sender, EventArgs e)
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

        private void buttonCommandEmergency_Click(object sender, EventArgs e)
        {
            Emergency();
        }

        private void buttonCommandFlatTrim_Click(object sender, EventArgs e)
        {
            FlatTrim();
        }

        private void buttonSnapshot_Click(object sender, EventArgs e)
        {
            TakeSnapshot();
        }

        private void buttonVideoStart_Click(object sender, EventArgs e)
        {
            StartVideoCapture();
        }

        private void buttonVideoEnd_Click(object sender, EventArgs e)
        {
            EndVideoCapture();
        }

        private void buttonInputSettings_Click(object sender, EventArgs e)
        {
            ConfigInput configInput = new ConfigInput(inputManager);
            configInput.ShowDialog();
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
            MessageBox.Show(this, "Successfully compressed video!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            UpdateUI();
        }

        private void videoRecorder_CompressionComplete(object sender, EventArgs e)
        {
            BeginInvoke(new EventHandler(videoRecoderSync_CompressionComplete), this, e);
        }

        private void videoRecoderSync_CompressionError(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(this, e.GetException().Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Error);
            UpdateUI();
        }

        private void videoRecorder_CompressionError(object sender, ErrorEventArgs e)
        {
            BeginInvoke(new ErrorEventHandler(videoRecoderSync_CompressionError), this, e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            videoRecorder.Dispose();
            instrumentsManager.stopManage();
            Disconnect();
        }
    }
}