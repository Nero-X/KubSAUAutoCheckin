using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCheckin
{
    public partial class FormMsgConf : Form
    {
        public FormMsgConf()
        {
            InitializeComponent();
        }

        private void radioButton_Off_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) dgv.Enabled = false;
        }

        private void radioButton_On_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked) dgv.Enabled = true;
        }
    }
}
