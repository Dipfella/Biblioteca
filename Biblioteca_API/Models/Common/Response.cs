namespace Biblioteca_API.Models.Common
{
    public class Response
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
    }
}
