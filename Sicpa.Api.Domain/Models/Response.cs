namespace Sicpa.Api.Domain.Models
{
    public class Response<TDocument>
    {
        public bool ok { get; set; } = false;

        public TDocument resp { get; set; } = default;

        public string message { get; set; } = "";
    }
}
