using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Infraestructure;
using TiendaServicios.Api.Autor.Model;

namespace TiendaServicios.Api.Autor.Aplication
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto>
        {
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            private readonly ContextoAutor _contexto;
            private readonly IMapper _mapper;
            public Manejador(ContextoAutor contexto, IMapper mapper)
            {
                _contexto = contexto;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autorLibro = await _contexto.AutorLibros.FirstOrDefaultAsync(x => x.AutorLibroGuid == request.AutorGuid);
                if (autorLibro == null)
                    throw new Exception("No se encontro el autor");

                var autorDto = _mapper.Map<AutorLibro, AutorDto>(autorLibro);

                return autorDto;
            }
        }


    }
}
