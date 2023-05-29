using System.Text.Json.Serialization;

namespace Codebridge.Business.Dtos
{
    public class DogDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        [JsonPropertyName("tail_length")]
        public int TailLength { get; set; }
        public int Weight { get; set; }
    }
}
