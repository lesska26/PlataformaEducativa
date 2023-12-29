namespace PlataformaEducativa.Logica
{
    public class TraduccionSemanasInglesh
    {
        public static string Traduccion(string Semana)
        {
            switch(Semana)
            {
                case "Lunes":
                    return "Monday";
                    break;
                case "Marte":
                    return "Tuesday";
                    break;
                case "Miercoles":
                    return "Wednesday";
                    break;
                case "Jueves":
                    return "Thursday";
                    break;
                case "Viernes":
                    return "Friday";
                    break;
                case "Sabado":
                    return "Saturday";
                    break;
                case "Domingo":
                    return "Sunday";
                    break;
                default:
                    return "Semana Invalida";
                    break;
            }
        }
    }
}
