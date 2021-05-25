using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelipolisEngine.DataBase;
using System.Net.Mail;
using System.IO;
using System.Threading;

namespace IntelipolisEngine.Helper
{
    public class NotificationHelper : IDisposable
    {
        public void Dispose() { }

        public void InsertaNotificacion(string secr, string dep, string clave_empl, string message, string url)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                Tbl_Notificaciones notificacion = new Tbl_Notificaciones();
                notificacion.clave_depe = dep;
                notificacion.clave_empl = clave_empl;
                notificacion.clave_secr = secr;
                notificacion.fecha = DateTime.Now;
                notificacion.leido = false;
                notificacion.mensaje = message;
                notificacion.url = url;
                db.Tbl_Notificaciones.InsertOnSubmit(notificacion);
                db.SubmitChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Envia las notificaciones a el departamento de Eventos
        /// </summary>
        /// <param name="folio_evento"></param>
        public void EnviaNotificacionNuevoEventoDepEventos(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);


                //obtiene las horas para la validacion de eventos
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 3).horas_atencion.Value;

                string mensaje = String.Format("Se ha creado el evento: <b>{0}</b> folio {3} con fecha de <b>{1}</b>. Favor de entrar a la plataforma y revisar los requerimientos necesarios, cuenta con {2} horas laborales para completar la información.'",
                  evento.nombre_evento, evento.fecha, horasAtencion, evento.folio);

                //correo de eventos
                string correoEventos = "ricardo.eslava89@gmail.com";

                //secretaria y dependencia de el departamentos de Eventos
                string secretaria = "313", dependencia = "280";

                //envia el correo
                EnviaCorreo(correoEventos, "Nuevo evento creado (Departamento Eventos)", mensaje);

                //Inserta notificacion
                InsertaNotificacion(secretaria, dependencia, "", mensaje, String.Format("/Eventos2015/Requerimientos.aspx?folio={0}", evento.folio));

                //Inserta etapa
                notificaciones_eventos noti_evento = new notificaciones_eventos();
                noti_evento.activo = true;
                noti_evento.fecha = DateTime.Now;
                noti_evento.folio_evento = folio_evento;
                noti_evento.idEtapa = 3;
                db.notificaciones_eventos.InsertOnSubmit(noti_evento);
                db.SubmitChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Envia las notificaciones al departamento de Comunicaciones 
        /// </summary>
        /// <param name="folio_evento"></param>
        public void EnviaNotificacionNuevoEventoDepComunicacion(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);


