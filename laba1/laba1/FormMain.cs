using Laba1.Models;
using Laba1.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace laba1
{
    public partial class FormMain : Form
    {
        private FileService fileService = new FileService();
        private UserService userService = new UserService();

        private List<User> users;
        private string passPhrase;
        private User currentUser;

        public FormMain()
        {
            InitializeComponent();
        }

        private void сменитьПарольToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormChangePassword(currentUser);
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                users.Remove(users.Find(rec => rec.Login == currentUser.Login));
                users.Add(currentUser);
            }
        }

        private void пользователиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormUsers(users);
            form.ShowDialog(this);
            users = form.Users;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAbout();
            form.ShowDialog(this);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Запрашиваем парольную фразу
            if (!RequestPassPhrase())
            {
                Application.Exit();
                return;
            }
            // Загружаем учетные данные пользователей из файла
            if (!LoadUsers())
            {
                Application.Exit();
                return;
            }
            // Запрашиваем имя пользователя и пароль
            if (!AuthenticateUser())
            {
                Application.Exit();
                return;
            }
            textBoxUserLogin.Text = currentUser.Login;
            пользователиToolStripMenuItem.Enabled = userService.IsAdmin(currentUser.Login);
        }

        private bool RequestPassPhrase()
        {
            var form = new FormPassPhrase();
            form.ShowDialog(this);
            if (form.DialogResult != DialogResult.OK || string.IsNullOrEmpty(form.PassPhrase))
            {
                MessageBox.Show(this, "Не введена парольная фраза", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            passPhrase = form.PassPhrase;
            return true;
        }

        private bool LoadUsers()
        {
            try
            {
                users = fileService.LoadUsersFile(passPhrase);
                // Нет пользователя ADMIN, значит файл неправильно расшифровался
                if (users.Find(rec => rec.Login == UserService.ADMIN) == null)
                {
                    MessageBox.Show(this, "Неправильная парольная фраза", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
            catch (IOException)
            {
                MessageBox.Show(this, "Ошибка загрузки учетных данных пользователей", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show(this, "Неправильная парольная фраза", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool AuthenticateUser()
        {
            string login = "";
            User user = null;
            bool authenticated = false;
            int wrongPasswordCount = 0;
            int tryPasswordCount = 3;
            // Даём три попытки ввода пароля
            do
            {
                var form = new FormLogin();
                form.Login = login;
                form.ShowDialog(this);
                if (form.DialogResult != DialogResult.OK)
                {
                    MessageBox.Show(this, "Не пройдена аутентификация", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                login = form.Login;
                string password = form.Password;
                // Ищем пользователя
                user = users.Find(rec => rec.Login == login);
                if (user == null)
                {
                    MessageBox.Show(this, "Не найден пользователь " + login, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    authenticated = false;
                }
                // Проверяем блокировку пользователя
                else if (user.Blocked)
                {
                    MessageBox.Show(this, "Пользователь " + login + " заблокирован", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    authenticated = false;
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    // Если пустой пароль, то запрашиваем новый пароль
                    authenticated = RequestNewPassword(ref user);
                    if (authenticated)
                    {
                        users.Remove(users.Find(rec => rec.Login == user.Login));
                        users.Add(user);
                    }
                }
                else
                {
                    // Проверяем пароль
                    authenticated = userService.CheckPassword(user.Password, password);
                    if (!authenticated)
                    {
                        MessageBox.Show(this, $"Неправильный пароль. Осталось попыток: {tryPasswordCount}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tryPasswordCount--;
                        wrongPasswordCount++;
                        // При трехкратном вводе неверного пароля работа программы должна завершаться
                        if (wrongPasswordCount >= 3)
                        {
                            MessageBox.Show(this, "Попытки авторизации закончены.", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return false;
                        }   
                    }
                }
            } while (user == null || !authenticated);
            currentUser = user;
            return true;
        }

        private bool RequestNewPassword(ref User user)
        {
            var form = new FormChangePassword(user);
            form.ShowDialog(this);
            if (form.DialogResult == DialogResult.OK)
            {
                user = form.User;
            }
            return form.DialogResult == DialogResult.OK;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (currentUser != null)
                {
                    fileService.SaveUsersFile(users, passPhrase);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
