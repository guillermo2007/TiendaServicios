using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Infraestructure
{
    public class ContextoLibreria : DbContext
    {
        public virtual DbSet<LibreriaMaterial> LibreriaMateriales { get; set; }
        public ContextoLibreria()
        {
        }

        public ContextoLibreria(DbContextOptions options) : base(options)
        {
        }
    }
}
