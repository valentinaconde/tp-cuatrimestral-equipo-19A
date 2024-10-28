using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int stockactual { get; set; }
        public int stockminimo { get; set; }
        public float ganancia { get; set; }
        public int idmarca { get; set; }
        public int idcategoria { get; set; }


    }
}
