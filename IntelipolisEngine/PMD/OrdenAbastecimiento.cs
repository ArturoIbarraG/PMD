using IntelipolisEngine.DataBase;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IntelipolisEngine.PMD
{
    public class OrdenAbastecimiento
    {

        public class Material
        {
            public int idRequerimiento { get; set; }
            public decimal cantidad { get; set; }
        }


        public void GuardaOrdenAbastecimiento(List<Material> materiales, int idAlmacen, int idSecretaria, int idDireccion, int anio, int mes, int empleado, int idSubactividad)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
            var contratoDetalle = (from c in db.cat_contratos.AsEnumerable()
                                   join m in materiales on c.id equals m.idRequerimiento
                                   select new
                                   {
                                       idMaterial = c.id,
                                       c.codigo_contrato,
                                       c.nombre_contrato,
                                       material = c.requerimiento,
                                       c.proveedor,
                                       m.cantidad,
                                       c.iva,
                                       c.total,
                                       c.unidad,
                                       c.clave_gastos,
                                       c.costoUnitario,
                                       c.ieps,
                                       totalFinal = (c.total * m.cantidad)
                                   }).ToList();

            var proveedores = contratoDetalle.Select(x => x.proveedor).Distinct();

            int folio = 1;
            try { folio = db.tblOrdenesSurtido.Max(x => x.folio).Value + 1; } catch { }
            foreach (var p in proveedores)
            {
                var ordenAbastecimiento = new DataBase.tblOrdenesSurtido();
                ordenAbastecimiento.año = anio;
                ordenAbastecimiento.estatus = "Enviada";
                ordenAbastecimiento.fechaEnviado = DateTime.Now;
                ordenAbastecimiento.idAlmacen = idAlmacen;
                ordenAbastecimiento.idDireccion = idDireccion;
                ordenAbastecimiento.idSecretaria = idSecretaria;
                ordenAbastecimiento.idSubActividad = idSubactividad;
                ordenAbastecimiento.mes = mes;
                ordenAbastecimiento.folio = folio;
                ordenAbastecimiento.proveedor = p;
                ordenAbastecimiento.usuarioEnviado = db.Usuarios.FirstOrDefault(x => x.Clave_empl == empleado).Nombr_empl;
                db.tblOrdenesSurtido.InsertOnSubmit(ordenAbastecimiento);
                
                var contratos = contratoDetalle.Where(x => x.proveedor == p);
                foreach (var c in contratos)
                {
                    decimal totalFinal = c.totalFinal.Value;
                    var presupuesto = db.tblPresupuestoDireccionContrato.FirstOrDefault(x => x.idSecretaria == idSecretaria && x.idDireccion == idDireccion && x.mes == mes && x.año == anio && x.codigoContrato == c.codigo_contrato);
                    if (presupuesto.presupuestoDisponible < totalFinal)
                        throw new Exception(String.Format("El material {2} perteneciente al contrato {0}-{1} no se puede solicitar, se sobrepasa el presupuesto.", c.codigo_contrato, c.nombre_contrato, c.material));

                    presupuesto.presupuestoDisponible = (presupuesto.presupuestoDisponible - totalFinal);
                    presupuesto.presupuestoUtilizado = (presupuesto.presupuestoUtilizado + totalFinal);

                    var abastecimientoDetalle = new DataBase.tblOrdenesSurtidoDetalle();
                    abastecimientoDetalle.cantidad = c.cantidad;
                    abastecimientoDetalle.codigoContrato = c.codigo_contrato;
                    abastecimientoDetalle.contrato = c.nombre_contrato;
                    abastecimientoDetalle.estatus = "Enviada";
                    abastecimientoDetalle.idRequerimiento = c.idMaterial;
                    abastecimientoDetalle.iva = c.iva;
                    abastecimientoDetalle.precioUnitario = c.costoUnitario;
                    abastecimientoDetalle.requirimiento = c.material;
                    abastecimientoDetalle.tblOrdenesSurtido = ordenAbastecimiento;
                    abastecimientoDetalle.unidad = c.unidad;
                    abastecimientoDetalle.total = c.totalFinal;
                    db.tblOrdenesSurtidoDetalle.InsertOnSubmit(abastecimientoDetalle);
                }

                folio++;
            }

            db.SubmitChanges();
        }

        public string RegresaFormatoOrdenAbastecimiento(int folioOrdenAbastecimiento)
        {
            try
            {
                string PATH_FILES = String.Format("/Archivos/Formatos/OrdenAbastecimiento/");
                string urlFile = HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("OA-{0}.pdf", folioOrdenAbastecimiento.ToString().PadLeft(5, '0')));
                if (!File.Exists(urlFile))
                {
                    //Inserta el registro del oficio
                    IntelipolisDBDataContext db = new IntelipolisDBDataContext("Data Source=187.176.54.246; Initial Catalog=Planeacion_Financiera;User ID=usrPlaneacion;Password=usrPlaneacion");
                    var ordenAbastecimiento = db.tblOrdenesSurtido.FirstOrDefault(x => x.id == folioOrdenAbastecimiento);

                    if (ordenAbastecimiento.estatus == "Enviada")
                        throw new Exception("La solicitud aún no ha sido autorizada por su Enlace, no se puede generar el documento.");
                    else if(ordenAbastecimiento.estatus == "Rechazada")
                        throw new Exception("La solicitud fue rechazada por su Enlace, no se puede generar el documento.");

                    StreamReader fil = File.OpenText(HttpContext.Current.Server.MapPath("~/FormatoHTML/OficioOrdenAbastecimiento.html"));
                    String body = fil.ReadToEnd();
                    fil.Close();

                    var direccion = db.Direcciones.FirstOrDefault(x => x.IdDireccion == ordenAbastecimiento.idDireccion.ToString() && x.IdSecretaria == ordenAbastecimiento.idSecretaria.ToString());
                    var secretaria = db.Secretarias.FirstOrDefault(x => x.IdSecretaria == ordenAbastecimiento.idSecretaria);
                    var subAct = db.Tbl_SubActividad.FirstOrDefault(x => x.Id == ordenAbastecimiento.idSubActividad);
                    var act = db.Concentrado_Pmd.FirstOrDefault(x => x.ID == subAct.Id_Linea);

                    //Carga informacion del Certificado
                    body = body.Replace("<%REFERENCIA%>", String.Format("OA/{0}/{1}", ordenAbastecimiento.folio, DateTime.Now.Year));
                    body = body.Replace("<%FECHA%>", ordenAbastecimiento.fechaEnviado.Value.ToString("dd/MM/yyyy"));
                    body = body.Replace("<%PROVEEDOR%>", ordenAbastecimiento.proveedor);
                    body = body.Replace("<%DIRECCION%>", direccion.Nombr_direccion);
                    body = body.Replace("<%SECRETARIA%>", String.Format("{0} ({1})", secretaria.Nombr_secretaria, secretaria.IdSecretaria));
                    body = body.Replace("<%ACTIVIDAD%>", String.Format("{0} - {1}", act.ID, act.Descr_estrategia));
                    body = body.Replace("<%SUBACTIVIDAD%>", String.Format("{0} - {1}", subAct.Id_Subactividad, subAct.Nombre));

                    body = body.Replace("<%SUBTOTAL%>", String.Format("{0:c2}", ordenAbastecimiento.tblOrdenesSurtidoDetalle.Sum(x => (x.precioUnitario * x.cantidad))));
                    body = body.Replace("<%IVA%>", String.Format("{0:c2}", ordenAbastecimiento.tblOrdenesSurtidoDetalle.Sum(x => ((x.iva / 100) * (x.precioUnitario * x.cantidad)))));
                    body = body.Replace("<%TOTAL%>", String.Format("{0:c2}", ordenAbastecimiento.tblOrdenesSurtidoDetalle.Sum(x => x.total)));

                    StringBuilder sb = new StringBuilder();
                    int i = 1;
                    foreach (var d in ordenAbastecimiento.tblOrdenesSurtidoDetalle)
                    {
                        sb.AppendLine(String.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4:c2}</td><td>{5:c2}</td></tr>", 
                            i, d.requirimiento, d.cantidad, d.unidad, d.precioUnitario, (d.precioUnitario * d.cantidad)));
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
                           = new System.IO.FileStream(HttpContext.Current.Server.MapPath("~/" + PATH_FILES + String.Format("OA-{0}.pdf", folioOrdenAbastecimiento.ToString().PadLeft(5, '0'))),
                                                      FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                    newFile.Write(pdfStream.ToArray(), 0, pdfStream.ToArray().Length);
                    newFile.Dispose();


                }

                return String.Format("~/Archivos/Formatos/OrdenAbastecimiento/OA-{0}.pdf", folioOrdenAbastecimiento.ToString().PadLeft(5, '0'));
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        public void AutorizarOrdenAbastecimiento(List<int> ordenes, string usuario)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext();
                foreach (var o in ordenes)
                {
                    var orden = db.tblOrdenesSurtido.FirstOrDefault(x => x.id == o);
                    if (orden.estatus == "Autorizada")
                        throw new Exception(String.Format("La orden de Abastecimiento con folio {0} ya ha sido autorizada, favor de validar.", orden.folio));
                    else if(orden.estatus == "Rechazada")
                        throw new Exception(String.Format("La orden de Abastecimiento con folio {0} ya ha sido rechazada, favor de validar.", orden.folio));

                    orden.estatus = "Autorizada";

                    var autorizada = new tblHistoricoAprobacionOrdenAbastecimiento();
                    autorizada.fechaAprobacion = DateTime.Now;
                    autorizada.idOrdenAbastecimiento = o;
                    autorizada.usuarioAprobacion = usuario;
                    db.tblHistoricoAprobacionOrdenAbastecimiento.InsertOnSubmit(autorizada);
                }

                db.SubmitChanges();
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public void RechazaOrdenAbastecimiento(List<int> ordenes, int idMotivo, string usuario, string comentarios)
        {
            try
            {
                IntelipolisDBDataContext db = new IntelipolisDBDataContext();
                foreach (var o in ordenes)
                {
                    var orden = db.tblOrdenesSurtido.FirstOrDefault(x => x.id == o);
                    if (orden.estatus == "Autorizada")
                        throw new Exception(String.Format("La orden de Abastecimiento con folio {0} ya ha sido autorizada, favor de validar.", orden.folio));
                    else if (orden.estatus == "Rechazada")
                        throw new Exception(String.Format("La orden de Abastecimiento con folio {0} ya ha sido rechazada, favor de validar.", orden.folio));

                    orden.estatus = "Rechazada";
                    var rechazada = new tblHistoricoRechazoOrdenAbastecimiento();
                    rechazada.comentarios = comentarios;
                    rechazada.fechaRechazo = DateTime.Now;
                    rechazada.idMotivoRechazo = idMotivo;
                    rechazada.idOrdenAbastecimiento = o;
                    rechazada.usuarioRechazo = usuario;
                    db.tblHistoricoRechazoOrdenAbastecimiento.InsertOnSubmit(rechazada);

                    foreach (var c in orden.tblOrdenesSurtidoDetalle)
                    {
                        decimal totalFinal = c.total.Value;
                        var presupuesto = db.tblPresupuestoDireccionContrato.FirstOrDefault(x => x.idSecretaria == orden.idSecretaria && x.idDireccion == orden.idDireccion && x.mes == orden.mes && x.año == orden.año && x.codigoContrato == c.codigoContrato);
                        
                        presupuesto.presupuestoDisponible = (presupuesto.presupuestoDisponible + totalFinal);
                        presupuesto.presupuestoUtilizado = (presupuesto.presupuestoUtilizado - totalFinal);
                    }
                }

                db.SubmitChanges();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
