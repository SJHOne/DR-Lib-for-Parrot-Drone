using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace ARDrone.Input
{
    public class JoystickInput : GenericInput
    {
        enum Axis
        {
            Axis_X, Axis_Y, Axis_Z, Axis_R, Axis_POV_1
        }
        enum Button
        {
            Button_1, Button_2, Button_3, Button_4, Button_5,
            Button_6, Button_7, Button_8, Button_9, Button_10,
            Button_11, Button_12, Button_13, Button_14, Button_15
        }

        Device device = null;

        public override string DeviceName
        {
            get
            {
                if (device == null) { return string.Empty; }
                else { return device.Properties.ProductName; }
            }
        }

        public override string FilePrefix
        {
            get
            {
                if (device == null) { return string.Empty; }
                else { return device.Properties.TypeName; }
            }
        }

        public JoystickInput(Device device) : base()
        {
            this.device = device;

            List<String> validAxes = new List<String>();
            foreach (Axis axis in Enum.GetValues(typeof(Axis)))
            {
                validAxes.Add(axis.ToString());
            }

            List<String> validButtons = new List<String>();
            foreach (Button button in Enum.GetValues(typeof(Button)))
            {
                validButtons.Add(button.ToString());
            }

            mapping = new InputMapping(validButtons, validAxes);
            if (!LoadMapping())
            {
                // Initial mapping (if no saved mapping could be found)
                if (device.Properties.ProductName == "T.Flight Stick X")
                {
                    mapping.SetAxisMappings(Axis.Axis_X, Axis.Axis_Y, Axis.Axis_R, Axis.Axis_POV_1);
                    mapping.SetButtonMappings(Button.Button_11, Button.Button_4, Button.Button_4, Button.Button_10, Button.Button_2, Button.Button_5);
                }
                else
                {
                    mapping.SetAxisMappings(Axis.Axis_X, Axis.Axis_Y, "Button_1-Button_3", "Button_2-Button_4");
                    mapping.SetButtonMappings(Button.Button_6, Button.Button_10, Button.Button_10, Button.Button_8, Button.Button_5, Button.Button_9);
                }
            }
        }

        public override void Dispose()
        {
            device.Unacquire();
        }

        public override List<String> GetPressedButtons()
        {
            JoystickState state = device.CurrentJoystickState;

            List<String> buttonsPressed = new List<String>();
            byte[] buttons = state.GetButtons();
            for (int j = 0; j < buttons.Length; j++)
            {
                if (buttons[j] != 0)
                {
                    buttonsPressed.Add("Button_" + (j + 1));
                }
            }

            return buttonsPressed;
        }

        public override Dictionary<String, float> GetAxisValues()
        {
            JoystickState state = device.CurrentJoystickState;

            Dictionary<String, float> axisValues = new Dictionary<String, float>();
            axisValues[Axis.Axis_X.ToString()] = GetFloatValue(state.X);
            axisValues[Axis.Axis_Y.ToString()] = GetFloatValue(state.Y);
            axisValues[Axis.Axis_Z.ToString()] = GetFloatValue(state.Z);
            axisValues[Axis.Axis_R.ToString()] = GetFloatValue(state.Rz);
            axisValues[Axis.Axis_POV_1.ToString()] = CalculatePOVValue(state.GetPointOfView()[0]);

            return axisValues;
        }

        private float GetFloatValue(int input)
        {
            return (float)(input - short.MaxValue) / (float)short.MaxValue;
        }

        private float CalculatePOVValue(int povInput)
        {
            if (povInput == -1 || povInput == 9000 || povInput == 27000) return 0.0f;
            else if (povInput == 0 || povInput == 4500 || povInput == 31500) return 1.0f;
            else return -1.0f;
        }
    }
}