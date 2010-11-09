using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace ARDrone.Input
{
    class KeyboardInput : GenericInput
    {
        private ArrayList keysPressedBefore = new ArrayList();

        Device device = null;

        /// <summary>
        /// Added to support serialization
        /// </summary>
        public KeyboardInput()
            : base()
        {
            List<String> validAxes = new List<String>();
            List<String> validButtons = new List<String>();
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (!validButtons.Contains(key.ToString()))
                {
                    validButtons.Add(key.ToString());
                }
            }
        }

        public override string DeviceName
        {
            get
            {
                return "Keyboard";
            }
        }

        public KeyboardInput(Device device) : base()
        {
            this.device = device;

            List<String> validAxes = new List<String>();
            List<String> validButtons = new List<String>();
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (!validButtons.Contains(key.ToString()))
                {
                    validButtons.Add(key.ToString());
                }
            }

            mapping = new InputMapping(validButtons, validAxes);
            mapping.SetAxisMappings("A-D", "W-S", "LeftArrow-Right", "DownArrow-Up");
            mapping.SetButtonMappings("C", "Return", "Return", "NumPad0", "Space", "F");
        }

        public override void Dispose()
        {
            device.Unacquire();
        }

        public override List<String> GetPressedButtons()
        {
            KeyboardState state = device.GetCurrentKeyboardState();

            List<String> buttonsPressed = new List<String>();
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (state[key])
                {
                    if (!buttonsPressed.Contains(key.ToString()))
                    {
                        buttonsPressed.Add(key.ToString());
                    }
                }
            }

            return buttonsPressed;
        }

        public override Dictionary<String, float> GetAxisValues()
        {
            return new Dictionary<String, float>();
        }
    }
}