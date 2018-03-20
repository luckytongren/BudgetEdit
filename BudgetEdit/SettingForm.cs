using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BudgetEdit
{
    public partial class SettingForm : Form
    {
        bool[] setting;
        public SettingForm()
        {
            InitializeComponent();
        }

        public SettingForm(bool[] setting)
        {
            InitializeComponent();
            this.setting = setting;
            checkBox1.Checked = setting[0];
            checkBox2.Checked = setting[1];
            checkBox3.Checked=setting[2];
            checkBox4.Checked= setting[3];
            checkBox5.Checked = setting[4];
            checkBox6.Checked= setting[5];
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            setting[0] = checkBox1.Checked;
            setting[1] = checkBox2.Checked;
            setting[2] = checkBox3.Checked;
            setting[3] = checkBox4.Checked;
            setting[4] = checkBox5.Checked;
            setting[5] = checkBox6.Checked;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
