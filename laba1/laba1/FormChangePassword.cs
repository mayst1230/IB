using Laba1.Models;
using Laba1.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace laba1
{
    public partial class FormChangePassword : Form
    {
        private UserService userService = new UserService();

        public User User;

        public FormChangePassword(User user)
        {
            InitializeComponent();
            User = user;
            textBoxLogin.Text = user.Login;
            if (string.IsNullOrEmpty(user.Password))
            {
                // Старого пароля нет
                textBoxOldPassword.Enabled = false;
            }
            else
            {
                textBoxOldPassword.Enabled = true;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // Check old password
            if (!string.IsNullOrEmpty(User.Password))
            {
                if (!userService.CheckPassword(User.Password, textBoxOldPassword.Text))
                {
                    MessageBox.Show(this, "Неправильный старый пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            // Check new password
            string newPassword = textBoxNewPassword.Text;
            string repeatPassword = textBoxRepeatPasword.Text;
            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show(this, "Не введен новый пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (User.LimitPassword && !userService.VerifyPassword(newPassword, new Regex(@"^([A-z]+[А-яЁё]+|[А-яЁё]+[A-z])")))
            {
                MessageBox.Show(this, "Пароль должен содержать латинские буквы и символы кириллицы",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (newPassword != repeatPassword)
            {
                MessageBox.Show(this, "Пароли не совпадают", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            User.Password = userService.SetNewPassword(newPassword);
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
