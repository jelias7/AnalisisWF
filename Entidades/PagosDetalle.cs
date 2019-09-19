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
        public decimal Monto { get; set; }
        public PagosDetalle()
        {
            PagosDetalleId = 0;
            Monto = 0;
        }
        public PagosDetalle(decimal monto)
        {
            Monto = monto;
        }
    }
}
