using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_API.Entities.Biblioteca
{
    [Table("Libros")]
    public class Libros
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Titulo")]
        public string Titulo { get; set; } = null!;

        [Column("Autor")]
        public string Autor { get; set; } = null!;

        [Column("Genero")]
        public string Genero { get; set; } = null!;
    }
}
