﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Pacientes
    {
        [Key]
        public int PacienteId { get; set; }
        public string Nombres { get; set; }
        public Pacientes()
        {
            PacienteId = 0;
            Nombres = string.Empty;
        }
    }
}
