using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScheduleForm
{
    public partial class FormTest : Form
    {

        private ScheduleHandling.ScheduleHandler _scheduleHandler;

        public FormTest()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            //_scheduleHandler = new ScheduleHandler();

        }
    }
}
