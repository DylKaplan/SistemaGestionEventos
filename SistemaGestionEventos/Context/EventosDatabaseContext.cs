using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaGestionEventos.Models;

using System.Collections.Generic;
namespace SistemaGestionEventos.Context
{
    public class EventosDatabaseContext : DbContext
    {
        public EventosDatabaseContext(DbContextOptions<EventosDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Lugar> Lugares { get; set; }

        public DbSet<Personal> Personal { get; set; }
        public DbSet<EventoPersonal> EventoPersonal { get; set; }
    }
}

