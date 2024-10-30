using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleVenta
    {
        public int id { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public int VentaId { get; set; } 
        public Producto Producto { get; set; }
    }
}
