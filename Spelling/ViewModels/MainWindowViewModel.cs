using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spelling.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
            DictionaryVM = new DictionaryViewModel();
            SpellingVM = new SpellingViewModel();
        }

        public DictionaryViewModel DictionaryVM { get; }
        public SpellingViewModel SpellingVM { get; } 
    }
}
