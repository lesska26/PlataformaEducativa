namespace PlataformaEducativa.Logica
{
    public class Matricula
    {
        public static string createMatricula(string Nombre, string Apellido)
        {
            if(string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido)) 
            {

                return "Error al crear la Matricula";
            }
            Random numero = new Random(DateTime.Now.Millisecond);
            string matricula = string.Format($" {Nombre[0].ToString().ToUpper() + Apellido[0].ToString().ToUpper()}-{numero.Next()}").Trim();
            return matricula;
        }
    }
}
