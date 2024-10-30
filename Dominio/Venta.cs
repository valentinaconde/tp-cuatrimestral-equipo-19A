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
        public string nroFactura { get; set; }
        public int clienteID { get; set; }
        public int usuarioID { get; set; }
        public List<DetalleVenta> Detalles {  get; set; }
    }
}
