using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace ARDrone.Input
{
    class KeyboardInput : Input
    {
        private ArrayList keysPressedBefore = new ArrayList();

        public KeyboardInput(Device device)
        {
            this.device = device;

            mapping = new InputMapping();
            mapping.RollAxisMapping = "A-D"; mapping.PitchAxisMapping = "W-S"; mapping.YawAxisMapping = "LeftArrow-RightArrow"; mapping.GazAxisMapping = "DownArrow-UpArrow";
            mapping.CameraSwapButton = "C";
            mapping.TakeOffButton = "Return"; mapping.LandButton = "Return"; mapping.HoverButton = "NumPad0";
            mapping.EmergencyButton = "Space"; mapping.FlatTrimButton = "F";
        }

        public override InputState GetCurrentState()
        {
            KeyboardState state = device.GetCurrentKeyboardState();

            float roll = getAxisValue(mapping.RollAxisMapping, state);
            float pitch = getAxisValue(mapping.PitchAxisMapping, state);
            float yaw = getAxisValue(mapping.YawAxisMapping, state);
            float gaz = getAxisValue(mapping.GazAxisMapping, state);

            bool cameraSwap = isFlightButtonPressed(mapping.CameraSwapButton, state);
            bool takeOff = isFlightButtonPressed(mapping.TakeOffButton, state);
            bool land = isFlightButtonPressed(mapping.LandButton, state);
            bool hover = isFlightButtonPressed(mapping.HoverButton, state);
            bool emergency = isFlightButtonPressed(mapping.EmergencyButton, state);
            bool flatTrim = isFlightButtonPressed(mapping.FlatTrimButton, state);

            keysPressedBefore = new ArrayList();
            if (cameraSwap) { keysPressedBefore.Add(mapping.CameraSwapButton); }
            if (takeOff) { keysPressedBefore.Add(mapping.TakeOffButton); }
            if (land) { keysPressedBefore.Add(mapping.LandButton); }
            if (hover) { keysPressedBefore.Add(mapping.HoverButton); }
            if (emergency) { keysPressedBefore.Add(mapping.EmergencyButton); }
            if (flatTrim) { keysPressedBefore.Add(mapping.FlatTrimButton); }

            return new InputState(roll, pitch, yaw, gaz, cameraSwap, takeOff, land, hover, emergency, flatTrim);
        }

        private float getAxisValue(String mapping, KeyboardState state)
        {
            float value = 0.0f;
            String[] mappingValues = mapping.Split('-');
            Key firstKeyValue = getKeyFromString(mappingValues[0]);
            Key secondKeyValue = getKeyFromString(mappingValues[1]);

            if (state[firstKeyValue] == true)
            {
                value = -1.0f;
            }
            else if (state[secondKeyValue] == true)
            {
                value = 1.0f;
            }

            return value;
        }

        private bool isFlightButtonPressed(String mapping, KeyboardState state)
        {
            Key keyValue = getKeyFromString(mapping);
            return state[keyValue] && !keysPressedBefore.Contains(mapping);
        }

        private Key getKeyFromString(String keyText)
        {
            try
            {
                return (Key)Enum.Parse(typeof(Key), keyText);
            }
            catch (Exception)
            {
                return Key.Yen;
            }
        }
    }
}
