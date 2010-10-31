namespace ARDrone
{
    partial class InputTestForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerJoystickUpdate = new System.Windows.Forms.Timer(this.components);
            this.textBoxXAxis = new System.Windows.Forms.TextBox();
            this.textBoxYAxis = new System.Windows.Forms.TextBox();
            this.textBoxZAxis = new System.Windows.Forms.TextBox();
            this.textBoxRAxis = new System.Windows.Forms.TextBox();
            this.labelXAxis = new System.Windows.Forms.Label();
            this.labelYAxis = new System.Windows.Forms.Label();
            this.labelRAxis = new System.Windows.Forms.Label();
            this.labelZAxis = new System.Windows.Forms.Label();
            this.checkBoxLand = new System.Windows.Forms.CheckBox();
            this.checkBoxTakeOff = new System.Windows.Forms.CheckBox();
            this.checkBoxEmergency = new System.Windows.Forms.CheckBox();
            this.checkBoxFlatTrim = new System.Windows.Forms.CheckBox();
            this.checkBoxCameraSwap = new System.Windows.Forms.CheckBox();
            this.checkBoxHover = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // timerJoystickUpdate
            // 
            this.timerJoystickUpdate.Interval = 50;
            this.timerJoystickUpdate.Tick += new System.EventHandler(this.timerJoystickUpdate_Tick);
            // 
            // textBoxXAxis
            // 
            this.textBoxXAxis.BackColor = System.Drawing.Color.White;
            this.textBoxXAxis.Location = new System.Drawing.Point(63, 12);
            this.textBoxXAxis.Name = "textBoxXAxis";
            this.textBoxXAxis.ReadOnly = true;
            this.textBoxXAxis.Size = new System.Drawing.Size(100, 20);
            this.textBoxXAxis.TabIndex = 0;
            // 
            // textBoxYAxis
            // 
            this.textBoxYAxis.BackColor = System.Drawing.Color.White;
            this.textBoxYAxis.Location = new System.Drawing.Point(63, 38);
            this.textBoxYAxis.Name = "textBoxYAxis";
            this.textBoxYAxis.ReadOnly = true;
            this.textBoxYAxis.Size = new System.Drawing.Size(100, 20);
            this.textBoxYAxis.TabIndex = 1;
            // 
            // textBoxZAxis
            // 
            this.textBoxZAxis.BackColor = System.Drawing.Color.White;
            this.textBoxZAxis.Location = new System.Drawing.Point(63, 90);
            this.textBoxZAxis.Name = "textBoxZAxis";
            this.textBoxZAxis.ReadOnly = true;
            this.textBoxZAxis.Size = new System.Drawing.Size(100, 20);
            this.textBoxZAxis.TabIndex = 2;
            // 
            // textBoxRAxis
            // 
            this.textBoxRAxis.BackColor = System.Drawing.Color.White;
            this.textBoxRAxis.Location = new System.Drawing.Point(63, 64);
            this.textBoxRAxis.Name = "textBoxRAxis";
            this.textBoxRAxis.ReadOnly = true;
            this.textBoxRAxis.Size = new System.Drawing.Size(100, 20);
            this.textBoxRAxis.TabIndex = 3;
            // 
            // labelXAxis
            // 
            this.labelXAxis.AutoSize = true;
            this.labelXAxis.Location = new System.Drawing.Point(12, 15);
            this.labelXAxis.Name = "labelXAxis";
            this.labelXAxis.Size = new System.Drawing.Size(33, 13);
            this.labelXAxis.TabIndex = 5;
            this.labelXAxis.Text = "x axis";
            // 
            // labelYAxis
            // 
            this.labelYAxis.AutoSize = true;
            this.labelYAxis.Location = new System.Drawing.Point(12, 41);
            this.labelYAxis.Name = "labelYAxis";
            this.labelYAxis.Size = new System.Drawing.Size(33, 13);
            this.labelYAxis.TabIndex = 6;
            this.labelYAxis.Text = "y axis";
            // 
            // labelRAxis
            // 
            this.labelRAxis.AutoSize = true;
            this.labelRAxis.Location = new System.Drawing.Point(12, 67);
            this.labelRAxis.Name = "labelRAxis";
            this.labelRAxis.Size = new System.Drawing.Size(31, 13);
            this.labelRAxis.TabIndex = 7;
            this.labelRAxis.Text = "r axis";
            // 
            // labelZAxis
            // 
            this.labelZAxis.AutoSize = true;
            this.labelZAxis.Location = new System.Drawing.Point(12, 93);
            this.labelZAxis.Name = "labelZAxis";
            this.labelZAxis.Size = new System.Drawing.Size(33, 13);
            this.labelZAxis.TabIndex = 8;
            this.labelZAxis.Text = "z axis";
            // 
            // checkBoxLand
            // 
            this.checkBoxLand.AutoSize = true;
            this.checkBoxLand.Enabled = false;
            this.checkBoxLand.Location = new System.Drawing.Point(103, 124);
            this.checkBoxLand.Name = "checkBoxLand";
            this.checkBoxLand.Size = new System.Drawing.Size(50, 17);
            this.checkBoxLand.TabIndex = 9;
            this.checkBoxLand.Text = "Land";
            this.checkBoxLand.UseVisualStyleBackColor = true;
            // 
            // checkBoxTakeOff
            // 
            this.checkBoxTakeOff.AutoSize = true;
            this.checkBoxTakeOff.Enabled = false;
            this.checkBoxTakeOff.Location = new System.Drawing.Point(12, 124);
            this.checkBoxTakeOff.Name = "checkBoxTakeOff";
            this.checkBoxTakeOff.Size = new System.Drawing.Size(66, 17);
            this.checkBoxTakeOff.TabIndex = 9;
            this.checkBoxTakeOff.Text = "Take off";
            this.checkBoxTakeOff.UseVisualStyleBackColor = true;
            // 
            // checkBoxEmergency
            // 
            this.checkBoxEmergency.AutoSize = true;
            this.checkBoxEmergency.Enabled = false;
            this.checkBoxEmergency.Location = new System.Drawing.Point(12, 147);
            this.checkBoxEmergency.Name = "checkBoxEmergency";
            this.checkBoxEmergency.Size = new System.Drawing.Size(79, 17);
            this.checkBoxEmergency.TabIndex = 10;
            this.checkBoxEmergency.Text = "Emergency";
            this.checkBoxEmergency.UseVisualStyleBackColor = true;
            // 
            // checkBoxFlatTrim
            // 
            this.checkBoxFlatTrim.AutoSize = true;
            this.checkBoxFlatTrim.Enabled = false;
            this.checkBoxFlatTrim.Location = new System.Drawing.Point(103, 147);
            this.checkBoxFlatTrim.Name = "checkBoxFlatTrim";
            this.checkBoxFlatTrim.Size = new System.Drawing.Size(62, 17);
            this.checkBoxFlatTrim.TabIndex = 11;
            this.checkBoxFlatTrim.Text = "Flat trim";
            this.checkBoxFlatTrim.UseVisualStyleBackColor = true;
            // 
            // checkBoxCameraSwap
            // 
            this.checkBoxCameraSwap.AutoSize = true;
            this.checkBoxCameraSwap.Enabled = false;
            this.checkBoxCameraSwap.Location = new System.Drawing.Point(103, 170);
            this.checkBoxCameraSwap.Name = "checkBoxCameraSwap";
            this.checkBoxCameraSwap.Size = new System.Drawing.Size(62, 17);
            this.checkBoxCameraSwap.TabIndex = 35;
            this.checkBoxCameraSwap.Text = "Camera";
            this.checkBoxCameraSwap.UseVisualStyleBackColor = true;
            // 
            // checkBoxHover
            // 
            this.checkBoxHover.AutoSize = true;
            this.checkBoxHover.Enabled = false;
            this.checkBoxHover.Location = new System.Drawing.Point(12, 170);
            this.checkBoxHover.Name = "checkBoxHover";
            this.checkBoxHover.Size = new System.Drawing.Size(55, 17);
            this.checkBoxHover.TabIndex = 34;
            this.checkBoxHover.Text = "Hover";
            this.checkBoxHover.UseVisualStyleBackColor = true;
            // 
            // InputTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 195);
            this.Controls.Add(this.checkBoxCameraSwap);
            this.Controls.Add(this.checkBoxHover);
            this.Controls.Add(this.checkBoxFlatTrim);
            this.Controls.Add(this.checkBoxEmergency);
            this.Controls.Add(this.checkBoxTakeOff);
            this.Controls.Add(this.checkBoxLand);
            this.Controls.Add(this.labelZAxis);
            this.Controls.Add(this.labelRAxis);
            this.Controls.Add(this.labelYAxis);
            this.Controls.Add(this.labelXAxis);
            this.Controls.Add(this.textBoxRAxis);
            this.Controls.Add(this.textBoxZAxis);
            this.Controls.Add(this.textBoxYAxis);
            this.Controls.Add(this.textBoxXAxis);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputTestForm";
            this.Text = "Input test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerJoystickUpdate;
        private System.Windows.Forms.TextBox textBoxXAxis;
        private System.Windows.Forms.TextBox textBoxYAxis;
        private System.Windows.Forms.TextBox textBoxZAxis;
        private System.Windows.Forms.TextBox textBoxRAxis;
        private System.Windows.Forms.Label labelXAxis;
        private System.Windows.Forms.Label labelYAxis;
        private System.Windows.Forms.Label labelRAxis;
        private System.Windows.Forms.Label labelZAxis;
        private System.Windows.Forms.CheckBox checkBoxLand;
        private System.Windows.Forms.CheckBox checkBoxTakeOff;
        private System.Windows.Forms.CheckBox checkBoxEmergency;
        private System.Windows.Forms.CheckBox checkBoxFlatTrim;
        private System.Windows.Forms.CheckBox checkBoxCameraSwap;
        private System.Windows.Forms.CheckBox checkBoxHover;
    }
}

