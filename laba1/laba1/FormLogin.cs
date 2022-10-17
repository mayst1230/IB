using System;
using System.Windows.Forms;

namespace laba1
{
    public partial class FormLogin : Form
    {
        public string Login
        {
            get
            {
                return textBoxLogin.Text;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    textBoxLogin.Text = value;
                    textBoxPassword.Select();
                    textBoxPassword.Focus();
                }
            }
        }

        public string Password
        {
            get
            {
                return textBoxPassword.Text;
            }
        }

        public FormLogin()
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
