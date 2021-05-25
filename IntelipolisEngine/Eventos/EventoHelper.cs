using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using IntelipolisEngine.DataBase;
using SelectPdf;
using IntelipolisEngine.Helper;

namespace IntelipolisEngine.Eventos
{
    public class EventoHelper : IDisposable
    {

        public string RegresaFormatoOficioURL(int folioOficio)
        {
            try
            {
                string PATH_FILES = String.Format("/Archivos/Formatos/OficiosSuficiencia/");
                string urlFile = HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("OS-{0}.pdf", folioOficio.ToString().PadLeft(5, '0')));
                if (!File.Exists(urlFile))
                {
                    //Inserta el registro del oficio
                    IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                    var infoOficio = db.Tbl_SubActividadClaveGastos.FirstOrDefault(x => x.Id == folioOficio);
                    tbl_ClaveGastosOficio oficio = new tbl_ClaveGastosOficio();
                    oficio.claveGasto = infoOficio.Clave_Gastos.ToString();
                    oficio.fechaOficio = DateTime.Now;
                    oficio.folioSFYTSSPF = ObtieneFolioSST();
                    oficio.idSubActividad = infoOficio.Id_SubActividad;
                    oficio.idSubActividadGasto = folioOficio;
                    oficio.montoAutorizado = infoOficio.MontoTotal;
                    db.tbl_ClaveGastosOficio.InsertOnSubmit(oficio);
                    db.SubmitChanges();

                    var nuevoFolio = db.tbl_ClaveGastosOficio.FirstOrDefault(x => x.id == oficio.id);
                    int folio = 1;
                    try { folio = db.tbl_ClaveGastosOficio.Max(x => x.folioOficio).Value + 1; } catch { }
                    nuevoFolio.folioOficio = folio;
                    db.SubmitChanges();

                    var formatoOficio = db.ObtieneInfoFormatoOficio(oficio.id).FirstOrDefault();

                    StreamReader fil = File.OpenText(HttpContext.Current.Server.MapPath("~/FormatoHTML/OficioSuficiencia.html"));
                    String body = fil.ReadToEnd();
                    fil.Close();

                    //Carga informacion del Certificado
                    body = body.Replace("<%FECHA_FORMATO%>", formatoOficio.FechaAutorizacion.Value.ToString("dd/MM/yyyy"));
                    body = body.Replace("<%FECHA_DE_FOLIO_EN_SISTEMA%>", formatoOficio.fecha_captura.ToString("dd/MM/yyyy"));
                    body = body.Replace("<%ACTIVIDAD%>", formatoOficio.Actividad);
                    body = body.Replace("<%NUMERO_DE_FOLIO_DE_SISTEMA%>", formatoOficio.folioSFYTSSPF);
                    body = body.Replace("<%MONTO%>", String.Format("{0:c2}", formatoOficio.MontoTotal.Value));
                    body = body.Replace("<%MONTO_LETRA%>", NumberToText(formatoOficio.MontoTotal.Value.ToString()));
                    body = body.Replace("<%EJERCICIO_FISCAL%>", formatoOficio.EjericioFiscal.ToString());
                    body = body.Replace("<%DESCRIPCION_DEL_FOLIO_EN_SISTEMA%>", formatoOficio.DescripcionEvento);
                    body = body.Replace("<%FOLIO_SISTEMA%>", oficio.folioSFYTSSPF);
                    body = body.Replace("<%FUENTE_DE_FINANCIAMIENTO_DE_SISTEMA%>", formatoOficio.FuenteFinanciamiento);
                    body = body.Replace("<%NOMBRE_DE_DIRECTOR_SOLICITANTE%>", formatoOficio.director.ToString().ToUpper());
                    body = body.Replace("<%PUESTO_DE_DIRECTOR_SOLICITANTE%>", formatoOficio.Puesto.ToString().ToUpper());

                    // instantiate a html to pdf converter object
                    HtmlToPdf converter = new HtmlToPdf();
                    converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                    converter.Options.MarginBottom = 5;
                    converter.Options.MarginLeft = 5;
                    converter.Options.MarginRight = 5;
                    converter.Options.MarginTop = 5;

                    // create a new pdf document converting an url
                    PdfDocument doc = converter.ConvertHtmlString(body);

                    // create memory stream to save PDF
                    MemoryStream pdfStream = new MemoryStream();

                    // save pdf document into a MemoryStream
                    doc.Save(pdfStream);

                    // reset stream position
                    pdfStream.Position = 0;

                    //crea el PDF en memoria

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/" + PATH_FILES)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/" + PATH_FILES));

