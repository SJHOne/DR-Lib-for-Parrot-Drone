namespace TestDLL
{
    partial class Form1
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
            this.bConnect = new System.Windows.Forms.Button();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.arDroneCtl1 = new ARDroneFormsCtl.ARDroneCtl();
            this.joystickControl1 = new maxCustomControl.joystickControl();
            this.joystickControl2 = new maxCustomControl.joystickControl();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.airSpeedIndicatorInstrumentControl1 = new AviationInstruments.AirSpeedIndicatorInstrumentControl();
            this.altimeterInstrumentControl1 = new AviationInstruments.AltimeterInstrumentControl();
            this.attitudeIndicatorInstrumentControl1 = new AviationInstruments.AttitudeIndicatorInstrumentControl();
            this.headingIndicatorInstrumentControl1 = new AviationInstruments.HeadingIndicatorInstrumentControl();
            this.turnCoordinatorInstrumentControl1 = new AviationInstruments.TurnCoordinatorInstrumentControl();
            this.verticalSpeedIndicatorInstrumentControl1 = new AviationInstruments.VerticalSpeedIndicatorInstrumentControl();
            this.lblDroneState = new System.Windows.Forms.Label();
            this.lblBatt = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bConnect
            // 
            this.bConnect.Location = new System.Drawing.Point(235, 12);
            this.bConnect.Name = "bConnect";
            this.bConnect.Size = new System.Drawing.Size(75, 23);
            this.bConnect.TabIndex = 0;
            this.bConnect.Text = "Startup";
            this.bConnect.UseVisualStyleBackColor = true;
            this.bConnect.Click += new System.EventHandler(this.bConnect_Click);
            // 
            // tbOutput
            // 
            this.tbOutput.BackColor = System.Drawing.SystemColors.MenuText;
            this.tbOutput.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbOutput.ForeColor = System.Drawing.Color.Yellow;
            this.tbOutput.Location = new System.Drawing.Point(185, 466);
            this.tbOutput.Multiline = true;
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(458, 160);
            this.tbOutput.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(517, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Shutdown";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // arDroneCtl1
            // 
            this.arDroneCtl1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.arDroneCtl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.arDroneCtl1.Location = new System.Drawing.Point(234, 41);
            this.arDroneCtl1.Name = "arDroneCtl1";
            this.arDroneCtl1.Size = new System.Drawing.Size(358, 270);
            this.arDroneCtl1.TabIndex = 4;
            // 
            // joystickControl1
            // 
            this.joystickControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.joystickControl1.Location = new System.Drawing.Point(235, 318);
            this.joystickControl1.Name = "joystickControl1";
            this.joystickControl1.Size = new System.Drawing.Size(125, 138);
            this.joystickControl1.TabIndex = 5;
            // 
            // joystickControl2
            // 
            this.joystickControl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.joystickControl2.Location = new System.Drawing.Point(467, 318);
            this.joystickControl2.Name = "joystickControl2";
            this.joystickControl2.Size = new System.Drawing.Size(125, 138);
            this.joystickControl2.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(365, 318);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(96, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "EMERGENCY";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(376, 376);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "View";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(376, 433);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 9;
            this.button4.Text = "Up/Down";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // airSpeedIndicatorInstrumentControl1
            // 
            this.airSpeedIndicatorInstrumentControl1.Location = new System.Drawing.Point(649, 461);
            this.airSpeedIndicatorInstrumentControl1.Name = "airSpeedIndicatorInstrumentControl1";
            this.airSpeedIndicatorInstrumentControl1.Size = new System.Drawing.Size(170, 170);
            this.airSpeedIndicatorInstrumentControl1.TabIndex = 10;
            this.airSpeedIndicatorInstrumentControl1.Text = "airSpeedIndicatorInstrumentControl1";
            // 
            // altimeterInstrumentControl1
            // 
            this.altimeterInstrumentControl1.Location = new System.Drawing.Point(599, 12);
            this.altimeterInstrumentControl1.Name = "altimeterInstrumentControl1";
            this.altimeterInstrumentControl1.Size = new System.Drawing.Size(220, 220);
            this.altimeterInstrumentControl1.TabIndex = 11;
            this.altimeterInstrumentControl1.Text = "altimeterInstrumentControl1";
            // 
            // attitudeIndicatorInstrumentControl1
            // 
            this.attitudeIndicatorInstrumentControl1.Location = new System.Drawing.Point(8, 12);
            this.attitudeIndicatorInstrumentControl1.Name = "attitudeIndicatorInstrumentControl1";
            this.attitudeIndicatorInstrumentControl1.Size = new System.Drawing.Size(220, 220);
            this.attitudeIndicatorInstrumentControl1.TabIndex = 12;
            this.attitudeIndicatorInstrumentControl1.Text = "attitudeIndicatorInstrumentControl1";
            // 
            // headingIndicatorInstrumentControl1
            // 
            this.headingIndicatorInstrumentControl1.Location = new System.Drawing.Point(8, 238);
            this.headingIndicatorInstrumentControl1.Name = "headingIndicatorInstrumentControl1";
            this.headingIndicatorInstrumentControl1.Size = new System.Drawing.Size(220, 220);
            this.headingIndicatorInstrumentControl1.TabIndex = 13;
            this.headingIndicatorInstrumentControl1.Text = "headingIndicatorInstrumentControl1";
            // 
            // turnCoordinatorInstrumentControl1
            // 
            this.turnCoordinatorInstrumentControl1.Location = new System.Drawing.Point(599, 238);
            this.turnCoordinatorInstrumentControl1.Name = "turnCoordinatorInstrumentControl1";
            this.turnCoordinatorInstrumentControl1.Size = new System.Drawing.Size(220, 220);
            this.turnCoordinatorInstrumentControl1.TabIndex = 14;
            this.turnCoordinatorInstrumentControl1.Text = "turnCoordinatorInstrumentControl1";
            // 
            // verticalSpeedIndicatorInstrumentControl1
            // 
            this.verticalSpeedIndicatorInstrumentControl1.Location = new System.Drawing.Point(8, 461);
            this.verticalSpeedIndicatorInstrumentControl1.Name = "verticalSpeedIndicatorInstrumentControl1";
            this.verticalSpeedIndicatorInstrumentControl1.Size = new System.Drawing.Size(170, 170);
            this.verticalSpeedIndicatorInstrumentControl1.TabIndex = 15;
            this.verticalSpeedIndicatorInstrumentControl1.Text = "verticalSpeedIndicatorInstrumentControl1";
            // 
            // lblDroneState
            // 
            this.lblDroneState.AutoSize = true;
            this.lblDroneState.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDroneState.ForeColor = System.Drawing.Color.Yellow;
            this.lblDroneState.Location = new System.Drawing.Point(362, 14);
            this.lblDroneState.Name = "lblDroneState";
            this.lblDroneState.Size = new System.Drawing.Size(107, 17);
            this.lblDroneState.TabIndex = 16;
            this.lblDroneState.Text = "DRONE_STATE";
            // 
            // lblBatt
            // 
            this.lblBatt.AutoSize = true;
            this.lblBatt.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatt.ForeColor = System.Drawing.Color.Lime;
            this.lblBatt.Location = new System.Drawing.Point(380, 408);
            this.lblBatt.Name = "lblBatt";
            this.lblBatt.Size = new System.Drawing.Size(71, 17);
            this.lblBatt.TabIndex = 17;
            this.lblBatt.Text = "100/100";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(830, 636);
            this.Controls.Add(this.lblBatt);
            this.Controls.Add(this.lblDroneState);
            this.Controls.Add(this.verticalSpeedIndicatorInstrumentControl1);
            this.Controls.Add(this.turnCoordinatorInstrumentControl1);
            this.Controls.Add(this.headingIndicatorInstrumentControl1);
            this.Controls.Add(this.attitudeIndicatorInstrumentControl1);
            this.Controls.Add(this.altimeterInstrumentControl1);
            this.Controls.Add(this.airSpeedIndicatorInstrumentControl1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.joystickControl2);
            this.Controls.Add(this.joystickControl1);
            this.Controls.Add(this.arDroneCtl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.bConnect);
            this.Name = "Form1";
            this.Text = "AR Drone .Net Control Test";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bConnect;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button button1;
        private ARDroneFormsCtl.ARDroneCtl arDroneCtl1;
        private maxCustomControl.joystickControl joystickControl1;
        private maxCustomControl.joystickControl joystickControl2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private AviationInstruments.AirSpeedIndicatorInstrumentControl airSpeedIndicatorInstrumentControl1;
        private AviationInstruments.AltimeterInstrumentControl altimeterInstrumentControl1;
        private AviationInstruments.AttitudeIndicatorInstrumentControl attitudeIndicatorInstrumentControl1;
        private AviationInstruments.HeadingIndicatorInstrumentControl headingIndicatorInstrumentControl1;
        private AviationInstruments.TurnCoordinatorInstrumentControl turnCoordinatorInstrumentControl1;
        private AviationInstruments.VerticalSpeedIndicatorInstrumentControl verticalSpeedIndicatorInstrumentControl1;
        private System.Windows.Forms.Label lblDroneState;
        private System.Windows.Forms.Label lblBatt;
    }
}

