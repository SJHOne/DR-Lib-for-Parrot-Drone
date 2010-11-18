using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ARDrone.Input;

namespace ARDrone.UI
{
    public partial class ConfigInput : Form
    {
        private enum Control { None, AxisRoll, AxisPitch, AxisYaw, AxisGaz, ButtonTakeoff, ButtonLand, ButtonHover, ButtonEmergency, ButtonFlatTrim, ButtonChangeCamera };
        private enum ControlType { None, Axis, Button };

        private Dictionary<String, Control> nameControlMap = null;
        private Dictionary<Control, ControlType> controlTypeMap = null;

        private ARDrone.Input.InputManager inputManager = null;

        private GenericInput selectedDevice = null;
        private Control selectedControl = Control.None;
        private ControlType selectedControlType = ControlType.None;

        private String tempAxisInput = "";

        Dictionary<String, GenericInput> devices = null;

        public ConfigInput()
        {
            InitializeComponent();

            inputManager = new ARDrone.Input.InputManager(this.Handle);
            Init(inputManager);
        }

        public ConfigInput(ARDrone.Input.InputManager inputManager)
        {
            InitializeComponent();
            Init(inputManager);
        }

        public void Init(ARDrone.Input.InputManager inputManager)
        {
            comboBoxDevices.SelectedIndex = 0;
            InitializeTimers();

            InitializeControlMap();

            this.inputManager = inputManager;
            InitializeDeviceList();
        }

        public void InitializeControlMap()
        {
            nameControlMap = new Dictionary<String, Control>();

            nameControlMap.Add(textBoxAxisRoll.Name, Control.AxisRoll); nameControlMap.Add(textBoxAxisPitch.Name, Control.AxisPitch);
            nameControlMap.Add(textBoxAxisYaw.Name, Control.AxisYaw); nameControlMap.Add(textBoxAxisGaz.Name, Control.AxisGaz);

            nameControlMap.Add(textBoxButtonTakeOff.Name, Control.ButtonTakeoff); nameControlMap.Add(textBoxButtonLand.Name, Control.ButtonLand);
            nameControlMap.Add(textBoxButtonHover.Name, Control.ButtonHover); nameControlMap.Add(textBoxButtonEmergency.Name, Control.ButtonEmergency);
            nameControlMap.Add(textBoxButtonFlatTrim.Name, Control.ButtonFlatTrim); nameControlMap.Add(textBoxButtonChangeCamera.Name, Control.ButtonChangeCamera);

            controlTypeMap = new Dictionary<Control, ControlType>();

            controlTypeMap.Add(Control.AxisRoll, ControlType.Axis); controlTypeMap.Add(Control.AxisPitch, ControlType.Axis);
            controlTypeMap.Add(Control.AxisYaw, ControlType.Axis); controlTypeMap.Add(Control.AxisGaz, ControlType.Axis);

            controlTypeMap.Add(Control.ButtonTakeoff, ControlType.Button); controlTypeMap.Add(Control.ButtonLand, ControlType.Button);
            controlTypeMap.Add(Control.ButtonHover, ControlType.Button); controlTypeMap.Add(Control.ButtonEmergency, ControlType.Button);
            controlTypeMap.Add(Control.ButtonFlatTrim, ControlType.Button); controlTypeMap.Add(Control.ButtonChangeCamera, ControlType.Button);
        }

        public void InitializeTimers()
        {
            timerInputUpdate.Start();
        }

        public void InitializeDeviceList()
        {
            devices = new Dictionary<String, GenericInput>();

            foreach (GenericInput inputDevice in inputManager.InputDevices)
            {
                devices.Add(inputDevice.DeviceName, inputDevice);
                comboBoxDevices.Items.Add(inputDevice.DeviceName);
            }
        }

        private void ChangeInputDevice()
        {
            String selectedDeviceName = (String)comboBoxDevices.Items[comboBoxDevices.SelectedIndex];
            
            if (selectedDeviceName == "-- No device selected --")
            {
                return;
            }
            if ((String)comboBoxDevices.Items[0] == "-- No device selected --")
            {
                comboBoxDevices.Items.RemoveAt(0);
            }

            if (selectedDeviceName != null)
            {
                SetMappingEnabledState(true);

                selectedDevice = devices[selectedDeviceName];
                selectedDevice.InitCurrentlyInvokedInput();

                TakeOverMapping(selectedDevice.Mapping);
            }
        }

