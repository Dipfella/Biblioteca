using Biblioteca_API.Models.Common;
using MediatR;

namespace Biblioteca_API.Features.Libros.Commands
{
    public class DeleteLibroCommand : IRequest<Response>
    {
        public long Id { get; set; }
    }
}
