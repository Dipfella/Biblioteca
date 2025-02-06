using Biblioteca_API.Entities.Biblioteca;
using Biblioteca_API.Features.Libros.Commands;
using Biblioteca_API.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Biblioteca_API.Features.Libros.Handlers
{
    public class PutLibroHandler : IRequestHandler<PutLibroCommand, Response>
    {
        private readonly BibliotecaDbContext _db;

        public PutLibroHandler(BibliotecaDbContext db)
        {
            _db = db;
        }

        public async Task<Response> Handle(PutLibroCommand command, CancellationToken cancellationToken)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var libro = await _db.Libros.Where(x => x.Id == command.Id).FirstOrDefaultAsync(cancellationToken);

                if (libro == null)
                {
                    var result = new Response
                    {
                        Code = 400,
                        Success = false,
                        Message = "El registro que quiere actualizar no existe"
                    };
                    return result;
                }

                libro.Titulo = command.Titulo;
                libro.Autor = command.Autor;
                libro.Genero = command.Genero;
                libro.PortadaUrl = command.PortadaUrl;

                _db.Entry(libro).State = EntityState.Modified;

                var ejemplares = await _db.Ejemplares.Where(x => x.LibroId == libro.Id).ToListAsync(cancellationToken);
                if (ejemplares.Count > 0)
                {
                    _db.Ejemplares.RemoveRange(ejemplares);
                    await _db.SaveChangesAsync(cancellationToken);
                }

                if (command.Ejemplares != null)
                {
                    foreach (var item in command.Ejemplares)
                    {
                        var ejemplar = new Ejemplares
                        {
                            CodigoInventario = item.CodigoInventario,
                            Disponible = item.Disponible,
                            LibroId = libro.Id,
                        };
                        await _db.Ejemplares.AddAsync(ejemplar, cancellationToken);
                    }
                    await _db.SaveChangesAsync(cancellationToken);
                }

                var response = new Response
                {
                    Code = 200,
                    Success = true,
                    Message = "Proceso ejecutado exitosamente"
                };
                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                var response = new Response
                {
                    Code = 400,
                    Success = false,
                    Message = ex.Message
                };
                return response;
            }
        }
    }
}
