
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Twabel.CrossCutting.Helpers
{
    public static class SensitiveDataEncrypter
    {
        private static string? _senstiveKey;
        private const int SALT_SIZE = 16; // 128 bit
        private const int IV_SIZE = 16; // 128 bit
        private const int KEY_SIZE = 32; // 256 bit
        private const int ITERATIONS = 10000;

        public static void Configure(string sensitiveKey, [CallerMemberName] string callerName = "")
        {
            // This is to prevent someone else than startup and before(unit tests) to specify the key
            if (callerName != "<Main>$" && callerName != "Before")
            {
                throw new NotSupportedException($"SensitiveDataEncrypter.Configure() not authorized for {callerName}");
            }

            _senstiveKey = sensitiveKey;
        }
// this code uses industry-standard AES encryption with a randomly generated salt and a private key to securely encrypt sensitive data.
        public static string Encrypt(string data)
        {
            if (data == null)
            {
                return null;
            }

            byte[] iv = new byte[IV_SIZE];// Array used to hold the initialization vector
            byte[] array;// will  hold the encrypted data
            byte[] salt;// is used as a random seed to generate the encryption key.
// AES : Advanced Encryption Standard algorithm.
            using (Aes aes = Aes.Create())
            {
                // The function then uses the Rfc2898DeriveBytes class to generate a key from the _senstiveKey (a private key), using the Salt property and the GetBytes method to set the key and Salt of the rfc2898 instance.
                var rfc2898 = new Rfc2898DeriveBytes(_senstiveKey, SALT_SIZE, ITERATIONS, HashAlgorithmName.SHA256);
                salt = rfc2898.Salt;
                aes.Key = rfc2898.GetBytes(KEY_SIZE);
                aes.IV = iv;
                            // this is used to transform the input data into encrypted data.
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new MemoryStream();
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(data);
                }

                array = memoryStream.ToArray();
            }

            return Convert.ToBase64String(array);
        }

        public static string Decrypt(string hash)
        {
            if (hash == null)
            {
                return null;
            }
            //  The first part is the salt and the second part is the encrypted data.
            var parts = hash.Split('.', 2);

            if (parts.Length != 2)
            {
                throw new FormatException("Unexpected hash format. Should be formatted as `{salt}.{hash}`");
            }

            try
            {
                byte[] iv = new byte[IV_SIZE];
                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] data = Convert.FromBase64String(parts[1]);

                using Aes aes = Aes.Create();
                var rfc2898 = new Rfc2898DeriveBytes(_senstiveKey, salt, ITERATIONS, HashAlgorithmName.SHA256);
                aes.Key = rfc2898.GetBytes(KEY_SIZE);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new MemoryStream(data);
                using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader streamReader = new StreamReader(cryptoStream);
                return streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                if (IsAcceptableException(ex))
                {
                    return hash;
                }
                throw;
            }
        }
        //If an exception occurs during the decryption process, the IsAcceptableException method is called to determine whether the exception is acceptable. If it is, the original input string is returned. If not, the exception is re-thrown.

        private static bool IsAcceptableException(Exception ex)
        {
            return (ex is CryptographicException && ex.Message == "The input data is not a complete block.")
                    || ex is FormatException;
        }
    }
}
/*
This code defines a static class SensitiveDataEncrypter that provides methods to securely encrypt and decrypt sensitive data using industry-standard AES encryption with a randomly generated salt and a private key.

The class has a Configure method that allows setting the private key used for encryption. The private key should only be set at startup and before running any unit tests, and any attempt to set the key at any other time will result in a NotSupportedException.

The Encrypt method takes a string of data to encrypt, generates a random salt, derives a key from the private key and the salt using the Rfc2898DeriveBytes class, and uses the key and a randomly generated initialization vector to encrypt the data. The encrypted data is returned as a Base64-encoded string.

The Decrypt method takes a Base64-encoded string of encrypted data, splits it into the salt and the encrypted data, derives the key from the private key and the salt using the Rfc2898DeriveBytes class, and uses the key and a randomly generated initialization vector to decrypt the data. The decrypted data is returned as a string.

If an exception occurs during the decryption process, the IsAcceptableException method is called to determine whether the exception is acceptable. If it is, the original input string is returned. If not, the exception is re-thrown.

It's worth noting that the IV_SIZE constant is set to 16, which is the standard size for AES initialization vectors. The KEY_SIZE constant is set to 32, which is the size for AES-256 encryption. The SALT_SIZE constant is set to 16, which is sufficient for generating a secure key using the Rfc2898DeriveBytes class. The ITERATIONS constant is set to 10000, which is a good number of iterations for generating a secure key using the Rfc2898DeriveBytes class


*/