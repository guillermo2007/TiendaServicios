using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaServicios.Api.CarritoCompra.Aplication
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FecraCreacionSesion { get; set; }
        public ICollection<CarritoDetalleDto> ListaProductos { get; set; }
    }
}
