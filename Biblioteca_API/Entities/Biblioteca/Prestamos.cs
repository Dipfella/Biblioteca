using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca_API.Entities.Biblioteca
{
    [Table("Prestamos")]
    public class Prestamos
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("EjemplarId")]
        public int EjemplarId { get; set; }

        [Column("UsuarioId")]
        public int UsuarioId { get; set; }

        [Column("FechaPrestamo")]
        public DateTime FechaPrestamo { get; set; }

        [Column("FechaDevolucion")]
        public DateTime? FechaDevolucion { get; set; }
    }
}
