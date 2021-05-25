using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using  IntelipolisEngine.DataBase;
using SelectPdf;

namespace IntelipolisEngine.PMD
{
    public class Requisiciones
    {
        public class SolicitudRequisicion
        {
            public int idSolicitud { get; set; }
        }

        public void AutorizaRequisicion(List<int> solicitudes , int idProveedor, int secretaria, int direccion, int anio, int mes, string usuario)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            int folio = 1;
            try { folio = db.tblRequisiciones.Max(x => x.folio).Value + 1; } catch { }

            foreach (var idSol in solicitudes)
            {
                var solicitud = db.tblSolicitudRequisicion.FirstOrDefault(x => x.id == idSol);
                var autorizacion = new DataBase.tblRequisiciones();
                autorizacion.año = solicitud.año;
                autorizacion.estatus = "Autorizada";
                autorizacion.fechaAutorizado = DateTime.Now;
                
                autorizacion.folio = folio;
                autorizacion.idAlmacen = solicitud.idAlmacen;
                autorizacion.idDireccion = solicitud.idDireccion;
                autorizacion.idSecretaria = solicitud.idSecretaria;
                autorizacion.idSubActividad = solicitud.idSubActividad;
                autorizacion.mes = solicitud.mes;
                autorizacion.idProveedor = idProveedor;
                autorizacion.usuarioAutorizado = db.Usuarios.FirstOrDefault(x => x.Clave_empl == int.Parse(usuario)).Nombr_empl; 
                autorizacion.idSolicitudRequisicion = solicitud.id;
                db.tblRequisiciones.InsertOnSubmit(autorizacion);

                foreach (var solDetalle in solicitud.tblSolicitudRequisicionDetalle)
                {
                    var detalle = new DataBase.tblRequisicionesDetalle();
                    detalle.idSolicitudRequisicionDetalle = solDetalle.id;
                    detalle.cantidad = solDetalle.cantidad;
                    detalle.estatus = "Autorizada";
                    detalle.idMaterial = solDetalle.idMaterial;
                    detalle.material = solDetalle.material;
                    detalle.tipo = solDetalle.tipo;
                    detalle.cantidad = solDetalle.cantidad;
                    detalle.iva = solDetalle.iva;
                    detalle.precioUnitario = solDetalle.precioUnitario;
                    detalle.total = solDetalle.total;
                    detalle.unidad = solDetalle.unidad;
                    detalle.tblRequisiciones = autorizacion;

                    string claveGastos = "";
                    if (solDetalle.tipo == 1)
                    {
                        //Cuando es contrato
                        var contrato = db.cat_contratos.FirstOrDefault(x => x.id == solDetalle.idMaterial);
                        detalle.codigoContrato = contrato.codigo_contrato;
                        detalle.contrato = contrato.nombre_contrato;
                        claveGastos = contrato.clave_gastos;
                    }
                    else if (solDetalle.tipo == 2)
                    {
                        //Cuando es Requisicion
                        var requi = db.cat_requisiciones.FirstOrDefault(x => x.id_requisicion == solDetalle.idMaterial);
                        claveGastos = requi.clave_gastos;
                    }
                    else if (solDetalle.tipo == 3)
                    {
                        //Cuando es Orden de Servicio
                        var ordenServicio = db.cat_ordenservicio.FirstOrDefault(x => x.id_os == solDetalle.idMaterial);
                        claveGastos = ordenServicio.clave_gastos;
                    }

                    //Actualiza la solicitud
                    solicitud.estatus = "Autorizada";

                    ////Actualiza el presupuesto
                    var presupuesto = db.tblPresupuestoDireccionClaveGasto.FirstOrDefault(x => x.idSecretaria == secretaria && x.idDireccion == direccion && x.mes == mes && x.año == anio && x.claveGastos == int.Parse(claveGastos));
                    //if (presupuesto.presupuestoLibre < solDetalle.total)
                    //    throw new Exception(String.Format("El material {0} perteneciente a la solicitud {1} no se puede solicitar, se sobrepasa el presupuesto.", solDetalle.material, solicitud.id));

                    //presupuesto.presupuestoLibre = (presupuesto.presupuestoLibre - solicitud.total);
                    presupuesto.presupuestoComprometido = (presupuesto.presupuestoComprometido + solDetalle.total);
                    presupuesto.presupuestoPreComprometido = (presupuesto.presupuestoPreComprometido - solDetalle.total);

                    db.tblRequisicionesDetalle.InsertOnSubmit(detalle);
                }

                folio++;
            }

