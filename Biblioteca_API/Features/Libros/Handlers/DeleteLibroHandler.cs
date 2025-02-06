using Biblioteca_API.Entities.Biblioteca;
using Biblioteca_API.Features.Libros.Commands;
using Biblioteca_API.Models.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_API.Features.Libros.Handlers
{
    public class DeleteLibroHandler : IRequestHandler<DeleteLibroCommand, Response>
    {
        private readonly BibliotecaDbContext _db;

        public DeleteLibroHandler(BibliotecaDbContext db)
        {
            _db = db;
        }

        public async Task<Response> Handle(DeleteLibroCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var libro = await _db.Libros
                            .Where(x => x.Id == command.Id)
                            .FirstOrDefaultAsync(cancellationToken);

                if (libro == null)
                {
                    var result = new Response
                    {
                        Code = 400,
                        Success = false,
                        Message = "El registro que quiere eliminar no existe"
                    };
                    return result;
                }
                var ejemplares = await _db.Ejemplares
                                             .Where(x => x.LibroId == libro.Id)
                                             .ToListAsync(cancellationToken);

                if (ejemplares.Any())
                {
                    _db.Ejemplares.RemoveRange(ejemplares);
                    await _db.SaveChangesAsync(cancellationToken);
                }

                _db.Libros.Remove(libro);
                await _db.SaveChangesAsync(cancellationToken);
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