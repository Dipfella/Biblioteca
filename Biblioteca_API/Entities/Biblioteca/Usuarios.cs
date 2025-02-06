using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca_API.Entities.Biblioteca
{
    [Table("Usuarios")]
    public class Usuarios
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; } = null!;

        [Column("Apellido")]
        public string Apellido { get; set; } = null!;

        [Column("NumeroIdentificacion")]
        public string NumeroIdentificacion { get; set; } = null!;

        [Column("Telefono")]
        public string? Telefono { get; set; }

        [Column("Correo")]
        public string Correo { get; set; } = null!;

        [Column("FechaRegistro")]
        public DateTime FechaRegistro { get; set; }
    }
}