            db.SubmitChanges();
        }

        public void RechazaRequisicion(List<int> solicitudes, int idMotivo, string comentarios, int empleado)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            

            foreach (var idSol in solicitudes)
            {
                var solicitud = db.tblSolicitudRequisicion.FirstOrDefault(x => x.id == idSol);

                var rechazo = new tblHistoricoRechazosSolicitudRequisicion();
                rechazo.idSolicitud = solicitud.id;
                rechazo.comentarios = comentarios;
                rechazo.fechaRechazo = DateTime.Now;
                rechazo.idMotivoRechazo = idMotivo;
                rechazo.usuarioRechazo = db.Usuarios.FirstOrDefault(x => x.Clave_empl == empleado).Nombr_empl;
                db.tblHistoricoRechazosSolicitudRequisicion.InsertOnSubmit(rechazo);

                solicitud.estatus = "Rechazada";
                foreach (var solDetalle in solicitud.tblSolicitudRequisicionDetalle)
                {
                    string claveGastos = "";
                    if (solDetalle.tipo == 1)
                    {
                        //Cuando es contrato
                        var contrato = db.cat_contratos.FirstOrDefault(x => x.id == solDetalle.idMaterial);
                        claveGastos = contrato.clave_gastos;
                    }
                    else if (solDetalle.tipo == 2)
                    {
                        //Cuando es Requisicion
                        var requi = db.cat_requisiciones.FirstOrDefault(x => x.id_requisicion == solDetalle.idMaterial);
                        claveGastos = requi.clave_gastos;
                    }
                    else if (solDetalle.tipo == 3)
                    {
                        //Cuando es Orden de Servicio
                        var ordenServicio = db.cat_ordenservicio.FirstOrDefault(x => x.id_os == solDetalle.idMaterial);
                        claveGastos = ordenServicio.clave_gastos;
                    }

                    ////Actualiza el presupuesto
                    var presupuesto = db.tblPresupuestoDireccionClaveGasto.FirstOrDefault(x => x.idSecretaria == solicitud.idSecretaria && x.idDireccion == solicitud.idDireccion && x.mes == solicitud.mes && x.año == solicitud.año && x.claveGastos == int.Parse(claveGastos));
                    presupuesto.presupuestoPreComprometido = (presupuesto.presupuestoPreComprometido - solDetalle.total);
                    presupuesto.presupuestoLibre = (presupuesto.presupuestoLibre + solDetalle.total);
                }
            }

