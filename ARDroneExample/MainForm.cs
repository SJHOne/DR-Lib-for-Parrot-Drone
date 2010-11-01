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
using ARDrone.Input;

namespace ARDrone
{
    public partial class MainForm : Form
    {
        private bool droneEnabled = true;

        InputManager input = null;

        private bool isConnected = false;

        private bool isFlying = false;
        private bool isTakeOffProcedureWorking = false;
        private bool isHovering = false;
        private bool isEmergency = false;

        public MainForm()
        {
            InitializeComponent();
            input = new InputManager(this);

            timerInputUpdate.Start();
            timerARDroneUpdate.Start();

            UpdateStatus();
            UpdateUI();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            buttonShutdown.Enabled = false;
        }

        private void Connect()
        {
            if (isConnected) { return; }

            int connectResult = 0;
            if (droneEnabled) connectResult = arDroneControl.Connect();

            if (connectResult == 0)
            {
                textboxOutput.AppendText("Connected to Drone\r\n");
                isConnected = true;
            }
            else
            {
                textboxOutput.AppendText("InitDrone() returned " + connectResult.ToString() + "\r\n");
            }

            UpdateUI();
        }

        private void Disconnect()
        {
            if (!isConnected) { return; }

            isFlying = false;
            isHovering = false;
            isEmergency = false;

            if (!droneEnabled || arDroneControl.Shutdown())
            {
                textboxOutput.AppendText("Shutdown Drone\r\n");
                isConnected = false;
            }
            else
            {
                textboxOutput.AppendText("Error shutting down Drone\r\n");
            }

            UpdateUI();
        }

        private void SwapCamera()
        {
            if (!isConnected) { return; }

            if (droneEnabled) arDroneControl.SwapCamera();
            textboxOutput.AppendText("Changing camera\r\n");

            UpdateUI();
        }

        private void StartTakeOffProcedure()
        {
            if (!isConnected || isEmergency) { return; }

            if (droneEnabled) arDroneControl.Takeoff();
            textboxOutput.AppendText("Starting take off procedure\r\n");

            isFlying = true;
            isTakeOffProcedureWorking = true;

            timerStartDelay.Enabled = true;

            UpdateUI();
        }

        private void EndTakeOffProcedure()
        {
            if (!isConnected) { return; }

            textboxOutput.AppendText("Ending take off procedure\r\n");

            timerStartDelay.Enabled = false;
            isTakeOffProcedureWorking = false;
        }

        private void Land()
        {
            if (!isConnected || isTakeOffProcedureWorking) { return; }

            if (droneEnabled) arDroneControl.Land();
            textboxOutput.AppendText("Landing\r\n");

            isFlying = false;
            isHovering = false;

            UpdateUI();
        }

        private void Emergency()
        {
            if (!isConnected && !isEmergency) { return; }

            if (droneEnabled) arDroneControl.Emergency();
            textboxOutput.AppendText("Emergency button hit\r\n");

            timerStartDelay.Enabled = false;
 
            isEmergency = true;
            isFlying = false;
            isTakeOffProcedureWorking = false;
            isHovering = false;

            UpdateUI();
        }

        private void FlatTrim()
        {
            if (!isConnected) { return; }

            if (droneEnabled) arDroneControl.FlatTrim();
            textboxOutput.AppendText("Sending flat trim\r\n");

            isEmergency = false;

            UpdateUI();
        }

        private void Navigate(float roll, float pitch, float yaw, float gaz)
        {
            if (!isConnected) { return; }

            if (!isHovering && droneEnabled)
            {
                arDroneControl.SendCommand(false, roll, pitch, gaz, yaw);
             }
        }

        private void EnterHoveringMode()
        {
            if (!isConnected) { return; }

            if (droneEnabled) arDroneControl.SendCommand(true, 0.0f, 0.0f, 0.0f, 0.0f);
            textboxOutput.AppendText("Entering hover mode\r\n");

            isHovering = true;

            UpdateUI();
        }

        private void LeaveHoveringMode()
        {
            if (!isConnected) { return; }

            textboxOutput.AppendText("Leaving hover mode\r\n");

            isHovering = false;
            UpdateUI();
        }

