using Biblioteca_API.Entities.Biblioteca;

namespace Biblioteca_API.Models
{
    public class Libros
    {
        public class EjemplarResponse
        {
            public int Id { get; set; }
            public string CodigoInventario { get; set; } = null!;
            public int LibroId { get; set; }
            public bool Disponible { get; set; }
        }
        public class LibrosResponse
        {
            public int Id { get; set; }
            public string Titulo { get; set; } = null!;
            public string Autor { get; set; } = null!;
            public string Genero { get; set; } = null!;
            public string PortadaUrl { get; set; } = null!;
            public int CantidadEjemplares { get; set; }
            public List<EjemplarResponse> Ejemplares { get; set; } = new List<EjemplarResponse>();
        }

        public class Ejemplares
        {
            public string CodigoInventario { get; set; } = null!;
            public bool Disponible { get; set; }
        }
    } 
}
