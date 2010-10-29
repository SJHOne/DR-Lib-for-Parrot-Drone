using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace ARDrone.Input
{
    abstract class Input
    {
        protected Device device = null;
        protected InputMapping mapping;

        public void Dispose()
        {
            if (device != null)
            {
                device.Unacquire();
                device = null;
            }
        }

        abstract public InputState GetCurrentState();
    }
}
