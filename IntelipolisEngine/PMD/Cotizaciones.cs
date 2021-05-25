using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IntelipolisEngine.DataBase;
using System.IO;
using System.Web;
using SelectPdf;

namespace IntelipolisEngine.PMD
{
    public class Cotizaciones
    {
        public class Cotizacion
        {
            public int id { get; set; }
            public int anio { get; set; }
            public int mes { get; set; }
            public int idSecretaria { get; set; }
            public int idDireccion { get; set; }
            public int idSubActividad { get; set; }
            public int empleado { get; set; }
            public int idTipo { get; set; }
            public int idUbicacion { get; set; }
            public List<CotizacionDetalle> Productos { get; set; }
        }

        public class CotizacionDetalle
        {
            public int idDetalle { get; set; }
            public int idTipo { get; set; }
            public string nombre { get; set; }
            public string descripcion { get; set; }
            public string justificacion { get; set; }
            public decimal cantidad { get; set; }
            public int idUnidad { get; set; }
            public DateTime fechaInicio { get; set; }
            public DateTime fechaTermino { get; set; }
            public string vigencia { get; set; }
            public DataTable Especificaciones { get; set; }
            public DataTable Archivos { get; set; }

        }

        public int GuardaCotizacion(int idCotizacion, int anio, int mes, int idSecretaria, int idDireccion, int idSubactividad, int empleado, int idAlmacen, List<CotizacionDetalle> productos)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext();
            string usuario = Helper.Helper.NombreEmpleado(empleado);
            string nombrePuesto = Helper.Helper.PuestoEmpleado(empleado);

            int folio = 1;
            try { folio = db.tblCotizacion.Max(x => x.folio).Value + 1; } catch { }

            var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == idCotizacion);
            if (cotizacion == null)
            {
                cotizacion = new tblCotizacion();
                cotizacion.anio = anio;
                cotizacion.estatus = "Enviada";
                cotizacion.fechaEnvio = DateTime.Now;
                cotizacion.folio = folio;
                cotizacion.idDireccion = idDireccion;
                cotizacion.idSecretaria = idSecretaria;
                cotizacion.idSubActividad = idSubactividad;
                cotizacion.mes = mes;
                cotizacion.idAlmacen = idAlmacen;
                cotizacion.usuarioEnvio = usuario;
                cotizacion.puestoEnvio = nombrePuesto;
                db.tblCotizacion.InsertOnSubmit(cotizacion);
            }
            else
            {
                cotizacion.idAlmacen = idAlmacen;
            }

            foreach (var d in productos)
            {
                var detalle = db.tblCotizacionDetalle.FirstOrDefault(x => x.idCotizacion == idCotizacion && x.id == d.idDetalle);

                if (detalle != null)
                {
                    detalle.producto = d.nombre;
                    detalle.comentarios = d.descripcion;
                    detalle.cantidad = d.cantidad;
                    detalle.fechaInicio = d.fechaInicio;
                    detalle.fechaTermino = d.fechaTermino;
                    detalle.idTipo = d.idTipo;
                    detalle.justificacion = d.justificacion;
                    detalle.idUnidad = d.idUnidad;
                    detalle.vigencia = d.vigencia;
                }
                else
                {
                    detalle = new tblCotizacionDetalle();
                    detalle.producto = d.nombre;
                    detalle.comentarios = d.descripcion;
                    detalle.cantidad = d.cantidad;
                    if (d.idTipo == 1)
                    {
                        detalle.fechaInicio = null;
                        detalle.fechaTermino = null;
                    }
                    else
                    {
                        detalle.fechaInicio = d.fechaInicio;
                        detalle.fechaTermino = d.fechaTermino;
                    }

                    detalle.idTipo = d.idTipo;
                    detalle.justificacion = d.justificacion;
                    detalle.idUnidad = d.idUnidad;
                    detalle.vigencia = d.vigencia;
                    detalle.tblCotizacion = cotizacion;
                    db.tblCotizacionDetalle.InsertOnSubmit(detalle);
                }

                var especificaciones = db.tblCotizacionDetalleEspecificacion.Where(x => x.idCotizacionDetalle == d.idDetalle);
                db.tblCotizacionDetalleEspecificacion.DeleteAllOnSubmit(especificaciones);
                var archivos = db.tblCotizacionDetalleArchivos.Where(x => x.idCotizacionDetalle == d.idDetalle);
                db.tblCotizacionDetalleArchivos.DeleteAllOnSubmit(archivos);

                //Agrega las especificaciones
                foreach (DataRow row in d.Especificaciones.Rows)
                {
                    if (!bool.Parse(row["Eliminado"].ToString()))
                    {
                        var espec = new tblCotizacionDetalleEspecificacion();
                        espec.tblCotizacionDetalle = detalle;
                        espec.idEspecificacion = int.Parse(row["Id"].ToString());
                        espec.especificacion = row["Especificacion"].ToString();
                        db.tblCotizacionDetalleEspecificacion.InsertOnSubmit(espec);
                    }
                }
            }

