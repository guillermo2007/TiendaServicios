using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Infraestructure;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Aplication
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibroMaterialDto>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibroMaterialDto>>
        {
            private readonly ContextoLibreria _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoLibreria contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<List<LibroMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var librosMateriales = await _contexto.LibreriaMateriales.ToListAsync();

                var librosMaterialesDto = _mapper.Map<List<LibreriaMaterial>, List<LibroMaterialDto>>(librosMateriales);

                return librosMaterialesDto;
            }
        }
    }
}
