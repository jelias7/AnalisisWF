using Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseInitializer: DropCreateDatabaseIfModelChanges<Contexto>
    {
        protected override void Seed(Contexto context)
        {
            var Paciente = new List<Pacientes>
            {
                new Pacientes
                {
                   Nombres="Ramon Jose"
                },
                new Pacientes
                {
                    Nombres="Pepe Grillo"
                },
                new Pacientes
                {
                    Nombres="John Doe"
                }
            };
            Paciente.ForEach(s => context.Paciente.Add(s));
            context.SaveChanges();
        }
    }
}
