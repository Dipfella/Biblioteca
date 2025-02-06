using Microsoft.EntityFrameworkCore;
using System;
namespace Biblioteca_API.Models.Common
{
    public class PaginadorGenerico<T> where T : class
    {
        public int PaginaActual { get; set; } = 0;
        /// <summary>
        /// Número de registros de la página devuelta.
        /// </summary>
        public int RegistrosPorPagina { get; set; } = 0;
        /// <summary>
        /// Total de registros de consulta.
        /// </summary>
        public int TotalRegistros { get; set; } = 0;
        /// <summary>
        /// Total de páginas de la consulta.
        /// </summary>
        public int TotalPaginas { get; set; } = 0;
        /// <summary>
        /// Columna por la que esta ordenada la consulta actual.
        /// </summary>
        public IEnumerable<T> Resultado { get; set; } = null!;
    }
}

