using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace KisiselBlog.Security
{
    public class Sha1
    {
        public string encoder(string data)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();

            string hashData = Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(data)));

            return hashData;
        }
    }
}