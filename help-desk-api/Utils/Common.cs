using Amazon.Runtime;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using UAParser;

namespace Utils
{
    public class Common
    {
        #region Encrption Function
        public static string EncryptText(string strText)
        {
            strText = strText.Trim();
            return Encrypt(strText, "&%#@?,:*").Replace("+", "|").Replace("/", "!");
        }
        private static string Encrypt(string strText, string strEncrKey)
        {
            Byte[] bykey = new Byte[8];
            Byte[] IV = new Byte[8];

            try
            {
                bykey = ASCIIEncoding.ASCII.GetBytes(strEncrKey);
                IV = ASCIIEncoding.ASCII.GetBytes(strEncrKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();

                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        #endregion

        #region  Decrypt the text
        public static string DecryptText(string strText)
        {
            strText = strText.Trim();
            strText = strText.Replace("|", "+");
            strText = strText.Replace("!", "/");
            strText = strText.Replace(" ", "+").Trim();
            return Decrypt(strText, "&%#@?,:*");
        }

        private static string Decrypt(string strText, string strDecrKey)
        {
            Byte[] bykey = new Byte[8];
            Byte[] IV = new Byte[8];
            Byte[] inputByteArray = new Byte[strText.Length];

            try
            {
                bykey = ASCIIEncoding.ASCII.GetBytes(strDecrKey);
                IV = ASCIIEncoding.ASCII.GetBytes(strDecrKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(bykey, IV), CryptoStreamMode.Write);

                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8;
                System.Text.Encoding encoding;
                encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch
            {
                return null;
            }
        }
        #endregion


        public static Tuple<ClientInfo, IPAddress> GetBrowserIpInformation(HttpContext httpContext,
HttpRequest request)
        {


            //var name = Dns.GetHostName(); // get container id
            //IPAddress ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
            var userAgent = Convert.ToString(httpContext.Request.Headers["User-Agent"]);


            var uaParser = Parser.GetDefault();
            ClientInfo client = uaParser.Parse(userAgent);
            IPAddress ip = new IPAddress(0);
            var headers = httpContext.Request.Headers.ToList();

            if (headers.Exists((kvp) => kvp.Key == "X-Forwarded-For"))
            {
                // when running behind a load balancer you can expect this header
                var header = headers.First((kvp) => kvp.Key == "X-Forwarded-For").Value.ToString();
                var allIps = header.Split(',');
                if (allIps.Length > 0)
                {
                    ip = allIps[0].IndexOf(':') > 0 ? IPAddress.Parse(allIps[0].Remove(allIps[0].IndexOf(':'))) : IPAddress.Parse(allIps[0]);
                }
                //Console.WriteLine("Header Info: "+header);
                // in case the IP contains a port, remove ':' and everything after
                //ip = header.IndexOf(':') > 0 ? IPAddress.Parse(header.Remove(header.IndexOf(':'))) : IPAddress.Parse(header);
            }
            else
            {
                //CustomLog.LogInformation("*** else condition ** ");
                // this will always have a value (running locally in development won't have the header)
                ip = request.HttpContext.Connection.RemoteIpAddress;
            }
            return Tuple.Create(client, ip);
        }
        public static string ReplaceTextWithContent(string text, Dictionary<string, string> replaceContent)
        {
            if (text == null) return "";
            foreach (var key in replaceContent.Keys)
            {
                text = text.Replace(key, replaceContent[key]);
            }
            return text;
        }
    }
}
