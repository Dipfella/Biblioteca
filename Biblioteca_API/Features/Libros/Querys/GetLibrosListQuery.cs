using Biblioteca_API.Models.Common;
using MediatR;
using static Biblioteca_API.Models.Libros;

namespace Biblioteca_API.Features.Libros.Querys
{
    public class GetLibrosListQuery : IRequest<List<Entities.Biblioteca.Libros>>
    {
        public string? Titulo { get; set; }
        public string? Autor { get; set; }
        public string? Genero { get; set; }
        public long? CantidadRegistros { get; set; }
    }
}
