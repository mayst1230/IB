using Laba1.Models;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace Laba1.Services
{
    public class FileService
    {
        private const string USERS_FILE = "users.dat";

        public List<User> LoadUsersFile(string passPhrase)
        {
            List<User> list = new List<User>();
            if (File.Exists(USERS_FILE))
            {
                byte[] encryptedBytes = File.ReadAllBytes(USERS_FILE);
                byte[] bytes = DecodeData(encryptedBytes, passPhrase);
                list = DeserializeUserList(bytes);
            }
            else
            {
                // Файла с учетными данными нет. Создаём нового администратора.
                User admin = new User
                {
                    Login = UserService.ADMIN
                };
                list.Add(admin);
            }
            return list;
        }

        public void SaveUsersFile(List<User> list, string passPhrase)
        {
            byte[] bytes = SerializeUserList(list);
            byte[] encryptedBytes = EncodeData(bytes, passPhrase);
            File.WriteAllBytes(USERS_FILE, encryptedBytes);
        }

        private byte[] SerializeUserList(List<User> list)
        {
            XmlSerializer bf = new XmlSerializer(list.GetType());
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms, Encoding.UTF8);
            bf.Serialize(sw, list);
            sw.Flush();
            return ms.ToArray();
        }

        private List<User> DeserializeUserList(byte[] bytes)
        {
            XmlSerializer bf = new XmlSerializer(new List<User>().GetType());
            MemoryStream ms = new MemoryStream(bytes);
            return (List<User>)bf.Deserialize(ms);
        }

        private byte[] EncodeData(byte[] bytes, string passPhrase)
        {
            DES provider = CreateDESProvider(passPhrase);
            ICryptoTransform transform = provider.CreateEncryptor();
            MemoryStream encryptedStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(encryptedStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            return encryptedStream.ToArray();
        }

        private byte[] DecodeData(byte[] encryptedBytes, string passPhrase)
        {
            DES provider = CreateDESProvider(passPhrase);
            ICryptoTransform transform = provider.CreateDecryptor();

            MemoryStream decryptedStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(decryptedStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            cryptoStream.FlushFinalBlock();
            return decryptedStream.ToArray();
        }

        private DES CreateDESProvider(string passPhrase)
        {
            // Генерируем ключ из пароля при помощи алгоритма PBKDF2
            byte[] salt = new byte[] { 134, 77, 21, 3, 83, 52, 16, 117 };
            Rfc2898DeriveBytes keyGenerator = new Rfc2898DeriveBytes(passPhrase, salt, 1000);
            byte[] key = keyGenerator.GetBytes(8);
            DES provider = DES.Create();
            //.NET не поддерживает OFB и CFB. Поэтому используется ECB
            provider.Mode = CipherMode.ECB;
            provider.Key = key;
            return provider;
        }
    }
}
