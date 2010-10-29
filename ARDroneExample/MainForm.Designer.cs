namespace TestDLL
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
            this.labelDroneState = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelBattery = new System.Windows.Forms.Label();
            this.timerBattery = new System.Windows.Forms.Timer(this.components);
            this.arDroneControl = new ARDroneFormsControl.ARDroneControl();
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
            this.buttonEmergency.Location = new System.Drawing.Point(12, 340);
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
            this.buttonStartStop.Text = "Up/Down";
            this.buttonStartStop.UseVisualStyleBackColor = true;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // labelDroneState
            // 
            this.labelDroneState.AutoSize = true;
            this.labelDroneState.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDroneState.ForeColor = System.Drawing.Color.Yellow;
            this.labelDroneState.Location = new System.Drawing.Point(140, 8);
            this.labelDroneState.Name = "labelDroneState";
            this.labelDroneState.Size = new System.Drawing.Size(107, 17);
            this.labelDroneState.TabIndex = 16;
            this.labelDroneState.Text = "DRONE_STATE";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(270, 340);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "Flat trim";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelBattery
            // 
            this.labelBattery.AutoSize = true;
            this.labelBattery.BackColor = System.Drawing.Color.Transparent;
            this.labelBattery.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBattery.ForeColor = System.Drawing.Color.GreenYellow;
            this.labelBattery.Location = new System.Drawing.Point(176, 318);
            this.labelBattery.Name = "labelBattery";
            this.labelBattery.Size = new System.Drawing.Size(41, 16);
            this.labelBattery.TabIndex = 18;
            this.labelBattery.Text = "100%";
            // 
            // timerBattery
            // 
            this.timerBattery.Interval = 1000;
            this.timerBattery.Tick += new System.EventHandler(this.timerBattery_Tick);
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
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(378, 540);
            this.Controls.Add(this.labelBattery);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelDroneState);
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
            this.Text = "AR Drone .Net Control Test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
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
        private System.Windows.Forms.Label labelDroneState;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelBattery;
        private System.Windows.Forms.Timer timerBattery;
    }
}

