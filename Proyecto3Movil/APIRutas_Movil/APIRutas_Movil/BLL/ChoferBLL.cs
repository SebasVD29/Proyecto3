using APIRutas_Movil.IBLL;
using APIRutas_Movil.IRepositorySQL;
using APIRutas_Movil.Modelo;
using System.Security.Cryptography;
using System.Text;

namespace APIRutas_Movil.BLL
{
    public class ChoferBLL : IChoferBLL
    {
        private readonly IChoferRepository _choferRepository;

        public ChoferBLL(IChoferRepository choferRepository)
        {
            _choferRepository = choferRepository;
        }

        public async Task<ResponseChofer> LoginChofer(string password, string email)
        {
            try
            {
                var chofer = await _choferRepository.SP_LoginChofer(await Encriptacion(password), email);

                ResponseChofer responseChofer = new ResponseChofer();
                ResponseModel responseModel = new ResponseModel();


                if (chofer != null)
                {
                    responseChofer.chofer = chofer;
                    responseModel.errorcode = 0;
                    responseModel.errormsg = "Chofer encontrado con éxito";

                }
                else
                {
                    responseModel.errorcode = 1;
                    responseModel.errormsg = "No se ha podido encontrar el chofer: " + email;
                }

                responseChofer.errores = responseModel;
                return responseChofer;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<string> Encriptacion(string password)
        {
            MemoryStream memStream = null;
            try
            {
                byte[] key = { };
                byte[] IV = { 12, 21, 43, 17, 57, 35, 67, 27 };
                string encryptKey = "aXb2uy4z";
                key = Encoding.UTF8.GetBytes(encryptKey);
                byte[] byteInput = Encoding.UTF8.GetBytes(password);
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
    }
}
