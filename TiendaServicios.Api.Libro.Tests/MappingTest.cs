using AutoMapper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using TiendaServicios.Api.Libro.Aplication;
using TiendaServicios.Api.Libro.Model;

namespace TiendaServicios.Api.Libro.Tests
{
    public class MappingTest : Profile
    {
        public MappingTest()
        {
            CreateMap<LibreriaMaterial, LibroMaterialDto>();
        }
    }
}
