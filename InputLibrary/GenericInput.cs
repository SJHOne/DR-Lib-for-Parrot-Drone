using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InputLibrary
{
    public abstract class GenericInput
    {
        protected InputMapping mapping = null;

        public InputMapping Mapping
        {
            get { return mapping; }
            set { mapping = value; } // <<< SJH TODO - copy the validation strings from the previous object 
        }

        public virtual string DeviceName
        { 
            get { return string.Empty; } 
        }

        List<String> buttonsPressedBefore = new List<String>();

        public GenericInput()
        {
            // Used for axis, buttons and string initialization
            mapping = new InputMapping(new List<String>(), new List<String>());
        }

        abstract public void Dispose();

        public InputState GetCurrentState()
        {
            List<String> buttonsPressed = GetPressedButtons();
            Dictionary<String, float> axisValues = GetAxisValues();

            if (buttonsPressed.Contains("")) { buttonsPressed.Remove(""); }
            if (axisValues.ContainsKey("")) { axisValues.Remove(""); }

            float roll = GetAxisValue(mapping.RollAxisMapping, buttonsPressed, axisValues);
            float pitch = GetAxisValue(mapping.PitchAxisMapping, buttonsPressed, axisValues);
            float yaw = GetAxisValue(mapping.YawAxisMapping, buttonsPressed, axisValues);
            float gaz = GetAxisValue(mapping.GazAxisMapping, buttonsPressed, axisValues);

            bool cameraSwap = IsFlightButtonPressed(mapping.CameraSwapButton, buttonsPressed);
            bool takeOff = IsFlightButtonPressed(mapping.TakeOffButton, buttonsPressed);
            bool land = IsFlightButtonPressed(mapping.LandButton, buttonsPressed);
            bool hover = IsFlightButtonPressed(mapping.HoverButton, buttonsPressed);
            bool emergency = IsFlightButtonPressed(mapping.EmergencyButton, buttonsPressed);
            bool flatTrim = IsFlightButtonPressed(mapping.FlatTrimButton, buttonsPressed);

            buttonsPressedBefore = new List<String>();
            for (int i = 0; i < buttonsPressed.Count; i++)
            {
                buttonsPressedBefore.Add(buttonsPressed[i]);
            }

            return new InputState(roll, pitch, yaw, gaz, cameraSwap, takeOff, land, hover, emergency, flatTrim);
        }

        private float GetAxisValue(String mappingValue, List<String> buttonsPressed, Dictionary<String, float> axisValues)
        {
            float value = 0.0f;

            if (axisValues.ContainsKey(mappingValue))
            {
                value = axisValues[mappingValue];
            }
            else
            {
                String[] mappingValues = mappingValue.Split('-');
                String firstButton = mappingValues[0];
                String secondButton = mappingValues[1];

                if (buttonsPressed.Contains(firstButton))
                {
                    value = -1.0f;
                }
                else if (buttonsPressed.Contains(secondButton))
                {
                    value = 1.0f;
                }
                else
                {
                    value = 0.0f;
                }
            }

            return TrimFloatValue(value);
        }

        private bool IsFlightButtonPressed(String mappingValue, List<String> buttonsPressed)
        {
            return (buttonsPressed.Contains(mappingValue) && !buttonsPressedBefore.Contains(mappingValue));
        }

        private float TrimFloatValue(float inputValue)
        {
            if (inputValue > 1) return 1.0f;
            if (inputValue < -1) return -1.0f;
            return inputValue;
        }

        abstract public List<String> GetPressedButtons();
        abstract public Dictionary<String, float> GetAxisValues();
    }
}
