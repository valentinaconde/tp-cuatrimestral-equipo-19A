﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }

        public string email { get; set; }
        public string password { get; set; }

        public int rol_id { get; set; }
        public bool activo { get; set; }

    }
}
