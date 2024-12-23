using NetTaste;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace ManagementSystem.Common.Helper
{
    public class Utils
    {
        /// <summary>
        /// 验证是否是正整数
        /// </summary>
        public static bool IsPositiveInteger(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            string pat = @"^[0-9]\d*$";
            Regex r = new Regex(pat, RegexOptions.Compiled);
            Match m = r.Match(value);
            if (!m.Success)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 计算时间天数差
        /// </summary>
        public static double DiffDays(DateTime startTime, DateTime endTime)
        {
            TimeSpan daysSpan = new TimeSpan(endTime.Ticks - startTime.Ticks);
            return daysSpan.TotalDays;
        }

        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="key">加密key</param>
        /// <param name="str">要加密的字符串</param>
        public static string ToEncryptString(string key, string str)
        {
            try
            {
                //将密钥字符串转换为字节序列
                var P_byte_key = Encoding.Unicode.GetBytes(key);
                //将字符串转换为字节序列
                var P_byte_data = Encoding.Unicode.GetBytes(str);
                //创建内存流对象
                MemoryStream mStream = new MemoryStream();
                {
                    using (CryptoStream P_CryptStream_Stream = new CryptoStream(mStream, new DESCryptoServiceProvider().CreateEncryptor(P_byte_key, P_byte_key), CryptoStreamMode.Write))
                    {
                        //向加密流中写入字节序列
                        P_CryptStream_Stream.Write(P_byte_data, 0, P_byte_data.Length);
                        //将数据压入基础流
                        P_CryptStream_Stream.FlushFinalBlock();
                        //从内存流中获取字节序列
                        var res = mStream.ToArray();
                        P_CryptStream_Stream.Dispose();
                        mStream.Dispose();
                        return Convert.ToBase64String(res);
                    }
                }
            }
            catch (CryptographicException ce)
            {
                throw new Exception(ce.Message);
            }
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="key">加密key</param>
        /// <param name="str">要解密的字符串</param>
        public static string ToDecryptString(string key, string str)
        {
            try
            {
                //将密钥字符串转换为字节序列
                var P_byte_key = Encoding.Unicode.GetBytes(key);
                //将加密后的字符串转换为字节序列
                var P_byte_data = Convert.FromBase64String(str);
                //创建内存流对象并写入数据,创建加密流对象
                CryptoStream cStream = new CryptoStream(new MemoryStream(P_byte_data), new DESCryptoServiceProvider().CreateDecryptor(P_byte_key, P_byte_key), CryptoStreamMode.Read);
                //创建字节序列对象
                var tempDate = new byte[200];
                //创建内存流对象
                MemoryStream mStream = new MemoryStream();
                //创建记数器
                int i = 0;
                //使用while循环得到解密数据
                while ((i = cStream.Read(tempDate, 0, tempDate.Length)) > 0)
                {
                    //将解密后的数据放入内存流
                    mStream.Write(tempDate, 0, i);
                }
                var res = Encoding.Unicode.GetString(mStream.ToArray());
                mStream.Dispose();
                cStream.Dispose();
                return res;
            }
            catch (CryptographicException ce)
            {
                throw new Exception(ce.Message);
            }
        }

        /// <summary>
        /// 取MD5
        /// </summary>
        /// <param name="value">要加密的字符串</param>
        public static string GetMD5Value(string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] targetData = md5.ComputeHash(Encoding.Unicode.GetBytes(value));
            string resString = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                resString += targetData[i].ToString("x");
            }
            return resString;
        }

        /// <summary>
        /// 取数字
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GetNum(string md5, int len)
        {
            Regex regex = new Regex(@"\d");
            MatchCollection listMatch = regex.Matches(md5);
            string str = "";
            for (int i = 0; i < len; i++)
            {
                str += listMatch[i].Value;
            }
            while (str.Length < len)
            {
                //不足补0
                str += "0";
            }
            return str;
        }

        /// <summary>
        /// 获取网络时间
        /// </summary>
        public static DateTime GetInternetTime()
        {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 1000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;

                foreach (var h in headerCollection.AllKeys)
                {
                    if (h == "Date")
                    {
                        datetime = headerCollection[h];

                        var dt = DateTime.Parse(datetime);
                        return dt;
                    }
                }
                return new DateTime();
            }
            catch (Exception) { return new DateTime(); }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }

        /// <summary>
        /// 获取当前时间（如果有网络则读取网络时间，否则获取本机时间）
        /// </summary>
        /// <returns></returns>
        public static DateTime GetDateTimeNow()
        {
            DateTime nowTime = DateTime.Now;
            try
            {
                nowTime = Utils.GetInternetTime();
                if (nowTime.ToString() == "0001/1/1 0:00:00")
                {
                    nowTime = DateTime.Now;
                }
            }
            catch
            {
                throw;
            }
            return nowTime;
        }

        /// <summary>
        /// 获取行政区划代码
        /// </summary>
        /// <param name="areaArr">前端传入的行政区划数组</param>
        /// <returns></returns>
        public static string GetAreaCode(string areaArrStr)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(areaArrStr) && areaArrStr.Contains(","))
            {
                string[] areaArr = areaArrStr.Split(',');
                result = areaArr[areaArr.Length - 1];
            }
            return result;
        }

        /// <summary>
        /// 获取行政区划代码数组
        /// </summary>
        /// <param name="areaArr">前端传入的行政区划</param>
        /// <returns></returns>
        public static string GetAreaCodeArray(string regionCode)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(regionCode))
            {
                // 省市区乡镇编码
                if (regionCode.Length == 9)
                {
                    string provinceCode = regionCode.Substring(0, 2);// 获取省编码
                    string cityCode = regionCode.Substring(0, 4);// 获取市编码
                    string districtCode = regionCode.Substring(0, 6); // 获取区编码
                    result = $"{provinceCode},{cityCode},{districtCode},{regionCode}";
                }
                // 省市区编码
                if (regionCode.Length == 6)
                {
                    string provinceCode = regionCode.Substring(0, 2);// 获取省编码
                    string cityCode = regionCode.Substring(0, 4);// 获取市编码         
                    result = $"{provinceCode},{cityCode},,{regionCode}";
                }
                // 省市编码
                if (regionCode.Length == 4)
                {
                    string provinceCode = regionCode.Substring(0, 2); // 获取省编码
                    result = $"{provinceCode},{regionCode}";
                }
            }
            return result;
        }

        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="birthDate">出生日期</param>
        /// <returns></returns>
        public static int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            int days = today.Day - birthDate.Day;
            if (days < 0)
            {
                // 如果今天的天数小于出生日的天数，需要从今天的天数中加上一个月的天数，然后减去1年
                days += DateTime.DaysInMonth(today.Year, today.Month);
                if (birthDate.Month == today.Month)
                {
                    age--;
                }
                else
                {
                    age--;
                    today = today.AddMonths(-1);
                    days += DateTime.DaysInMonth(today.Year, today.Month);
                }
            }
            if (birthDate > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
        /// <summary>
        /// 将图片转换为base64字符串
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }

        /// <summary>
        /// 将实体对象转换成XML
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="obj">实体对象</param>
        public static string XmlSerialize<T>(T obj)
        {
            try
            {
                using (StringWriter sw = new StringWriter())
                {
                    Type t = obj.GetType();
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(sw, obj, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
                    sw.Close();
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }

        /// <summary>
        /// 将对象转换成XML
        /// </summary>
        /// <param name="xml">xml字符串</param>
        /// <returns></returns>
        public static string ObjectToXml<T>(T obj)
        {
            StringBuilder xmlBuilder = new StringBuilder();
            Type type = obj.GetType();            // 获取对象的类型
            // 遍历所有属性
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                // 为每个属性添加XML元素
                xmlBuilder.Append("<" + property.Name + ">" + property.GetValue(obj, null) + "</" + property.Name + ">");
            }
            return xmlBuilder.ToString();
        }
    }
}
