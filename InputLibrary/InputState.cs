using System;
using System.Collections.Generic;
using System.Text;

namespace InputLibrary
{
    public class InputState
    {
        public float Roll  { get; set; }
        public float Pitch { get; set; }
        public float Yaw   { get; set; }
        public float Gaz   { get; set; }

        public bool CameraSwap { get; set; }
        public bool TakeOff    { get; set; }
        public bool Land       { get; set; }
        public bool Hover      { get; set; }
        public bool Emergency  { get; set; }
        public bool FlatTrim   { get; set; }

        public InputState()
        {
            Roll = 0; Pitch = 0; Gaz = 0;
            TakeOff = false; Land = false; Emergency = false; FlatTrim = false;
        }

        public InputState(float roll, float pitch, float yaw, float gaz, bool cameraSwapButton, bool takeOffButton, bool landButton, bool hoverButton, bool emergencyButton, bool flatTrimButton)
        {
            Roll = roll; Pitch = pitch; Yaw = yaw; Gaz = gaz;
            CameraSwap = cameraSwapButton;
            TakeOff = takeOffButton; Land = landButton; Hover = hoverButton;
            Emergency = emergencyButton; FlatTrim = flatTrimButton;
        }
    }

    public class InputMapping
    {
        private List<String> validButtons = null;
        private List<String> validAxes = null;

        private String rollAxisMapping = "";
        private String pitchAxisMapping = "";
        private String yawAxisMapping = "";
        private String gazAxisMapping = "";

        private String cameraSwapButton = "";
        private String takeOffButton = "";
        private String landButton = "";
        private String hoverButton = "";
        private String emergencyButton = "";
        private String flatTrimButton = "";

        public InputMapping(List<String> validButtons, List<String> validAxes)
        {
            this.validButtons = new List<String>();
            this.validAxes = new List<String>();

            for (int i = 0; i < validButtons.Count; i++)
            {
                if (validButtons[i].Contains("-")) { throw new Exception("'-' is not allowed within button names (button name '" + validButtons[i] + "')"); }
                if (validButtons[i] == null) { throw new Exception("Null is not allowed as a button name"); }
                this.validButtons.Add(validButtons[i]);
            }
            for (int i = 0; i < validAxes.Count; i++)
            {
                if (validAxes[i].Contains("-")) { throw new Exception("'-' is not allowed within axis names (axis name '" + validButtons[i] + "')"); }
                if (validAxes[i] == null) { throw new Exception("Null is not allowed as an axis name"); }
                this.validAxes.Add(validAxes[i]);
            }

            if (!this.validButtons.Contains("")) { this.validButtons.Add(""); }
            if (!this.validAxes.Contains("")) { this.validAxes.Add(""); }

            if (this.validButtons == null) { this.validButtons = new List<String>(); }
            if (this.validAxes == null) { this.validAxes = new List<String>(); }
        }

        public void SetAxisMappings(Object rollAxisMapping, Object pitchAxisMapping, Object yawAxisMapping, Object gazAxisMapping)
        {
            RollAxisMapping = rollAxisMapping.ToString();
            PitchAxisMapping = pitchAxisMapping.ToString();
            YawAxisMapping = yawAxisMapping.ToString();
            GazAxisMapping = gazAxisMapping.ToString();
        }

        public void SetButtonMappings(Object cameraSwapButtonMapping, Object takeOffButtonMapping, Object landButtonMapping, Object hoverButtonMapping, Object emergencyButtonMapping, Object flatTrimButtonMapping)
        {
            CameraSwapButton = cameraSwapButtonMapping.ToString();
            TakeOffButton = takeOffButtonMapping.ToString();
            LandButton = landButtonMapping.ToString();
            HoverButton = hoverButtonMapping.ToString();
            EmergencyButton = emergencyButtonMapping.ToString();
            FlatTrimButton = flatTrimButtonMapping.ToString();
        }

        public bool isValidButton(String buttonValue)
        {
            return validButtons.Contains(buttonValue);
        }

        public bool isValidAxis(String axisValue)
        {
            if (validAxes.Contains(axisValue))
            {
                return true;
            }
            else
            {
                String[] axisValues = axisValue.Split('-');
                return (axisValues.Length == 2 && validButtons.Contains(axisValues[0]) && validButtons.Contains(axisValues[1]));
            }
        }

        public String RollAxisMapping
        {
            get { return rollAxisMapping; }
            set
            {
                if (!isValidAxis(value)) { throw new Exception("Value is not a valid axis value"); }
                rollAxisMapping = value;
            }
        }

        public String PitchAxisMapping
        {
            get { return pitchAxisMapping; }
            set
            {
                if (!isValidAxis(value)) { throw new Exception("Value is not a valid axis value"); }
                pitchAxisMapping = value;
            }
        }

        public String YawAxisMapping
        {
            get { return yawAxisMapping; }
            set
            {
                if (!isValidAxis(value)) { throw new Exception("Value is not a valid axis value"); }
                yawAxisMapping = value;
            }
        }

        public String GazAxisMapping
        {
            get { return gazAxisMapping; }
            set
            {
                if (!isValidAxis(value)) { throw new Exception("Value is not a valid axis value"); }
                gazAxisMapping = value;
            }
        }

        public String CameraSwapButton
        {
            get { return cameraSwapButton; }
            set
            {
                if (!isValidButton(value)) { throw new Exception("Value is not a valid button value"); }
                cameraSwapButton = value;
            }
        }

        public String TakeOffButton
        {
            get { return takeOffButton; }
            set
            {
                if (!isValidButton(value)) { throw new Exception("Value is not a valid button value"); }
                takeOffButton = value;
            }
        }

        public String LandButton
        {
            get { return landButton; }
            set
            {
                if (!isValidButton(value)) { throw new Exception("Value is not a valid button value"); }
                landButton = value;
            }
        }

        public String HoverButton
        {
            get { return hoverButton; }
            set
            {
                if (!isValidButton(value)) { throw new Exception("Value is not a valid button value"); }
                hoverButton = value;
            }
        }

        public String EmergencyButton
        {
            get { return emergencyButton; }
            set
            {
                if (!isValidButton(value)) { throw new Exception("Value is not a valid button value"); }
                emergencyButton = value;
            }
        }

        public String FlatTrimButton
        {
            get { return flatTrimButton; }
            set
            {
                if (!isValidButton(value)) { throw new Exception("Value is not a valid button value"); }
                flatTrimButton = value;
            }
        }
    }
}
