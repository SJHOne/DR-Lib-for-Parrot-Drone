using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ARDrone.Input
{
    public partial class ConfigInput : Form
    {

        InputManager _owner = null;

        public ConfigInput(InputManager owner)
        {
            InitializeComponent();
            _owner = owner;  // TODO : Ideally this should be exposed through an event, to facilitate unit testing

            UpdateUI();
            cbDevice.Items.Clear();

            foreach (GenericInput gM in _owner.InputDevices)
            {
                cbDevice.Items.Add(gM.DeviceName);
            }

            cbDevice.SelectedIndex = 0;
        }

        private void UpdateUI()
        {

            if (cbDevice.SelectedIndex > 0)
            {

                tbCamSwap.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.CameraSwapButton; ;
                tbEmergency.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.EmergencyButton; ;
                tbFlatTrim.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.FlatTrimButton;
                tbGaz.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.GazAxisMapping;
                tbLand.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.LandButton;
                tbPitch.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.PitchAxisMapping;
                tbRoll.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.RollAxisMapping;
                tbTakeOff.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.TakeOffButton;
                tbYaw.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.YawAxisMapping;
                tbHover.Text = _owner.InputDevices[cbDevice.SelectedIndex].Mapping.HoverButton;
            }
        }

        private void ConfigInput_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display permissable entries
            txtOutput.Text = "Possible Axis Mappings\r\n";
            foreach (string s in _owner.InputDevices[cbDevice.SelectedIndex].Mapping.ValidAxes)
            {
                txtOutput.AppendText(s + ", ");
            }
            txtOutput.AppendText("Possible Button Mappings\r\n");
            foreach (string s in _owner.InputDevices[cbDevice.SelectedIndex].Mapping.ValidButtons)
            {
                txtOutput.AppendText(s + ", ");
            }
            this.UpdateUI();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _owner.SaveInputDevices();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _owner.LoadInputDevices();
            UpdateUI();
        }


        private void btnSet_Click(object sender, EventArgs e)
        {
            try
            {
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.CameraSwapButton = tbCamSwap.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.EmergencyButton = tbEmergency.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.FlatTrimButton = tbFlatTrim.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.GazAxisMapping = tbGaz.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.LandButton = tbLand.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.PitchAxisMapping = tbPitch.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.RollAxisMapping = tbRoll.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.TakeOffButton = tbTakeOff.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.YawAxisMapping = tbYaw.Text;
                _owner.InputDevices[cbDevice.SelectedIndex].Mapping.HoverButton = tbHover.Text;

            }
            catch (Exception ex)
            {
                txtOutput.Text = ex.Message;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnSet_Click(null, null);
        }
    }
}
