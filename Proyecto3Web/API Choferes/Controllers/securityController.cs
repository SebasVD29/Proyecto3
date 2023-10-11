using System.Text;

namespace API_Choferes.Controllers
{

    public class securityController
    {
        public string EncriptarBase64(string cadenaAEncritar) 
        {
            return Convert.ToBase64String(Encoding.Unicode.GetBytes(cadenaAEncritar));
        }
        public string DesencriptarBase64(string cadenaADesencritar)
        {
            return Encoding.Unicode.GetString(Convert.FromBase64String(cadenaADesencritar));
        }
    }
}
