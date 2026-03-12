using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spelling.Dictionary.DTOs
{
    public class Meaning
    {
        [JsonPropertyName("partOfSpeech")]
        public string partOfSpeech { get; set; } = string.Empty;
        [JsonPropertyName("definitions")]
        public List<Definition> Definitions { get; set; } = null!;
    }
}
