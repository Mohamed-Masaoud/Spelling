using Spelling.Dictionary;

namespace Spelling.Test
{
    public class TestDictionary
    {
        [Fact]
        public void Test_valid()
        {
            IEnglishDictionary d = new IFreeDictionaryAPIImplementation();
            var result = d.GetWordDefinitionsAsync("hello").Result;

            var meaning = 
        }
    }
}