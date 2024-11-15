using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public float total { get; set; }
        public string numero_factura { get; set; }
        public int cliente_id { get; set; }
        public int usuario_id { get; set; }
        public List<DetalleVenta> Detalles {  get; set; }
        public bool activo { get; set; }
    }
}
