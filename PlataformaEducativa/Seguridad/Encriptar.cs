using System.Security.Cryptography;
using System.Text;

namespace PlataformaEducativa.Seguridad
{
    public class Encriptar
    {
        static public string Encriptars(string password)
        {
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encodig = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < stream.Length;i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
                


            return sb.ToString(); 
        }
    }
}
