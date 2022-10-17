using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;
using System.Text.RegularExpressions;

namespace Laba1.Services
{
    /// <summary>
    /// Служба для работы с пользователями.
    /// </summary>
    public class UserService
    {
        /// <summary>
        /// Роль "Администратор".
        /// </summary>
        public const string ADMIN = "ADMIN";

        /// <summary>
        /// Пользователь является администратором.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <returns>Результат валидации роли пользователя.</returns>
        public bool IsAdmin(string login)
        {
            return login == ADMIN;
        }

        /// <summary>
        /// Задать новый пароль.
        /// </summary>
        /// <param name="plainPassword">Вводимый пользователем пароль.</param>
        /// <returns>Захешированный пароль.</returns>
        public string SetNewPassword(string plainPassword)
        {
            return Hash(plainPassword);
        }

        /// <summary>
        /// Валидация введенного пароля.
        /// </summary>
        /// <param name="password">Пароль пользователя.</param>
        /// <param name="plainPassword">Пароль вводимый пользователем.</param>
        /// <returns>Результат валидации введенного пароля.</returns>
        public bool CheckPassword(string password, string plainPassword)
        {
            return Hash(plainPassword) == password;
        }

        /// <summary>
        /// Хеширование пароля.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <returns>Хешированный пароль.</returns>
        private string Hash(string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = DigestUtilities.CalculateDigest("MD5", bytes);
            return Hex.ToHexString(hashBytes);
        }

        /// <summary>
        /// Валидация правильности пароля.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <param name="expression">Регулярное выражение.</param>
        /// <returns>Результат валидации правильности пароля.</returns>
        public bool VerifyPassword(string password, Regex expression)
        {
            return expression.IsMatch(password);
        }
    }
}
