﻿using System;
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

            inputManager = new ARDrone.Input.InputManager(Utility.GetWindowHandle(this));
            Init(inputManager);
        }

        public ConfigInput(ARDrone.Input.InputManager inputManager)
        {
            InitializeComponent();
            Init(inputManager);
        }

        public void Init(ARDrone.Input.InputManager inputManager)
        {
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

            nameControlMap.Add(textBoxButtonTakeoff.Name, Control.ButtonTakeoff); nameControlMap.Add(textBoxButtonLand.Name, Control.ButtonLand);
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
            textBoxAxisRoll.IsEnabled = enabled; textBoxAxisPitch.IsEnabled = enabled;
            textBoxAxisYaw.IsEnabled = enabled; textBoxAxisGaz.IsEnabled = enabled;

            textBoxButtonTakeoff.IsEnabled = enabled; textBoxButtonLand.IsEnabled = enabled;
            textBoxButtonHover.IsEnabled = enabled; textBoxButtonEmergency.IsEnabled = enabled;
            textBoxButtonFlatTrim.IsEnabled = enabled; textBoxButtonChangeCamera.IsEnabled = enabled;
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

            MessageBoxResult result = MessageBox.Show(this, "Do you really want to reset the setting to default values?", "Reset mapping", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
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

            textBoxButtonTakeoff.Text = mapping.TakeOffButton;
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
            inputValues.Add(textBoxButtonTakeoff.Text); inputValues.Add(textBoxButtonLand.Text); inputValues.Add(textBoxButtonHover.Text);
            inputValues.Add(textBoxButtonEmergency.Text); inputValues.Add(textBoxButtonFlatTrim.Text); inputValues.Add(textBoxButtonChangeCamera.Text);

            CheckDoubleInputEntry(textBoxAxisRoll, inputValues); CheckDoubleInputEntry(textBoxAxisPitch, inputValues);
            CheckDoubleInputEntry(textBoxAxisYaw, inputValues); CheckDoubleInputEntry(textBoxAxisGaz, inputValues);
            CheckDoubleInputEntry(textBoxButtonTakeoff, inputValues); CheckDoubleInputEntry(textBoxButtonLand, inputValues); CheckDoubleInputEntry(textBoxButtonHover, inputValues);
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            buttonSubmit.Focus();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RevertMapping();
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

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            ResetMapping();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            RevertMapping();
            Close();
        }

        private void buttonSubmit_Click(object sender, RoutedEventArgs e)
        {
            SaveMapping();
            Close();
        }
    }
}