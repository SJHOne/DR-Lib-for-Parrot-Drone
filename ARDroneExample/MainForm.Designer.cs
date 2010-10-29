namespace ARDrone
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
            this.buttonEmergency = new System.Windows.Forms.Button();
            this.buttonChangeCamera = new System.Windows.Forms.Button();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.buttonFlattrim = new System.Windows.Forms.Button();
            this.timerARDroneUpdate = new System.Windows.Forms.Timer(this.components);
            this.labelCamera = new System.Windows.Forms.Label();
            this.timerInputUpdate = new System.Windows.Forms.Timer(this.components);
            this.checkBoxFlatTrim = new System.Windows.Forms.CheckBox();
            this.checkBoxEmergency = new System.Windows.Forms.CheckBox();
            this.checkBoxTakeOff = new System.Windows.Forms.CheckBox();
            this.checkBoxLand = new System.Windows.Forms.CheckBox();
            this.labelZAxis = new System.Windows.Forms.Label();
            this.labelRAxis = new System.Windows.Forms.Label();
            this.labelYAxis = new System.Windows.Forms.Label();
            this.labelXAxis = new System.Windows.Forms.Label();
            this.textBoxRAxis = new System.Windows.Forms.TextBox();
            this.textBoxZAxis = new System.Windows.Forms.TextBox();
            this.textBoxYAxis = new System.Windows.Forms.TextBox();
            this.textBoxXAxis = new System.Windows.Forms.TextBox();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.checkBoxCameraSwap = new System.Windows.Forms.CheckBox();
            this.checkBoxHover = new System.Windows.Forms.CheckBox();
            this.groupBoxStatus = new System.Windows.Forms.GroupBox();
            this.labelEmergencyStatus = new System.Windows.Forms.Label();
            this.labelEmergencyStatusInfo = new System.Windows.Forms.Label();
            this.labelConnectedStatus = new System.Windows.Forms.Label();
            this.labelConnectedStatusInfo = new System.Windows.Forms.Label();
            this.labelHoveringStatus = new System.Windows.Forms.Label();
            this.labelFlyingStatus = new System.Windows.Forms.Label();
            this.labelAltitudeStatus = new System.Windows.Forms.Label();
            this.labelCameraStatus = new System.Windows.Forms.Label();
            this.labelBatteryStatus = new System.Windows.Forms.Label();
            this.labelHoveringStatusInfo = new System.Windows.Forms.Label();
            this.labelFlyingStatusInfo = new System.Windows.Forms.Label();
            this.labelAltitudeStatusInfo = new System.Windows.Forms.Label();
            this.labelCameraStatusInfo = new System.Windows.Forms.Label();
            this.labelBatteryStatusInfo = new System.Windows.Forms.Label();
            this.buttonHover = new System.Windows.Forms.Button();
            this.timerStartDelay = new System.Windows.Forms.Timer(this.components);
            this.arDroneControl = new ARDroneFormsControl.ARDroneControl();
            this.groupBoxInput.SuspendLayout();
            this.groupBoxStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(13, 6);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
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
            this.textboxOutput.Location = new System.Drawing.Point(12, 369);
            this.textboxOutput.Multiline = true;
            this.textboxOutput.Name = "textboxOutput";
            this.textboxOutput.ReadOnly = true;
            this.textboxOutput.Size = new System.Drawing.Size(358, 160);
            this.textboxOutput.TabIndex = 1;
            // 
            // buttonShutdown
            // 
            this.buttonShutdown.Location = new System.Drawing.Point(295, 5);
            this.buttonShutdown.Name = "buttonShutdown";
            this.buttonShutdown.Size = new System.Drawing.Size(75, 23);
            this.buttonShutdown.TabIndex = 2;
            this.buttonShutdown.Text = "Shutdown";
            this.buttonShutdown.UseVisualStyleBackColor = true;
            this.buttonShutdown.Click += new System.EventHandler(this.buttonShutdown_Click);
            // 
            // buttonEmergency
            // 
            this.buttonEmergency.Location = new System.Drawing.Point(157, 310);
            this.buttonEmergency.Name = "buttonEmergency";
            this.buttonEmergency.Size = new System.Drawing.Size(107, 23);
            this.buttonEmergency.TabIndex = 7;
            this.buttonEmergency.Text = "Emergency";
            this.buttonEmergency.UseVisualStyleBackColor = true;
            this.buttonEmergency.Click += new System.EventHandler(this.buttonEmergency_Click);
            // 
            // buttonChangeCamera
            // 
            this.buttonChangeCamera.Location = new System.Drawing.Point(12, 311);
            this.buttonChangeCamera.Name = "buttonChangeCamera";
            this.buttonChangeCamera.Size = new System.Drawing.Size(107, 23);
            this.buttonChangeCamera.TabIndex = 8;
            this.buttonChangeCamera.Text = "Change camera";
            this.buttonChangeCamera.UseVisualStyleBackColor = true;
            this.buttonChangeCamera.Click += new System.EventHandler(this.buttonChangeCamera_Click);
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Location = new System.Drawing.Point(270, 311);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(100, 23);
            this.buttonStartStop.TabIndex = 9;
            this.buttonStartStop.Text = "Take off";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // buttonFlattrim
            // 
            this.buttonFlattrim.Location = new System.Drawing.Point(157, 340);
            this.buttonFlattrim.Name = "buttonFlattrim";
            this.buttonFlattrim.Size = new System.Drawing.Size(107, 23);
            this.buttonFlattrim.TabIndex = 17;
            this.buttonFlattrim.Text = "Flat trim";
            this.buttonFlattrim.UseVisualStyleBackColor = true;
            this.buttonFlattrim.Click += new System.EventHandler(this.buttonFlattrim_Click);
            // 
            // timerARDroneUpdate
            // 
            this.timerARDroneUpdate.Interval = 1000;
            this.timerARDroneUpdate.Tick += new System.EventHandler(this.timerARDroneUpdate_Tick);
            // 
            // labelCamera
            // 
            this.labelCamera.AutoSize = true;
            this.labelCamera.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamera.ForeColor = System.Drawing.Color.Goldenrod;
            this.labelCamera.Location = new System.Drawing.Point(151, 11);
            this.labelCamera.Name = "labelCamera";
            this.labelCamera.Size = new System.Drawing.Size(69, 16);
            this.labelCamera.TabIndex = 19;
            this.labelCamera.Text = "No picture";
            // 
            // timerInputUpdate
            // 
            this.timerInputUpdate.Interval = 50;
            this.timerInputUpdate.Tick += new System.EventHandler(this.timerInputUpdate_Tick);
            // 
            // checkBoxFlatTrim
            // 
            this.checkBoxFlatTrim.AutoSize = true;
            this.checkBoxFlatTrim.Enabled = false;
            this.checkBoxFlatTrim.Location = new System.Drawing.Point(104, 154);
            this.checkBoxFlatTrim.Name = "checkBoxFlatTrim";
            this.checkBoxFlatTrim.Size = new System.Drawing.Size(62, 17);
            this.checkBoxFlatTrim.TabIndex = 31;
            this.checkBoxFlatTrim.Text = "Flat trim";
            this.checkBoxFlatTrim.UseVisualStyleBackColor = true;
            // 
            // checkBoxEmergency
            // 
            this.checkBoxEmergency.AutoSize = true;
            this.checkBoxEmergency.Enabled = false;
            this.checkBoxEmergency.Location = new System.Drawing.Point(13, 154);
            this.checkBoxEmergency.Name = "checkBoxEmergency";
            this.checkBoxEmergency.Size = new System.Drawing.Size(79, 17);
            this.checkBoxEmergency.TabIndex = 30;
            this.checkBoxEmergency.Text = "Emergency";
            this.checkBoxEmergency.UseVisualStyleBackColor = true;
            // 
            // checkBoxTakeOff
            // 
            this.checkBoxTakeOff.AutoSize = true;
            this.checkBoxTakeOff.Enabled = false;
            this.checkBoxTakeOff.Location = new System.Drawing.Point(13, 131);
            this.checkBoxTakeOff.Name = "checkBoxTakeOff";
            this.checkBoxTakeOff.Size = new System.Drawing.Size(66, 17);
            this.checkBoxTakeOff.TabIndex = 28;
            this.checkBoxTakeOff.Text = "Take off";
            this.checkBoxTakeOff.UseVisualStyleBackColor = true;
            // 
            // checkBoxLand
            // 
            this.checkBoxLand.AutoSize = true;
            this.checkBoxLand.Enabled = false;
            this.checkBoxLand.Location = new System.Drawing.Point(104, 131);
            this.checkBoxLand.Name = "checkBoxLand";
            this.checkBoxLand.Size = new System.Drawing.Size(50, 17);
            this.checkBoxLand.TabIndex = 29;
            this.checkBoxLand.Text = "Land";
            this.checkBoxLand.UseVisualStyleBackColor = true;
            // 
            // labelZAxis
            // 
            this.labelZAxis.AutoSize = true;
            this.labelZAxis.Location = new System.Drawing.Point(13, 100);
            this.labelZAxis.Name = "labelZAxis";
            this.labelZAxis.Size = new System.Drawing.Size(33, 13);
            this.labelZAxis.TabIndex = 27;
            this.labelZAxis.Text = "z axis";
            // 
            // labelRAxis
            // 
            this.labelRAxis.AutoSize = true;
            this.labelRAxis.Location = new System.Drawing.Point(13, 74);
            this.labelRAxis.Name = "labelRAxis";
            this.labelRAxis.Size = new System.Drawing.Size(31, 13);
            this.labelRAxis.TabIndex = 26;
            this.labelRAxis.Text = "r axis";
            // 
            // labelYAxis
            // 
            this.labelYAxis.AutoSize = true;
            this.labelYAxis.Location = new System.Drawing.Point(13, 48);
            this.labelYAxis.Name = "labelYAxis";
            this.labelYAxis.Size = new System.Drawing.Size(33, 13);
            this.labelYAxis.TabIndex = 25;
            this.labelYAxis.Text = "y axis";
            // 
            // labelXAxis
            // 
            this.labelXAxis.AutoSize = true;
            this.labelXAxis.Location = new System.Drawing.Point(13, 22);
            this.labelXAxis.Name = "labelXAxis";
            this.labelXAxis.Size = new System.Drawing.Size(33, 13);
            this.labelXAxis.TabIndex = 24;
            this.labelXAxis.Text = "x axis";
            // 
            // textBoxRAxis
            // 
            this.textBoxRAxis.BackColor = System.Drawing.Color.White;
            this.textBoxRAxis.Location = new System.Drawing.Point(64, 71);
            this.textBoxRAxis.Name = "textBoxRAxis";
            this.textBoxRAxis.ReadOnly = true;
            this.textBoxRAxis.Size = new System.Drawing.Size(100, 20);
            this.textBoxRAxis.TabIndex = 23;
            // 
            // textBoxZAxis
            // 
            this.textBoxZAxis.BackColor = System.Drawing.Color.White;
            this.textBoxZAxis.Location = new System.Drawing.Point(64, 97);
            this.textBoxZAxis.Name = "textBoxZAxis";
            this.textBoxZAxis.ReadOnly = true;
            this.textBoxZAxis.Size = new System.Drawing.Size(100, 20);
            this.textBoxZAxis.TabIndex = 22;
            // 
            // textBoxYAxis
            // 
            this.textBoxYAxis.BackColor = System.Drawing.Color.White;
            this.textBoxYAxis.Location = new System.Drawing.Point(64, 45);
            this.textBoxYAxis.Name = "textBoxYAxis";
            this.textBoxYAxis.ReadOnly = true;
            this.textBoxYAxis.Size = new System.Drawing.Size(100, 20);
            this.textBoxYAxis.TabIndex = 21;
            // 
            // textBoxXAxis
            // 
            this.textBoxXAxis.BackColor = System.Drawing.Color.White;
            this.textBoxXAxis.Location = new System.Drawing.Point(64, 19);
            this.textBoxXAxis.Name = "textBoxXAxis";
            this.textBoxXAxis.ReadOnly = true;
            this.textBoxXAxis.Size = new System.Drawing.Size(100, 20);
            this.textBoxXAxis.TabIndex = 20;
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.checkBoxCameraSwap);
            this.groupBoxInput.Controls.Add(this.checkBoxHover);
            this.groupBoxInput.Controls.Add(this.textBoxXAxis);
            this.groupBoxInput.Controls.Add(this.checkBoxFlatTrim);
            this.groupBoxInput.Controls.Add(this.textBoxYAxis);
            this.groupBoxInput.Controls.Add(this.checkBoxEmergency);
            this.groupBoxInput.Controls.Add(this.textBoxZAxis);
            this.groupBoxInput.Controls.Add(this.checkBoxTakeOff);
            this.groupBoxInput.Controls.Add(this.textBoxRAxis);
            this.groupBoxInput.Controls.Add(this.checkBoxLand);
            this.groupBoxInput.Controls.Add(this.labelXAxis);
            this.groupBoxInput.Controls.Add(this.labelZAxis);
            this.groupBoxInput.Controls.Add(this.labelYAxis);
            this.groupBoxInput.Controls.Add(this.labelRAxis);
            this.groupBoxInput.Location = new System.Drawing.Point(376, 35);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(184, 206);
            this.groupBoxInput.TabIndex = 32;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Input";
            // 
            // checkBoxCameraSwap
            // 
            this.checkBoxCameraSwap.AutoSize = true;
            this.checkBoxCameraSwap.Enabled = false;
            this.checkBoxCameraSwap.Location = new System.Drawing.Point(104, 177);
            this.checkBoxCameraSwap.Name = "checkBoxCameraSwap";
            this.checkBoxCameraSwap.Size = new System.Drawing.Size(62, 17);
            this.checkBoxCameraSwap.TabIndex = 33;
            this.checkBoxCameraSwap.Text = "Camera";
            this.checkBoxCameraSwap.UseVisualStyleBackColor = true;
            // 
            // checkBoxHover
            // 
            this.checkBoxHover.AutoSize = true;
            this.checkBoxHover.Enabled = false;
            this.checkBoxHover.Location = new System.Drawing.Point(13, 177);
            this.checkBoxHover.Name = "checkBoxHover";
            this.checkBoxHover.Size = new System.Drawing.Size(55, 17);
            this.checkBoxHover.TabIndex = 32;
            this.checkBoxHover.Text = "Hover";
            this.checkBoxHover.UseVisualStyleBackColor = true;
            // 
            // groupBoxStatus
            // 
            this.groupBoxStatus.Controls.Add(this.labelEmergencyStatus);
            this.groupBoxStatus.Controls.Add(this.labelEmergencyStatusInfo);
            this.groupBoxStatus.Controls.Add(this.labelConnectedStatus);
            this.groupBoxStatus.Controls.Add(this.labelConnectedStatusInfo);
            this.groupBoxStatus.Controls.Add(this.labelHoveringStatus);
            this.groupBoxStatus.Controls.Add(this.labelFlyingStatus);
            this.groupBoxStatus.Controls.Add(this.labelAltitudeStatus);
            this.groupBoxStatus.Controls.Add(this.labelCameraStatus);
            this.groupBoxStatus.Controls.Add(this.labelBatteryStatus);
            this.groupBoxStatus.Controls.Add(this.labelHoveringStatusInfo);
            this.groupBoxStatus.Controls.Add(this.labelFlyingStatusInfo);
            this.groupBoxStatus.Controls.Add(this.labelAltitudeStatusInfo);
            this.groupBoxStatus.Controls.Add(this.labelCameraStatusInfo);
            this.groupBoxStatus.Controls.Add(this.labelBatteryStatusInfo);
            this.groupBoxStatus.Location = new System.Drawing.Point(376, 247);
            this.groupBoxStatus.Name = "groupBoxStatus";
            this.groupBoxStatus.Size = new System.Drawing.Size(184, 180);
            this.groupBoxStatus.TabIndex = 33;
            this.groupBoxStatus.TabStop = false;
            this.groupBoxStatus.Text = "Status";
            // 
            // labelEmergencyStatus
            // 
            this.labelEmergencyStatus.AutoSize = true;
            this.labelEmergencyStatus.Location = new System.Drawing.Point(133, 155);
            this.labelEmergencyStatus.Name = "labelEmergencyStatus";
            this.labelEmergencyStatus.Size = new System.Drawing.Size(29, 13);
            this.labelEmergencyStatus.TabIndex = 42;
            this.labelEmergencyStatus.Text = "false";
            // 
            // labelEmergencyStatusInfo
            // 
            this.labelEmergencyStatusInfo.AutoSize = true;
            this.labelEmergencyStatusInfo.Location = new System.Drawing.Point(14, 155);
            this.labelEmergencyStatusInfo.Name = "labelEmergencyStatusInfo";
            this.labelEmergencyStatusInfo.Size = new System.Drawing.Size(60, 13);
            this.labelEmergencyStatusInfo.TabIndex = 41;
            this.labelEmergencyStatusInfo.Text = "Emergency";
            // 
            // labelConnectedStatus
            // 
            this.labelConnectedStatus.AutoSize = true;
            this.labelConnectedStatus.Location = new System.Drawing.Point(133, 99);
            this.labelConnectedStatus.Name = "labelConnectedStatus";
            this.labelConnectedStatus.Size = new System.Drawing.Size(29, 13);
            this.labelConnectedStatus.TabIndex = 40;
            this.labelConnectedStatus.Text = "false";
            // 
            // labelConnectedStatusInfo
            // 
            this.labelConnectedStatusInfo.AutoSize = true;
            this.labelConnectedStatusInfo.Location = new System.Drawing.Point(14, 99);
            this.labelConnectedStatusInfo.Name = "labelConnectedStatusInfo";
            this.labelConnectedStatusInfo.Size = new System.Drawing.Size(59, 13);
            this.labelConnectedStatusInfo.TabIndex = 39;
            this.labelConnectedStatusInfo.Text = "Connected";
            // 
            // labelHoveringStatus
            // 
            this.labelHoveringStatus.AutoSize = true;
            this.labelHoveringStatus.Location = new System.Drawing.Point(133, 137);
            this.labelHoveringStatus.Name = "labelHoveringStatus";
            this.labelHoveringStatus.Size = new System.Drawing.Size(29, 13);
            this.labelHoveringStatus.TabIndex = 38;
            this.labelHoveringStatus.Text = "false";
            // 
            // labelFlyingStatus
            // 
            this.labelFlyingStatus.AutoSize = true;
            this.labelFlyingStatus.Location = new System.Drawing.Point(133, 118);
            this.labelFlyingStatus.Name = "labelFlyingStatus";
            this.labelFlyingStatus.Size = new System.Drawing.Size(29, 13);
            this.labelFlyingStatus.TabIndex = 37;
            this.labelFlyingStatus.Text = "false";
            // 
            // labelAltitudeStatus
            // 
            this.labelAltitudeStatus.AutoSize = true;
            this.labelAltitudeStatus.Location = new System.Drawing.Point(133, 66);
            this.labelAltitudeStatus.Name = "labelAltitudeStatus";
            this.labelAltitudeStatus.Size = new System.Drawing.Size(13, 13);
            this.labelAltitudeStatus.TabIndex = 36;
            this.labelAltitudeStatus.Text = "0";
            // 
            // labelCameraStatus
            // 
            this.labelCameraStatus.AutoSize = true;
            this.labelCameraStatus.Location = new System.Drawing.Point(133, 45);
            this.labelCameraStatus.Name = "labelCameraStatus";
            this.labelCameraStatus.Size = new System.Drawing.Size(33, 13);
            this.labelCameraStatus.TabIndex = 35;
            this.labelCameraStatus.Text = "None";
            // 
            // labelBatteryStatus
            // 
            this.labelBatteryStatus.AutoSize = true;
            this.labelBatteryStatus.Location = new System.Drawing.Point(133, 23);
            this.labelBatteryStatus.Name = "labelBatteryStatus";
            this.labelBatteryStatus.Size = new System.Drawing.Size(27, 13);
            this.labelBatteryStatus.TabIndex = 34;
            this.labelBatteryStatus.Text = "N/A";
            // 
            // labelHoveringStatusInfo
            // 
            this.labelHoveringStatusInfo.AutoSize = true;
            this.labelHoveringStatusInfo.Location = new System.Drawing.Point(14, 137);
            this.labelHoveringStatusInfo.Name = "labelHoveringStatusInfo";
            this.labelHoveringStatusInfo.Size = new System.Drawing.Size(50, 13);
            this.labelHoveringStatusInfo.TabIndex = 4;
            this.labelHoveringStatusInfo.Text = "Hovering";
            // 
            // labelFlyingStatusInfo
            // 
            this.labelFlyingStatusInfo.AutoSize = true;
            this.labelFlyingStatusInfo.Location = new System.Drawing.Point(14, 118);
            this.labelFlyingStatusInfo.Name = "labelFlyingStatusInfo";
            this.labelFlyingStatusInfo.Size = new System.Drawing.Size(34, 13);
            this.labelFlyingStatusInfo.TabIndex = 3;
            this.labelFlyingStatusInfo.Text = "Flying";
            // 
            // labelAltitudeStatusInfo
            // 
            this.labelAltitudeStatusInfo.AutoSize = true;
            this.labelAltitudeStatusInfo.Location = new System.Drawing.Point(14, 66);
            this.labelAltitudeStatusInfo.Name = "labelAltitudeStatusInfo";
            this.labelAltitudeStatusInfo.Size = new System.Drawing.Size(42, 13);
            this.labelAltitudeStatusInfo.TabIndex = 2;
            this.labelAltitudeStatusInfo.Text = "Altitude";
            // 
            // labelCameraStatusInfo
            // 
            this.labelCameraStatusInfo.AutoSize = true;
            this.labelCameraStatusInfo.Location = new System.Drawing.Point(14, 45);
            this.labelCameraStatusInfo.Name = "labelCameraStatusInfo";
            this.labelCameraStatusInfo.Size = new System.Drawing.Size(77, 13);
            this.labelCameraStatusInfo.TabIndex = 1;
            this.labelCameraStatusInfo.Text = "Camera shown";
            // 
            // labelBatteryStatusInfo
            // 
            this.labelBatteryStatusInfo.AutoSize = true;
            this.labelBatteryStatusInfo.Location = new System.Drawing.Point(14, 23);
            this.labelBatteryStatusInfo.Name = "labelBatteryStatusInfo";
            this.labelBatteryStatusInfo.Size = new System.Drawing.Size(71, 13);
            this.labelBatteryStatusInfo.TabIndex = 0;
            this.labelBatteryStatusInfo.Text = "Battery status";
            // 
            // buttonHover
            // 
            this.buttonHover.Location = new System.Drawing.Point(270, 340);
            this.buttonHover.Name = "buttonHover";
            this.buttonHover.Size = new System.Drawing.Size(100, 23);
            this.buttonHover.TabIndex = 34;
            this.buttonHover.Text = "Hover";
            this.buttonHover.UseVisualStyleBackColor = true;
            this.buttonHover.Click += new System.EventHandler(this.buttonHover_Click);
            // 
            // timerStartDelay
            // 
            this.timerStartDelay.Interval = 250;
            this.timerStartDelay.Tick += new System.EventHandler(this.timerStartDelay_Tick);
            // 
            // arDroneControl
            // 
            this.arDroneControl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.arDroneControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.arDroneControl.Location = new System.Drawing.Point(12, 35);
            this.arDroneControl.Name = "arDroneControl";
            this.arDroneControl.Size = new System.Drawing.Size(358, 270);
            this.arDroneControl.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(566, 540);
            this.Controls.Add(this.buttonHover);
            this.Controls.Add(this.groupBoxStatus);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.labelCamera);
            this.Controls.Add(this.buttonFlattrim);
            this.Controls.Add(this.buttonStartStop);
            this.Controls.Add(this.buttonChangeCamera);
            this.Controls.Add(this.buttonEmergency);
            this.Controls.Add(this.arDroneControl);
            this.Controls.Add(this.buttonShutdown);
            this.Controls.Add(this.textboxOutput);
            this.Controls.Add(this.buttonConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Drone Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            this.groupBoxStatus.ResumeLayout(false);
            this.groupBoxStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textboxOutput;
        private System.Windows.Forms.Button buttonShutdown;
        private ARDroneFormsControl.ARDroneControl arDroneControl;
        private System.Windows.Forms.Button buttonEmergency;
        private System.Windows.Forms.Button buttonChangeCamera;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.Button buttonFlattrim;
        private System.Windows.Forms.Timer timerARDroneUpdate;
        private System.Windows.Forms.Label labelCamera;
        private System.Windows.Forms.Timer timerInputUpdate;
        private System.Windows.Forms.CheckBox checkBoxFlatTrim;
        private System.Windows.Forms.CheckBox checkBoxEmergency;
        private System.Windows.Forms.CheckBox checkBoxTakeOff;
        private System.Windows.Forms.CheckBox checkBoxLand;
        private System.Windows.Forms.Label labelZAxis;
        private System.Windows.Forms.Label labelRAxis;
        private System.Windows.Forms.Label labelYAxis;
        private System.Windows.Forms.Label labelXAxis;
        private System.Windows.Forms.TextBox textBoxRAxis;
        private System.Windows.Forms.TextBox textBoxZAxis;
        private System.Windows.Forms.TextBox textBoxYAxis;
        private System.Windows.Forms.TextBox textBoxXAxis;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.GroupBox groupBoxStatus;
        private System.Windows.Forms.Label labelHoveringStatus;
        private System.Windows.Forms.Label labelFlyingStatus;
        private System.Windows.Forms.Label labelAltitudeStatus;
        private System.Windows.Forms.Label labelCameraStatus;
        private System.Windows.Forms.Label labelBatteryStatus;
        private System.Windows.Forms.Label labelHoveringStatusInfo;
        private System.Windows.Forms.Label labelFlyingStatusInfo;
        private System.Windows.Forms.Label labelAltitudeStatusInfo;
        private System.Windows.Forms.Label labelCameraStatusInfo;
        private System.Windows.Forms.Label labelBatteryStatusInfo;
        private System.Windows.Forms.Label labelConnectedStatus;
        private System.Windows.Forms.Label labelConnectedStatusInfo;
        private System.Windows.Forms.Button buttonHover;
        private System.Windows.Forms.Timer timerStartDelay;
        private System.Windows.Forms.Label labelEmergencyStatus;
        private System.Windows.Forms.Label labelEmergencyStatusInfo;
        private System.Windows.Forms.CheckBox checkBoxHover;
        private System.Windows.Forms.CheckBox checkBoxCameraSwap;
    }
}