            db.SubmitChanges();

            foreach (var d in productos)
            {
                //GUARDA LOS ARCHIVOS
                string ruta = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/Archivos/Cotizacion/{0}/", cotizacion.id));
                string rutaWeb = String.Format("/Archivos/Cotizacion/{0}/", cotizacion.id);
                foreach (DataRow row in d.Archivos.Rows)
                {
                    if (!bool.Parse(row["Eliminado"].ToString()))
                    {
                        var archivo = new tblCotizacionDetalleArchivos();
                        archivo.idCotizacionDetalle = d.idDetalle;
                        archivo.rutaArchivo = rutaWeb + row["Nombre"].ToString();
                        archivo.usuarioCarga = usuario;
                        archivo.fechaCarga = DateTime.Now;
                        archivo.nombreArchivo = row["Nombre"].ToString();
                        db.tblCotizacionDetalleArchivos.InsertOnSubmit(archivo);

                        //Guarda archivo
                        if (!String.IsNullOrEmpty(row["FilePath"].ToString()))
                        {
                            //valida que exista la carpeta
                            if (!Directory.Exists(ruta))
                                Directory.CreateDirectory(ruta);

                            System.IO.FileStream newFile = new System.IO.FileStream(ruta + row["Nombre"].ToString(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                            byte[] fileContent = File.ReadAllBytes(row["FilePath"].ToString());

                            newFile.Write(fileContent, 0, fileContent.Length);
                            newFile.Close();
                        }
                    }
                }
            }
            db.SubmitChanges();

            return folio;
        }

        public int GuardaCotizacion(int idCotizacion, int anio, int mes, int idSecretaria, int idDireccion, int idSubactividad, int idDetalle, string nombre, string descripcion, DataTable dtEspecificaciones, DataTable dtArchivos, int empleado, int idTipo, decimal? cantidad, int idUnidad, DateTime fechaInicio, DateTime fechaTermino, string vigencia, string justificacion, int idAlmacen)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext();
            string usuario = Helper.Helper.NombreEmpleado(empleado);
            string nombrePuesto = Helper.Helper.PuestoEmpleado(empleado);

            int folio = 1;
            try { folio = db.tblCotizacion.Max(x => x.folio).Value + 1; } catch { }

            var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == idCotizacion);
            if (cotizacion == null)
            {
                cotizacion = new tblCotizacion();
                cotizacion.anio = anio;
                cotizacion.estatus = "Borrador";
                cotizacion.fechaEnvio = DateTime.Now;
                cotizacion.folio = folio;
                cotizacion.idDireccion = idDireccion;
                cotizacion.idSecretaria = idSecretaria;
                cotizacion.idSubActividad = idSubactividad;
                cotizacion.mes = mes;
                cotizacion.idAlmacen = idAlmacen;
                cotizacion.usuarioEnvio = usuario;
                cotizacion.puestoEnvio = nombrePuesto;
                db.tblCotizacion.InsertOnSubmit(cotizacion);
            }
            else
            {
                cotizacion.idAlmacen = idAlmacen;
            }


            var detalle = db.tblCotizacionDetalle.FirstOrDefault(x => x.idCotizacion == idCotizacion && x.id == idDetalle);

            if (detalle != null)
            {
                detalle.producto = nombre;
                detalle.comentarios = descripcion;
                if (idTipo == 1)
                {
                    detalle.cantidad = cantidad;
                    detalle.idUnidad = idUnidad;
                }
                else
                {
                    detalle.fechaInicio = fechaInicio;
                    detalle.fechaTermino = fechaTermino;
                    detalle.vigencia = vigencia;
                }
                detalle.idTipo = idTipo;
                detalle.justificacion = justificacion;

            }
            else
            {
                detalle = new tblCotizacionDetalle();
                detalle.producto = nombre;
                detalle.comentarios = descripcion;
                detalle.cantidad = cantidad;
                if (idTipo == 1)
                {
                    detalle.fechaInicio = null;
                    detalle.fechaTermino = null;
                }
                else
                {
                    detalle.fechaInicio = fechaInicio;
                    detalle.fechaTermino = fechaTermino;
                }

                detalle.idTipo = idTipo;
                detalle.justificacion = justificacion;
                detalle.idUnidad = idUnidad;
                detalle.vigencia = vigencia;
                detalle.tblCotizacion = cotizacion;
                db.tblCotizacionDetalle.InsertOnSubmit(detalle);
            }

