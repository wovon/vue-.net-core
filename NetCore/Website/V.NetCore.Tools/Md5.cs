﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace V.NetCore.Tools
{
    public class Md5
    {
        /// <summary>
        /// MD5加密字符串
        /// </summary>
        public static string GetMD5String(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();

            var builder = new StringBuilder();
            for (int i = 0; i < md5data.Length - 1; i++)
            {
                builder.Append(md5data[i].ToString("X2"));
            }
            return builder.ToString();
        }
        
    }
}
