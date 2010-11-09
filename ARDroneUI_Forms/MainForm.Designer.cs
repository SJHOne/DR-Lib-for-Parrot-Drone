namespace ARDrone.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textboxOutput = new System.Windows.Forms.TextBox();
            this.buttonShutdown = new System.Windows.Forms.Button();
            this.buttonCommandEmergency = new System.Windows.Forms.Button();
            this.buttonCommandChangeCamera = new System.Windows.Forms.Button();
            this.buttonCommandTakeoff = new System.Windows.Forms.Button();
            this.buttonCommandFlatTrim = new System.Windows.Forms.Button();
            this.timerStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.labelCamera = new System.Windows.Forms.Label();
            this.timerInputUpdate = new System.Windows.Forms.Timer(this.components);
            this.checkBoxInputFlatTrim = new System.Windows.Forms.CheckBox();
            this.checkBoxInputEmergency = new System.Windows.Forms.CheckBox();
            this.checkBoxInputTakeoff = new System.Windows.Forms.CheckBox();
            this.checkBoxInputLand = new System.Windows.Forms.CheckBox();
            this.labelInputGazInfo = new System.Windows.Forms.Label();
            this.labelInputYawInfo = new System.Windows.Forms.Label();
            this.labelInputPitchInfo = new System.Windows.Forms.Label();
            this.labelInputRollInfo = new System.Windows.Forms.Label();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.labelInputRoll = new System.Windows.Forms.Label();
            this.labelInputGaz = new System.Windows.Forms.Label();
            this.labelInputPitch = new System.Windows.Forms.Label();
            this.labelInputYaw = new System.Windows.Forms.Label();
            this.checkBoxInputCameraSwap = new System.Windows.Forms.CheckBox();
            this.checkBoxInputHover = new System.Windows.Forms.CheckBox();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.labelStatusEmergency = new System.Windows.Forms.Label();
            this.labelStatusEmergencyInfo = new System.Windows.Forms.Label();
            this.labelStatusConnected = new System.Windows.Forms.Label();
            this.labelStatusConnectedInfo = new System.Windows.Forms.Label();
            this.labelStatusHovering = new System.Windows.Forms.Label();
            this.labelStatusFlying = new System.Windows.Forms.Label();
            this.labelStatusAltitude = new System.Windows.Forms.Label();
            this.labelStatusCamera = new System.Windows.Forms.Label();
            this.labelStatusBattery = new System.Windows.Forms.Label();
            this.labelStatusHoveringInfo = new System.Windows.Forms.Label();
            this.labelStatusFlyingInfo = new System.Windows.Forms.Label();
            this.labelStatusAltitudeInfo = new System.Windows.Forms.Label();
            this.labelStatusCameraInfo = new System.Windows.Forms.Label();
            this.labelStatusBatteryInfo = new System.Windows.Forms.Label();
            this.buttonCommandHover = new System.Windows.Forms.Button();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.pictureBoxVideo = new System.Windows.Forms.PictureBox();
            this.timerVideoUpdate = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxStatus.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.panelCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonConnect.Location = new System.Drawing.Point(0, 0);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 25);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Startup";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textboxOutput
            // 
            this.textboxOutput.BackColor = System.Drawing.SystemColors.MenuText;
            this.textboxOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textboxOutput.ForeColor = System.Drawing.Color.Yellow;
            this.textboxOutput.Location = new System.Drawing.Point(1, 3);
            this.textboxOutput.Multiline = true;
            this.textboxOutput.Name = "textboxOutput";
            this.textboxOutput.ReadOnly = true;
            this.textboxOutput.Size = new System.Drawing.Size(358, 160);
            this.textboxOutput.TabIndex = 1;
            // 
            // buttonShutdown
            // 
            this.buttonShutdown.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonShutdown.Location = new System.Drawing.Point(405, 0);
            this.buttonShutdown.Name = "buttonShutdown";
            this.buttonShutdown.Size = new System.Drawing.Size(75, 25);
            this.buttonShutdown.TabIndex = 2;
            this.buttonShutdown.Text = "Shutdown";
            this.buttonShutdown.UseVisualStyleBackColor = true;
            this.buttonShutdown.Click += new System.EventHandler(this.buttonShutdown_Click);
            // 
            // buttonCommandEmergency
            // 
            this.buttonCommandEmergency.Location = new System.Drawing.Point(361, 67);
            this.buttonCommandEmergency.Name = "buttonCommandEmergency";
            this.buttonCommandEmergency.Size = new System.Drawing.Size(107, 23);
            this.buttonCommandEmergency.TabIndex = 7;
            this.buttonCommandEmergency.Text = "Emergency";
            this.buttonCommandEmergency.UseVisualStyleBackColor = true;
            this.buttonCommandEmergency.Click += new System.EventHandler(this.buttonCommandEmergency_Click);
            // 
            // buttonCommandChangeCamera
            // 
            this.buttonCommandChangeCamera.Location = new System.Drawing.Point(361, 137);
            this.buttonCommandChangeCamera.Name = "buttonCommandChangeCamera";
            this.buttonCommandChangeCamera.Size = new System.Drawing.Size(107, 23);
            this.buttonCommandChangeCamera.TabIndex = 8;
            this.buttonCommandChangeCamera.Text = "Change camera";
            this.buttonCommandChangeCamera.UseVisualStyleBackColor = true;
            this.buttonCommandChangeCamera.Click += new System.EventHandler(this.buttonCommandChangeCamera_Click);
            // 
            // buttonCommandTakeoff
            // 
            this.buttonCommandTakeoff.Location = new System.Drawing.Point(361, 3);
            this.buttonCommandTakeoff.Name = "buttonCommandTakeoff";
            this.buttonCommandTakeoff.Size = new System.Drawing.Size(107, 23);
            this.buttonCommandTakeoff.TabIndex = 9;
            this.buttonCommandTakeoff.Text = "Take off";
            this.buttonCommandTakeoff.UseVisualStyleBackColor = true;
            this.buttonCommandTakeoff.Click += new System.EventHandler(this.buttonCommandTakeoff_Click);
            // 
            // buttonCommandFlatTrim
            // 
            this.buttonCommandFlatTrim.Location = new System.Drawing.Point(361, 92);
            this.buttonCommandFlatTrim.Name = "buttonCommandFlatTrim";
            this.buttonCommandFlatTrim.Size = new System.Drawing.Size(107, 23);
            this.buttonCommandFlatTrim.TabIndex = 17;
            this.buttonCommandFlatTrim.Text = "Flat trim";
            this.buttonCommandFlatTrim.UseVisualStyleBackColor = true;
            this.buttonCommandFlatTrim.Click += new System.EventHandler(this.buttonCommandFlatTrim_Click);
            // 
            // timerStatusUpdate
            // 
            this.timerStatusUpdate.Interval = 1000;
            this.timerStatusUpdate.Tick += new System.EventHandler(this.timerStatusUpdate_Tick);
            // 
            // labelCamera
            // 
            this.labelCamera.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamera.ForeColor = System.Drawing.Color.Goldenrod;
            this.labelCamera.Location = new System.Drawing.Point(75, 0);
            this.labelCamera.Name = "labelCamera";
            this.labelCamera.Size = new System.Drawing.Size(330, 25);
            this.labelCamera.TabIndex = 19;
            this.labelCamera.Text = "No picture";
            this.labelCamera.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerInputUpdate
            // 
            this.timerInputUpdate.Interval = 50;
            this.timerInputUpdate.Tick += new System.EventHandler(this.timerInputUpdate_Tick);
            // 
            // checkBoxInputFlatTrim
            // 
            this.checkBoxInputFlatTrim.AutoSize = true;
            this.checkBoxInputFlatTrim.Enabled = false;
            this.checkBoxInputFlatTrim.Location = new System.Drawing.Point(104, 130);
            this.checkBoxInputFlatTrim.Name = "checkBoxInputFlatTrim";
            this.checkBoxInputFlatTrim.Size = new System.Drawing.Size(62, 17);
            this.checkBoxInputFlatTrim.TabIndex = 31;
            this.checkBoxInputFlatTrim.Text = "Flat trim";
            this.checkBoxInputFlatTrim.UseVisualStyleBackColor = true;
            // 
            // checkBoxInputEmergency
            // 
            this.checkBoxInputEmergency.AutoSize = true;
            this.checkBoxInputEmergency.Enabled = false;
            this.checkBoxInputEmergency.Location = new System.Drawing.Point(13, 130);
            this.checkBoxInputEmergency.Name = "checkBoxInputEmergency";
            this.checkBoxInputEmergency.Size = new System.Drawing.Size(79, 17);
            this.checkBoxInputEmergency.TabIndex = 30;
            this.checkBoxInputEmergency.Text = "Emergency";
            this.checkBoxInputEmergency.UseVisualStyleBackColor = true;
            // 
            // checkBoxInputTakeoff
            // 
            this.checkBoxInputTakeoff.AutoSize = true;
            this.checkBoxInputTakeoff.Enabled = false;
            this.checkBoxInputTakeoff.Location = new System.Drawing.Point(13, 107);
            this.checkBoxInputTakeoff.Name = "checkBoxInputTakeoff";
            this.checkBoxInputTakeoff.Size = new System.Drawing.Size(66, 17);
            this.checkBoxInputTakeoff.TabIndex = 28;
            this.checkBoxInputTakeoff.Text = "Take off";
            this.checkBoxInputTakeoff.UseVisualStyleBackColor = true;
            // 
            // checkBoxInputLand
            // 
            this.checkBoxInputLand.AutoSize = true;
            this.checkBoxInputLand.Enabled = false;
            this.checkBoxInputLand.Location = new System.Drawing.Point(104, 107);
            this.checkBoxInputLand.Name = "checkBoxInputLand";
            this.checkBoxInputLand.Size = new System.Drawing.Size(50, 17);
            this.checkBoxInputLand.TabIndex = 29;
            this.checkBoxInputLand.Text = "Land";
            this.checkBoxInputLand.UseVisualStyleBackColor = true;
            // 
            // labelInputGazInfo
            // 
            this.labelInputGazInfo.AutoSize = true;
            this.labelInputGazInfo.Location = new System.Drawing.Point(13, 82);
            this.labelInputGazInfo.Name = "labelInputGazInfo";
            this.labelInputGazInfo.Size = new System.Drawing.Size(26, 13);
            this.labelInputGazInfo.TabIndex = 27;
            this.labelInputGazInfo.Text = "Gaz";
            // 
            // labelInputYawInfo
            // 
            this.labelInputYawInfo.AutoSize = true;
            this.labelInputYawInfo.Location = new System.Drawing.Point(13, 62);
            this.labelInputYawInfo.Name = "labelInputYawInfo";
            this.labelInputYawInfo.Size = new System.Drawing.Size(28, 13);
            this.labelInputYawInfo.TabIndex = 26;
            this.labelInputYawInfo.Text = "Yaw";
            // 
            // labelInputPitchInfo
            // 
            this.labelInputPitchInfo.AutoSize = true;
            this.labelInputPitchInfo.Location = new System.Drawing.Point(13, 42);
            this.labelInputPitchInfo.Name = "labelInputPitchInfo";
            this.labelInputPitchInfo.Size = new System.Drawing.Size(31, 13);
            this.labelInputPitchInfo.TabIndex = 25;
            this.labelInputPitchInfo.Text = "Pitch";
            // 
            // labelInputRollInfo
            // 
            this.labelInputRollInfo.AutoSize = true;
            this.labelInputRollInfo.Location = new System.Drawing.Point(13, 22);
            this.labelInputRollInfo.Name = "labelInputRollInfo";
            this.labelInputRollInfo.Size = new System.Drawing.Size(25, 13);
            this.labelInputRollInfo.TabIndex = 24;
            this.labelInputRollInfo.Text = "Roll";
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.labelInputRoll);
            this.groupBoxInput.Controls.Add(this.labelInputGaz);
            this.groupBoxInput.Controls.Add(this.labelInputPitch);
            this.groupBoxInput.Controls.Add(this.labelInputYaw);
            this.groupBoxInput.Controls.Add(this.checkBoxInputCameraSwap);
            this.groupBoxInput.Controls.Add(this.checkBoxInputHover);
            this.groupBoxInput.Controls.Add(this.checkBoxInputFlatTrim);
            this.groupBoxInput.Controls.Add(this.checkBoxInputEmergency);
            this.groupBoxInput.Controls.Add(this.checkBoxInputTakeoff);
            this.groupBoxInput.Controls.Add(this.checkBoxInputLand);
            this.groupBoxInput.Controls.Add(this.labelInputRollInfo);
            this.groupBoxInput.Controls.Add(this.labelInputGazInfo);
            this.groupBoxInput.Controls.Add(this.labelInputPitchInfo);
            this.groupBoxInput.Controls.Add(this.labelInputYawInfo);
            this.groupBoxInput.Location = new System.Drawing.Point(3, 6);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(184, 179);
            this.groupBoxInput.TabIndex = 32;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input";
            // 
            // labelInputRoll
            // 
            this.labelInputRoll.AutoSize = true;
            this.labelInputRoll.Location = new System.Drawing.Point(92, 22);
            this.labelInputRoll.Name = "labelInputRoll";
            this.labelInputRoll.Size = new System.Drawing.Size(34, 13);
            this.labelInputRoll.TabIndex = 34;
            this.labelInputRoll.Text = "0,000";
            // 
            // labelInputGaz
            // 
            this.labelInputGaz.AutoSize = true;
            this.labelInputGaz.Location = new System.Drawing.Point(92, 82);
            this.labelInputGaz.Name = "labelInputGaz";
            this.labelInputGaz.Size = new System.Drawing.Size(34, 13);
            this.labelInputGaz.TabIndex = 37;
            this.labelInputGaz.Text = "0,000";
            // 
            // labelInputPitch
            // 
            this.labelInputPitch.AutoSize = true;
            this.labelInputPitch.Location = new System.Drawing.Point(92, 42);
            this.labelInputPitch.Name = "labelInputPitch";
            this.labelInputPitch.Size = new System.Drawing.Size(34, 13);
            this.labelInputPitch.TabIndex = 35;
            this.labelInputPitch.Text = "0,000";
            // 
            // labelInputYaw
            // 
            this.labelInputYaw.AutoSize = true;
            this.labelInputYaw.Location = new System.Drawing.Point(92, 62);
            this.labelInputYaw.Name = "labelInputYaw";
            this.labelInputYaw.Size = new System.Drawing.Size(34, 13);
            this.labelInputYaw.TabIndex = 36;
            this.labelInputYaw.Text = "0,000";
            // 
            // checkBoxInputCameraSwap
            // 
            this.checkBoxInputCameraSwap.AutoSize = true;
            this.checkBoxInputCameraSwap.Enabled = false;
            this.checkBoxInputCameraSwap.Location = new System.Drawing.Point(104, 153);
            this.checkBoxInputCameraSwap.Name = "checkBoxInputCameraSwap";
            this.checkBoxInputCameraSwap.Size = new System.Drawing.Size(62, 17);
            this.checkBoxInputCameraSwap.TabIndex = 33;
            this.checkBoxInputCameraSwap.Text = "Camera";
            this.checkBoxInputCameraSwap.UseVisualStyleBackColor = true;
            // 
            // checkBoxInputHover
            // 
            this.checkBoxInputHover.AutoSize = true;
            this.checkBoxInputHover.Enabled = false;
            this.checkBoxInputHover.Location = new System.Drawing.Point(13, 153);
            this.checkBoxInputHover.Name = "checkBoxInputHover";
            this.checkBoxInputHover.Size = new System.Drawing.Size(55, 17);
            this.checkBoxInputHover.TabIndex = 32;
            this.checkBoxInputHover.Text = "Hover";
            this.checkBoxInputHover.UseVisualStyleBackColor = true;
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Controls.Add(this.labelStatusEmergency);
            this.groupBoxStatus.Controls.Add(this.labelStatusEmergencyInfo);
            this.groupBoxStatus.Controls.Add(this.labelStatusConnected);
            this.groupBoxStatus.Controls.Add(this.labelStatusConnectedInfo);
            this.groupBoxStatus.Controls.Add(this.labelStatusHovering);
            this.groupBoxStatus.Controls.Add(this.labelStatusFlying);
            this.groupBoxStatus.Controls.Add(this.labelStatusAltitude);
            this.groupBoxStatus.Controls.Add(this.labelStatusCamera);
            this.groupBoxStatus.Controls.Add(this.labelStatusBattery);
            this.groupBoxStatus.Controls.Add(this.labelStatusHoveringInfo);
            this.groupBoxStatus.Controls.Add(this.labelStatusFlyingInfo);
            this.groupBoxStatus.Controls.Add(this.labelStatusAltitudeInfo);
            this.groupBoxStatus.Controls.Add(this.labelStatusCameraInfo);
            this.groupBoxStatus.Controls.Add(this.labelStatusBatteryInfo);
            this.groupBoxStatus.Location = new System.Drawing.Point(3, 191);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(184, 179);
            this.groupBoxStatus.TabIndex = 33;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // labelStatusEmergency
            // 
            this.labelStatusEmergency.AutoSize = true;
            this.labelStatusEmergency.Location = new System.Drawing.Point(133, 155);
            this.labelStatusEmergency.Name = "labelStatusEmergency";
            this.labelStatusEmergency.Size = new System.Drawing.Size(29, 13);
            this.labelStatusEmergency.TabIndex = 42;
            this.labelStatusEmergency.Text = "false";
            // 
            // labelStatusEmergencyInfo
            // 
            this.labelStatusEmergencyInfo.AutoSize = true;
            this.labelStatusEmergencyInfo.Location = new System.Drawing.Point(14, 155);
            this.labelStatusEmergencyInfo.Name = "labelStatusEmergencyInfo";
            this.labelStatusEmergencyInfo.Size = new System.Drawing.Size(60, 13);
            this.labelStatusEmergencyInfo.TabIndex = 41;
            this.labelStatusEmergencyInfo.Text = "Emergency";
            // 
            // labelStatusConnected
            // 
            this.labelStatusConnected.AutoSize = true;
            this.labelStatusConnected.Location = new System.Drawing.Point(133, 95);
            this.labelStatusConnected.Name = "labelStatusConnected";
            this.labelStatusConnected.Size = new System.Drawing.Size(29, 13);
            this.labelStatusConnected.TabIndex = 40;
            this.labelStatusConnected.Text = "false";
            // 
            // labelStatusConnectedInfo
            // 
            this.labelStatusConnectedInfo.AutoSize = true;
            this.labelStatusConnectedInfo.Location = new System.Drawing.Point(14, 95);
            this.labelStatusConnectedInfo.Name = "labelStatusConnectedInfo";
            this.labelStatusConnectedInfo.Size = new System.Drawing.Size(59, 13);
            this.labelStatusConnectedInfo.TabIndex = 39;
            this.labelStatusConnectedInfo.Text = "Connected";
            // 
            // labelStatusHovering
            // 
            this.labelStatusHovering.AutoSize = true;
            this.labelStatusHovering.Location = new System.Drawing.Point(133, 135);
            this.labelStatusHovering.Name = "labelStatusHovering";
            this.labelStatusHovering.Size = new System.Drawing.Size(29, 13);
            this.labelStatusHovering.TabIndex = 38;
            this.labelStatusHovering.Text = "false";
            // 
            // labelStatusFlying
            // 
            this.labelStatusFlying.AutoSize = true;
            this.labelStatusFlying.Location = new System.Drawing.Point(133, 115);
            this.labelStatusFlying.Name = "labelStatusFlying";
            this.labelStatusFlying.Size = new System.Drawing.Size(29, 13);
            this.labelStatusFlying.TabIndex = 37;
            this.labelStatusFlying.Text = "false";
            // 
            // labelStatusAltitude
            // 
            this.labelStatusAltitude.AutoSize = true;
            this.labelStatusAltitude.Location = new System.Drawing.Point(133, 60);
            this.labelStatusAltitude.Name = "labelStatusAltitude";
            this.labelStatusAltitude.Size = new System.Drawing.Size(27, 13);
            this.labelStatusAltitude.TabIndex = 36;
            this.labelStatusAltitude.Text = "N/A";
            // 
            // labelStatusCamera
            // 
            this.labelStatusCamera.AutoSize = true;
            this.labelStatusCamera.Location = new System.Drawing.Point(133, 40);
            this.labelStatusCamera.Name = "labelStatusCamera";
            this.labelStatusCamera.Size = new System.Drawing.Size(33, 13);
            this.labelStatusCamera.TabIndex = 35;
            this.labelStatusCamera.Text = "None";
            // 
            // labelStatusBattery
            // 
            this.labelStatusBattery.AutoSize = true;
            this.labelStatusBattery.Location = new System.Drawing.Point(133, 20);
            this.labelStatusBattery.Name = "labelStatusBattery";
            this.labelStatusBattery.Size = new System.Drawing.Size(27, 13);
            this.labelStatusBattery.TabIndex = 34;
            this.labelStatusBattery.Text = "N/A";
            // 
            // labelStatusHoveringInfo
            // 
            this.labelStatusHoveringInfo.AutoSize = true;
            this.labelStatusHoveringInfo.Location = new System.Drawing.Point(14, 135);
            this.labelStatusHoveringInfo.Name = "labelStatusHoveringInfo";
            this.labelStatusHoveringInfo.Size = new System.Drawing.Size(50, 13);
            this.labelStatusHoveringInfo.TabIndex = 4;
            this.labelStatusHoveringInfo.Text = "Hovering";
            // 
            // labelStatusFlyingInfo
            // 
            this.labelStatusFlyingInfo.AutoSize = true;
            this.labelStatusFlyingInfo.Location = new System.Drawing.Point(14, 115);
            this.labelStatusFlyingInfo.Name = "labelStatusFlyingInfo";
            this.labelStatusFlyingInfo.Size = new System.Drawing.Size(34, 13);
            this.labelStatusFlyingInfo.TabIndex = 3;
            this.labelStatusFlyingInfo.Text = "Flying";
            // 
            // labelStatusAltitudeInfo
            // 
            this.labelStatusAltitudeInfo.AutoSize = true;
            this.labelStatusAltitudeInfo.Location = new System.Drawing.Point(14, 60);
            this.labelStatusAltitudeInfo.Name = "labelStatusAltitudeInfo";
            this.labelStatusAltitudeInfo.Size = new System.Drawing.Size(42, 13);
            this.labelStatusAltitudeInfo.TabIndex = 2;
            this.labelStatusAltitudeInfo.Text = "Altitude";
            // 
            // labelStatusCameraInfo
            // 
            this.labelStatusCameraInfo.AutoSize = true;
            this.labelStatusCameraInfo.Location = new System.Drawing.Point(14, 40);
            this.labelStatusCameraInfo.Name = "labelStatusCameraInfo";
            this.labelStatusCameraInfo.Size = new System.Drawing.Size(77, 13);
            this.labelStatusCameraInfo.TabIndex = 1;
            this.labelStatusCameraInfo.Text = "Camera shown";
            // 
            // labelStatusBatteryInfo
            // 
            this.labelStatusBatteryInfo.AutoSize = true;
            this.labelStatusBatteryInfo.Location = new System.Drawing.Point(14, 20);
            this.labelStatusBatteryInfo.Name = "labelStatusBatteryInfo";
            this.labelStatusBatteryInfo.Size = new System.Drawing.Size(71, 13);
            this.labelStatusBatteryInfo.TabIndex = 0;
            this.labelStatusBatteryInfo.Text = "Battery status";
            // 
            // buttonCommandHover
            // 
            this.buttonCommandHover.Location = new System.Drawing.Point(361, 27);
            this.buttonCommandHover.Name = "buttonCommandHover";
            this.buttonCommandHover.Size = new System.Drawing.Size(107, 23);
            this.buttonCommandHover.TabIndex = 34;
            this.buttonCommandHover.Text = "Hover";
            this.buttonCommandHover.UseVisualStyleBackColor = true;
            this.buttonCommandHover.Click += new System.EventHandler(this.buttonCommandHover_Click);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.button1);
            this.panelRight.Controls.Add(this.groupBoxInput);
            this.panelRight.Controls.Add(this.groupBoxStatus);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(480, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(192, 549);
            this.panelRight.TabIndex = 35;
            // 
            // panelBottom
            // 
            this.panelBottom.Controls.Add(this.textboxOutput);
            this.panelBottom.Controls.Add(this.buttonCommandEmergency);
            this.panelBottom.Controls.Add(this.buttonCommandHover);
            this.panelBottom.Controls.Add(this.buttonCommandTakeoff);
            this.panelBottom.Controls.Add(this.buttonCommandFlatTrim);
            this.panelBottom.Controls.Add(this.buttonCommandChangeCamera);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 385);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(480, 164);
            this.panelBottom.TabIndex = 36;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelCamera);
            this.panelTop.Controls.Add(this.buttonConnect);
            this.panelTop.Controls.Add(this.buttonShutdown);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(480, 25);
            this.panelTop.TabIndex = 37;
            // 
            // panelCenter
            // 
            this.panelCenter.Controls.Add(this.pictureBoxVideo);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 25);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Size = new System.Drawing.Size(480, 360);
            this.panelCenter.TabIndex = 38;
            // 
            // pictureBoxVideo
            // 
            this.pictureBoxVideo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxVideo.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxVideo.Name = "pictureBoxVideo";
            this.pictureBoxVideo.Size = new System.Drawing.Size(480, 360);
            this.pictureBoxVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxVideo.TabIndex = 0;
            this.pictureBoxVideo.TabStop = false;
            // 
            // timerVideoUpdate
            // 
            this.timerVideoUpdate.Interval = 50;
            this.timerVideoUpdate.Tick += new System.EventHandler(this.timerVideoUpdate_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 388);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "Input Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(672, 549);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelRight);
            this.MinimumSize = new System.Drawing.Size(688, 587);
            this.Name = "MainForm";
            this.Text = "Drone Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textboxOutput;
        private System.Windows.Forms.Button buttonShutdown;
        private System.Windows.Forms.Button buttonCommandEmergency;
        private System.Windows.Forms.Button buttonCommandChangeCamera;
        private System.Windows.Forms.Button buttonCommandTakeoff;
        private System.Windows.Forms.Button buttonCommandFlatTrim;
        private System.Windows.Forms.Timer timerStatusUpdate;
        private System.Windows.Forms.Label labelCamera;
        private System.Windows.Forms.Timer timerInputUpdate;
        private System.Windows.Forms.CheckBox checkBoxInputFlatTrim;
        private System.Windows.Forms.CheckBox checkBoxInputEmergency;
        private System.Windows.Forms.CheckBox checkBoxInputTakeoff;
        private System.Windows.Forms.CheckBox checkBoxInputLand;
        private System.Windows.Forms.Label labelInputGazInfo;
        private System.Windows.Forms.Label labelInputYawInfo;
        private System.Windows.Forms.Label labelInputPitchInfo;
        private System.Windows.Forms.Label labelInputRollInfo;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Label labelStatusHovering;
        private System.Windows.Forms.Label labelStatusFlying;
        private System.Windows.Forms.Label labelStatusAltitude;
        private System.Windows.Forms.Label labelStatusCamera;
        private System.Windows.Forms.Label labelStatusBattery;
        private System.Windows.Forms.Label labelStatusHoveringInfo;
        private System.Windows.Forms.Label labelStatusFlyingInfo;
        private System.Windows.Forms.Label labelStatusAltitudeInfo;
        private System.Windows.Forms.Label labelStatusCameraInfo;
        private System.Windows.Forms.Label labelStatusBatteryInfo;
        private System.Windows.Forms.Label labelStatusConnected;
        private System.Windows.Forms.Label labelStatusConnectedInfo;
        private System.Windows.Forms.Button buttonCommandHover;
        private System.Windows.Forms.Label labelStatusEmergency;
        private System.Windows.Forms.Label labelStatusEmergencyInfo;
        private System.Windows.Forms.CheckBox checkBoxInputHover;
        private System.Windows.Forms.CheckBox checkBoxInputCameraSwap;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.PictureBox pictureBoxVideo;
        private System.Windows.Forms.Timer timerVideoUpdate;
        private System.Windows.Forms.Label labelInputRoll;
        private System.Windows.Forms.Label labelInputGaz;
        private System.Windows.Forms.Label labelInputPitch;
        private System.Windows.Forms.Label labelInputYaw;
        private System.Windows.Forms.Button button1;
    }
}

