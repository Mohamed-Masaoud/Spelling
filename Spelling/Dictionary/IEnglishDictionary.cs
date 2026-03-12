using Spelling.Dictionary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelling.Dictionary
{
    public interface IEnglishDictionary
    {
        Task<List<WordDefinitions>> GetWordDefinitionsAsync(string word);
    }
}
