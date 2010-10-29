using System;
using System.Collections;
using System.Text;

namespace ARDrone.Input
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

    public struct InputMapping
    {
        public String RollAxisMapping;
        public String PitchAxisMapping;
        public String YawAxisMapping;
        public String GazAxisMapping;

        public String CameraSwapButton;
        public String TakeOffButton;
        public String LandButton;
        public String HoverButton;
        public String EmergencyButton;
        public String FlatTrimButton;
    }
}
