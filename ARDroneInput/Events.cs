using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ARDrone.Input
{
    public delegate void InputDeviceLostHandler(object sender, InputDeviceLostEventArgs e);
    public delegate void NewInputDeviceHandler(object sender, NewInputDeviceEventArgs e);
    public delegate void RawInputReceivedHandler(object sender, RawInputReceivedEventArgs e);
    public delegate void NewInputStateHandler(object sender, NewInputStateEventArgs e);

    public class DeviceSpecificInputEventArgs : EventArgs
    {
        private String deviceId = null;

        public DeviceSpecificInputEventArgs(String deviceId)
        {
            this.deviceId = deviceId;
        }

        public String DeviceId
        {
            get
            {
                return deviceId;
            }
        }
    }

    public class NewInputDeviceEventArgs : DeviceSpecificInputEventArgs
    {
        private GenericInput input = null;

        public NewInputDeviceEventArgs(String deviceId, GenericInput input)
            : base(deviceId)
        {
            this.input = input;
        }

        public GenericInput Input
        {
            get
            {
                return input;
            }
        }
    }

    public class InputDeviceLostEventArgs : DeviceSpecificInputEventArgs
    {
        public InputDeviceLostEventArgs(String deviceId)
            : base(deviceId)
        { }
    }

    public class RawInputReceivedEventArgs : DeviceSpecificInputEventArgs
    {
        private String inputString = null;
        private bool isAxis = false;

        public RawInputReceivedEventArgs(String deviceId, String inputString, bool isAxis)
            : base(deviceId)
        {
            this.inputString = inputString;
            this.isAxis = isAxis;
        }

        public String InputString
        {
            get
            {
                return inputString;
            }
        }

        public bool IsAxis
        {
            get
            {
                return isAxis;
            }
        }
    }

    public class NewInputStateEventArgs : EventArgs
    {
        InputState currentInputState = null;

        public NewInputStateEventArgs(InputState currentInputState)
        {
            this.currentInputState = currentInputState;
        }

        public InputState CurrentInputState
        {
            get
            {
                return currentInputState;
            }
        }
    }
}
