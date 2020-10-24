using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TiendaServicios.Api.Libro.Aplication;
using TiendaServicios.Api.Libro.Infraestructure;
using TiendaServicios.Api.Libro.Model;
using Xunit;

namespace TiendaServicios.Api.Libro.Tests
{
    public class LibrosServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(30);

            lista[0].LibreriaMaterialId = Guid.Empty;

            return lista;

        }

        private Mock<ContextoLibreria> CrearContexto()
        {
            var data = ObtenerDataPrueba().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>()
                    .Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                    .Returns(new AsyncEnumerator<LibreriaMaterial>(data.GetEnumerator()));

            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(data.Provider));

            var contexto = new Mock<ContextoLibreria>();
            contexto.Setup(x => x.LibreriaMateriales).Returns(dbSet.Object);
            
            return contexto;

        }

        private IMapper CrearMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            return mapperConfiguration.CreateMapper();
        }

        [Fact]
        public async void GetLibroById()
        {
            var mockContexto = CrearContexto();
            var mapper = CrearMapper();

            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);

            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == request.LibroId);
        }

        [Fact]
        public async void GetLibros()
        {            
            var mockContexto = CrearContexto();
            var mapper = CrearMapper();

            Consulta.Manejador manejador = new Consulta.Manejador(mockContexto.Object, mapper);

            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request: request, new System.Threading.CancellationToken());

            Assert.True(lista.Any());
        }

        [Fact]
        public async void GuardarLibro()
        {
            var options = new DbContextOptionsBuilder<ContextoLibreria>()
                .UseInMemoryDatabase(databaseName:"BaseDatosLibro")
                .Options;

            var contexto = new ContextoLibreria(options);

            var request = new Nuevo.Ejecuta
            {
                AutorLibro = Guid.Empty ,
                Titulo = "Microservicios",
                FechaPublicacion = DateTime.Now
            };

            var manejador = new Nuevo.Manejador(contexto);

            var libro = await manejador.Handle(request, new CancellationToken());

            Assert.True(libro != null);
        }
    }
}
