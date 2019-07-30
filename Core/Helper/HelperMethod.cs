using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Core.Helper
{
    public class HelperMethod
    {
        #region MD5 Encrypt
        public static string encrypt(string message)
        {
            string passphrase = "Password@123";
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            //to create the object for UTF8Encoding  class
            //TO create the object for MD5CryptoServiceProvider 
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            //to convert to binary passkey
            //TO create the object for  TripleDESCryptoServiceProvider 
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;//to  pass encode key
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] encrypt_data = utf8.GetBytes(message);
            //to convert the string to utf encoding binary 
            try
            {
                //To transform the utf binary code to md5 encrypt    
                ICryptoTransform encryptor = desalg.CreateEncryptor();
                results = encryptor.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);
            }
            finally
            {
                //to clear the allocated memory
                desalg.Clear();
                md5.Clear();
            }
            //to convert to 64 bit string from converted md5 algorithm binary code
            return Convert.ToBase64String(results);
        }

        public static string decrypt(string message)
        {
            string passphrase = "Password@123";
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] decrypt_data = Convert.FromBase64String(message);
            try
            {
                //To transform the utf binary code to md5 decrypt
                ICryptoTransform decryptor = desalg.CreateDecryptor();
                results = decryptor.TransformFinalBlock(decrypt_data, 0, decrypt_data.Length);
            }
            finally
            {
                desalg.Clear();
                md5.Clear();
            }
            //TO convert decrypted binery code to string
            return utf8.GetString(results);
        }
        #endregion

        #region RSA Encrypt
        public static string RSAencrypt(string inputString)
        {
            RSAParam param = GetRSAParamEncrypt();
            // do Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider =
                                          new RSACryptoServiceProvider(param.dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(param.xmlString);
            int keySize = param.dwKeySize / 8;
            byte[] bytes = Encoding.UTF32.GetBytes(inputString);
            // The hash function in use by the .NET RSACryptoServiceProvider here 
            // is SHA1
            // int maxLength = ( keySize ) - 2 - 
            //              ( 2 * SHA1.Create().ComputeHash( rawBytes ).Length );
            int maxLength = keySize - 42;
            int dataLength = bytes.Length;
            int iterations = dataLength / maxLength;
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i <= iterations; i++)
            {
                byte[] tempBytes = new byte[
                        (dataLength - maxLength * i > maxLength) ? maxLength :
                                                      dataLength - maxLength * i];
                Buffer.BlockCopy(bytes, maxLength * i, tempBytes, 0,
                                  tempBytes.Length);
                byte[] encryptedBytes = rsaCryptoServiceProvider.Encrypt(tempBytes,
                                                                          true);
                // Be aware the RSACryptoServiceProvider reverses the order of 
                // encrypted bytes. It does this after encryption and before 
                // decryption. If you do not require compatibility with Microsoft 
                // Cryptographic API (CAPI) and/or other vendors. Comment out the 
                // next line and the corresponding one in the DecryptString function.
                Array.Reverse(encryptedBytes);
                // Why convert to base 64?
                // Because it is the largest power-of-two base printable using only 
                // ASCII characters
                stringBuilder.Append(Convert.ToBase64String(encryptedBytes));
            }
            return stringBuilder.ToString();
        }

        public static string RSAdecrypt(string inputString)
        {
            RSAParam param = GetRSAParamDecrypt();
            // do Add Proper Exception Handlers
            RSACryptoServiceProvider rsaCryptoServiceProvider
                                     = new RSACryptoServiceProvider(param.dwKeySize);
            rsaCryptoServiceProvider.FromXmlString(param.xmlString);
            int base64BlockSize = ((param.dwKeySize / 8) % 3 != 0) ?
              (((param.dwKeySize / 8) / 3) * 4) + 4 : ((param.dwKeySize / 8) / 3) * 4;
            int iterations = inputString.Length / base64BlockSize;
            ArrayList arrayList = new ArrayList();
            for (int i = 0; i < iterations; i++)
            {
                byte[] encryptedBytes = Convert.FromBase64String(
                     inputString.Substring(base64BlockSize * i, base64BlockSize));
                // Be aware the RSACryptoServiceProvider reverses the order of 
                // encrypted bytes after encryption and before decryption.
                // If you do not require compatibility with Microsoft Cryptographic 
                // API (CAPI) and/or other vendors.
                // Comment out the next line and the corresponding one in the 
                // EncryptString function.
                Array.Reverse(encryptedBytes);
                arrayList.AddRange(rsaCryptoServiceProvider.Decrypt(
                                    encryptedBytes, true));
            }
            return Encoding.UTF32.GetString(arrayList.ToArray(
                                      Type.GetType("System.Byte")) as byte[]);
        }
        private static RSAParam GetRSAParamEncrypt()
        {
            return new RSAParam
            {
                dwKeySize = 1024,
                xmlString = "<RSAKeyValue><Modulus>3aJL3AXLXb7kUewwhuTHeIrqi6zzXo2JiLsDJkTu3qi+L7lXLyZb10EMLMVBvgtcX8p4zQ8nGebwTTWrlv10JgawSA1tt2MPuB43pCUODCeQ1Gq1qhuNFqz+axb4H6dBpKDyNhVmJEioiCqFZvRk12tHNiU4BgB4qbybAJ87S9E=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
            };
        }
        private static RSAParam GetRSAParamDecrypt()
        {
            return new RSAParam
            {
                dwKeySize = 1024,
                xmlString = "<RSAKeyValue><Modulus>3aJL3AXLXb7kUewwhuTHeIrqi6zzXo2JiLsDJkTu3qi+L7lXLyZb10EMLMVBvgtcX8p4zQ8nGebwTTWrlv10JgawSA1tt2MPuB43pCUODCeQ1Gq1qhuNFqz+axb4H6dBpKDyNhVmJEioiCqFZvRk12tHNiU4BgB4qbybAJ87S9E=</Modulus><Exponent>AQAB</Exponent><P>6shhJJTWGWPtnNo1r/8mlsqmdbA7fFpigs32II458FZ8BEbZR+SV0zTKZgBM089l8uNMzVigH1IkQDbVjxE8Cw==</P><Q>8am5gEgdpm3KwTxXzhJmYCN8mS3aVAuKT6TMdKyOMd9sEau5RrtDJW/f0zkR98ZpHclYYguqN2N+aMCUi3rlEw==</Q><DP>TMSqZuC3xOOUzUXYaiy+vG2U0kSGntma/nRD908qCLjuoiNcZpKKnt3TFFkixds51ZqXAUnqSXN9YPXNDJOxEw==</DP><DQ>GP5Oux+6QU41nAqboipxABxrdIQzgmG3KkmHA8Ic6reKu8Eg5lnTtfl+EqBiZVfV40jBpVJDhnr5xee09T/+lQ==</DQ><InverseQ>dCAtjB6yylky/kpLly9ezJnUoGGRqY7ouYouRh1E2vRGEJp6bi0v4awooPgDqFog8k9F3Sr1cp4thzro4TXYRw==</InverseQ><D>FIv5k7yRyETB2U/ChpmVCHl7Gax/T2e4YA2o/WuXPYXP7pAJzhw0mCBhUZE5RrYPlUMpryZVeKhD5NSHQ33caNyQW8NS5bWE3OiMk2izBdOWHEfTFN4ME8GSqvIu1yXwGvBD7NQPdBHx7ynHOQZs0xhwiznPAZnRCR8/+NyfefU=</D></RSAKeyValue>"
            };
        }
        private class RSAParam
        {
            public int dwKeySize { get; set; }
            public string xmlString { get; set; }
        }
        #endregion

        public static bool ConvertStringToBool(string value)
        {
            bool result = false;
            try
            {
                result = bool.Parse(value);
            }
            catch
            {
            };
            return result;
        }
        public static Guid GenerateID()
        {
            return System.Guid.NewGuid();
        }
        public static bool isValidVideo(string filetype)
        {
            bool result = false;
            switch (filetype.ToLower())
            {
                case "mp4":
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
    }
}
