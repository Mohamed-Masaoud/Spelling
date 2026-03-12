using Spelling.Dictionary.DTOs;
using Spelling.Properties;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Spelling.Dictionary
{
    public class IFreeDictionaryAPIImplementation : IEnglishDictionary
    {
        private string _url;

        public IFreeDictionaryAPIImplementation()
        {
            _url = Properties.Settings.Default.FreeDictionaryApiUrl;
        }
        public async Task<List<WordDefinitions>> GetWordDefinitionsAsync(string word)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage reponse = await client.GetAsync($"{_url}{word}");

            if(!reponse.IsSuccessStatusCode) throw new Exception($"Failed to fetch definitions for the word: {word}");

            string jsonResponse = await reponse.Content.ReadAsStringAsync();
            var definitions = JsonSerializer.Deserialize<List<WordDefinitions>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});


            return definitions ?? new List<WordDefinitions>();
        }
    }
}
