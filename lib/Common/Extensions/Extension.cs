using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class Extension
    {
        public static bool IsValidJson(string strInput)
        {
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static string GetIpAddress()
        {
            string localIP = Dns.GetHostEntry(Dns.GetHostName())
                           .AddressList
                           .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork)?
                           .ToString();
            return localIP;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }


        public static string GenarateUrlQrLink(string value)
        {
            string url = Environment.GetEnvironmentVariable("URL_Mobile");
            return string.Join("", url, "?token=", Base64Encode(value));
        }


        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static bool IsEmailValidator(string email)
        {
            try
            {
                const string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(emailPattern);
                return regex.IsMatch(email); 
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool CheckIdCardNo(string idCardNo, bool allowNull = false)
        {
            if (string.IsNullOrEmpty(idCardNo))
            {
                return allowNull;
            }
            else if (idCardNo.Length != 13)
            {
                return false;
            }
            else
            {
                int sum = 0;
                for (int i = 0; i < 12; i++)
                {
                    sum += (int)char.GetNumericValue(idCardNo[i]) * (13 - i);
                }

                if ((11 - (sum % 11)) % 10 != (int)char.GetNumericValue(idCardNo[12]))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


    }
}
