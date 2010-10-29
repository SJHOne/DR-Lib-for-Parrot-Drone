using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace ARDrone.Input
{
    class JoystickInput : Input
    {
        private ArrayList buttonsPressedBefore = new ArrayList();

        public JoystickInput(Device device)
        {
            this.device = device;

            mapping = new InputMapping();
            if (device.Properties.ProductName == "T.Flight Stick X")
            {
                mapping.RollAxisMapping = "x"; mapping.PitchAxisMapping = "y"; mapping.YawAxisMapping = "r"; mapping.GazAxisMapping = "z";
                mapping.CameraSwapButton = "11";
                mapping.TakeOffButton = "4"; mapping.LandButton = "4"; mapping.HoverButton = "10";
                mapping.EmergencyButton = "2"; mapping.FlatTrimButton = "5";
            }
            else
            {
                mapping.RollAxisMapping = "x"; mapping.PitchAxisMapping = "y"; mapping.YawAxisMapping = "1-3"; mapping.GazAxisMapping = "2-4";
                mapping.CameraSwapButton = "6";
                mapping.TakeOffButton = "10"; mapping.LandButton = "10"; mapping.HoverButton = "8";
                mapping.EmergencyButton = "5"; mapping.FlatTrimButton = "9";
            }
        }

        
        public override InputState GetCurrentState()
        {
            JoystickState state = device.CurrentJoystickState;

            float xAxisValue = calculateFloatValue(state.X);
            float yAxisValue = calculateFloatValue(state.Y);
            float rAxisValue = calculateFloatValue(state.Rz);
            float zAxisValue = calculatePOVValue(device.CurrentJoystickState.GetPointOfView()[0]);

            ArrayList buttonsPressed = new ArrayList();
            byte[] buttons = state.GetButtons();
            for (int j = 0; j < buttons.Length; j++)
            {
                if (buttons[j] != 0)
                {
                    buttonsPressed.Add(j + 1);
                }
            }

            float roll = getAxisValue(mapping.RollAxisMapping, xAxisValue, yAxisValue, rAxisValue, zAxisValue, buttonsPressed);
            float pitch = getAxisValue(mapping.PitchAxisMapping, xAxisValue, yAxisValue, rAxisValue, zAxisValue, buttonsPressed);
            float yaw = getAxisValue(mapping.YawAxisMapping, xAxisValue, yAxisValue, rAxisValue, zAxisValue, buttonsPressed);
            float gaz = getAxisValue(mapping.GazAxisMapping, xAxisValue, yAxisValue, rAxisValue, zAxisValue, buttonsPressed);

            bool cameraSwap = getFlightButtonValue(mapping.CameraSwapButton, buttonsPressed);
            bool takeOff = getFlightButtonValue(mapping.TakeOffButton, buttonsPressed);
            bool land = getFlightButtonValue(mapping.LandButton, buttonsPressed);
            bool hover = getFlightButtonValue(mapping.HoverButton, buttonsPressed);
            bool emergency = getFlightButtonValue(mapping.EmergencyButton, buttonsPressed);
            bool flatTrim = getFlightButtonValue(mapping.FlatTrimButton, buttonsPressed);

            buttonsPressedBefore = buttonsPressed;

            return new InputState(roll, pitch, yaw, gaz, cameraSwap, takeOff, land, hover, emergency, flatTrim);
        }

        private float calculateFloatValue(int input)
        {
            float fpValue = ((float)(input - short.MaxValue) / (float)short.MaxValue);

            if (fpValue > 1) return 1.0f;
            if (fpValue < -1) return -1.0f;
            return fpValue;
        }

        private float calculatePOVValue(int povInput)
        {
            if (povInput == -1 || povInput == 9000 || povInput == 27000) return 0.0f;
            else if (povInput == 0 || povInput == 4500 || povInput == 31500) return 1.0f;
            else return -1.0f;
        }

        private float getAxisValue(String mapping, float xAxisValue, float yAxisValue, float rAxisValue, float zAxisValue, ArrayList buttonsPressed)
        {
            float value = 0.0f;
            if (mapping == "x") { value = xAxisValue; }
            else if (mapping == "y") { value = yAxisValue; }
            else if (mapping == "r") { value = rAxisValue; }
            else if (mapping == "z") { value = zAxisValue; }
            else
            {
                String[] mappingValues = mapping.Split('-');
                int firstButtonNumber = Int32.Parse(mappingValues[0]);
                int secondButtonNumber = Int32.Parse(mappingValues[1]);

                if (buttonsPressed.Contains(firstButtonNumber))
                {
                    value = -1.0f;
                }
                else if (buttonsPressed.Contains(secondButtonNumber))
                {
                    value = 1.0f;
                }
            }

            return value;
        }

        private bool getFlightButtonValue(String mapping, ArrayList buttonsPressed)
        {
            int buttonNumber = Int32.Parse(mapping);
            return buttonsPressed.Contains(buttonNumber) && !buttonsPressedBefore.Contains(buttonNumber);
        }
    }
}
