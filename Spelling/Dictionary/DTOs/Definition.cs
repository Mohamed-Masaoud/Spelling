using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spelling.Dictionary.DTOs
{
    public class Definition
    {
        [JsonPropertyName("definition")]
        public string Text { get; set; } = string.Empty;
        [JsonPropertyName("example")]
        public string Example { get; set; } = string.Empty;
    }
}
