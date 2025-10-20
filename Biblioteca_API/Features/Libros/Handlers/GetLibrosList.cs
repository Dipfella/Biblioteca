using Biblioteca_API.Entities.Biblioteca;
using Biblioteca_API.Features.Libros.Commands;
using Biblioteca_API.Features.Libros.Querys;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Biblioteca_API.Models.Libros;

namespace Biblioteca_API.Features.Libros.Handlers
{
    public class GetLibrosList : IRequestHandler<GetLibrosListQuery, List<LibrosResponse>>
    {
        private readonly BibliotecaDbContext _db;

        public GetLibrosList(BibliotecaDbContext db)
        {
            _db = db;
        }

        public async Task<List<LibrosResponse>> Handle(GetLibrosListQuery command, CancellationToken cancellationToken)
        {
            try
            {
                var query = _db.Libros.AsQueryable();

                // Aplicar filtros dinámicamente
                if (!string.IsNullOrEmpty(command.Titulo))
                    query = query.Where(l => l.Titulo.Contains(command.Titulo));

                if (!string.IsNullOrEmpty(command.Autor))
                    query = query.Where(l => l.Autor.Contains(command.Autor));

                if (!string.IsNullOrEmpty(command.Genero))
                    query = query.Where(l => l.Genero.Contains(command.Genero));

                if (command.CantidadRegistros.HasValue && command.CantidadRegistros > 0)
                    query = query.Take((int)command.CantidadRegistros.Value);

                // Armar respuesta con ejemplares y conteo
                var response = await query
                    .Select(l => new LibrosResponse
                    {
                        Id = l.Id,
                        Titulo = l.Titulo,
                        Autor = l.Autor,
                        Genero = l.Genero,
                        PortadaUrl = l.PortadaUrl ?? string.Empty,
                        CantidadEjemplares = _db.Ejemplares.Count(e => e.LibroId == l.Id),
                        Ejemplares = _db.Ejemplares
                            .Where(e => e.LibroId == l.Id)
                            .Select(e => new EjemplarResponse
                            {
                                Id = e.Id,
                                CodigoInventario = e.CodigoInventario,
                                LibroId = e.LibroId,
                                Disponible = e.Disponible
                            }).ToList()
                    })
                    .ToListAsync(cancellationToken);

                return response;
            }
            catch (Exception ex)
            {
                // Opcional: loggear el error
                Console.WriteLine($"Error en GetLibrosListQuery: {ex.Message}");
                return new List<LibrosResponse>();
            }
        }

    }
}
