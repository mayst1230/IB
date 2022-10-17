using Laba1.Models;
using Laba1.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace laba1
{
    public partial class FormUsers : Form
    {
        public List<User> Users { get; }

        private UserService userService = new UserService();

        public FormUsers(List<User> users)
        {
            InitializeComponent();
            Users = users;
            var bindingList = new BindingList<User>(Users);
            var source = new BindingSource(bindingList, null);
            dataGridView.DataSource = source;
        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            User user = (User)e.Row.DataBoundItem;
            if (userService.IsAdmin(user.Login))
            {
                MessageBox.Show(this, "Администратора удалять нельзя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
                return;
            }
            if (MessageBox.Show(this, "Удалить пользователя " + user.Login + "?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }

        private void dataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            // Следим за уникальностью имён пользователей
            if (e.ColumnIndex == 0)
            {
                DataGridViewRow row = dataGridView.Rows[e.RowIndex];
                string login = e.FormattedValue.ToString();
                for (int i = 0; i < Users.Count; i++)
                {
                    if (Users[i].Login == login && i != e.RowIndex)
                    {
                        e.Cancel = true;
                        row.ErrorText = "Пользователь " + login + " уже существует!";
                        return;
                    }
                }
                row.ErrorText = "";
            }
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // У Администратора запрещаем редактировать всё, кроме ограничений на пароль
            User user = Users[e.RowIndex];
            if (userService.IsAdmin(user.Login))
            {
                e.Cancel = e.ColumnIndex != 3;
            }
        }

        private void FormUsers_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
