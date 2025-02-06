using Biblioteca_API.Models.Common;
using MediatR;
using static Biblioteca_API.Models.Libros;

namespace Biblioteca_API.Features.Libros.Commands
{
    public class AddLibroCommand : IRequest<Response>
    {
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public List<Ejemplares> Ejemplares { get; set; } = null!;
        public bool Disponible { get; set; }
        public string? PortadaUrl { get; set; }
    }
}
