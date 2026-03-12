using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Spelling.Dictionary.DTOs
{
    public class WordDefinitions
    {
        [JsonPropertyName("word")]
        public string Word { get; set; } = string.Empty;
        [JsonPropertyName("phonetic")]
        public string Phonetic { get; set; } = string.Empty;

        [JsonPropertyName("phonetics")]
        public List<Phonetic>? Phonetics { get; set; }
        [JsonPropertyName("meanings")]
        public List<Meaning>? Meanings { get; set; }
    }
}