            var especificaciones = db.tblCotizacionDetalleEspecificacion.Where(x => x.idCotizacionDetalle == idDetalle);
            db.tblCotizacionDetalleEspecificacion.DeleteAllOnSubmit(especificaciones);
            var archivos = db.tblCotizacionDetalleArchivos.Where(x => x.idCotizacionDetalle == idDetalle);
            db.tblCotizacionDetalleArchivos.DeleteAllOnSubmit(archivos);

            //Agrega las especificaciones
            if (dtEspecificaciones != null)
            {
                foreach (DataRow row in dtEspecificaciones.Rows)
                {
                    if (!bool.Parse(row["Eliminado"].ToString()))
                    {
                        var espec = new tblCotizacionDetalleEspecificacion();
                        espec.tblCotizacionDetalle = detalle;
                        espec.idEspecificacion = int.Parse(row["Id"].ToString());
                        espec.especificacion = row["Especificacion"].ToString();
                        db.tblCotizacionDetalleEspecificacion.InsertOnSubmit(espec);
                    }
                }
            }

            db.SubmitChanges();

            //GUARDA LOS ARCHIVOS
            if (dtArchivos != null)
            {
                string ruta = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/Archivos/Cotizacion/{0}/", cotizacion.id));
                string rutaWeb = String.Format("/Archivos/Cotizacion/{0}/", cotizacion.id);
                foreach (DataRow row in dtArchivos.Rows)
                {
                    if (!bool.Parse(row["Eliminado"].ToString()))
                    {
                        var archivo = new tblCotizacionDetalleArchivos();
                        archivo.idCotizacionDetalle = detalle.id;
                        archivo.rutaArchivo = rutaWeb + row["Nombre"].ToString();
                        archivo.usuarioCarga = usuario;
                        archivo.fechaCarga = DateTime.Now;
                        archivo.nombreArchivo = row["Nombre"].ToString();
                        db.tblCotizacionDetalleArchivos.InsertOnSubmit(archivo);

                        //Guarda archivo
                        if (!String.IsNullOrEmpty(row["FilePath"].ToString()))
                        {
                            //valida que exista la carpeta
                            if (!Directory.Exists(ruta))
                                Directory.CreateDirectory(ruta);

                            System.IO.FileStream newFile = new System.IO.FileStream(ruta + row["Nombre"].ToString(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                            byte[] fileContent = File.ReadAllBytes(row["FilePath"].ToString());

                            newFile.Write(fileContent, 0, fileContent.Length);
                            newFile.Close();
                        }
                    }
                }

                db.SubmitChanges();
            }

            return folio;
        }

        public void SolicitarAutorizacionControlAdministrativo(List<int> cotizaciones, string usuario)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            string nombreEmpl = Helper.Helper.NombreEmpleado(int.Parse(usuario));
            string nombrePuesto = Helper.Helper.PuestoEmpleado(int.Parse(usuario));

            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                if (!(cotizacion.estatus == "Borrador" || cotizacion.estatus == "Complementar"))
                    throw new Exception(String.Format("La cotizacion con folio {0} tiene el estatus {1}, no se puede cambiar.", cotizacion.folio, cotizacion.estatus));

                cotizacion.estatus = "Pendiente autorización";

                var historico = new tblHistoricoEstatusCotizacion();
                historico.idCotizacion = id;
                historico.fechaUsuario = DateTime.Now;
                historico.estatus = "Autorizacion pendiente por Control Administrativo";
                historico.usuario = nombreEmpl;
                db.tblHistoricoEstatusCotizacion.InsertOnSubmit(historico);

                var log = new Logs();
                log.clave_empl = int.Parse(usuario);
                log.fecha = DateTime.Now;
                log.Log = String.Format("El empleado {0} con puesto {1} solicito la autorizacion de la cotizacion con folio {2} por parte del Control administrativo.", nombreEmpl, nombrePuesto, cotizacion.folio);

                var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();

                string mensajeCorreo = String.Format("El empleado {0} con puesto {1} ha solicitado la validacion de la cotizacion con folio {2}.", nombreEmpl, nombrePuesto, cotizacion.folio);

                string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                                   join u in db.Usuarios on d.empleado equals u.Clave_empl
                                                   where ((d.idDireccion == cotizacion.idDireccion
                                                       && d.idSecretaria == cotizacion.idSecretaria)
                                                         || d.idDireccion == 245)
                                                   select u.correo));

                notificacionHelper.EnviaCorreo(correos, "Autorizar Cotizacion ", mensajeCorreo);
            }

            db.SubmitChanges();
        }

        public void EliminaCotizacion(int idCotizacion)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext();
            var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == idCotizacion);
            cotizacion.estatus = "Eliminada";
            db.SubmitChanges();
        }

        public string GeneraDocumento(int idCotizacion)
        {
            try
            {
                //Inserta el registro del oficio
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == idCotizacion);

                string PATH_FILES = String.Format("/Archivos/Formatos/Cotizacion/");
                string urlFile = HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("C-{0}.pdf", cotizacion.folio.ToString().PadLeft(5, '0')));
                if (!File.Exists(urlFile))
                {
                    StreamReader fil = File.OpenText(HttpContext.Current.Server.MapPath("~/FormatoHTML/OficioCotizacion.html"));
                    String body = fil.ReadToEnd();
                    fil.Close();

                    var direccion = db.Direcciones.FirstOrDefault(x => x.IdDireccion == cotizacion.idDireccion.ToString() && x.IdSecretaria == cotizacion.idSecretaria.ToString());
                    var secretaria = db.Secretarias.FirstOrDefault(x => x.IdSecretaria == cotizacion.idSecretaria);
                    var subAct = db.Tbl_SubActividad.FirstOrDefault(x => x.Id == cotizacion.idSubActividad);
                    var act = db.Concentrado_Pmd.FirstOrDefault(x => x.ID == subAct.Id_Linea);
                    var cotizacionDetalle = cotizacion.tblCotizacionDetalle.FirstOrDefault();
                    var unidad = db.tblCatalogoUnidad.FirstOrDefault(x => x.id == cotizacionDetalle.idUnidad);
                    var almacen = db.TblAlmacen.FirstOrDefault(x => x.id == cotizacion.idAlmacen);

                    //Carga informacion del Certificado
                    body = body.Replace("<%FECHA%>", cotizacion.fechaEnvio.Value.ToString("dd ' de ' MMMM ' del ' yyyy"));
                    body = body.Replace("<%DIRECCION%>", direccion.Nombr_direccion);
                    body = body.Replace("<%SECRETARIA%>", String.Format("{0}", secretaria.Nombr_secretaria));
                    body = body.Replace("<%NOMBRE_DESCRIPCION%>", String.Format("{0}", cotizacionDetalle.producto));
                    body = body.Replace("<%NOMBRE%>", cotizacion.usuarioEnvio);
                    body = body.Replace("<%PUESTO%>", cotizacion.puestoEnvio);
                    body = body.Replace("<%UNIDAD_SOLICITANTE%>", String.Format("{0} ({1}-{2})", direccion.Nombr_direccion, secretaria.IdSecretaria, direccion.IdDireccion));
                    body = body.Replace("<%JUSTIFICACION%>", cotizacionDetalle.justificacion);
                    body = body.Replace("<%COMENTARIOS%>", cotizacionDetalle.comentarios);
                    body = body.Replace("<%SECRETARIA%>", secretaria.Nombr_secretaria);

                    //ESPECIFICACIONES
                    var especificaciones = cotizacionDetalle.tblCotizacionDetalleEspecificacion;
                    StringBuilder sb = new StringBuilder();
                    if (especificaciones.Count > 0)
                    {
                        sb.Append("<table cellpading=\"0\" cellspacing=\"0\" class=\"tabla-especificaciones\"><thead><tr><th><b>ID</b></th><th><b>Tipo</b></th><th><b>Especificación</b></th></tr></thead><tbody>");
                        int i = 1;
                        foreach (tblCotizacionDetalleEspecificacion esp in especificaciones)
                        {
                            sb.Append(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><tr>", 1, esp.tblCatalogoEspecificacion.nombre, esp.especificacion));
                            i++;
                        }
                        sb.Append("</tbody></table>");
                    }
                    else
                        sb.Append("<h3>No hay especificaciones</h3>");

                    body = body.Replace("<%ESPECIFICACIONES%>", sb.ToString());
                    body = body.Replace("<%LUGAR_ENTREGA%>", almacen.nombre);

                    StringBuilder sbInfoExtra = new StringBuilder();
                    if (cotizacionDetalle.idTipo == 1)
                    {
                        //PRODUCTO
                        sbInfoExtra.Append("<tr><td><br /><br /></td></tr>");
                        sbInfoExtra.Append(String.Format("<tr><td><b>Cantidad: </b>{0} {1}</td></tr>", cotizacionDetalle.cantidad, unidad.nombre));
                        sbInfoExtra.Append("<tr><td><br /><br /></td></tr>");

                    }
                    else
                    {
                        //SERVICIO
                        sbInfoExtra.Append(String.Format("<tr><td><b>Vigencia: </b>{0}</td></tr>", cotizacionDetalle.vigencia));
                        sbInfoExtra.Append("<tr><td><br /><br /></td></tr>");
                        sbInfoExtra.Append(String.Format("<tr><td><b>Inicio: </b>{0:dd ' de ' MMMM ' del ' yyyy}</td></tr>", cotizacionDetalle.fechaInicio));
                        sbInfoExtra.Append("<tr><td><br /><br /></td></tr>");
                        sbInfoExtra.Append(String.Format("<tr><td><b>Terminación: </b>{0:dd ' de ' MMMM ' del ' yyyy}</td></tr>", cotizacionDetalle.fechaTermino));
                    }
                    body = body.Replace("<%INFORMACION_ESPECIFICA%>", sbInfoExtra.ToString());

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
                           = new System.IO.FileStream(HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("C-{0}.pdf", cotizacion.folio.ToString().PadLeft(5, '0'))),
                                                      FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    newFile.Write(pdfStream.ToArray(), 0, pdfStream.ToArray().Length);
                    newFile.Dispose();
                }

                return String.Format("~/Archivos/Formatos/Cotizacion/C-{0}.pdf", cotizacion.folio.ToString().PadLeft(5, '0'));
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void AutorizaCotizacionControlAdministrativo(List<int> cotizaciones, string usuario)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            string nombreEmpl = Helper.Helper.NombreEmpleado(int.Parse(usuario));
            string nombrePuesto = Helper.Helper.PuestoEmpleado(int.Parse(usuario));

            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                cotizacion.estatus = "Validada";

                var historico = new tblHistoricoEstatusCotizacion();
                historico.idCotizacion = id;
                historico.fechaUsuario = DateTime.Now;
                historico.estatus = "Validada por Control Administrativo";
                historico.usuario = nombreEmpl;
                db.tblHistoricoEstatusCotizacion.InsertOnSubmit(historico);

                var log = new Logs();
                log.clave_empl = int.Parse(usuario);
                log.fecha = DateTime.Now;
                log.Log = String.Format("El empleado {0} con puesto {1} autorizo la cotizacion con folio {2}.", nombreEmpl, nombrePuesto, cotizacion.folio);

                var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();

                string mensajeCorreo = String.Format("Control Administrativo ha validado la cotizacion con folio {0}.", cotizacion.folio);

                string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                                   join u in db.Usuarios on d.empleado equals u.Clave_empl
                                                   where ((d.idDireccion == cotizacion.idDireccion
                                                       && d.idSecretaria == cotizacion.idSecretaria)
                                                         || d.idDireccion == 245)
                                                   select u.correo));

                notificacionHelper.EnviaCorreo(correos, "Cotizacion rechazada", mensajeCorreo);
            }

            db.SubmitChanges();
        }

        public void RechazaCotizacionControlAdministrativo(List<int> cotizaciones, int idMotivo, string comentarios, int empleado)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            string nombreEmpl = Helper.Helper.NombreEmpleado(empleado);
            string nombrePuesto = Helper.Helper.PuestoEmpleado(empleado);

            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                cotizacion.estatus = "Rechazada por Control Administrativo";

                var historico = new tblHistoricoEstatusCotizacion();
                historico.comentario = comentarios;
                historico.fechaUsuario = DateTime.Now;
                historico.idCotizacion = id;
                historico.idMotivo = idMotivo;
                historico.usuario = nombreEmpl;
                historico.estatus = "Rechazada por Control Administrativo";
                db.tblHistoricoEstatusCotizacion.InsertOnSubmit(historico);

                var log = new Logs();
                log.clave_empl = empleado;
                log.fecha = DateTime.Now;
                log.Log = String.Format("El empleado {0} con puesto {1} rechazo la cotizacion con folio {2}.", nombreEmpl, nombrePuesto, cotizacion.folio);


                var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();
                string mensajeCorreo = String.Format("Control Administrativo ha rechazado su cotizacion con folio {0}.", cotizacion.folio);

                string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                                   join u in db.Usuarios on d.empleado equals u.Clave_empl
                                                   where d.idDireccion == cotizacion.idDireccion
                                                       && d.idSecretaria == cotizacion.idSecretaria
                                                   select u.correo));

                notificacionHelper.EnviaCorreo(correos, "Cotizacion rechazada", mensajeCorreo);
            }

            db.SubmitChanges();
        }

        public void RechazaCotizacionAdquisicioens(List<int> cotizaciones, int idMotivo, string comentarios, int empleado)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            string nombreEmpl = Helper.Helper.NombreEmpleado(empleado);
            string nombrePuesto = Helper.Helper.PuestoEmpleado(empleado);

            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                cotizacion.estatus = "Rechazada por Adquisiciones";

                var historico = new tblHistoricoEstatusCotizacion();
                historico.comentario = comentarios;
                historico.fechaUsuario = DateTime.Now;
                historico.idCotizacion = id;
                historico.idMotivo = idMotivo;
                historico.usuario = nombreEmpl;
                historico.estatus = "Rechazada por Adquisiciones";
                db.tblHistoricoEstatusCotizacion.InsertOnSubmit(historico);

                var log = new Logs();
                log.clave_empl = empleado;
                log.fecha = DateTime.Now;
                log.Log = String.Format("El empleado {0} con puesto {1} rechazo la cotizacion con folio {2}.", nombreEmpl, nombrePuesto, cotizacion.folio);

                var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();
                string mensajeCorreo = String.Format("El departamento de Adquisiciones ha Rechazado su cotizacion con folio {0}.", cotizacion.folio);

                string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                                   join u in db.Usuarios on d.empleado equals u.Clave_empl
                                                   where d.idDireccion == cotizacion.idDireccion
                                                       && d.idSecretaria == cotizacion.idSecretaria
                                                   select u.correo));

                notificacionHelper.EnviaCorreo(correos, "Cotizacion rechazada por adquisiciones", mensajeCorreo);
            }

            db.SubmitChanges();
        }

        public void AutorizaCotizacionAdquisiciones(List<int> cotizaciones, string usuario)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            string nombreEmpl = Helper.Helper.NombreEmpleado(int.Parse(usuario));
            string nombrePuesto = Helper.Helper.PuestoEmpleado(int.Parse(usuario));
            int folio = 1;
            try { folio = db.tblRequisiciones.Max(x => x.folio).Value + 1; } catch { }

            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                cotizacion.estatus = "Autorizada Adquisicioens";

                var requisicion = new tblRequisiciones();
                requisicion.año = cotizacion.anio;
                requisicion.estatus = "Autorizada";
                requisicion.fechaAutorizado = DateTime.Now;
                requisicion.folio = folio;
                requisicion.idDireccion = cotizacion.idDireccion;
                requisicion.idSecretaria = cotizacion.idSecretaria;
                //requisicion.
                folio++;

                var historico = new tblHistoricoEstatusCotizacion();
                historico.comentario = "";
                historico.fechaUsuario = DateTime.Now;
                historico.idCotizacion = id;
                historico.usuario = nombreEmpl;
                historico.estatus = "Autorizada por Adquisiciones";
                db.tblHistoricoEstatusCotizacion.InsertOnSubmit(historico);

                var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();
                string mensajeCorreo = String.Format("El departamento de Adquisiciones ha autorizado su cotizacion con folio {0}.", cotizacion.folio);

                string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                                   join u in db.Usuarios on d.empleado equals u.Clave_empl
                                                   where d.idDireccion == cotizacion.idDireccion
                                                       && d.idSecretaria == cotizacion.idSecretaria
                                                   select u.correo));

                notificacionHelper.EnviaCorreo(correos, "Cotizacion autorizada por adquisiciones", mensajeCorreo);
            }

            db.SubmitChanges();
        }

        public void HabilitarRequisicion(List<int> cotizaciones, string usuario)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext();
            string nombreEmpl = Helper.Helper.NombreEmpleado(int.Parse(usuario));
            string nombrePuesto = Helper.Helper.PuestoEmpleado(int.Parse(usuario));
            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                if (cotizacion.estatus != "Documento seleccionado")
                    throw new Exception(String.Format("La cotizacion con folio {0} tien el estatus {1}, no se puede cambiar.", cotizacion.folio, cotizacion.estatus));

                cotizacion.estatus = "Solicitud de Requisicion Habilitada";

                var historico = new tblHistoricoEstatusCotizacion();
                historico.comentario = "";
                historico.fechaUsuario = DateTime.Now;
                historico.idCotizacion = id;
                historico.usuario = nombreEmpl;
                historico.estatus = "Solicitud de Requisicion Habilitada";
                db.tblHistoricoEstatusCotizacion.InsertOnSubmit(historico);
            }

            db.SubmitChanges();

            var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();
            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                string mensajeCorreo = String.Format("Se ha autorizado la solicitud de Requisicion de su cotizacion con folio {0}", cotizacion.folio);

                string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                                   join u in db.Usuarios on d.empleado equals u.Clave_empl
                                                   where ((d.idDireccion == cotizacion.idDireccion
                                                   && d.idSecretaria == cotizacion.idSecretaria))
                                                   select u.correo));

                notificacionHelper.EnviaCorreo(correos, "Solicitud de Requisicion Autorizada", mensajeCorreo);
                notificacionHelper.InsertaNotificacion(cotizacion.idSecretaria.ToString(), cotizacion.idDireccion.ToString(), usuario.ToString(), mensajeCorreo, "");
            }
        }

        public void AgregaComentarioCotizacion(int idCotizacionDetalle, int empleado, string mensaje)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext();
            var comentario = new tblCotizacionDetalleComentarios();
            comentario.claveEmpleadoEnvia = empleado.ToString();
            comentario.comentario = mensaje;
            comentario.fechaEnvio = DateTime.Now;
            comentario.idCotizacionDetalle = idCotizacionDetalle;
            db.tblCotizacionDetalleComentarios.InsertOnSubmit(comentario);
            db.SubmitChanges();

            var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();
            var cotizacionDetalle = db.tblCotizacionDetalle.FirstOrDefault(x => x.id == idCotizacionDetalle);
            var usuario = db.Usuarios.FirstOrDefault(x => x.Clave_empl == empleado);
            string correo = usuario.correo;
            if (!String.IsNullOrEmpty(correo))
            {

                string mensajeCorreo = String.Format("El usuario {0} ha agregado el siguiente comentario en el producto {1} de la cotizacion con folio {2}. <br />Comentario:<br />{3}",
                    usuario.Nombr_empl, cotizacionDetalle.producto, cotizacionDetalle.tblCotizacion.folio, mensaje);

                string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                                   join u in db.Usuarios on d.empleado equals u.Clave_empl
                                                   where ((d.idDireccion == cotizacionDetalle.tblCotizacion.idDireccion
                                                   && d.idSecretaria == cotizacionDetalle.tblCotizacion.idSecretaria)
                                                   || d.idDireccion == 245)
                                                   select u.correo));

                notificacionHelper.EnviaCorreo(correos, "Nuevo comentario cotizacion", mensajeCorreo);
            }

            string mensajeNotificacion = String.Format("El usuario {0} ha agregado un comentario en el producto {1} de la cotizacion con folio {2}",
                   usuario.Nombr_empl, cotizacionDetalle.producto, cotizacionDetalle.tblCotizacion.folio);
            notificacionHelper.InsertaNotificacion(cotizacionDetalle.tblCotizacion.idSecretaria.ToString(), cotizacionDetalle.tblCotizacion.idDireccion.ToString(), empleado.ToString(), mensajeNotificacion, "");
        }

        public void ComplementarCotizacionAdquisiciones(List<int> cotizaciones, int usuario, string mensaje)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            string nombreEmpl = Helper.Helper.NombreEmpleado(usuario);

            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                cotizacion.estatus = "Complementar";

                var historico = new tblHistoricoEstatusCotizacion();
                historico.idCotizacion = id;
                historico.fechaUsuario = DateTime.Now;
                historico.estatus = "Complementar";
                historico.usuario = nombreEmpl;
                historico.comentario = mensaje;
                db.tblHistoricoEstatusCotizacion.InsertOnSubmit(historico);
            }
            db.SubmitChanges();

            var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();
            foreach (var id in cotizaciones)
            {
                var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
                var empleado = db.Usuarios.FirstOrDefault(x => x.Clave_empl == usuario);
                string correo = empleado.correo;
                if (!String.IsNullOrEmpty(correo))
                {

                    string mensajeCorreo = String.Format("Se ha solicitado Complementar la Cotizacion con folio {0}.", cotizacion.folio);
                    string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                                       join u in db.Usuarios on d.empleado equals u.Clave_empl
                                                       where ((d.idDireccion == cotizacion.idDireccion
                                                       && d.idSecretaria == cotizacion.idSecretaria)
                                                       || d.idDireccion == 245)
                                                       select u.correo));

                    notificacionHelper.EnviaCorreo(correos, "Nuevo comentario cotizacion", mensajeCorreo);
                }

                string mensajeNotificacion = String.Format("Se ha solicitado Complementar la Cotizacion con folio {0}.", cotizacion.folio);
                notificacionHelper.InsertaNotificacion(cotizacion.idSecretaria.ToString(), cotizacion.idDireccion.ToString(), usuario.ToString(), mensajeNotificacion, "");
            }
        }

        public void GuardaDocumentosCotizacion(int idCotizacion, DataTable dtDocumentos, int empleado)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext();
            string usuario = Helper.Helper.NombreEmpleado(empleado);
            string nombrePuesto = Helper.Helper.PuestoEmpleado(empleado);
            var archivos = db.tblCotizacionDocumentos.Where(x => x.idCotizacion == idCotizacion);
            db.tblCotizacionDocumentos.DeleteAllOnSubmit(archivos);

            //GUARDA LOS ARCHIVOS
            string ruta = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/Archivos/Cotizacion/Documentos/{0}/", idCotizacion));
            string rutaWeb = String.Format("/Archivos/Cotizacion/Documentos/{0}/", idCotizacion);
            foreach (DataRow row in dtDocumentos.Rows)
            {
                if (!bool.Parse(row["Eliminado"].ToString()))
                {
                    var documento = new tblCotizacionDocumentos();
                    documento.idCotizacion = idCotizacion;
                    documento.rutaArchivo = rutaWeb + row["Nombre"].ToString();
                    documento.usuarioCarga = usuario;
                    documento.fechaCarga = DateTime.Now;
                    documento.nombreArchivo = row["Nombre"].ToString();
                    db.tblCotizacionDocumentos.InsertOnSubmit(documento);

                    //Guarda archivo
                    if (!String.IsNullOrEmpty(row["FilePath"].ToString()))
                    {
                        //valida que exista la carpeta
                        if (!Directory.Exists(ruta))
                            Directory.CreateDirectory(ruta);

                        System.IO.FileStream newFile = new System.IO.FileStream(ruta + row["Nombre"].ToString(), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                        byte[] fileContent = File.ReadAllBytes(row["FilePath"].ToString());

                        newFile.Write(fileContent, 0, fileContent.Length);
                        newFile.Close();
                    }
                }
            }
            db.SubmitChanges();
        }

        public void GuardaSeleccionDocumento(int idCotizacion, int idDocumento, int empleado)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext();
            string usuario = Helper.Helper.NombreEmpleado(empleado);
            string nombrePuesto = Helper.Helper.PuestoEmpleado(empleado);

            var documento = db.tblCotizacionDocumentos.FirstOrDefault(x => x.id == idDocumento && x.idCotizacion == idCotizacion);
            documento.usuarioSeleccioando = usuario;
            documento.fechaSeleccionado = DateTime.Now;
            documento.seleccionado = true;

            var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == idCotizacion);
            cotizacion.estatus = "Documento seleccionado";

            var historico = new tblHistoricoEstatusCotizacion();
            historico.comentario = "";
            historico.fechaUsuario = DateTime.Now;
            historico.idCotizacion = idCotizacion;
            historico.usuario = usuario;
            historico.estatus = "Documento seleccionado";
            db.tblHistoricoEstatusCotizacion.InsertOnSubmit(historico);

            db.SubmitChanges();

            var notificacionHelper = new IntelipolisEngine.Helper.NotificationHelper();
            string mensajeCorreo = String.Format("El usuario {0} ha seleccionado el documento de cotizacion de los propuestos por adquisiciones.", usuario);

            string correos = String.Join(",", (from d in db.tblUsuariosDireccion
                                               join u in db.Usuarios on d.empleado equals u.Clave_empl
                                               where d.idDireccion == 245
                                               select u.correo));

            notificacionHelper.EnviaCorreo(correos, "Documento propuesto seleccionado", mensajeCorreo);

        }

        public class InfoCotizacion
        {
            public int IdCotizacion { get; set; }
            public int Folio { get; set; }
            public string Estatus { get; set; }
            public string Comentarios { get; set; }
            public string Motivo { get; set; }
        }

        public InfoCotizacion ObtieneEstatusCotizacion(int id)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            var info = db.tblHistoricoEstatusCotizacion.Where(x => x.idCotizacion == id).OrderByDescending(x => x.fechaUsuario).FirstOrDefault();
            InfoCotizacion infoCotizacion = new InfoCotizacion();
            infoCotizacion.Comentarios = info.comentario;
            infoCotizacion.Estatus = info.estatus;
            infoCotizacion.IdCotizacion = id;
            var cotizacion = db.tblCotizacion.FirstOrDefault(x => x.id == id);
            infoCotizacion.Folio = cotizacion.folio.Value;
            if (info.idMotivo != null && info.idMotivo > 0)
                infoCotizacion.Motivo = db.tblMotivosRechazoSolicitudRequisicion.FirstOrDefault(x => x.id == info.idMotivo).nombre;

            return infoCotizacion;
        }
    }
}
