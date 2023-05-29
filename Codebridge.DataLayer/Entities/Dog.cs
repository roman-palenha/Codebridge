using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Codebridge.DataLayer.Entities
{
    public class Dog
    {
        [Key]
        [Column("name")]
        public string Name { get; set; }
        [Column("color")]
        public string Color { get; set; }
        [Column("tail_length")]
        [JsonPropertyName("tail_length")]
        public int TailLength { get; set; }
        [Column("weight")]
        public int Weight { get; set; }
    }
}