        private void Focus(TextBox textBox)
        {
            if (textBox != null && nameControlMap.ContainsKey(textBox.Name))
            {
                selectedControl = nameControlMap[textBox.Name];
                selectedControlType = controlTypeMap[selectedControl];

                textBox.ForeColor = Color.LightGray;
                textBox.Text = "-- Assigning a value --";
            }
        }

        private void Unfocus(TextBox textBox)
        {
            if (textBox != null && nameControlMap.ContainsKey(textBox.Name))
            {
                selectedControl = Control.None;
                selectedControlType = ControlType.None;

                textBox.ForeColor = Color.Black;

                if (selectedDevice != null)
                {
                    TakeOverMapping(selectedDevice.Mapping);
                }
            }
        }

        private void SetMappingEnabledState(bool enabled)
        {
            textBoxAxisRoll.Enabled = enabled; textBoxAxisPitch.Enabled = enabled;
            textBoxAxisYaw.Enabled = enabled; textBoxAxisGaz.Enabled = enabled;

            textBoxButtonTakeOff.Enabled = enabled; textBoxButtonLand.Enabled = enabled;
            textBoxButtonHover.Enabled = enabled; textBoxButtonEmergency.Enabled = enabled;
            textBoxButtonFlatTrim.Enabled = enabled; textBoxButtonChangeCamera.Enabled = enabled;

            if (enabled)
            {
                textBoxAxisRoll.BackColor = Color.White; textBoxAxisPitch.BackColor = Color.White;
                textBoxAxisYaw.BackColor = Color.White; textBoxAxisGaz.BackColor = Color.White;

                textBoxButtonTakeOff.BackColor = Color.White; textBoxButtonLand.BackColor = Color.White;
                textBoxButtonHover.BackColor = Color.White; textBoxButtonEmergency.BackColor = Color.White;
                textBoxButtonFlatTrim.BackColor = Color.White; textBoxButtonChangeCamera.BackColor = Color.White;
            }
        }

        private void SaveMapping()
        {
            if (selectedDevice != null)
            {
                selectedDevice.SaveMapping();
            }
        }

        private void RevertMapping()
        {
            if (selectedDevice != null)
            {
                selectedDevice.RevertMapping();
            }
        }

        private void ResetMapping()
        {
            if (selectedDevice == null)
            {
                return;
            }

            DialogResult result = MessageBox.Show(this, "Do you really want to reset the setting to default values?", "Reset mapping", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                selectedDevice.SetDefaultMapping();
                TakeOverMapping(selectedDevice.Mapping);
            }
        }

        private void TakeOverMapping(InputMapping mapping)
        {
            textBoxAxisRoll.Text = mapping.RollAxisMapping;
            textBoxAxisPitch.Text = mapping.PitchAxisMapping;
            textBoxAxisYaw.Text = mapping.YawAxisMapping;
            textBoxAxisGaz.Text = mapping.GazAxisMapping;

            textBoxButtonTakeOff.Text = mapping.TakeOffButton;
            textBoxButtonLand.Text = mapping.LandButton;
            textBoxButtonHover.Text = mapping.HoverButton;
            textBoxButtonEmergency.Text = mapping.EmergencyButton;
            textBoxButtonFlatTrim.Text = mapping.FlatTrimButton;
            textBoxButtonChangeCamera.Text = mapping.CameraSwapButton;

            CheckForDoubleInput();
        }

        private void UpdateMapping(InputMapping mapping, Control control, String inputValue)
        {
            if (control == Control.AxisRoll) { mapping.RollAxisMapping = inputValue; }
            if (control == Control.AxisPitch) { mapping.PitchAxisMapping = inputValue; }
            if (control == Control.AxisYaw) { mapping.YawAxisMapping = inputValue; }
            if (control == Control.AxisGaz) { mapping.GazAxisMapping = inputValue; }

            if (control == Control.ButtonTakeoff) { mapping.TakeOffButton = inputValue; }
            if (control == Control.ButtonLand) { mapping.LandButton = inputValue; }
            if (control == Control.ButtonHover) { mapping.HoverButton = inputValue; }
            if (control == Control.ButtonEmergency) { mapping.EmergencyButton = inputValue; }
            if (control == Control.ButtonFlatTrim) { mapping.FlatTrimButton = inputValue; }
            if (control == Control.ButtonChangeCamera) { mapping.CameraSwapButton = inputValue; }
        }

