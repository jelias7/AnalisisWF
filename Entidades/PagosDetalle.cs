﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class PagosDetalle
    {
        [Key]
        public int PagosDetalleId { get; set; }
        public int AnalisisId { get; set; }
        public decimal MontoAnalisis { get; set; }
        public decimal MontoPagado { get; set; }
        public PagosDetalle()
        {
            PagosDetalleId = 0;
            AnalisisId = 0;
            MontoAnalisis = 0;
            MontoPagado = 0;
        }
        public PagosDetalle(int analisisid, decimal montoanalisis, decimal montopagado)
        {
            AnalisisId = analisisid;
            MontoAnalisis = montoanalisis;
            MontoPagado = montopagado;
        }
    }
}
