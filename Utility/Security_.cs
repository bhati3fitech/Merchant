
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Security_
    {

        public static string encrypt(string encryptString)
        {
            string EncryptionKey = "UPBA@$1209846AASUI908ADMIN";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "UPBA@$1209846AASUI908ADMIN";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string encryptUserId(string encryptString)
        {
            string EncryptionKey = "UPBA@$1209846AASUI908ADMIN";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray()).Replace('/', '_').Replace('+', '-');
                }
            }
            return encryptString;
            //return encryptString.Replace('/', '_');
        }

        public static string DecryptUserId(string cipherText)
        {
            string EncryptionKey = "UPBA@$1209846AASUI908ADMIN";
            //cipherText = cipherText.Replace(" ", "+").Replace('_', '/');
            cipherText = cipherText.Replace(" ", "%2B").Replace('_', '/').Replace('-', '+');
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static string GenerateCode()
        {
            Random random = new Random();
            string UPBAPrefix = "ConUPBA";

            // Get the current date and time
            DateTime now = DateTime.Now;

            // Extract date, hour, minute, and second components
            string dateComponent = now.ToString("yyyyMMdd");
            string hourComponent = now.ToString("HH");
            string minuteComponent = now.ToString("mm");
            string secondComponent = now.ToString("ss");

            // Generate a random 4-digit number for the remaining part of the code
            string randomPart = random.Next(1000, 10000).ToString();

            // Combine all components to form the 16-digit code
            string code = UPBAPrefix + dateComponent + hourComponent + minuteComponent + secondComponent + randomPart;

            return code;
        }

        //Used to Authenticate User and Load the same in to the Current Session.
        public static bool IsAuthenticatedUser(DataSet dsUser, string strGivenPassword)
        {
            string strDbPassword = dsUser.Tables[0].Rows[0]["UserPassword"].ToString();
            string strDbPasswordKey = dsUser.Tables[0].Rows[0]["PasswordKey"].ToString();

            strDbPassword = Decrypt(strDbPassword, strDbPasswordKey);
            if (strDbPassword.CompareTo(strGivenPassword) != 0)
            {
                return false;
            }

            return true;
        }
        public static string Decrypt(string encryptedText, string decryptionKey)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(decryptionKey);
                aesAlg.IV = new byte[16]; // Example assumes AES uses a 16-byte IV

                using (MemoryStream msDecrypt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV), CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The method create a Base64 encoded string from a normal string.
        /// </summary>
        /// <param name="toEncode">toEncode</param>
        /// <returns>The Base64 encoded string.</returns>
        public static string Base64Encode(string toEncode)
        {
            byte[] encData_byte = new byte[toEncode.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(toEncode);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        /// <summary>
        /// The method to decode your Base64 string.
        /// </summary>
        /// <param name="encodedData">encodedData</param>
        /// <returns>A String containing the results of decoding the specified sequence</returns>
        public static string Base64Decode(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
        public static string GetIPAddress()
        {
            string IPAddress = "";
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }

        //public static string GeneratePrivateKey(string email, string phoneNumber)
        //{
        //    // Ensure email has at least 5 characters
        //    string emailPart = email.Length >= 5 ? email.Substring(0, 5) : email.PadRight(5, 'X');

        //    // Ensure phone number has at least 5 digits
        //    string phonePart = phoneNumber.Length >= 5 ? phoneNumber.Substring(0, 5) : phoneNumber.PadRight(5, '0');

        //    // Generate a random 4-digit number
        //    Random random = new Random();
        //    string randomPart = random.Next(1000, 9999).ToString();

        //    // Combine all parts to create PrivateKey
        //    return emailPart + phonePart + randomPart;
        //}


        //public static string GenerateMerchantId()
        //{
        //    byte[] randomBytes = new byte[5]; // 5 bytes = 10 hexadecimal characters
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        rng.GetBytes(randomBytes);
        //    }

        //    string randomHex = BitConverter.ToString(randomBytes).Replace("-", "").ToUpper();

        //    return $"UPBPM-{randomHex}";
        //}
        public static string GeneratePrivateKey()
        {
            return Guid.NewGuid().ToString("N").ToUpper(); // 32-character unique key
        }

        public static string GenerateMerchantId()
        {
            string guidPart = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper(); // Take first 10 characters
            return $"UPBPM-{guidPart}";
        }



    }
}
