using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;

namespace ManagementSystem.Common.Helper
{
    public class SM4Helper
    {
        private readonly static string key;
        private readonly static string ecbAlgo;
        private readonly static string cbcAlgo;
        private readonly static byte[] iv;

        static SM4Helper()
        {
            key = AppSettings.app(new string[] { "SM4Config", "Key" });
            ecbAlgo = AppSettings.app(new string[] { "SM4Config", "EcbAlgo" });
            cbcAlgo = AppSettings.app(new string[] { "SM4Config", "CbcAlgo" });
            iv = Encoding.UTF8.GetBytes(AppSettings.app(new string[] { "SM4Config", "Iv" }));//初始化向量，也可以使用GenerateRandomIv生成随机数，这里使用配置文件中的固定向量
        }
        /// <summary>
        /// SM4 ECB 加密
        /// </summary>
        /// <param name="plaintext">明文</param></param>
        /// <returns>密文</returns>

        public static string EncryptECB(string plaintext)
        {
            return PerformEncryptionDecryption(plaintext, false, true);
        }

        /// <summary>
        /// SM4 ECB 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <returns>明文</returns>
        public static string DecryptECB(string ciphertext)
        {
            return PerformEncryptionDecryption(ciphertext, false, false);
        }
        /// <summary>
        /// SM4 CBC 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <returns>密文</returns>

        public static string EncryptCBC(string plaintext)
        {
            return PerformEncryptionDecryption(plaintext, true, true);
        }

        /// <summary>
        /// SM4 CBC 解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <returns>明文</returns>

        public static string DecryptCBC(string ciphertext)
        {
            return PerformEncryptionDecryption(ciphertext, true, false);
        }
        /// <summary>
        /// SM4 加密或解密
        /// </summary>
        /// <param name="input">待加密或解密的数据</param>
        /// <param name="isCbc">是否使用 CBC 模式</param>
        /// <param name="encrypt">是否加密</param>
        /// <returns></returns>
        private static string PerformEncryptionDecryption(string input, bool isCbc, bool encrypt)
        {
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(key);
                byte[] data = encrypt ? MyPadding(Encoding.UTF8.GetBytes(input)) : Hex.Decode(input);
                CheckKeyLength(keyBytes, data);
                IBufferedCipher cipher = CipherUtilities.GetCipher(isCbc ? cbcAlgo : ecbAlgo);
                KeyParameter keyParam = ParameterUtilities.CreateKeyParameter("SM4", keyBytes);
                cipher.Init(encrypt, isCbc ? new ParametersWithIV(keyParam, iv) : keyParam);
                byte[] processed = cipher.DoFinal(data);
                return encrypt ? Hex.ToHexString(processed) : Encoding.UTF8.GetString(RemovePadding(processed));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 检查密钥长度是否合法
        /// </summary>
        /// <param name="keyBytes">密钥字节数组</param>
        /// <param name="data">待加密或解密的数据字节数组</param>
        /// <exception cref="ArgumentException">当密钥长度不为 16 时抛出</exception>
        private static void CheckKeyLength(byte[] keyBytes, byte[] data)
        {
            if (keyBytes.Length != 16)
            {
                throw new ArgumentException("错误：密钥长度应为 16 字节。");
            }
            if (data.Length % 16 != 0)
            {
                throw new ArgumentException("错误：加密时数据长度应是 16 字节的整数倍。");
            }
        }
        /// <summary>
        /// 添加填充
        /// </summary>
        /// <param name="input">加密数据</param>
        /// <returns></returns>
        private static byte[] MyPadding(byte[] input)
        {
            // 计算填充所需的字节数，使得填充后的数组长度为16的倍数
            int paddingSize = 16 - (input.Length % 16);
            // 创建一个填充数组，填充数组的长度为所需的填充字节数
            byte[] padding = new byte[paddingSize];
            // 填充数组中的每个字节都用填充大小（paddingSize）填充
            Array.Fill(padding, (byte)paddingSize);
            // 创建一个新的数组，该数组的长度为原始输入数组的长度加上填充数组的长度
            byte[] paddedInput = new byte[input.Length + paddingSize];
            // 将原始输入数组复制到新数组的前部分
            Array.Copy(input, paddedInput, input.Length);
            // 将填充数组复制到新数组的末尾部分
            Array.Copy(padding, 0, paddedInput, input.Length, padding.Length);
            return paddedInput;
        }
        /// <summary>
        /// 去除填充
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        private static byte[] RemovePadding(byte[] input)
        {
            int paddingSize = input[input.Length - 1];
            // 创建一个新的字节数组，长度为原始数组长度减去填充大小
            // 这是因为填充字节位于数组的末尾
            byte[] plain = new byte[input.Length - paddingSize];
            // 将原始数组中除去填充部分的数据复制到新数组
            // 从索引0开始复制，复制的长度是原始数组长度减去填充大小
            Array.Copy(input, 0, plain, 0, input.Length - paddingSize);
            return plain;
        }
        /// <summary>
        /// 生成随机向量
        /// </summary>
        /// <returns></returns>
        private static byte[] GenerateRandomIv()
        {
            // 创建一个长度为16的字节数组，用于存储IV
            byte[] iv = new byte[16];
            // 使用SecureRandom实例来填充iv数组的随机字节
            // SecureRandom是Bouncy Castle库中用于生成加密安全的随机数的类
            new SecureRandom().NextBytes(iv);
            return iv;
        }
    }
}
