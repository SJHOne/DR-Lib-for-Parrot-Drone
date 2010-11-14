using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using ARDrone.Input;

namespace ARDrone.UI
{
    public partial class ConfigInput : Window
    {
        private enum Control { None, AxisRoll, AxisPitch, AxisYaw, AxisGaz, ButtonTakeoff, ButtonLand, ButtonHover, ButtonEmergency, ButtonFlatTrim, ButtonChangeCamera };
        private enum ControlType { None, Axis, Button };

        private Dictionary<String, Control> nameControlMap = null;
        private Dictionary<Control, ControlType> controlTypeMap = null;

        private DispatcherTimer timerInputUpdate;

        private ARDrone.Input.InputManager inputManager = null;

        private GenericInput selectedDevice = null;
        private Control selectedControl = Control.None;
        private ControlType selectedControlType = ControlType.None;

        private String tempAxisInput = "";

        Dictionary<String, GenericInput> devices = null;

        public ConfigInput()
        {
            InitializeComponent();
            InitializeTimers();

            InitializeControlMap();
   
            inputManager = new ARDrone.Input.InputManager(Utility.GetWindowHandle(this));
            InitializeDeviceList();
        }

        public void InitializeControlMap()
        {
            nameControlMap = new Dictionary<String, Control>();

            nameControlMap.Add(textBoxRoll.Name, Control.AxisRoll); nameControlMap.Add(textBoxPitch.Name, Control.AxisPitch);
            nameControlMap.Add(textBoxYaw.Name, Control.AxisYaw); nameControlMap.Add(textBoxGaz.Name, Control.AxisGaz);

            nameControlMap.Add(textBoxTakeoff.Name, Control.ButtonTakeoff); nameControlMap.Add(textBoxLand.Name, Control.ButtonLand);
            nameControlMap.Add(textBoxHover.Name, Control.ButtonHover); nameControlMap.Add(textBoxEmergency.Name, Control.ButtonEmergency);
            nameControlMap.Add(textBoxFlatTrim.Name, Control.ButtonFlatTrim); nameControlMap.Add(textBoxChangeCamera.Name, Control.ButtonChangeCamera);

            controlTypeMap = new Dictionary<Control, ControlType>();

            controlTypeMap.Add(Control.AxisRoll, ControlType.Axis); controlTypeMap.Add(Control.AxisPitch, ControlType.Axis);
            controlTypeMap.Add(Control.AxisYaw, ControlType.Axis); controlTypeMap.Add(Control.AxisGaz, ControlType.Axis);

            controlTypeMap.Add(Control.ButtonTakeoff, ControlType.Button); controlTypeMap.Add(Control.ButtonLand, ControlType.Button);
            controlTypeMap.Add(Control.ButtonHover, ControlType.Button); controlTypeMap.Add(Control.ButtonEmergency, ControlType.Button);
            controlTypeMap.Add(Control.ButtonFlatTrim, ControlType.Button); controlTypeMap.Add(Control.ButtonChangeCamera, ControlType.Button);
        }

        public void InitializeTimers()
        {
            timerInputUpdate = new DispatcherTimer();
            timerInputUpdate.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timerInputUpdate.Tick += new EventHandler(timerInputUpdate_Tick);
            timerInputUpdate.Start();
        }

        public void InitializeDeviceList()
        {
            devices = new Dictionary<String, GenericInput>();

            foreach (GenericInput inputDevice in inputManager.InputDevices)
            {
                devices.Add(inputDevice.DeviceName, inputDevice);

                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = inputDevice.DeviceName;
                comboBoxDevices.Items.Add(newItem); 
            }
        }

        private void ChangeInputDevice()
        {
            object comboBoxContent = ((ComboBoxItem)comboBoxDevices.SelectedValue).Content;

            if (((ComboBoxItem)comboBoxDevices.Items[0]).Content != null &&
                ((ComboBoxItem)comboBoxDevices.Items[0]).Content.ToString() == "-- No device selected --")
            {
                comboBoxDevices.Items.RemoveAt(0);
            }

            if (comboBoxContent != null)
            {
                SetMappingEnabledState(true);

                String selectedDeviceName = comboBoxContent.ToString();
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

                textBox.Foreground = new SolidColorBrush(Colors.LightGray);
                textBox.Text = "-- Assigning a value --";
            }
        }

