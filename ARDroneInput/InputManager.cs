using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.DirectX.DirectInput;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using WiimoteLib;

namespace ARDrone.Input
{
    public class InputManager
    {
        private List<GenericInput> inputDevices = null;
        
        public List<GenericInput> InputDevices
        {
            get { return inputDevices; }
        }

        public InputManager(IntPtr windowHandle)
        {
            inputDevices = new List<GenericInput>();
            CreateInputDevices(windowHandle);
        }

        private void CreateInputDevices(IntPtr windowHandle)
        {
            AddKeyboardDevices(windowHandle);
            AddJoystickDevices(windowHandle);
            AddWiimoteDevices();
        }

        private void UpdateDevices()
        {
            
        }

        private void AddKeyboardDevices(IntPtr windowHandle)
        {
            DeviceList keyboardControllerList = Manager.GetDevices(DeviceClass.Keyboard, EnumDevicesFlags.AttachedOnly);
            for (int i = 0; i < keyboardControllerList.Count; i++)
            {
                keyboardControllerList.MoveNext();
                DeviceInstance deviceInstance = (DeviceInstance)keyboardControllerList.Current;

                Device device = new Device(deviceInstance.InstanceGuid);
                device.SetCooperativeLevel(windowHandle, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
                device.SetDataFormat(DeviceDataFormat.Keyboard);
                device.Acquire();

                KeyboardInput input = new KeyboardInput(device);
                inputDevices.Add(input);
            }
        }

        private void AddJoystickDevices(IntPtr windowHandle)
        {
            DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
            for (int i = 0; i < gameControllerList.Count; i++)
            {
                gameControllerList.MoveNext();
                DeviceInstance deviceInstance = (DeviceInstance)gameControllerList.Current;

                Device device = new Device(deviceInstance.InstanceGuid);

                if (device.DeviceInformation.ProductGuid != new Guid("0306057e-0000-0000-0000-504944564944"))       // Wiimotes are excluded
                {
                    device.SetCooperativeLevel(windowHandle, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
                    device.SetDataFormat(DeviceDataFormat.Joystick);
                    device.Acquire();

                    JoystickInput input = new JoystickInput(device);
                    inputDevices.Add(input);
                }
            }
        }

        private void AddWiimoteDevices()
        {
            WiimoteCollection wiiMoteCollection = new WiimoteCollection();

            try
            {
                wiiMoteCollection.FindAllWiimotes();
            }
            catch (WiimoteNotFoundException)
            {
                Console.WriteLine("Wiimote not found error");
            }
            catch (WiimoteException)
            {
                Console.WriteLine("Wiimote error");
            }

            foreach (Wiimote wiimote in wiiMoteCollection)
            {
                WiimoteInput wiimoteInput = new WiimoteInput(wiimote);
                inputDevices.Add(wiimoteInput);

                wiimote.SetLEDs(false, false, false, false);
            }
        }

        public void Dispose()
        {
            for (int i = 0; i < inputDevices.Count; i++)
            {
                inputDevices[i].Dispose();
            }
        }

        public InputState GetCurrentState()
        {
            InputState currentInputState = new InputState();

            for (int i = 0; i < inputDevices.Count; i++)
            {
                currentInputState = inputDevices[i].GetCurrentState();

                if (currentInputState.Roll != 0.0f || currentInputState.Pitch != 0.0f || currentInputState.Yaw != 0.0f || currentInputState.Gaz != 0.0f ||
                    currentInputState.CameraSwap || currentInputState.TakeOff || currentInputState.Land || currentInputState.Hover || currentInputState.Emergency || currentInputState.FlatTrim)
                {
                    return currentInputState;
                }
            }

            return currentInputState;
        }

        public void SetFlags(bool isConnected, bool isFlying, bool isHovering, bool isEmergency)
        {
            for (int i = 0; i < inputDevices.Count; i++)
            {
                if (inputDevices[i] is WiimoteInput)
                {
                    WiimoteInput wiimoteInput = (WiimoteInput)inputDevices[i];
                    wiimoteInput.SetLEDs(isConnected, isFlying, isHovering, isEmergency);
                }
            }
        }
    }
}