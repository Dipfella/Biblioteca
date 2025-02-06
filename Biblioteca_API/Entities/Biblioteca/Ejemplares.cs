using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_API.Entities.Biblioteca
{
    [Table("Ejemplares")]
    public class Ejemplares
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("LibroId")]
        public int LibroId { get; set; }

        [Column("CodigoInventario")]
        public string CodigoInventario { get; set; } = null!;

        [Column("Disponible")]
        public bool Disponible { get; set; }
    }
}
