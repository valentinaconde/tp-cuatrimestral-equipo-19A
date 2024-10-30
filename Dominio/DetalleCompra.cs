using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class DetalleCompra
    {

        public int id { get; set; }
        public int Cantidad { get; set; }
        public float PrecioUnitario { get; set; }
        public int CompraId { get; set; }
        public int ProductoId { get; set; }

    }
}
