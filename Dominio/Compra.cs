using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Compra
    {

        public int id { get; set; }
        public DateTime fecha { get; set; }
        public float total { get; set; }
        public int proveedorID { get; set; }
        public List<DetalleCompra> Detalles { get; set; }

    }
}
