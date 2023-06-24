using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace BaziMarket2
{
    public static class HashPass
    {
        public static string GetSha256(string Password)
        {
            var message = Encoding.ASCII.GetBytes(Password);
            SHA256Managed sHA256 = new SHA256Managed();
            string hex = "";
            var hashValue = sHA256.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
    }
}