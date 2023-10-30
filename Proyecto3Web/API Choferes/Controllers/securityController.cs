using System.Security.Cryptography;

namespace API_Choferes.Controllers
{

    public class securityController
    {

        public string Encriptar(string clave)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key =
                {
                    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                    0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                };
                aes.Key = key;

                byte[] iv = aes.IV;

                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);

                    using (CryptoStream cryptoStream = new CryptoStream(
                        ms,
                        aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                    {
                        using (StreamWriter encryptWriter = new StreamWriter(cryptoStream))
                        {
                            encryptWriter.WriteLine(clave);
                        }
                    }

                    // Devuelve la clave cifrada como una cadena Base64
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }



    }
}
