using System;
using System.Collections;
using System.Text;
using Microsoft.DirectX.DirectInput;

namespace ARDrone.Input
{
    class InputManager
    {
        private ArrayList inputDevices = null;

        public InputManager(System.Windows.Forms.Form form)
        {
            inputDevices = new ArrayList();

            CreateJoystickInputDevices(form);
        }

        private void CreateJoystickInputDevices(System.Windows.Forms.Form form)
        {
            AddKeyboardDevices(form);
            AddJoystickDevices(form);
        }

        private void AddKeyboardDevices(System.Windows.Forms.Form form)
        {
            DeviceList keyboardControllerList = Manager.GetDevices(DeviceClass.Keyboard, EnumDevicesFlags.AttachedOnly);
            for (int i = 0; i < keyboardControllerList.Count; i++)
            {
                keyboardControllerList.MoveNext();
                DeviceInstance deviceInstance = (DeviceInstance)keyboardControllerList.Current;

                Device device = new Device(deviceInstance.InstanceGuid);
                device.SetCooperativeLevel(form, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
                device.SetDataFormat(DeviceDataFormat.Keyboard);
                device.Acquire();

                KeyboardInput input = new KeyboardInput(device);
                inputDevices.Add(input);
            }
        }

        private void AddJoystickDevices(System.Windows.Forms.Form form)
        {
            DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
            for (int i = 0; i < gameControllerList.Count; i++)
            {
                gameControllerList.MoveNext();
                DeviceInstance deviceInstance = (DeviceInstance)gameControllerList.Current;

                Device device = new Device(deviceInstance.InstanceGuid);
                device.SetCooperativeLevel(form, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
                device.SetDataFormat(DeviceDataFormat.Joystick);
                device.Acquire();

                JoystickInput input = new JoystickInput(device);
                inputDevices.Add(input);
            }
        }


        public void Dispose()
        {
            for (int i = 0; i < inputDevices.Count; i++)
            {
                ((JoystickInput)inputDevices[i]).Dispose();
            }
        }

        public InputState GetCurrentState()
        {
            InputState currentInputState = new InputState();

            for (int i = 0; i < inputDevices.Count; i++)
            {
                currentInputState = ((Input)inputDevices[i]).GetCurrentState();

                if (currentInputState.Roll != 0.0f || currentInputState.Pitch != 0.0f || currentInputState.Yaw != 0.0f || currentInputState.Gaz != 0.0f ||
                    currentInputState.CameraSwap || currentInputState.TakeOff || currentInputState.Land || currentInputState.Hover || currentInputState.Emergency || currentInputState.FlatTrim)
                {
                    return currentInputState;
                }
            }

            return currentInputState;
        }
    }
}
