using MediatR;
using Biblioteca_API.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Biblioteca_API.Features.Libros.Querys;
using Biblioteca_API.Features.Libros.Commands;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;

namespace Biblioteca_API.Features.Libros
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LibrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Entities.Biblioteca.Libros>> GetLibroList(string? genero, string? autor, string? titulo, long cantidadRegistros)
        {
            var objeto = await _mediator.Send(new GetLibrosListQuery() { Genero = genero, Autor = autor, Titulo = titulo, CantidadRegistros = cantidadRegistros });
            return objeto;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> PostLibro(AddLibroCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return result;
            }
            catch (Exception ex)
            {
                var res = new Response
                {
                    Code = 400,
                    Success = false,
                    Message = ex.Message
                };
                return res;
            }
        }

        [HttpDelete("{id}")]
        public async Task<Response> DeleteLibro(long id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteLibroCommand { Id = id });
                return result;
            }
            catch (Exception ex)
            {
                var res = new Response
                {
                    Code = 400,
                    Success = false,
                    Message = ex.Message
                };
                return res;
            }
        }

        [HttpPut]
        public async Task<Response> PutLibro(PutLibroCommand model)
        {
            try
            {
                var result = await _mediator.Send(model);
                return result;
            }
            catch (Exception ex)
            {
                var res = new Response
                {
                    Code = 400,
                    Success = false,
                    Message = ex.Message
                };
                return res;
            }
        }
    }
}