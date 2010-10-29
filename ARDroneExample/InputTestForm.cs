using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ARDrone.Input;

namespace ARDrone
{
    public partial class InputTestForm : Form
    {
        InputManager input = null;

        public InputTestForm()
        {
            InitializeComponent();
            input = new InputManager(this);

            timerJoystickUpdate.Start();
        }

        private void timerJoystickUpdate_Tick(object sender, EventArgs e)
        {
            InputState inputState = input.GetCurrentState();

            textBoxXAxis.Text = inputState.Roll.ToString();
            textBoxYAxis.Text = inputState.Pitch.ToString();
            textBoxRAxis.Text = inputState.Yaw.ToString();
            textBoxZAxis.Text = inputState.Gaz.ToString();

            checkBoxTakeOff.Checked = inputState.TakeOff;
            checkBoxLand.Checked = inputState.Land;
            checkBoxEmergency.Checked = inputState.Emergency;
            checkBoxFlatTrim.Checked = inputState.FlatTrim;
        }
    }
}
