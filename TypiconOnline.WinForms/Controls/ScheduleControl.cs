using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypiconOnline.WinForms.Controls
{
    public partial class ScheduleControl : UserControl
    {
        public ScheduleControl()
        {
            InitializeComponent();
        }

        private void checkBoxDocx_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxIsDocxOpen.Enabled = checkBoxDocx.Checked;
            if (!checkBoxDocx.Checked)
                checkBoxIsDocxOpen.Checked = false;

            CheckEnablingExecuteButton();
        }

        private void checkBoxBigDocx_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxIsBigDocxOpen.Enabled = checkBoxBigDocx.Checked;
            if (!checkBoxBigDocx.Checked)
                checkBoxIsBigDocxOpen.Checked = false;

            CheckEnablingExecuteButton();
        }

        private void CheckEnablingExecuteButton()
        {
            buttonExecute.Enabled = checkBoxDocx.Checked || checkBoxBigDocx.Checked || checkBoxTxt.Checked;
        }

        private void checkBoxTxt_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxWordpress.Enabled = checkBoxTxt.Checked;
            if (!checkBoxTxt.Checked)
                checkBoxWordpress.Checked = false;

            CheckEnablingExecuteButton();
        }
    }
}
