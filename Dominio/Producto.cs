﻿using System;
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
        public int precio_unitario { get; set; }
        public float ganancia { get; set; }
        public int idmarca { get; set; }
        public string nombremarca { get; set; }
        public int idcategoria { get; set; }
        public string nombrecategoria { get; set; }
        public bool activo { get; set; }


    }
}
