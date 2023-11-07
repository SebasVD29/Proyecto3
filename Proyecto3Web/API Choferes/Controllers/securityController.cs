using System.Security.Cryptography;
using System.Text;

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
        public string Desencriptar(string claveEncriptada)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] key =
                    {
                        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                    };
                    aes.Key = key;

                    // La clave encriptada es una cadena Base64, la convertimos de nuevo a un array de bytes
                    byte[] encryptedBytes = Convert.FromBase64String(claveEncriptada);

                    // Tomamos los primeros bytes para el IV
                    byte[] iv = new byte[aes.IV.Length];
                    Array.Copy(encryptedBytes, iv, iv.Length);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(
                            new MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length),
                            aes.CreateDecryptor(key, iv),
                            CryptoStreamMode.Read))
                        {
                            using (StreamReader decryptReader = new StreamReader(cryptoStream))
                            {
                                
                                string claveDesencriptada = decryptReader.ReadLine();
                                return claveDesencriptada;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"La desencriptación falló. {ex}");
                throw;
            }
        }
    }
}
