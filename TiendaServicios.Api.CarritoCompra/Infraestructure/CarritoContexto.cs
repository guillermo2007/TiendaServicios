using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Model;

namespace TiendaServicios.Api.CarritoCompra.Infraestructure
{
    public class CarritoContexto : DbContext
    {
        public DbSet<CarritoSesion> CarritosSesiones { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }
        public CarritoContexto (DbContextOptions options) : base(options)
        {
        }
    }
}