        private void Unfocus(TextBox textBox)
        {
            if (textBox != null && nameControlMap.ContainsKey(textBox.Name))
            {
                selectedControl = Control.None;
                selectedControlType = ControlType.None;

                textBox.Foreground = new SolidColorBrush(Colors.Black);

                if (selectedDevice != null)
                {
                    TakeOverMapping(selectedDevice.Mapping);
                }
            }
        }

        private void SetMappingEnabledState(bool enabled)
        {
            textBoxRoll.IsEnabled = enabled; textBoxPitch.IsEnabled = enabled;
            textBoxYaw.IsEnabled = enabled; textBoxGaz.IsEnabled = enabled;

            textBoxTakeoff.IsEnabled = enabled; textBoxLand.IsEnabled = enabled;
            textBoxHover.IsEnabled = enabled; textBoxEmergency.IsEnabled = enabled;
            textBoxFlatTrim.IsEnabled = enabled; textBoxChangeCamera.IsEnabled = enabled;
        }

        private void SaveMapping()
        {
            selectedDevice.SaveMapping();
        }

        private void ResetMapping()
        {
            selectedDevice.ResetMapping();
        }

        private void TakeOverMapping(InputMapping mapping)
        {
            textBoxRoll.Text = mapping.RollAxisMapping;
            textBoxPitch.Text = mapping.PitchAxisMapping;
            textBoxYaw.Text = mapping.YawAxisMapping;
            textBoxGaz.Text = mapping.GazAxisMapping;

            textBoxTakeoff.Text = mapping.TakeOffButton;
            textBoxLand.Text = mapping.LandButton;
            textBoxHover.Text = mapping.HoverButton;
            textBoxEmergency.Text = mapping.EmergencyButton;
            textBoxFlatTrim.Text = mapping.FlatTrimButton;
            textBoxChangeCamera.Text = mapping.CameraSwapButton;

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

            inputValues.AddRange(textBoxRoll.Text.Split('-')); inputValues.AddRange(textBoxPitch.Text.Split('-'));
            inputValues.AddRange(textBoxYaw.Text.Split('-')); inputValues.AddRange(textBoxGaz.Text.Split('-'));
            inputValues.Add(textBoxTakeoff.Text); inputValues.Add(textBoxLand.Text); inputValues.Add(textBoxHover.Text);
            inputValues.Add(textBoxEmergency.Text); inputValues.Add(textBoxFlatTrim.Text); inputValues.Add(textBoxChangeCamera.Text);

            CheckDoubleInputEntry(textBoxRoll, inputValues); CheckDoubleInputEntry(textBoxPitch, inputValues);
            CheckDoubleInputEntry(textBoxYaw, inputValues); CheckDoubleInputEntry(textBoxGaz, inputValues);
            CheckDoubleInputEntry(textBoxTakeoff, inputValues); CheckDoubleInputEntry(textBoxLand, inputValues); CheckDoubleInputEntry(textBoxHover, inputValues);
            CheckDoubleInputEntry(textBoxEmergency, inputValues); CheckDoubleInputEntry(textBoxFlatTrim, inputValues); CheckDoubleInputEntry(textBoxChangeCamera, inputValues);
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
                textBox.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                textBox.Foreground = new SolidColorBrush(Colors.Black);
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

        private void comboBoxDevices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeInputDevice();
        }

        private void textBoxControl_GotFocus(object sender, RoutedEventArgs e)
        {
            Focus((TextBox)e.OriginalSource);
        }

        private void textBoxControl_LostFocus(object sender, RoutedEventArgs e)
        {
            Unfocus((TextBox)e.OriginalSource);
        }

        private void timerInputUpdate_Tick(object sender, EventArgs e)
        {
            UpdateInputDevice();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            ResetMapping();
            Close();
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            SaveMapping();
            Close();
        }
    }
}