using Biblioteca_API.Entities.Biblioteca;
using Biblioteca_API.Features.Libros.Commands;
using Biblioteca_API.Features.Libros.Querys;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Biblioteca_API.Models.Libros;

namespace Biblioteca_API.Features.Libros.Handlers
{
    public class GetLibrosList : IRequestHandler<GetLibrosListQuery, List<Entities.Biblioteca.Libros>>
    {
        private readonly BibliotecaDbContext _db;

        public GetLibrosList(BibliotecaDbContext db)
        {
            _db = db;
        }

        public async Task<List<Entities.Biblioteca.Libros>> Handle(GetLibrosListQuery command, CancellationToken cancellationToken)
        {
            try
            {
                using var context = new BibliotecaDbContext();

                var query = context.Libros.AsQueryable();

                if (!string.IsNullOrEmpty(command.Titulo))
                {
                    query = query.Where(l => l.Titulo.Contains(command.Titulo));
                }

                if (!string.IsNullOrEmpty(command.Autor))
                {
                    query = query.Where(l => l.Autor.Contains(command.Autor));
                }

                if (!string.IsNullOrEmpty(command.Genero))
                {
                    query = query.Where(l => l.Genero.Contains(command.Genero));
                }

                if (command.CantidadRegistros.HasValue && command.CantidadRegistros > 0)
                {
                    query = query.Take((int)command.CantidadRegistros.Value);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                var response = new List<Entities.Biblioteca.Libros>();
                return response;
            }
        }
    }
}
