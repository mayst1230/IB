using System;

namespace Crypto
{
    public class Crypto
    {
        /// <summary>
        /// Ключ
        /// </summary>
        private UInt32[] key;

        /// <summary>
        /// Таблица замен
        /// </summary>
        private byte[,] replaceTable;

        /// <summary>
        /// Преобразование из UInt64 в 8 байт
        /// </summary>
        public static byte[] Get8BytesFromUInt64(UInt64 s)
        {
            var result = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                int shift = (56 - 8 * i);
                result[i] = (byte)(s >> shift);
                s &= (UInt64.MaxValue - ((UInt64)0xff << shift));
            }
            return result;
        }

        /// <summary>
        /// Преобразование из 8 байт в UInt64
        /// </summary>
        /// <param name="s">Массив байт</param>
        /// <param name="startIndex">Начальный индекс в массиве</param>
        public static UInt64 GetUint64From8Bytes(byte[] s, int startIndex)
        {
            UInt64 result = 0;
            for (int i = startIndex; i < startIndex + 8; i++)
            {
                int shift = (56 - 8 * i);
                result += ((UInt64)s[i] << shift);
            }
            return result;
        }


        /// <summary>
        /// Формирование имитовставки
        /// </summary>
        /// <param name="plainData">Блок открытых данных, размером кратным 8</param>
        /// <returns>Имитовставка.</returns>
        public byte[] ImitationPaste(byte[] plainData)
        {
            // Проверки
            if (plainData.Length % 8 != 0)
            {
                throw new ArgumentOutOfRangeException("Размер данных должен быть кратным 8!");
            }

            if (key == null)
            {
                throw new ArgumentNullException("Не задан ключ шифрования!");
            }

            if (replaceTable == null)
            {
                throw new ArgumentNullException("Не задана таблица замен!");
            }

            // Преобразование массива байт в массив uint64
            int lenght = plainData.Length / 8;
            var data = new UInt64[lenght];

            for (int i = 0; i < lenght; i++)
            {
                data[i] = GetUint64From8Bytes(plainData, 8 * i);
            }

            ulong crypt = ImitationPaste(data);

            // Преобразование массива uint64 в массив байт
            byte[] result = Get8BytesFromUInt64(crypt);

            return result;
        }

        /// <summary>
        /// Метод установки ключа - восьми 32-битовых элементов кода
        /// </summary>
        /// <param name="keyData">массив из 32 байт</param>
        public void SetKey(byte[] keyData)
        {
            if (keyData.Length != 32)
            {
                throw new ArgumentOutOfRangeException("Длина массива для ключа должна быть равна 32!");
            }

            key = new UInt32[8];
            for (int i = 0; i < keyData.Length; i += 4)
            {
                UInt32 k = keyData[i];
                k = k << 8;
                k = k | keyData[i + 1];
                k = k << 8;
                k = k | keyData[i + 2];
                k = k << 8;
                k = k | keyData[i + 3];
                key[i / 4] = k;
            }
        }