        private void CheckForDoubleInput()
        {
            List<String> inputValues = new List<String>();

            inputValues.AddRange(textBoxAxisRoll.Text.Split('-')); inputValues.AddRange(textBoxAxisPitch.Text.Split('-'));
            inputValues.AddRange(textBoxAxisYaw.Text.Split('-')); inputValues.AddRange(textBoxAxisGaz.Text.Split('-'));
            inputValues.Add(textBoxButtonTakeOff.Text); inputValues.Add(textBoxButtonLand.Text); inputValues.Add(textBoxButtonHover.Text);
            inputValues.Add(textBoxButtonEmergency.Text); inputValues.Add(textBoxButtonFlatTrim.Text); inputValues.Add(textBoxButtonChangeCamera.Text);

            CheckDoubleInputEntry(textBoxAxisRoll, inputValues); CheckDoubleInputEntry(textBoxAxisPitch, inputValues);
            CheckDoubleInputEntry(textBoxAxisYaw, inputValues); CheckDoubleInputEntry(textBoxAxisGaz, inputValues);
            CheckDoubleInputEntry(textBoxButtonTakeOff, inputValues); CheckDoubleInputEntry(textBoxButtonLand, inputValues); CheckDoubleInputEntry(textBoxButtonHover, inputValues);
            CheckDoubleInputEntry(textBoxButtonEmergency, inputValues); CheckDoubleInputEntry(textBoxButtonFlatTrim, inputValues); CheckDoubleInputEntry(textBoxButtonChangeCamera, inputValues);
        }

        private void CheckDoubleInputEntry(TextBox textBox, List<String> inputValues)
        {
            String[] textBoxValues = textBox.Text.Split('-');

            bool doubleEntry = false;
            for (int i = 0; i < textBoxValues.Length; i++)
            {
                if (inputValues.FindAll(delegate(String value) { return value == textBoxValues[i]; }).Count > 1)
                {
                    doubleEntry = true;
                    break;
                }
            }

            if (doubleEntry)
            {
                textBox.ForeColor = Color.Red;
            }
            else
            {
                textBox.ForeColor = Color.Black;
            }
        }

        private void UpdateInputDevice()
        {
            bool mappingSet = false;

            if (selectedDevice == null || selectedControl == Control.None)
            {
                return;
            }

            bool isAxis = false;
            String inputValue = selectedDevice.GetCurrentlyInvokedInput(out isAxis);


            if (inputValue != null)
            {
                if (selectedControlType == ControlType.Axis)
                {
                    if (isAxis)
                    {
                        UpdateMapping(selectedDevice.Mapping, selectedControl, inputValue);
                        mappingSet = true;
                    }
                    else
                    {
                        if (tempAxisInput == null || tempAxisInput == "")
                        {
                            tempAxisInput = inputValue;
                        }
                        else
                        {
                            tempAxisInput = tempAxisInput + "-" + inputValue;

                            UpdateMapping(selectedDevice.Mapping, selectedControl, tempAxisInput);
                            tempAxisInput = "";
                            mappingSet = true;
                        }
                    }
                }
                else if (selectedControlType == ControlType.Button && !isAxis)
                {
                    UpdateMapping(selectedDevice.Mapping, selectedControl, inputValue);
                    mappingSet = true;
                }
            }

            if (mappingSet)
            {
                buttonSubmit.Focus();
            }
        }

        private void ConfigInput_FormClosing(object sender, FormClosingEventArgs e)
        {
            RevertMapping();
        }

        private void ConfigInput_Click(object sender, EventArgs e)
        {
            buttonSubmit.Focus();
        }

        private void comboBoxDevices_SelectedValueChanged(object sender, EventArgs e)
        {
            ChangeInputDevice();
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            Focus((TextBox)sender);
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            Unfocus((TextBox)sender);
        }

        private void timerInputUpdate_Tick(object sender, EventArgs e)
        {
            UpdateInputDevice();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetMapping();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            RevertMapping();
            Close();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            SaveMapping();
            Close();
        }
    }
}
