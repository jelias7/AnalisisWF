using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Analisis> Analisis { get; set; }
        public DbSet<Pacientes> Paciente { get; set; }
        public DbSet<TiposAnalisis> TiposAnalisis { get; set; }
        public DbSet<AnalisisDetalle> Detalle { get; set; }
        public Contexto() : base("ConStr") { }
    }
}
