using Holidays;
using Nager.Date;

namespace PlataformaEducativa.Logica
{
    public class DiaFestivo
    {
        public static bool Festivo(DateTime Fecha)
        {
           var holiday = DateSystem.IsPublicHoliday(DateTime.Now,"DO");
           return holiday;

        }
    }
}
