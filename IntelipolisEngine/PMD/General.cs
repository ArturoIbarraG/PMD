using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelipolisEngine.PMD
{
    public class General
    {
        public string ObtieneDependencia(string direccion, string secretaria)
        {
            DataBase.IntelipolisDBDataContext db = new DataBase.IntelipolisDBDataContext();
            return db.Direcciones.FirstOrDefault(x => x.IdDireccion == direccion && x.IdSecretaria == secretaria).Nombr_direccion;
        }
    }
}
