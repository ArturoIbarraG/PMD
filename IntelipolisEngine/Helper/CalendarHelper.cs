using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelipolisEngine.Helper
{
    public class CalendarHelper
    {
        /// <summary>
        /// Regresa la cantidad de horas laborales que han transcurrido entre dos fechas
        /// </summary>
        /// <param name="dateIni"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public static int ObtieneHorasLaboralesEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                int totalHoras = 0;
                int horaInicioLaboral = Properties.Settings.Default.StartHour;
                int horaFinLaboral = Properties.Settings.Default.EndHour;

                for (var i = fechaInicio; i < fechaFin; i = i.AddHours(1))
                    if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                        if (i.TimeOfDay.Hours >= horaInicioLaboral && i.TimeOfDay.Hours < horaFinLaboral)
                            totalHoras++;

                return totalHoras;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        
    }
}
