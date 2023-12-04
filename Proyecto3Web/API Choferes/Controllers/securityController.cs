using Azure;
using System.Security.Cryptography;
using System.Text;

namespace API_Choferes.Controllers
{

    public class securityController
    {

        public string Encriptar(string clave)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] key = { };
                byte[] IV = { 12, 21, 43, 17, 57, 35, 67, 27 };
                string encryptKey = "aXb2uy4z";
                key = Encoding.UTF8.GetBytes(encryptKey);
                byte[] byteInput = Encoding.UTF8.GetBytes(clave);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                memStream = new MemoryStream();
                ICryptoTransform transform = provider.CreateEncryptor(key, IV);
                CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(byteInput, 0, byteInput.Length);
                cryptoStream.FlushFinalBlock();

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return Convert.ToBase64String(memStream.ToArray());
        }
        /*public string Desencriptar(string claveEncriptada)
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
        }*/
    }
}