                    System.IO.FileStream newFile
                           = new System.IO.FileStream(HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("OS-{0}.pdf", folioOficio.ToString().PadLeft(5, '0'))),
                                                      FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    newFile.Write(pdfStream.ToArray(), 0, pdfStream.ToArray().Length);
                    newFile.Dispose();


                }

                return String.Format("~/Archivos/Formatos/OficiosSuficiencia/OS-{0}.pdf", folioOficio.ToString().PadLeft(5, '0'));
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        private string ObtieneFolioSST()
        {
            DateTime now = DateTime.Now;
            IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            var totales = db.tbl_ClaveGastosOficio.Where(x => x.fechaOficio.Value.Date == now.Date);
            int consecutivo = 1;
            try { consecutivo = totales.Count() + 1; } catch { }
            return "SFYT/SSPF/" + now.Day.ToString("00") + now.Month.ToString("00") + now.Year.ToString("0000") + consecutivo.ToString("000");
        }

        public static string NumberToText(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try

            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " CON " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private static string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }

        public void ValidaEtapasEventos()
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                var eventosPendientes = db.reg_evento.Where(x => x.validada == 0 && (x.procesado == false || x.procesado == null)).ToList();

                foreach (var even in eventosPendientes)
                {
                    //obtiene la ultima etapa 
                    var etapa = db.notificaciones_eventos.Where(x => x.folio_evento == even.folio).OrderByDescending(x => x.fecha).FirstOrDefault();

                    if (etapa == null)
                    {
                        enviaNotificacionesPendientes(even.folio);
                    }
                    else
                    {
                        //ETAPA: EVENTO CREADO
                        if (etapa.idEtapa <= 5)
                        {
                            //si el evento solo tiene la notificacion de creado, entonces manda las notificaciones principales
                            enviaNotificacionesPendientes(even.folio);

                            //valida cual fue la ultima notificacion para validar las horas
                            var etapas = db.notificaciones_eventos.Where(x => x.folio_evento == even.folio);
                            int horas = 0;
                            DateTime fecha = DateTime.Now;
                            int idEtapa = 0;
                            bool terminado = true;
                            foreach (var e in etapas)
                            {
                                int diferencia = Helper.CalendarHelper.ObtieneHorasLaboralesEntreFechas(fecha, e.fecha.Value);
                                int horasAtencion = ObtieneHorasPorEtapa(e.idEtapa.Value);
                                if (diferencia >= horasAtencion)
                                    terminado = false;
                            }

                            //Cuando han pasado las horas de las etapas anteriores y se envia a autorizar por alcalde
                            if (terminado)
                            {
                                using (Helper.NotificationHelper helper = new Helper.NotificationHelper())
                                    helper.EnviaNotificacionNuevoEventoAlcalde(even.folio);
                            }
                        }
                        //ETAPA: NOTIFICACION A ALCALDE
                        else if (etapa.idEtapa == 6)
                        {
                            //valida si han pasado las horas de la etapa y si es asi, pasa a la siguiente etapa
                            int horas = Helper.CalendarHelper.ObtieneHorasLaboralesEntreFechas(etapa.fecha.Value, DateTime.Now);
                            int horasAtencion = ObtieneHorasPorEtapa(etapa.idEtapa.Value);

                            if (horas >= horasAtencion)
                            {
                                //pasa a la siguiente Etapa
                                using (Helper.NotificationHelper helper = new Helper.NotificationHelper())
                                    helper.EnviaNotificacionAreaOperacionEvento(even.folio);
                            }

                        }
                        //ETAPA: NOTIFICACION A OPERACION
                        else if (etapa.idEtapa == 7)
                        {
                            //valida si han pasado las horas de la etapa y si es asi, pasa a la siguiente etapa
                            int horas = Helper.CalendarHelper.ObtieneHorasLaboralesEntreFechas(etapa.fecha.Value, DateTime.Now);
                            int horasAtencion = ObtieneHorasPorEtapa(etapa.idEtapa.Value);

                            if (horas >= horasAtencion)
                            {
                                //pasa a la siguiente Etapa
                                using (Helper.NotificationHelper helper = new Helper.NotificationHelper())
                                    helper.EnviaNotificacionAreaPresupuestosEvento(even.folio);
                            }
                        }
                        //ETAPA: NOTIFICACION A PRESUPUESTO
                        else if (etapa.idEtapa == 8)
                        {

                        }
                    }

                }

            }
            catch (Exception x)
            {
                throw x;
            }
        }

        private void enviaNotificacionesPendientes(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                //si el evento solo tiene la notificacion de creado, entonces manda las notificaciones principales
                //envia notificacion a el departamento de Eventos si no se ha enviado
                if (!db.notificaciones_eventos.Where(x => x.folio_evento == folio_evento && x.idEtapa == 3).Any())
                {
                    using (Helper.NotificationHelper helper = new Helper.NotificationHelper())
                        helper.EnviaNotificacionNuevoEventoDepEventos(folio_evento);
                }

                //envia notificacion a Control Administrativo si no se ha enviado
                if (!db.notificaciones_eventos.Where(x => x.folio_evento == folio_evento && x.idEtapa == 2).Any())
                {
                    using (Helper.NotificationHelper helper = new Helper.NotificationHelper())
                        helper.EnviaNotificacionNuevoEventoDepControlAdministrativo(folio_evento);
                }

                //envia notificacion a Comunicacion si no se ha enviado
                if (!db.notificaciones_eventos.Where(x => x.folio_evento == folio_evento && x.idEtapa == 4).Any())
                {
                    using (Helper.NotificationHelper helper = new Helper.NotificationHelper())
                        helper.EnviaNotificacionNuevoEventoDepComunicacion(folio_evento);
                }

                //envia notificacion a Dependencias Invitados
                if (!db.notificaciones_eventos.Where(x => x.folio_evento == folio_evento && x.idEtapa == 5).Any())
                {
                    using (Helper.NotificationHelper helper = new Helper.NotificationHelper())
                        helper.EnviaNotificacionDireccionesInvitadas(folio_evento);
                }
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        private int ObtieneHorasPorEtapa(int idEtapa)
        {
            try
            {
                DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                return db.etapas_evento.FirstOrDefault(x => x.id == idEtapa).horas_atencion.Value;
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Regresa la Data Table de los eventos con presupuesto
        /// </summary>
        /// <param name="año"></param>
        /// <param name="estatus"></param>
        /// <returns></returns>
        public DataTable ObtieneGraficoEventos(int año, int estatus)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                var result = db.ObtieneGraficoEventosPresupuesto(año, estatus).ToList();

                return result.ToPivotTable(x => x.mes,
                    x => x.nombre,
                    xs => xs.Any() ? xs.Sum(x => x.total) : 0);
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public DataTable ObtieneEstatusValidacionEvento(int folio)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            var result = db.ValidaEstatusValidacionEvento(folio).ToList();

            return Helper.Helper.ConvertToDataTable(result);
        }

        public void AgregaValidacionEvento(int folio, int empleado, int orden)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            db.ValidarEvento(folio, empleado, orden, 0);
            switch (orden)
            {
                case 1:
                    {
                        //Lo valido el Enlace Administrativo
                        NotificationHelper helper = new NotificationHelper();
                        helper.EnviaNotificacionDirector(folio);
                    }break;
                case 2:
                    {
                        //Lo valido el Director
                        NotificationHelper helper = new NotificationHelper();
                        helper.EnviaNotificacionComunicacion(folio);
                    }
                    break;
                case 3:
                    {
                        //Lo valido el area de Comunicacion.
                        NotificationHelper helper = new NotificationHelper();
                        helper.EnviaNotificacionNuevoEventoAlcalde(folio);
                    }
                    break;
                case 4:
                    {
                        //Validado por el alcalde sin comentarios
                    }
                    break;
                case 5:
                    {
                        //Validado por el alcalde con comentarios
                        NotificationHelper helper = new NotificationHelper();
                        helper.EnviaNotificacionEnlaceAdministrativoComentariosAlcalde(folio);
                    }
                    break;
                case 6:
                    {

                    }
                    break;
            }

        }

        public class ContratoPresupuesto
        {
            public string Contrato { get; set; }

        }
        public void Dispose() { }
    }
}
