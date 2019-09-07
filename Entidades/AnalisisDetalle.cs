using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [Serializable]
    public class AnalisisDetalle
    {
        public int DetalleId { get; set; }
        public string Analisis { get; set; }
        public string Resultado { get; set; }
        public AnalisisDetalle()
        {
            DetalleId = 0;
            Analisis = string.Empty;
            Resultado = string.Empty;
        }
        public AnalisisDetalle(int id, string analisis, string resultado)
        {
            DetalleId = id;
            Analisis = analisis;
            Resultado = resultado;
        }
    }
}