            db.SubmitChanges();
        }

        //public void AutorizaRequisicion(List<SolicitudRequisicion> requisicions, string proveedor, int idAlmacen, int secretaria, int direccion, int idSubactividad, int anio, int mes, string usuario)
        //{
        //    DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
        //    var autorizacion = new DataBase.tblRequisiciones();
        //    autorizacion.año = anio;
        //    autorizacion.estatus = "Autorizada";
        //    autorizacion.fechaAutorizado = DateTime.Now;
        //    int folio = 1;
        //    try { folio = db.tblRequisiciones.Max(x => x.folio).Value + 1; } catch { }
        //    autorizacion.folio = folio;
        //    autorizacion.idAlmacen = idAlmacen;
        //    autorizacion.idDireccion = direccion;
        //    autorizacion.idSecretaria = secretaria;
        //    autorizacion.idSubActividad = idSubactividad;
        //    autorizacion.mes = mes;
        //    autorizacion.proveedor = proveedor;
        //    autorizacion.usuarioAutorizado = usuario;
        //    db.tblRequisiciones.InsertOnSubmit(autorizacion);

        //    foreach (var req in requisicions)
        //    {
        //        var solicitud = db.tblSolicitudRequisiciones.FirstOrDefault(x => x.id == req.idSolicitud);
        //        var detalle = new DataBase.tblRequisicionesDetalle();
        //        detalle.cantidad = solicitud.cantidad;
        //        detalle.estatus = "Autorizado";
        //        detalle.idMaterial = solicitud.idMaterial;
        //        detalle.idSolicitudRequisicion = solicitud.id;
        //        detalle.material = solicitud.material;
        //        detalle.tipo = solicitud.idTipo;
        //        detalle.cantidad = solicitud.cantidad;
        //        detalle.iva = solicitud.iva;
        //        detalle.precioUnitario = solicitud.precioUnitario;
        //        detalle.total = solicitud.total;
        //        detalle.unidad = solicitud.unidad;
        //        detalle.tblRequisiciones = autorizacion;

        //        string claveGastos = "";
        //        if (solicitud.idTipo == 1)
        //        {
        //            //Cuando es contrato
        //            var contrato = db.cat_contratos.FirstOrDefault(x => x.id == solicitud.idMaterial);
        //            detalle.codigoContrato = contrato.codigo_contrato;
        //            detalle.contrato = contrato.nombre_contrato;
        //            claveGastos = contrato.clave_gastos;
        //        }
        //        else if (solicitud.idTipo == 2)
        //        {
        //            //Cuando es Requisicion
        //            var requi = db.cat_requisiciones.FirstOrDefault(x => x.id_requisicion == solicitud.idMaterial);
        //            claveGastos = requi.clave_gastos;
        //        }
        //        else if (solicitud.idTipo == 3)
        //        {
        //            //Cuando es Orden de Servicio
        //            var ordenServicio = db.cat_ordenservicio.FirstOrDefault(x => x.id_os == solicitud.idMaterial);
        //            claveGastos = ordenServicio.clave_gastos;
        //        }

        //        //Actualiza la solicitud
        //        solicitud.estatus = "Autorizado";

        //        //Actualiza el presupuesto
        //        var presupuesto = db.tblPresupuestoDireccionClaveGasto.FirstOrDefault(x => x.idSecretaria == secretaria && x.idDireccion == direccion && x.mes == mes && x.año == anio && x.claveGastos == int.Parse(claveGastos));
        //        if (presupuesto.presupuestoLibre < solicitud.total)
        //            throw new Exception(String.Format("El material {0} perteneciente a la solicitud {1} no se puede solicitar, se sobrepasa el presupuesto.", solicitud.material, solicitud.id));

        //        //presupuesto.presupuestoLibre = (presupuesto.presupuestoLibre - solicitud.total);
        //        presupuesto.presupuestoComprometido = (presupuesto.presupuestoComprometido + solicitud.total);
        //        presupuesto.presupuestoPreComprometido = (presupuesto.presupuestoPreComprometido - solicitud.total);

        //        db.tblRequisicionesDetalle.InsertOnSubmit(detalle);
        //    }

        //    db.SubmitChanges();
        //}

        public class Material
        {
            public int idMaterial { get; set; }
            public decimal cantidad { get; set; }

            public int tipo { get; set; }
        }

        public void GuardaSolicitudRequisicion(List<Material> materiales, int idAlmacen, int idSecretaria, int idDireccion, int anio, int mes, int empleado, int idSubactividad, int idCotizacion)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            //Crea el heder de la solicitud
            int folio = 1;
            try { folio = db.tblSolicitudRequisicion.Max(x => x.folio).Value + 1; } catch { }
            var solicitud = new tblSolicitudRequisicion();
            solicitud.año = anio;
            solicitud.estatus = "Enviada";
            solicitud.fechaSolicitud = DateTime.Now;
            solicitud.folio = folio;
            solicitud.idAlmacen = idAlmacen;
            solicitud.idDireccion = idDireccion;
            solicitud.idSecretaria = idSecretaria;
            solicitud.idSubActividad = idSubactividad;
            solicitud.mes = mes;
            solicitud.idCotizacion = idCotizacion;
            solicitud.usuarioSolicitud = db.Usuarios.FirstOrDefault(x => x.Clave_empl == empleado).Nombr_empl;
            db.tblSolicitudRequisicion.InsertOnSubmit(solicitud);

            foreach (var mat in materiales)
            {
                var detalle = new tblSolicitudRequisicionDetalle();
                detalle.cantidad = mat.cantidad;
                detalle.estatus = "Enviada";
                detalle.idMaterial = mat.idMaterial;
                detalle.tblSolicitudRequisicion = solicitud;
                detalle.tipo = mat.tipo;

                string claveGastos = "";
                if (mat.tipo == 1)
                {
                    //Cuando es contrato
                    var contrato = db.cat_contratos.FirstOrDefault(x => x.id == mat.idMaterial);
                    detalle.codigoContrato = contrato.codigo_contrato;
                    detalle.contrato = contrato.nombre_contrato;
                    claveGastos = contrato.clave_gastos;
                    detalle.material = contrato.requerimiento;

                    detalle.precioUnitario = contrato.costoUnitario;
                    detalle.iva = contrato.iva;
                    detalle.unidad = contrato.unidad;
                    detalle.total = (contrato.total * mat.cantidad);
                }
                else if (mat.tipo == 2)
                {
                    //Cuando es Requisicion
                    var requi = db.cat_requisiciones.FirstOrDefault(x => x.id_requisicion == mat.idMaterial);
                    claveGastos = requi.clave_gastos;

                    detalle.precioUnitario = requi.costo;
                    detalle.iva = requi.porcentajeIVA;
                    detalle.unidad = requi.unidad;
                    detalle.total = (requi.total * mat.cantidad);
                    detalle.material = requi.requisicion;
                }
                else if (mat.tipo == 3)
                {
                    //Cuando es Orden de Servicio
                    var ordenServicio = db.cat_ordenservicio.FirstOrDefault(x => x.id_os == mat.idMaterial);
                    claveGastos = ordenServicio.clave_gastos;

                    detalle.precioUnitario = ordenServicio.costo;
                    detalle.iva = ordenServicio.porcentajeIVA;
                    detalle.unidad = ordenServicio.unidad;
                    detalle.total = (ordenServicio.total * mat.cantidad);
                    detalle.material = ordenServicio.ordenServicio;
                }

                //Valida presupuesto
                decimal totalFinal = detalle.total.Value;
                var presupuesto = db.tblPresupuestoDireccionClaveGasto.FirstOrDefault(x => x.idSecretaria == idSecretaria && x.idDireccion == idDireccion && x.mes == mes && x.año == anio && x.claveGastos == int.Parse(claveGastos));
                if (presupuesto.presupuestoLibre < totalFinal)
                    throw new Exception(String.Format("El material {0} no se puede solicitar, se sobrepasa el presupuesto.", detalle.material));

                presupuesto.presupuestoLibre = (presupuesto.presupuestoLibre - totalFinal);
                presupuesto.presupuestoPreComprometido = (presupuesto.presupuestoPreComprometido + totalFinal);

                db.tblSolicitudRequisicionDetalle.InsertOnSubmit(detalle);
            }

            db.SubmitChanges();
        }

        public string RegresaFormatoRequisicion(int idRequisicion)
        {
            try
            {
                //Inserta el registro del oficio
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                var requisicion = db.tblRequisiciones.FirstOrDefault(x => x.id == idRequisicion);

                if(requisicion == null)
                    throw new Exception("La solicitud fue rechazada por Adquisiciones, no se puede generar el documento.");
                else if (requisicion.estatus == "Enviada")
                    throw new Exception("La solicitud aún no ha sido autorizada por su Adquisiciones, no se puede generar el documento.");
                else if (requisicion.estatus == "Rechazada")
                    throw new Exception("La solicitud fue rechazada por Adquisiciones, no se puede generar el documento.");

                string PATH_FILES = String.Format("/Archivos/Formatos/Requisicion/");
                string urlFile = HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("R-{0}.pdf", requisicion.folio.ToString().PadLeft(5, '0')));
                if (!File.Exists(urlFile))
                {
                    StreamReader fil = File.OpenText(HttpContext.Current.Server.MapPath("~/FormatoHTML/OficioRequisicion.html"));
                    String body = fil.ReadToEnd();
                    fil.Close();

                    var direccion = db.Direcciones.FirstOrDefault(x => x.IdDireccion == requisicion.idDireccion.ToString() && x.IdSecretaria == requisicion.idSecretaria.ToString());
                    var secretaria = db.Secretarias.FirstOrDefault(x => x.IdSecretaria == requisicion.idSecretaria);
                    var subAct = db.Tbl_SubActividad.FirstOrDefault(x => x.Id == requisicion.idSubActividad);
                    var act = db.Concentrado_Pmd.FirstOrDefault(x => x.ID == subAct.Id_Linea);
                    var proveedor = db.tblProveedores.FirstOrDefault(x => x.id == requisicion.idProveedor);
                    //Carga informacion del Certificado
                    body = body.Replace("<%REFERENCIA%>", String.Format("R/{0}/{1}", requisicion.folio, DateTime.Now.Year));
                    body = body.Replace("<%FECHA%>", requisicion.fechaAutorizado.Value.ToString("dd/MM/yyyy"));
                    body = body.Replace("<%PROVEEDOR%>", proveedor.nombre);
                    body = body.Replace("<%DIRECCION%>", direccion.Nombr_direccion);
                    body = body.Replace("<%SECRETARIA%>", String.Format("{0} ({1})", secretaria.Nombr_secretaria, secretaria.IdSecretaria));
                    body = body.Replace("<%ACTIVIDAD%>", String.Format("{0} - {1}", act.ID, act.Descr_estrategia));
                    body = body.Replace("<%SUBACTIVIDAD%>", String.Format("{0} - {1}", subAct.Id_Subactividad, subAct.Nombre));

                    body = body.Replace("<%SUBTOTAL%>", String.Format("{0:c2}", requisicion.tblRequisicionesDetalle.Sum(x => (x.precioUnitario * x.cantidad))));
                    body = body.Replace("<%IVA%>", String.Format("{0:c2}", requisicion.tblRequisicionesDetalle.Sum(x => ((x.iva / 100) * (x.precioUnitario * x.cantidad)))));
                    body = body.Replace("<%TOTAL%>", String.Format("{0:c2}", requisicion.tblRequisicionesDetalle.Sum(x => x.total)));

                    StringBuilder sb = new StringBuilder();
                    int i = 1;
                    foreach (var d in requisicion.tblRequisicionesDetalle)
                    {
                        sb.AppendLine(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4:c2}</td><td>{5:c2}</td></tr>",
                            i, d.material, d.cantidad, d.unidad, d.precioUnitario, (d.precioUnitario * d.cantidad)));
                        i++;
                    }
                    body = body.Replace("<%CONTENT_TABLE%>", sb.ToString());

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
                           = new System.IO.FileStream(HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("R-{0}.pdf", requisicion.folio.ToString().PadLeft(5, '0'))),
                                                      FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    newFile.Write(pdfStream.ToArray(), 0, pdfStream.ToArray().Length);
                    newFile.Dispose();


                }

                return String.Format("~/Archivos/Formatos/Requisicion/R-{0}.pdf", requisicion.folio.ToString().PadLeft(5, '0'));
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public string RegresaFormatoSolicitudRequisicion(int idSolicitud)
        {
            try
            {
                //Inserta el registro del oficio
                IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                var requisicion = db.tblRequisiciones.FirstOrDefault(x => x.idSolicitudRequisicion == idSolicitud);

                if (requisicion == null)
                    throw new Exception("La solicitud fue rechazada por Adquisiciones, no se puede generar el documento.");
                else if (requisicion.estatus == "Enviada")
                    throw new Exception("La solicitud aún no ha sido autorizada por su Adquisiciones, no se puede generar el documento.");
                else if (requisicion.estatus == "Rechazada")
                    throw new Exception("La solicitud fue rechazada por Adquisiciones, no se puede generar el documento.");

                string PATH_FILES = String.Format("/Archivos/Formatos/Requisicion/");
                string urlFile = HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("R-{0}.pdf", requisicion.folio.ToString().PadLeft(5, '0')));
                if (!File.Exists(urlFile))
                {
                    StreamReader fil = File.OpenText(HttpContext.Current.Server.MapPath("~/FormatoHTML/OficioRequisicion.html"));
                    String body = fil.ReadToEnd();
                    fil.Close();

                    var direccion = db.Direcciones.FirstOrDefault(x => x.IdDireccion == requisicion.idDireccion.ToString() && x.IdSecretaria == requisicion.idSecretaria.ToString());
                    var secretaria = db.Secretarias.FirstOrDefault(x => x.IdSecretaria == requisicion.idSecretaria);
                    var subAct = db.Tbl_SubActividad.FirstOrDefault(x => x.Id == requisicion.idSubActividad);
                    var act = db.Concentrado_Pmd.FirstOrDefault(x => x.ID == subAct.Id_Linea);
                    var proveedor = db.tblProveedores.FirstOrDefault(x => x.id == requisicion.idProveedor);
                    //Carga informacion del Certificado
                    body = body.Replace("<%REFERENCIA%>", String.Format("R/{0}/{1}", requisicion.folio, DateTime.Now.Year));
                    body = body.Replace("<%FECHA%>", requisicion.fechaAutorizado.Value.ToString("dd/MM/yyyy"));
                    body = body.Replace("<%PROVEEDOR%>", proveedor.nombre);
                    body = body.Replace("<%DIRECCION%>", direccion.Nombr_direccion);
                    body = body.Replace("<%SECRETARIA%>", String.Format("{0} ({1})", secretaria.Nombr_secretaria, secretaria.IdSecretaria));
                    body = body.Replace("<%ACTIVIDAD%>", String.Format("{0} - {1}", act.ID, act.Descr_estrategia));
                    body = body.Replace("<%SUBACTIVIDAD%>", String.Format("{0} - {1}", subAct.Id_Subactividad, subAct.Nombre));

                    body = body.Replace("<%SUBTOTAL%>", String.Format("{0:c2}", requisicion.tblRequisicionesDetalle.Sum(x => (x.precioUnitario * x.cantidad))));
                    body = body.Replace("<%IVA%>", String.Format("{0:c2}", requisicion.tblRequisicionesDetalle.Sum(x => ((x.iva / 100) * (x.precioUnitario * x.cantidad)))));
                    body = body.Replace("<%TOTAL%>", String.Format("{0:c2}", requisicion.tblRequisicionesDetalle.Sum(x => x.total)));

                    StringBuilder sb = new StringBuilder();
                    int i = 1;
                    foreach (var d in requisicion.tblRequisicionesDetalle)
                    {
                        sb.AppendLine(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4:c2}</td><td>{5:c2}</td></tr>",
                            i, d.material, d.cantidad, d.unidad, d.precioUnitario, (d.precioUnitario * d.cantidad)));
                        i++;
                    }
                    body = body.Replace("<%CONTENT_TABLE%>", sb.ToString());

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
                           = new System.IO.FileStream(HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("R-{0}.pdf", requisicion.folio.ToString().PadLeft(5, '0'))),
                                                      FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    newFile.Write(pdfStream.ToArray(), 0, pdfStream.ToArray().Length);
                    newFile.Dispose();


                }

                return String.Format("~/Archivos/Formatos/Requisicion/R-{0}.pdf", requisicion.folio.ToString().PadLeft(5, '0'));
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
