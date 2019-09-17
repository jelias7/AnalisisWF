using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class AnalisisDetalle
    {
        [Key]
        public int AnalisisDetalleId { get; set; }
        public string Resultado { get; set; }
        public int TiposId { get; set; }
        public decimal Monto { get; set; }
        public AnalisisDetalle()
        {
            AnalisisDetalleId = 0;
            TiposId = 0;
            Resultado = string.Empty;
            Monto = 0;
        }
        public AnalisisDetalle(int analisis, string resultado, decimal monto)
        {
            TiposId = analisis;
            Resultado = resultado;
            Monto = monto;
        }
    }
}
