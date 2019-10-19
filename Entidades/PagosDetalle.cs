using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class PagosDetalle
    {
        public int PagosDetalleId { get; set; }
        public int AnalisisId { get; set; }
        public decimal MontoAnalisis { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime Fecha { get; set; }

        public PagosDetalle(int id, int analisisid, decimal montoanalisis, decimal montopagado, DateTime fecha)
        {
            PagosDetalleId = id;
            AnalisisId = analisisid;
            MontoAnalisis = montoanalisis;
            MontoPagado = montopagado;
            Fecha = fecha;
        }
    }
}
