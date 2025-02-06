using Biblioteca_API.Entities.Biblioteca;
using Biblioteca_API.Features.Libros.Commands;
using Biblioteca_API.Models.Common;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Biblioteca_API.Features.Libros.Handlers
{
    public class AddLibroHandler : IRequestHandler<AddLibroCommand, Response>
    {
        private readonly BibliotecaDbContext _db;

        public AddLibroHandler(BibliotecaDbContext db)
        {
            _db = db;
        }

        public async Task<Response> Handle(AddLibroCommand command, CancellationToken cancellationToken)
        {
            using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var libro = new Entities.Biblioteca.Libros
                {
                    Titulo = command.Titulo,
                    Autor = command.Autor,
                    Genero = command.Genero,
                    PortadaUrl = command.PortadaUrl,
                };
                await _db.Libros.AddAsync(libro, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

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
                await transaction.CommitAsync();
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
