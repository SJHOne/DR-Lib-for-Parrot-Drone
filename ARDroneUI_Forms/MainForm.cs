using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InputLibrary;

namespace ARDroneUI
{
    public partial class MainForm : Form
    {
        InputManager input = null;
        private ARDroneControl arDroneControl = null;

        public MainForm()
        {
            InitializeComponent();
            input = new InputManager(this.Handle);
            arDroneControl = new ARDroneControl();

            timerInputUpdate.Start();
            timerStatusUpdate.Start();

            UpdateStatus();
            UpdateUI();
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
            textboxOutput.AppendText(output + "\r\n");
        }

        private void UpdateUI()
        {
            input.SetFlags(arDroneControl.IsConnected, arDroneControl.IsEmergency, arDroneControl.IsFlying, arDroneControl.IsHovering);

            if (arDroneControl.CanConnect) { buttonConnect.Enabled = true; } else { buttonConnect.Enabled = false; }
            if (arDroneControl.CanDisconnect) { buttonShutdown.Enabled = true; } else { buttonShutdown.Enabled = false; }

            if (arDroneControl.CanTakeoff || arDroneControl.CanLand) { buttonCommandTakeoff.Enabled = true; } else { buttonCommandTakeoff.Enabled = false; }
            if (arDroneControl.CanEnterHoverMode || arDroneControl.CanLeaveHoverMode) { buttonCommandHover.Enabled = true; } else { buttonCommandHover.Enabled = false; }
            if (arDroneControl.CanCallEmergency) { buttonCommandEmergency.Enabled = true; } else { buttonCommandEmergency.Enabled = false; }
            if (arDroneControl.CanSendFlatTrim) { buttonCommandFlatTrim.Enabled = true; } else { buttonCommandFlatTrim.Enabled = false; }
            if (arDroneControl.CanChangeCamera) { buttonCommandChangeCamera.Enabled = true; } else { buttonCommandChangeCamera.Enabled = false; }

            if (!arDroneControl.IsFlying) { buttonCommandTakeoff.Text = "Take off"; } else { buttonCommandTakeoff.Text = "Land"; }
            if (!arDroneControl.IsHovering) { buttonCommandHover.Text = "Start hover"; } else { buttonCommandHover.Text = "Stop hover"; }
        }

        private void UpdateStatus()
        {
            if (!arDroneControl.IsConnected)
            {
                labelCamera.Text = "No picture";
                labelStatusCamera.Text = "None";

                labelStatusBattery.Text = "N/A";
                labelStatusAltitude.Text = "N/A";
            }
            else
            {
                ARDroneControl.DroneData data = new ARDroneControl.DroneData();
                data = arDroneControl.GetCurrentDroneData();

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
            }

            labelStatusConnected.Text = arDroneControl.IsConnected.ToString();
            labelStatusFlying.Text = arDroneControl.IsFlying.ToString();
            labelStatusHovering.Text = arDroneControl.IsHovering.ToString();
        }

        private void SetNewVideoImage()
        {
            Image currentImage = pictureBoxVideo.Image;
            Image newImage = arDroneControl.GetDisplayedImage();

            pictureBoxVideo.Image = newImage;

            if (currentImage != null)
            {
                currentImage.Dispose();
            }
        }

        // Event handlers

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
                LeaverHoverMode();
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
        
        private void timerStatusUpdate_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void timerInputUpdate_Tick(object sender, EventArgs e)
        {
            InputState inputState = input.GetCurrentState();

            labelInputRoll.Text = String.Format("{0:+0.000;-0.000;0.000}", inputState.Roll);
            labelInputPitch.Text = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Pitch);
            labelInputYaw.Text = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Yaw);
            labelInputGaz.Text = String.Format("{0:+0.000;-0.000;0.000}", -inputState.Gaz);

            checkBoxInputTakeoff.Checked = inputState.TakeOff;
            checkBoxInputLand.Checked = inputState.Land;
            checkBoxInputHover.Checked = inputState.Hover;
            checkBoxInputEmergency.Checked = inputState.Emergency;
            checkBoxInputFlatTrim.Checked = inputState.FlatTrim;
            checkBoxInputCameraSwap.Checked = inputState.CameraSwap;

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

        private void button1_Click(object sender, EventArgs e)
        {
            input.ShowSettingsDialog();
        }
    }
}