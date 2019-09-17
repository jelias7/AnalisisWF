using System;
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
        public int AnalisisId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public Pagos()
        {
            PagosId = 0;
            AnalisisId = 0;
            Monto = 0;
            Fecha = DateTime.Now;
        }
    }
}
