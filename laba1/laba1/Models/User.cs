using System;
using System.ComponentModel;

namespace Laba1.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [DisplayName("Имя")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [DisplayName("Пароль")]
        [ReadOnly(true)]
        public string Password { get; set; }

        /// <summary>
        /// Пользователь заблокирован.
        /// </summary>
        [DisplayName("Заблокирован")]
        public bool Blocked { get; set; }

        /// <summary>
        /// Ограничение пароля.
        /// </summary>
        [DisplayName("Ограничения пароля")]
        public bool LimitPassword { get; set; }
    }
}
