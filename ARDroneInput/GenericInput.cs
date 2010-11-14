using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ARDrone.Input
{
    public abstract class GenericInput
    {
        protected InputMapping mapping = null;
        protected InputMapping backupMapping = null;

        public InputMapping Mapping
        {
            get { return mapping; }
            set {
                // Copy the validation strings from the previous object 
                value.CopyValidation(mapping);
                value.SkipValidation = false;
                mapping = value;
            } 
        }

        public virtual string DeviceName
        {
            get { return string.Empty; } 
        }

        public virtual string FilePrefix
        { 
            get { return string.Empty; } 
        }

        List<String> buttonsPressedBefore = new List<String>();
        Dictionary<String, float> buttonAxisValuesInitial = new Dictionary<String, float>();

        public GenericInput()
        {
            mapping = new InputMapping(new List<String>(), new List<String>());
        }

        private String GetMappingFilePath()
        {
            String appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            String appFolder = Path.Combine(appDataFolder, "ARDrone.NET", "mappings");

            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            String mappingPath = Path.Combine(appFolder, FilePrefix + ".xml");
            return mappingPath;
        }

        public bool LoadMapping()
        {
            if (mapping == null)
            {
                return false;
            }

            String mappingPath = GetMappingFilePath();
            if (!File.Exists(mappingPath))
            {
                return false;
            }

            //TODO load mapping from file

            return true;
        }

        public void SaveMapping()
        {
            if (mapping == null)
            {
                return;
            }

            String mappingPath = GetMappingFilePath();
            if (!File.Exists(mappingPath))
            {
                File.Create(mappingPath);
            }
            File.OpenWrite(mappingPath);

            // TODO write mapping to file
        }

        public void ResetMapping()
        {
            mapping = backupMapping.Clone();
        }

        abstract public void Dispose();

        public void InitCurrentlyInvokedInput()
        {
            Dictionary<String, float> axisValues = GetAxisValues();
            buttonAxisValuesInitial = axisValues;
        }

        public String GetCurrentlyInvokedInput(out bool isAxis)
        {
            List<String> buttonsPressed = GetPressedButtons();
            Dictionary<String, float> axisValues = GetAxisValues();

            List<String> buttonsPressedBefore = this.buttonsPressedBefore;
            SetButtonsPressedBefore(buttonsPressed);

            while(buttonsPressed.Count > 0)
            {
                if (buttonsPressedBefore.Contains(buttonsPressed[0]))
                {
                    buttonsPressed.RemoveAt(0);
                    continue;
                }
                else
                {
                    isAxis = false;
                    return buttonsPressed[0];
                }
            }
            foreach (KeyValuePair<String, float> keyValuePair in axisValues)
            {
                String axis = keyValuePair.Key;
                float axisValue = keyValuePair.Value;

                if (buttonAxisValuesInitial.ContainsKey(axis) && Math.Abs(buttonAxisValuesInitial[axis] - axisValue) > 0.1f)
                {
                    isAxis = true;
                    return axis;
                }
            }

            isAxis = false;
            return null;
        }

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

            SetButtonsPressedBefore(buttonsPressed);

            return new InputState(roll, pitch, yaw, gaz, cameraSwap, takeOff, land, hover, emergency, flatTrim);
        }

        private void SetButtonsPressedBefore(List<String> buttonsPressed)
        {
            buttonsPressedBefore = new List<String>();
            for (int i = 0; i < buttonsPressed.Count; i++)
            {
                buttonsPressedBefore.Add(buttonsPressed[i]);
            }
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
