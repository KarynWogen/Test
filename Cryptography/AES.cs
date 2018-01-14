using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Server.Cryptography
{
    public static class AES
    {

        public static byte[] RandomKey
        {
            get {
                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.GenerateKey();
                    return aes.Key;
                }
            }
            
        }

        public static byte[] Encrypt(byte[] value, byte[] key)
        {
            byte[] encryptedValue = value;
            using (var aes = new AesCryptoServiceProvider()
            {
                Key = key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                aes.GenerateIV();
                var iv = aes.IV;
                using (var encrypter = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var cipherStream = new MemoryStream())
                {
                    using (var tCryptoStream = new CryptoStream(cipherStream, encrypter, CryptoStreamMode.Write))
                    using (var tBinaryWriter = new BinaryWriter(tCryptoStream))
                    {
                        //Prepend IV to data
                        //tBinaryWriter.Write(iv); This is the original broken code, it encrypts the iv
                        cipherStream.Write(aes.IV, 0, aes.IV.Length);  //Write iv to the plain stream (not tested though)
                        tBinaryWriter.Write(value);
                        tCryptoStream.FlushFinalBlock();
                    }

                    encryptedValue = cipherStream.ToArray();
                }

            }
            return encryptedValue;
        }

        public static byte[] Decrypt(byte[] value, byte[] key)
        {
            byte[] decryptedValue = value;
            using (var aes = new AesCryptoServiceProvider()
            {
                Key = key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            })
            {
                var iv = new byte[16];
                Array.Copy(value, 0, iv, 0, iv.Length);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(aes.Key, iv), CryptoStreamMode.Write))
                    using (var binaryWriter = new BinaryWriter(cs))
                    {
                        //Decrypt Cipher Text from Message
                        binaryWriter.Write(
                            value,
                            iv.Length,
                            value.Length - iv.Length
                        );
                    }

                    decryptedValue = ms.ToArray();
                }
            }
            return decryptedValue;

        }

    }
}