        private void UpdateUI()
        {
            input.SetFlags(isConnected, isEmergency, isFlying, isHovering);

            if (isConnected)
            {
                buttonShutdown.Enabled = true;
                buttonConnect.Enabled = false;

                buttonChangeCamera.Enabled = true;
                buttonEmergency.Enabled = true;
                buttonFlattrim.Enabled = true;
                buttonStartStop.Enabled = true;
                buttonHover.Enabled = true;
            }
            else
            {
                buttonShutdown.Enabled = false;
                buttonConnect.Enabled = true;

                buttonChangeCamera.Enabled = false;
                buttonEmergency.Enabled = false;
                buttonFlattrim.Enabled = false;
                buttonStartStop.Enabled = false;
                buttonHover.Enabled = false;

                return;
            }

            if (isEmergency)
            {
                buttonStartStop.Enabled = false;
                buttonHover.Enabled = false;
            }
            else
            {
                buttonStartStop.Enabled = true;
                buttonHover.Enabled = true;
            }

            if (!isFlying)
            {
                buttonHover.Enabled = false;

                buttonStartStop.Text = "Take off";
            }
            else
            {
                buttonHover.Enabled = true;

                buttonStartStop.Text = "Land";
            }

            if (!isHovering)
            {
                buttonHover.Text = "Start hover";
            }
            else
            {
                buttonHover.Text = "Stop hover";
            }
        }

        private void UpdateStatus()
        {
            if (!isConnected)
            {
                labelCamera.Text = "No picture";
                labelCameraStatus.Text = "None";

                labelBatteryStatus.Text = "N/A";
                labelAltitudeStatus.Text = "N/A";
            }
            else
            {
                ARDroneFormsControl.ARDroneControl.DroneData data  = new ARDroneFormsControl.ARDroneControl.DroneData();
                if (droneEnabled) data = arDroneControl.GetCurrentDroneData();

                if (!droneEnabled || arDroneControl.CurrentCameraType == ARDroneFormsControl.ARDroneControl.CameraType.FrontCamera)
                {
                    labelCamera.Text = "Front camera";
                    labelCameraStatus.Text = "Front";
                }
                else
                {
                    labelCamera.Text = "Bottom camera";
                    labelCameraStatus.Text = "Bottom";
                }

                labelBatteryStatus.Text = data.BatteryLevel.ToString() + "%";
                labelAltitudeStatus.Text = data.Altitude.ToString();
            }

            labelConnectedStatus.Text = isConnected.ToString();
            labelFlyingStatus.Text = isFlying.ToString();
            labelHoveringStatus.Text = isHovering.ToString();
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

        private void buttonChangeCamera_Click(object sender, EventArgs e)
        {
            SwapCamera();
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (!isFlying)
            {
                StartTakeOffProcedure();
            }
            else
            {
                Land();
            }
        }

        private void buttonHover_Click(object sender, EventArgs e)
        {
            if (!isHovering)
            {
                EnterHoveringMode();
            }
            else
            {
                LeaveHoveringMode();
            }
        }

        private void buttonEmergency_Click(object sender, EventArgs e)
        {
            Emergency();
        }

        private void buttonFlattrim_Click(object sender, EventArgs e)
        {
            FlatTrim();
        }
        
        private void timerARDroneUpdate_Tick(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void timerInputUpdate_Tick(object sender, EventArgs e)
        {
            Image currentImage = arDroneControl.GetDisplayedImage();
            if (currentImage != null)
            {
                Image oldImage = pictureBox1.Image;
                pictureBox1.Image = currentImage;
                if (oldImage != null)
                {
                    oldImage.Dispose();
                }
            }



            InputState inputState = input.GetCurrentState();

            textBoxXAxis.Text = inputState.Roll.ToString();
            textBoxYAxis.Text = (-inputState.Pitch).ToString();
            textBoxRAxis.Text = inputState.Yaw.ToString();
            textBoxZAxis.Text = inputState.Gaz.ToString();

            checkBoxTakeOff.Checked = inputState.TakeOff;
            checkBoxLand.Checked = inputState.Land;
            checkBoxHover.Checked = inputState.Hover;
            checkBoxEmergency.Checked = inputState.Emergency;
            checkBoxFlatTrim.Checked = inputState.FlatTrim;
            checkBoxCameraSwap.Checked = inputState.CameraSwap;

            if (inputState.CameraSwap)
            {
                SwapCamera();
            }

            if (inputState.TakeOff && !isFlying)
            {
                StartTakeOffProcedure();
            }
            else if (inputState.Land && isFlying)
            {
                Land();
            }

            if (inputState.Hover && !isHovering)
            {
                EnterHoveringMode();
            }
            else if (inputState.Hover && isHovering)
            {
                LeaveHoveringMode();
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

        private void timerStartDelay_Tick(object sender, EventArgs e)
        {
            EndTakeOffProcedure();
        }
    }
}