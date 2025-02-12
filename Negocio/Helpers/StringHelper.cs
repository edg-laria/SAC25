using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Security.Cryptography;
namespace Negocio.Helpers
{
    public static class StringHelper
    {
        public static string ObtenerMD5(string Source)
        {
            byte[] Bytes;
            StringBuilder sb = new StringBuilder();

            // Check for empty string.
            if (string.IsNullOrEmpty(Source))
                throw new ArgumentNullException();

            // Get bytes from string.
            Bytes = Encoding.Default.GetBytes(Source);

            // Get md5 hash
            Bytes = MD5.Create().ComputeHash(Bytes);

            // Loop though the byte array and convert each byte to hex.
            for (int x = 0; x <= Bytes.Length - 1; x++)
                sb.Append(Bytes[x].ToString("x2"));

            // Return md5 hash.
            return sb.ToString();
        }


        public static string GetDescriptionByEnum(Enum EnumConstant)
        {
            FieldInfo fi = EnumConstant.GetType().GetField(EnumConstant.ToString());
            DescriptionAttribute[] attr = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attr.Length > 0)
                return attr[0].Description;
            else
                return EnumConstant.ToString();
        }
    }
}