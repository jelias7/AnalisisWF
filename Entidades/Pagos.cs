﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class Pagos
    {
        [Key]
        public int PagosId { get; set; }
        public int PacienteId { get; set; }
        public decimal Pagado { get; set; }
        public DateTime Fecha { get; set; }
        public Pagos()
        {
            PagosId = 0;
            PacienteId = 0;
            Pagado = 0;
            Fecha = DateTime.Now;
        }
    }
}
