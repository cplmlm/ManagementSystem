using System;
using System.Management;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace ManagementSystem.Common.Helper
{
    /// <summary>
    /// 设备帮助类
    /// </summary>
    public class DeviceHelper
    {
        /// <summary>
        /// 获取本机MAC地址
        /// </summary>
        public static string GetMacByNetworkInterface()
        {
            try
            {
                string macAddress = "";
                // 根据平台获取MAC地址
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (NetworkInterface ni in interfaces)
                    {
                        macAddress = BitConverter.ToString(ni.GetPhysicalAddress().GetAddressBytes());
                    }
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // Linux获取MAC地址的代码
                }
                return macAddress;
            }
            catch (Exception)
            {
                return "UnknowMacInfo";
            }
        }

        /// <summary>
        /// 取CPU序列号
        /// </summary>
        public static string GetCpuID()
        {
            try
            {
                string cpuInfo = "";
                // 根据平台获取MAC地址
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    ManagementClass mc = new ManagementClass("Win32_Processor");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                    }
                    moc.Dispose();
                    mc.Dispose();
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // Linux获取MAC地址的代码
                }
                return cpuInfo;
            }
            catch
            {
                return "UnknowCpuInfo";
            }
        }

        /// <summary>
        /// 取硬盘序列号
        /// </summary>
        public static string GetDiskID()
        {
            try
            {
                string diskID = "";
                // 根据平台获取MAC地址
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        diskID = (string)mo.Properties["Model"].Value;
                    }
                    moc.Dispose();
                    mc.Dispose();
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    // Linux获取MAC地址的代码
                }
                return diskID;
            }
            catch
            {
                return "UnknowDiskInfo";
            }
        }
    }
}
