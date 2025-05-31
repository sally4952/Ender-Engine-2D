using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EnderEngine2D.Navite
{
    /// <summary>
    /// 提供一套用于获取程序集信息的操作。
    /// </summary>
    internal static class PublicAssemblyInfo
    {
        /// <summary>
        /// 获取此程序集的标题。
        /// </summary>
        public static string Title
        {
            get
            {
                try
                {
                    var asm = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTitleAttribute))).Title;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的说明。
        /// </summary>
        public static string Description
        {
            get
            {
                try
                {
                    var asm = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute))).Description;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的生成配置。
        /// </summary>
        public static string Configuration
        {
            get
            {
                try
                {
                    var asm = ((AssemblyConfigurationAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyConfigurationAttribute))).Configuration;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的公司。
        /// </summary>
        public static string Company
        {
            get
            {
                try
                {
                    var asm = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute))).Company;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的产品名。
        /// </summary>
        public static string Product
        {
            get
            {
                try
                {
                    var asm = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute))).Product;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的版权信息。
        /// </summary>
        public static string Copyright
        {
            get
            {
                try
                {
                    var asm = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute))).Copyright;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的商标属性。
        /// </summary>
        public static string Trademark
        {
            get
            {
                try
                {
                    var asm = ((AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyTrademarkAttribute))).Trademark;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集支持的区域。
        /// </summary>
        public static string Culture
        {
            get
            {
                try
                {
                    var asm = ((AssemblyCultureAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCultureAttribute))).Culture;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的全局唯一标识符。
        /// </summary>
        public static string Guid
        {
            get
            {
                try
                {
                    var asm = ((GuidAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute))).Value;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的版本。
        /// </summary>
        public static string Version
        {
            get
            {
                try
                {
                    var asm = ((AssemblyVersionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyVersionAttribute))).Version;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的文件版本。
        /// </summary>
        public static string FileVersion
        {
            get
            {
                try
                {
                    var asm = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyFileVersionAttribute))).Version;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 获取此程序集的COM组件是否可见。
        /// </summary>
        public static bool? ComVisible
        {
            get
            {
                try
                {
                    var asm = ((ComVisibleAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(ComVisibleAttribute))).Value;
                    return asm;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
