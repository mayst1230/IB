using System;
using System.Text;
using System.Windows.Forms;

namespace Crypto
{
    public partial class BaseForm : Form
    {
        byte[] encrByteFile, byteKey, decrByteFile;

        public BaseForm()
        {
            InitializeComponent();
        }

        private void fileEncryptButton_Click(object sender, EventArgs e)
        {
            if (decrTextBox.Text == "")
                MessageBox.Show("Введите данные для шифрования.");
            else
            {
                byte[] btFile = Encoding.UTF8.GetBytes(decrTextBox.Text);

                if ((byteKey == null) || (byteKey.Length != 32))
                    MessageBox.Show("Введдите 256-битный ключ.");
                else
                {
                    Crypto crypto = new Crypto();
                    crypto.SetKey(byteKey);
                    crypto.SetReplaceTable(ReplacementTab.Table0);
                    encrByteFile = crypto.SimpleEncoding(btFile);
                    var imitationBytes = crypto.ImitationPaste(btFile);
                    var binaryTest = "";
                    var binaryImit = "";

                    foreach (var byteT in encrByteFile)
                    {
                        binaryTest += convert(byteT);
                    }

                    foreach (var byteI in imitationBytes)
                    {
                        binaryImit += convert(byteI);
                    }

                    encrTextBox.Text = binaryTest;
                    textBoxImitation.Text = binaryImit;
                }
            }            
        }

        public static String convert(byte b)
        {
            StringBuilder str = new StringBuilder(8);
            int[] bl = new int[8];

            for (int i = 0; i < bl.Length; i++)
            {
                bl[bl.Length - 1 - i] = ((b & (1 << i)) != 0) ? 1 : 0;
            }

            foreach (int num in bl) str.Append(num);

            return str.ToString();
        }

        private void fileDecryptButton_Click(object sender, EventArgs e)
        {
            if ((encrTextBox.Text == "") && (decrTextBox.Text == ""))
                MessageBox.Show("Введите данные для расшифрования.");
            else
            {
                byte[] btFile = encrByteFile;

                if (btFile != null)
                {

                    if ((byteKey == null) || (byteKey.Length != 32))
                        MessageBox.Show("Введдите 256-битный ключ.");
                    else
                    {
                        Crypto crypto= new Crypto();
                        crypto.SetKey(byteKey);
                        crypto.SetReplaceTable(ReplacementTab.Table0);
                        decrByteFile = crypto.SimpleDecoding(btFile);
                        encrTextBox.Text = Encoding.UTF8.GetString(decrByteFile);
                    }
                }
            }
        }

        private void loadKeyButton_Click(object sender, EventArgs e)
        {
            FileWork fw = new FileWork();

            openFileDialog1.ShowDialog();
            string key = openFileDialog1.FileName;
            byteKey =  fw.FileToByte(key);

            if(byteKey != null)
                keyTextBox.Text = Encoding.UTF8.GetString(byteKey);
        }

        private void fileLoadButton_Click(object sender, EventArgs e)
        {
            FileWork fw = new FileWork();

            openFileDialog1.ShowDialog();
            string file = openFileDialog1.FileName;
            decrByteFile = fw.FileToByte(file);
            
            if(decrByteFile != null)
                decrTextBox.Text = Encoding.UTF8.GetString(decrByteFile);
        }

        private void fileSaveButton_Click(object sender, EventArgs e)
        {
            FileWork fw = new FileWork();

            saveFileDialog1.ShowDialog();
            string file = saveFileDialog1.FileName;
            fw.WriteToFile(file, Encoding.UTF8.GetBytes(encrTextBox.Text));
        }

        private void keyGenerateButton_Click(object sender, EventArgs e)
        {
            KeyGenerator kg = new KeyGenerator(KeyGenerator.Type.Key);
            kg.ShowDialog();
        }
    }
}
