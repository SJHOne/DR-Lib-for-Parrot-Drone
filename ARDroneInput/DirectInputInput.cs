using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace ARDrone.Input
{
    public abstract class DirectInputInput : GenericInput
    {
        protected Device device = null;

        public override void Dispose()
        {
            device.Unacquire();
        }

        public override String DeviceInstanceId
        {
            get
            {
                if (device == null) { return string.Empty; }
                else { return device.DeviceInformation.InstanceGuid.ToString(); }
            }
        }
    }
}
