using System;
using System.Windows.Forms;

namespace laba1
{
    public partial class FormPassPhrase : Form
    {
        public string PassPhrase
        {
            get
            {
                return textBoxPassPhrase.Text;
            }
        }

        public FormPassPhrase()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
