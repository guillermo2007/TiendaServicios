using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Permissions;
using TiendaServicios.Api.Autor.Model;

namespace TiendaServicios.Api.Autor.Infraestructure
{
    public class ContextoAutor : DbContext
    {

        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<GradoAcademico> GradosAcademicos { get; set; }

        public ContextoAutor(DbContextOptions options) : base(options)
        {
        }
    }
}
