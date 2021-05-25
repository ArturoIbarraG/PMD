using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelipolisEngine.DataBase;

namespace IntelipolisEngine.PMD
{
    public class Contratos
    {
        public void GuardaContrato(string nombre, string proveedor, string codigo, string claveGastos, DataTable dtContratos)
        {
            IntelipolisDBDataContext db = new IntelipolisDBDataContext();
            
            if (dtContratos != null)
            {
                var contratos = db.cat_contratos.Where(x => x.codigo_contrato == codigo);
                db.cat_contratos.DeleteAllOnSubmit(contratos);

                foreach (DataRow row in dtContratos.Rows)
                {
                    var contrato = new cat_contratos();
                    contrato.clave_gastos = claveGastos;
                    contrato.codigo_contrato = codigo;
                    contrato.costoUnitario = decimal.Parse(row["costoUnitario"].ToString());
                    contrato.ieps = decimal.Parse(row["ieps"].ToString());
                    contrato.iva = int.Parse(row["iva"].ToString());
                    contrato.proveedor = proveedor;
                    contrato.requerimiento = row["requerimiento"].ToString();
                    contrato.total = decimal.Parse(row["total"].ToString());
                    contrato.unidad = row["unidad"].ToString();
                    contrato.nombre_contrato = nombre;
                    contrato.eliminado = false;
                    db.cat_contratos.InsertOnSubmit(contrato);
                }
                db.SubmitChanges();
            }
            else
            {
                throw new Exception("Favor de agregar al menos un producto al contrato.");
            }
        }
    }
}
