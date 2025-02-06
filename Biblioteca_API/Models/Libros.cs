using Biblioteca_API.Entities.Biblioteca;

namespace Biblioteca_API.Models
{
    public class Libros
    {
        public class LibrosResponse
        {
            public int Id { get; set; }
            public string Titulo { get; set; } = null!;
            public string Autor { get; set; } = null!;
            public string Genero { get; set; } = null!;
        }

        public class Ejemplares
        {
            public string CodigoInventario { get; set; } = null!;
            public bool Disponible { get; set; }
        }
    } 
}