        /// <summary>
        /// Метод установки таблицы замен - матрицы размера 8 х 16, содержащей 4-битовые элементы
        /// </summary>
        /// <param name="replaceTableData">Таблица замен</param>
        public void SetReplaceTable(byte[] replaceTableData)
        {
            if (replaceTableData.Length != 128)
            {
                throw new ArgumentOutOfRangeException("Длина массива для ключа должна быть равна 128!");
            }

            replaceTable = new byte[8, 16];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    int index = i * 8 + j;
                    byte rtd = replaceTableData[index];
                    replaceTable[i, j * 2] = (byte)(rtd >> 4);
                    replaceTable[i, j * 2 + 1] = (byte)(rtd & 0x0F);
                }
            }
        }

        /// <summary>
        /// Расшифрование простой заменой
        /// </summary>
        /// <param name="plainData">блок открытых данных, размером кратным 8</param>
        /// <returns>Результат расшифрования</returns>
        public byte[] SimpleDecoding(byte[] plainData)
        {
            // Проверки
            if (plainData.Length % 8 != 0)
            {
                throw new ArgumentOutOfRangeException("Размер данных должен быть кратным 8!");
            }

            if (key == null)
            {
                throw new ArgumentNullException("Не задан ключ шифрования!");
            }

            if (replaceTable == null)
            {
                throw new ArgumentNullException("Не задана таблица замен!");
            }

            // Преобразование массива байт в массив uint64
            int lenght = plainData.Length / 8;
            var data = new UInt64[lenght];

            for (int i = 0; i < lenght; i++)
            {
                data[i] = GetUint64From8Bytes(plainData, 8 * i);
            }

            // Расшифрование
            ulong[] crypt = SimpleDecoding(data);

            // Преобразование массива uint64 в массив байт
            var result = new byte[plainData.Length];

            for (int i = 0; i < crypt.Length; i++)
            {
                byte[] bytes = Get8BytesFromUInt64(crypt[i]);
                for (int j = 0; j < 8; j++)
                {
                    result[i * 8 + j] = bytes[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Шифрование простой заменой
        /// </summary>
        /// <param name="plainData">блок открытых данных, размером кратным 8</param>
        /// <returns>Результат шифрования</returns>
        public byte[] SimpleEncoding(byte[] plainData)
        {
            // Проверки
            if (plainData.Length % 8 != 0)
            {
                throw new ArgumentOutOfRangeException("Размер данных должен быть кратным 8!");
            }

            if (key == null)
            {
                throw new ArgumentNullException("Не задан ключ шифрования!");
            }

            if (replaceTable == null)
            {
                throw new ArgumentNullException("Не задана таблица замен!");
            }

            // Преобразование массива байт в массив uint64
            int lenght = plainData.Length / 8;

            var data = new UInt64[lenght];

            for (int i = 0; i < lenght; i++)
            {
                data[i] = GetUint64From8Bytes(plainData, 8 * i);
            }

            // Шифрование
            ulong[] crypt = SimpleEncoding(data);

            // Преобразование массива uint64 в массив байт
            var result = new byte[plainData.Length];

            for (int i = 0; i < crypt.Length; i++)
            {
                byte[] bytes = Get8BytesFromUInt64(crypt[i]);
                for (int j = 0; j < 8; j++)
                {
                    result[i * 8 + j] = bytes[j];
                }
            }

            return result;
        }

        /// <summary>
        /// Цикл выработки имитовставки
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>Имитовставка</returns>
        private UInt64 BaseCycle32I(UInt64 data)
        {
            UInt64 result = data;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    result = MainCryptoStep(result, key[j]);
                }
            }

            return result;
        }

        /// <summary>
        /// Цикл расшифрования
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>Результат расшифрования</returns>
        private UInt64 BaseCycle32R(UInt64 data)
        {
            UInt64 result = data;

            for (int j = 0; j < 8; j++)
            {
                result = MainCryptoStep(result, key[j]);
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 7; j >= 0; j--)
                {
                    result = MainCryptoStep(result, key[j]);
                }
            }

            result = ((result & UInt32.MaxValue) << 32) | (result >> 32);

            return result;
        }

        /// <summary>
        /// Цикл шифрования
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>Результат шифрования</returns>
        private UInt64 BaseCycle32Z(UInt64 data)
        {
            UInt64 result = data;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    result = MainCryptoStep(result, key[j]);
                }
            }

            for (int j = 7; j >= 0; j--)
            {
                result = MainCryptoStep(result, key[j]);
            }

            result = ((result & UInt32.MaxValue) << 32) | (result >> 32);

            return result;
        }

        /// <summary>
        /// Сдвиг влево на 11
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns>Результат сдвига</returns>
        private uint CycleLeftShift11(uint value)
        {
            uint result = value << 11 | value >> (32 - 11);
            return result;
        }

        /// <summary>
        /// Вставка имитовставки
        /// </summary>
        /// <param name="data">Данные</param>
        /// <returns>Имитовставка</returns>
        private UInt64 ImitationPaste(UInt64[] data)
        {
            UInt64 s = 0;
            for (int i = 0; i < data.Length; i++)
            {
                s = BaseCycle32I(s ^ data[i]);
            }
            return s;
        }

        /// <summary>
        /// Основные шаги алгоритма ГОСТ 28147-89. Имитовставка
        /// </summary>
        /// <param name="data">Данные.</param>
        /// <param name="keyPart">Часть ключа</param>
        /// <returns>Полученное значение на последнем шаге алгоритма</returns>
        private UInt64 MainCryptoStep(UInt64 data, UInt32 keyPart)
        {
            // Шаг 0 - разбивание UInt64 на два UInt32
            var n2 = (UInt32)(data >> 32);
            var n1 = (UInt32)(data & UInt32.MaxValue);

            // Шаг 1 - сложение по модулю 2^32
            UInt32 step1Value = n1 + keyPart;

            // Шаг 2 - замена
            uint step2Value = ReplaceValues(step1Value);

            // Шаг 3 - циклический сдвиг на 11 бит влево. Результат
            // предыдущего шага сдвигается циклически на 11
            // бит в сторону старших разрядов и передается на следующий шаг.
            uint step3Value = CycleLeftShift11(step2Value);

            // Шаг 4 - побитовое сложение по модулю 2
            // Значение, полученное на шаге 3, побитно складывается по модулю 2
            // со старшей половиной преобразуемого блока
            uint step4Value = step3Value ^ n2;

            // Шаг 5 - сдвиг по цепочке
            // Младшая часть преобразуемого блока сдвигается на место старшей, а на
            // ее место помещается результат выполнения предыдущего шага
            n2 = n1;
            n1 = step4Value;

            // Шаг 6 - Полученное значение преобразуемого блока возвращается как результат выполнения
            // алгоритма основного шага криптопреобразования
            UInt64 step6Value = (UInt64)n2 << 32 | n1;

            return step6Value;
        }

        /// <summary>
        /// Замена значений (2 шаг)
        /// </summary>
        /// <param name="step1Value">Значение на 1 шаге алгоритма</param>
        /// <returns>Результат замены значения</returns>
        private uint ReplaceValues(uint step1Value)
        {
            // 32 - битовое значение,
            // полученное на предыдущем шаге,
            // интерпретируется как массив из восьми 4 - битовых
            // блоков кода
            uint result = 0;
            for (int i = 0; i < 8; i++)
            {
                result <<= 4;
                int shift = 32 - 4 - 4 * i;
                uint index = (step1Value >> shift) & 0xf;
                step1Value = step1Value & (UInt32.MaxValue - ((UInt32)0xf << shift));
                result += replaceTable[7 - i, index];
            }
            return result;
        }

        /// <summary>
        /// Режим дешифрования простой заменой
        /// </summary>
        /// <param name="encryptedData">Зашифрованные данные</param>
        /// <returns>Дешифрованные данные</returns>
        private UInt64[] SimpleDecoding(UInt64[] encryptedData)
        {
            var plainText = new UInt64[encryptedData.Length];
            for (int i = 0; i < encryptedData.Length; i++)
            {
                plainText[i] = BaseCycle32R(encryptedData[i]);
            }
            return plainText;
        }

        /// <summary>
        /// Режим шифрования простой заменой
        /// </summary>
        /// <param name="plainData">Исходные данные</param>
        /// <returns>Зашифрованные данные</returns>
        private UInt64[] SimpleEncoding(UInt64[] plainData)
        {
            var result = new UInt64[plainData.Length];
            for (int i = 0; i < plainData.Length; i++)
            {
                result[i] = BaseCycle32Z(plainData[i]);
            }
            return result;
        }
    }
}
