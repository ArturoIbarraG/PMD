using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelipolisEngine.Helper
{
    public class Helper
    {
        public static DataTable ConvertToDataTable<T>(IList<T> data, bool columnAsName = false, bool changeToDataType = false)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(columnAsName ? prop.Name.Replace("_", " ") : prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (changeToDataType)
                    {
                        if (prop.GetValue(item).GetType() == typeof(int))
                            row[columnAsName ? prop.Name.Replace("_", " ") : prop.Name] = String.Format("{0:n}", prop.GetValue(item)) ?? "";
                        else if (prop.GetValue(item).GetType() == typeof(decimal))
                            row[columnAsName ? prop.Name.Replace("_", " ") : prop.Name] = String.Format("{0:c0}", prop.GetValue(item)) ?? "";
                        else if (prop.GetValue(item).GetType() == typeof(DateTime))
                            row[columnAsName ? prop.Name.Replace("_", " ") : prop.Name] = String.Format("{0:dd/MM/yyyy}", prop.GetValue(item)) ?? "";
                        else
                            row[columnAsName ? prop.Name.Replace("_", " ") : prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                    else
                        row[columnAsName ? prop.Name.Replace("_", " ") : prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }
            return table;

        }


        /// <summary>
        /// Regresa el primer dia del mes de la fecha proporcionada
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime GetFirstDayMonth(DateTime fecha)
        {
            try
            {
                return new DateTime(fecha.Year, fecha.Month, 1);
            }
            catch
            {
                return fecha;
            }
        }

        /// <summary>
        /// Regresa el  último dia del mes de la fecha proporcionada
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime GetLastDayMonth(DateTime fecha)
        {
            try
            {
                return GetFirstDayMonth(fecha).AddMonths(1).AddDays(-1);
            }
            catch
            {
                return fecha;
            }
        }

        /// <summary>
        /// Regresa el primer minuto de la fecha proporcionada
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static DateTime GetFirstHourOfDay(DateTime fecha)
        {
            try
            {
                return new DateTime(fecha.Year, fecha.Month, fecha.Day, 0, 0, 0);
            }
            catch
            {
                return fecha;
            }
        }

        /// <summary>
        /// Regresa la ultima fecha del dia procporcionado
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        ///  public static DateTime GetLastHourOfDay(DateTime fecha)
        public static DateTime GetLastHourOfDay(DateTime fecha)
        {
            try
            {
                return new DateTime(fecha.Year, fecha.Month, fecha.Day, 23, 59, 59);
            }
            catch
            {
                return fecha;
            }
        }

        /// <summary>
        /// Regresa el Puesto de el empleado
        /// </summary>
        /// <param name="claveEmpl"></param>
        /// <returns></returns>
        public static string PuestoEmpleado(int claveEmpl)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            int puesto = int.Parse(db.Usuarios.FirstOrDefault(x => x.Clave_empl == claveEmpl).Puesto);
            string nombrePuesto = "";
            if (puesto == 6) nombrePuesto = "Secreatrio";
            else if (puesto == 1)
                nombrePuesto = "Director";
            else if (puesto == 2)
                nombrePuesto = "Coordinador";
            else if (puesto == 3)
                nombrePuesto = "Empleado";
            else if (puesto == 4)
                nombrePuesto = "Enlace administrativo";
            else if (puesto == 5)
                nombrePuesto = "Coordinador administrativo";

            return nombrePuesto;
        }

        public static string NombreEmpleado(int claveEmpl)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            return db.Usuarios.FirstOrDefault(x => x.Clave_empl == claveEmpl).Nombr_empl;
        }
    }
}