                //obtiene las horas para la validacion de comunicacion
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 4).horas_atencion.Value;

                string mensaje = String.Format("Se ha creado el evento: <b>{0}</b> folio {3} con fecha de <b>{1}</b>. Favor de entrar a la plataforma y revisar los requerimientos necesarios, cuenta con {2} horas laborales para completar la información.'",
                  evento.nombre_evento, evento.fecha, horasAtencion, evento.folio);

                //correo de comunicacion

                string correoEventos = "luciogallegos2@gmail.com";

                //secretaria y dependencia de el departamentos de Comunicacion
                string secretaria = "313", dependencia = "276";

                //envia el correo
                EnviaCorreo(correoEventos, "Nuevo evento creado (Departamento Comunicación)", mensaje);

                //Inserta notificacion
                InsertaNotificacion(secretaria, dependencia, "", mensaje, String.Format("/Eventos2015/Requerimientos.aspx?folio={0}", evento.folio));

                //Inserta etapa
                notificaciones_eventos noti_evento = new notificaciones_eventos();
                noti_evento.activo = true;
                noti_evento.fecha = DateTime.Now;
                noti_evento.folio_evento = folio_evento;
                noti_evento.idEtapa = 4;
                db.notificaciones_eventos.InsertOnSubmit(noti_evento);
                db.SubmitChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Envia las notificaciones a el departamento de Control Administrativo
        /// </summary>
        /// <param name="folio_evento"></param>
        public void EnviaNotificacionNuevoEventoDepControlAdministrativo(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);


                //obtiene las horas para la validacion de Control Administrativo
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 2).horas_atencion.Value;

                string mensaje = String.Format("Se ha creado el evento: <b>{0}</b> folio {3} con fecha de <b>{1}</b>. Favor de entrar a la plataforma y revisar los requerimientos necesarios, cuenta con {2} horas laborales para completar la información.'",
                  evento.nombre_evento, evento.fecha, horasAtencion, evento.folio);

                //correo de Control Administrativo
                string correoEventos = db.ObtieneEmailEnlacesAdministrativos(folio_evento).FirstOrDefault().Emails;

                //secretaria y dependencia de el Control Administrativo
                string secretaria = "304", dependencia = "266";

                //envia el correo
                EnviaCorreo(correoEventos, "Nuevo evento creado (Departamento Control Administrativo)", mensaje);

                //Inserta notificacion
                InsertaNotificacion(secretaria, dependencia, "", mensaje, String.Format("/Eventos2015/Requerimientos.aspx?folio={0}", evento.folio));

                //Inserta etapa
                notificaciones_eventos noti_evento = new notificaciones_eventos();
                noti_evento.activo = true;
                noti_evento.fecha = DateTime.Now;
                noti_evento.folio_evento = folio_evento;
                noti_evento.idEtapa = 2;
                db.notificaciones_eventos.InsertOnSubmit(noti_evento);
                db.SubmitChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Envia las notificaciones cuando se han terminado las etapas de eventos y se notifica al Alcalde
        /// </summary>
        /// <param name="folio_evento"></param>
        public void EnviaNotificacionNuevoEventoAlcalde(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);

                //obtiene las horas para la validacion de Alcalde
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 6).horas_atencion.Value;

                string mensaje = String.Format("Se ha creado el evento: <b>{0}</b> folio {3} con fecha de <b>{1}</b>. Favor de entrar a la plataforma y autorizar o rechazar el evento, cuenta con {2} horas laborales para complementar la información.'",
                  evento.nombre_evento, evento.fecha, horasAtencion, evento.folio);

                //correo de Control Administrativo
                string correoEventos = "ricardo.eslava89@gmail.com";

                //secretaria y dependencia de el Control Administrativo
                string alcalde = "9999";

                //envia el correo
                EnviaCorreo(correoEventos, "Nuevo evento creado (Alcalde)", mensaje);

                //Inserta notificacion
                InsertaNotificacion("", "", alcalde, mensaje, String.Format("/Eventos2015/Validacion.aspx", evento.folio));

                //Inserta etapa
                notificaciones_eventos noti_evento = new notificaciones_eventos();
                noti_evento.activo = true;
                noti_evento.fecha = DateTime.Now;
                noti_evento.folio_evento = folio_evento;
                noti_evento.idEtapa = 6;
                db.notificaciones_eventos.InsertOnSubmit(noti_evento);
                db.SubmitChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Envia las notificaciones a los departamentos invitados a el evento
        /// </summary>
        /// <param name="folio_evento"></param>
        public void EnviaNotificacionDireccionesInvitadas(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");

                //obtiene direcciones invitadas
                var secreatarias = (from f in db.reg_ficha
                                    join i in db.invitados on f.folio_ficha equals i.folio_ficha
                                    where f.folio_evento == folio_evento
                                    select i).ToList();

                foreach (var sec in secreatarias)
                {
                    var formato = db.ObtieneFormatoCorreoSecretarias(sec.invitados1, folio_evento).FirstOrDefault();

                    //inserta notificacion
                    InsertaNotificacion(sec.invitados1.ToString(), "", "", formato.Mensaje, String.Format("/Eventos2015/Requerimientos.aspx?folio={0}", folio_evento));

                    //envia correo
                    EnviaCorreo(formato.correo, String.Format("Nuevo evento creado ({0})", formato.Asunto), formato.Mensaje);
                }

                //Inserta etapa
                notificaciones_eventos noti_evento = new notificaciones_eventos();
                noti_evento.activo = true;
                noti_evento.fecha = DateTime.Now;
                noti_evento.folio_evento = folio_evento;
                noti_evento.idEtapa = 5;
                db.notificaciones_eventos.InsertOnSubmit(noti_evento);
                db.SubmitChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void EnviaNotificacionAreaOperacionEvento(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);

                //obtiene las horas para la validacion de Alcalde
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 7).horas_atencion.Value;

                string mensaje = String.Format("Se ha <b>AUTORIZADO</b> el evento: <b>{0}</b> folio {3} con fecha de <b>{1}</b>. Favor de entrar a la plataforma y validar los requerimientos, cuenta con {2} horas laborales para validar la información.'",
                  evento.nombre_evento, evento.fecha, horasAtencion, evento.folio);

                //correo de el departamento creador
                string correoEventos = db.correos_secretarias.FirstOrDefault(x => x.cve_secr == evento.id_secr).correo;//  "ricardo.eslava89@gmail.com";

                //envia el correo
                EnviaCorreo(correoEventos, "Nuevo evento Autorizado", mensaje);

                //Inserta notificacion
                InsertaNotificacion(evento.id_secr.ToString(), evento.id_depe.ToString(), "", mensaje, String.Format("http://admin.sanicolas.gob.mx/PlaneacionFinanciera/EventoDetalle.aspx?id={0}", evento.folio));

                //Inserta etapa
                notificaciones_eventos noti_evento = new notificaciones_eventos();
                noti_evento.activo = true;
                noti_evento.fecha = DateTime.Now;
                noti_evento.folio_evento = folio_evento;
                noti_evento.idEtapa = 7;
                db.notificaciones_eventos.InsertOnSubmit(noti_evento);
                db.SubmitChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void EnviaNotificacionAreaPresupuestosEvento(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);

                //obtiene las horas para la validacion de Presupuesto
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 8).horas_atencion.Value;

                string mensaje = String.Format("Se ha <b>AUTORIZADO</b> el evento: <b>{0}</b> folio {3} con fecha de <b>{1}</b>. Favor de entrar a la plataforma y validar los requerimientos, cuenta con {2} horas laborales para validar la información.'",
                  evento.nombre_evento, evento.fecha, horasAtencion, evento.folio);

                //correo de el departamento creador
                string correoPresupuesto = "ricardo.eslava89@gmail.com";

                //envia el correo
                EnviaCorreo(correoPresupuesto, "Nuevo evento Autorizado", mensaje);

                //Inserta notificacion
                InsertaNotificacion(evento.id_secr.ToString(), evento.id_depe.ToString(), "", mensaje, String.Format("http://admin.sanicolas.gob.mx/PlaneacionFinanciera/EventoDetalle.aspx?id={0}", evento.folio));

            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Envia un correo a un destinatarios con los parametros dados
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="asunto"></param>
        /// <param name="mensaje"></param>
        public void EnviaCorreo(string destinatario, string asunto, string mensaje)
        {
            bool ENVIADO = false;
            try
            {
                MailMessage msg = new MailMessage("intelipolis@gmail.com", destinatario);
                msg.Body = mensaje;
                msg.Subject = "[IGNORAR] " + asunto;
                msg.IsBodyHtml = true;

                //Now sending a mail with attachment ICS file.                     
                SmtpClient SmtpClient = new SmtpClient();
                SmtpClient.Port = 587;
                SmtpClient.Host = "smtp.gmail.com"; //-------this has To given the Mailserver IP
                SmtpClient.EnableSsl = true;
                SmtpClient.Credentials = new System.Net.NetworkCredential("intelipolis@gmail.com", "IntelipolisEventos2020");

                SmtpClient.Send(msg);
                ENVIADO = true;
            }
            catch (SmtpException x)
            {
                ENVIADO = false;
            }
            catch (Exception x)
            {
                ENVIADO = false;
            }
            finally
            {
                #region GUARDA EL LOG DE EL CORREO ENVIADO

                //Guarda el Log de el envio de correo
                IntelipolisDBDataContext db = new IntelipolisDBDataContext();
                tblBitacoraCorreo log = new tblBitacoraCorreo();
                log.fecha = DateTime.Now;
                log.destinatario = destinatario;
                log.asunto = asunto;
                log.CC = "";
                log.mensaje = mensaje;
                log.enviado = ENVIADO;
                db.tblBitacoraCorreo.InsertOnSubmit(log);
                db.SubmitChanges();

                #endregion
            }
        }

        /// <summary>
        /// Envia un correo con el archivo de calendar 
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="asunto"></param>
        /// <param name="titulo"></param>
        /// <param name="fechaIni"></param>
        /// <param name="fechaFin"></param>
        /// <param name="mensaje"></param>
        /// <param name="lugar"></param>
        public void EnviaCorreoNotificacionCalendario(string destinatario, string asunto, string titulo, DateTime fechaIni, DateTime fechaFin, string mensaje, string lugar)
        {
            bool ENVIADO = false;
            try
            {
                MailMessage msg = new MailMessage("intelipolis@gmail.com", destinatario);
                msg.Body = mensaje;
                msg.Subject = asunto;
                msg.IsBodyHtml = true;

                StringBuilder Str = new StringBuilder();
                Str.AppendLine("BEGIN:VCALENDAR");
                Str.AppendLine("PRODID:-//Schedule a Meeting");
                Str.AppendLine("VERSION:2.0");
                Str.AppendLine("METHOD:REQUEST");
                Str.AppendLine("BEGIN:VEVENT");
                Str.AppendLine(String.Format("DTSTART:{0}", fechaIni.ToUniversalTime().ToString("yyyyMMddTHHmmssZ")));
                //Str.AppendLine(String.Format("DTSTAMP:{0:yyyyMMddTHHmmssZ}", DateTime.UtcNow));
                Str.AppendLine(String.Format("DTEND:{0}", fechaFin.ToUniversalTime().ToString("yyyyMMddTHHmmssZ")));
                Str.AppendLine("LOCATION: " + lugar);
                Str.AppendLine("TITLE: " + titulo);
                Str.AppendLine(String.Format("UID:{0}", Guid.NewGuid()));
                Str.AppendLine(String.Format("DESCRIPTION:{0}", msg.Body));
                Str.AppendLine(String.Format("X-ALT-DESC;FMTTYPE=text/html:{0}", msg.Body));
                Str.AppendLine(String.Format("SUMMARY:{0}", msg.Subject));
                Str.AppendLine(String.Format("ORGANIZER:MAILTO:{0}", msg.From.Address));

                Str.AppendLine(String.Format("ATTENDEE;CN=\"{0}\";RSVP=TRUE:mailto:{1}", msg.To[0].DisplayName, msg.To[0].Address));

                Str.AppendLine("BEGIN:VALARM");
                Str.AppendLine("TRIGGER:-PT15M");
                Str.AppendLine("ACTION:DISPLAY");
                Str.AppendLine("DESCRIPTION:Reminder");
                Str.AppendLine("END:VALARM");
                Str.AppendLine("END:VEVENT");
                Str.AppendLine("END:VCALENDAR");

                byte[] byteArray = Encoding.ASCII.GetBytes(Str.ToString());
                MemoryStream stream = new MemoryStream(byteArray);

                Attachment attach = new Attachment(stream, "evento.ics");
                msg.Attachments.Add(attach);

                System.Net.Mime.ContentType contype = new System.Net.Mime.ContentType("text/calendar");
                contype.Parameters.Add("method", "REQUEST");

                AlternateView avCal = AlternateView.CreateAlternateViewFromString(Str.ToString(), contype);
                msg.AlternateViews.Add(avCal);

                System.Net.Mail.SmtpClient SmtpClient = new System.Net.Mail.SmtpClient();
                SmtpClient.Port = 587;
                SmtpClient.Host = "smtp.gmail.com";
                SmtpClient.EnableSsl = true;
                SmtpClient.Credentials = new System.Net.NetworkCredential("intelipolis@gmail.com", "IntelipolisEventos2020");
                SmtpClient.Send(msg);
                ENVIADO = true;
            }
            catch (SmtpException x)
            {
                ENVIADO = false;
            }
            catch (Exception x)
            {
                ENVIADO = false;
            }
            finally
            {
                #region GUARDA EL LOG DE EL CORREO ENVIADO

                //Guarda el Log de el envio de correo
                IntelipolisDBDataContext db = new IntelipolisDBDataContext();
                tblBitacoraCorreo log = new tblBitacoraCorreo();
                log.fecha = DateTime.Now;
                log.destinatario = destinatario;
                log.asunto = asunto;
                log.CC = "";
                log.mensaje = mensaje;
                log.enviado = ENVIADO;
                db.tblBitacoraCorreo.InsertOnSubmit(log);
                db.SubmitChanges();

                #endregion
            }
        }

        /// <summary>
        /// Envia correo asincrono
        /// </summary>
        /// <param name="destinatario"></param>
        /// <param name="asunto"></param>
        /// <param name="mensaje"></param>
        /// <returns></returns>
        public async Task EnviaCorreoAsync(string destinatario, string asunto, string mensaje)
        {
            bool ENVIADO = false;
            try
            {
                MailMessage msg = new MailMessage("intelipolis@gmail.com", destinatario);
                msg.Body = mensaje;
                msg.Subject = asunto;
                msg.IsBodyHtml = true;

                //Now sending a mail with attachment ICS file.                     
                SmtpClient SmtpClient = new SmtpClient();
                SmtpClient.Port = 587;
                SmtpClient.Host = "smtp.gmail.com"; //-------this has To given the Mailserver IP
                SmtpClient.EnableSsl = true;
                SmtpClient.Credentials = new System.Net.NetworkCredential("intelipolis@gmail.com", "IntelipolisEventos2020");

                await SmtpClient.SendMailAsync(msg);
                ENVIADO = true;
            }
            catch (SmtpException x)
            {
                ENVIADO = false;
            }
            catch (Exception x)
            {
                ENVIADO = false;
            }
            finally
            {
                #region GUARDA EL LOG DE EL CORREO ENVIADO

                //Guarda el Log de el envio de correo
                IntelipolisDBDataContext db = new IntelipolisDBDataContext();
                tblBitacoraCorreo log = new tblBitacoraCorreo();
                log.fecha = DateTime.Now;
                log.destinatario = destinatario;
                log.asunto = asunto;
                log.CC = "";
                log.mensaje = mensaje;
                log.enviado = ENVIADO;
                db.tblBitacoraCorreo.InsertOnSubmit(log);
                db.SubmitChanges();

                #endregion
            }
        }

        /// <summary>
        /// Envia las notificaciones a el departamento de Control Administrativo
        /// </summary>
        /// <param name="folio_evento"></param>
        public void EnviaNotificacionDirector(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);

                //obtiene las horas para la validacion de Presupuesto
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 8).horas_atencion.Value;

                string mensaje = String.Format("El evento <b>{0}</b> folio {1} con fecha de <b>{2:dd/MM/yyyy}</b> ha sido valido por el Enlace Administrativo. Favor de entrar a la plataforma y validar los requerimientos.'",
                  evento.nombre_evento, evento.folio, evento.fecha);

                //envia el correo a comunicacion
                var director = db.Usuarios.Where(x => x.clave_secr == evento.id_secr && x.clave_depe == evento.id_depe && x.Puesto == "1").Select(x => x.correo);
                string correosDirector = String.Join(";", director);

                //envia el correo
                EnviaCorreo(correosDirector, "Evento validado por Enlace Administrativo", mensaje);

                //Inserta notificacion
                InsertaNotificacion(evento.id_secr.ToString(), evento.id_depe.ToString(), "", mensaje, String.Format("http://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/RequerimientosDirector.aspx?folio={0}", evento.folio));

            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Envia las notificaciones a el departamento de Control Administrativo
        /// </summary>
        /// <param name="folio_evento"></param>
        public void EnviaNotificacionEnlaceAdministrativoComentariosAlcalde(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);

                //obtiene las horas para la validacion de Presupuesto
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 8).horas_atencion.Value;

                string mensaje = String.Format("El evento <b>{0}</b> folio {1} con fecha de <b>{2:dd/MM/yyyy}</b> ha sido Autorizado por el Alcalde con comentarios que hay que revisar. Favor de entrar a la plataforma y validar los requerimientos.'",
                  evento.nombre_evento, evento.folio, evento.fecha);

                //envia el correo a comunicacion
                int enlace = db.Secretarias.Where(x => x.IdSecretaria == evento.id_secr).FirstOrDefault().enlaceAdministrativo.Value;
                string enlaceCorreo =  db.Usuarios.Where(x => x.Clave_empl == enlace).FirstOrDefault().correo;

                //envia el correo
                EnviaCorreo(enlaceCorreo, "Evento pendiente de Validación", mensaje);

                //Inserta notificacion
                InsertaNotificacion("", "", enlace.ToString(), mensaje, String.Format("http://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/RequerimientosEventos.aspx?folio={0}", evento.folio));

            }
            catch (Exception x)
            {
                throw x;
            }
        }

        /// <summary>
        /// Envia las notificaciones a el departamento de Control Administrativo
        /// </summary>
        /// <param name="folio_evento"></param>
        public void EnviaNotificacionComunicacion(int folio_evento)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                reg_evento evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);

                //obtiene las horas para la validacion de Presupuesto
                int horasAtencion = db.etapas_evento.FirstOrDefault(x => x.id == 8).horas_atencion.Value;

                string mensaje = String.Format("El evento <b>{0}</b> folio {1} con fecha de <b>{2:dd/MM/yyyy}</b> ha sido valido por el Director de el Área. Favor de entrar a la plataforma y validar los requerimientos.'",
                  evento.nombre_evento, evento.folio, evento.fecha);

                //envia el correo a comunicacion
                var comunicacion = db.Usuarios.Where(x => x.clave_secr == 313 && x.clave_depe == 276).Select(x => x.correo);
                string correosComunicacion = String.Join(";", comunicacion);

                //envia el correo
                EnviaCorreo(correosComunicacion, "Evento Validado por Director", mensaje);

                //Inserta notificacion
                InsertaNotificacion("313", "276", "", mensaje, String.Format("http://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/RequerimientosComunicacion.aspx?folio={0}", evento.folio));

            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void NotificaUsuarioEventoCreado(int folio_evento, int empleado)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext();
                var evento = db.reg_evento.FirstOrDefault(x => x.folio == folio_evento);

                //Inserta notificacion
                InsertaNotificacion("", "", empleado.ToString(), String.Format("Ha generado el evento {1} con el folio {0}.", evento.folio, evento.nombre_evento), String.Format("http://admin.sanicolas.gob.mx/PlaneacionFinanciera/Eventos2015/Ficha.aspx?folio={0}", folio_evento));


                var empl = db.Tbl_Empleados.FirstOrDefault(x => x.clave_empl == empleado);
                //envia el correo
                EnviaCorreo(empl.email, "Evento Validado por Director", String.Format("Ha generado el evento {1} con el folio {0}.", evento.folio, evento.nombre_evento));
            }
            catch
            {

            }
        }
    }
}
