using Spelling.Commands;
using Spelling.Dictionary;
using Spelling.Dictionary.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Spelling.ViewModels
{
    public class DictionaryViewModel : BaseViewModel
    {
		// fields
		IEnglishDictionary _dictionary;
        private string _searchWord = string.Empty;
        private string _errorMessage = string.Empty;
        private ObservableCollection<WordDefinitions> _definitions = new();
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();

        // constructor
        public DictionaryViewModel()
        {
            _dictionary = new IFreeDictionaryAPIImplementation();
            Search = new RelayCommand(async () => await SearchWordAsync(), CanSearch);
            Clear = new RelayCommand(ClearSearch);
            PlayPhoneticAudio = new RelayCommand(PlayAudio);
        }



        // properties
        public string SearchWord
		{
			get { return _searchWord; }
			set 
			{ 
				_searchWord = value; 
				OnPropertyChanged(nameof(SearchWord));
            }
		}

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ObservableCollection<WordDefinitions> Definitions
        {
            get { return _definitions; }
            set
            {
                _definitions = value;
                OnPropertyChanged(nameof(Definitions));
            }
        }

        // commands
        public ICommand Search { get; }
        public ICommand Clear { get; }
        public ICommand PlayPhoneticAudio { get; }

        private void ClearSearch()
        {
            SearchWord = string.Empty;
            ErrorMessage = string.Empty;
            Definitions.Clear();
        }

        private bool CanSearch()
        {
            return !string.IsNullOrWhiteSpace(SearchWord);
        }

        private async Task SearchWordAsync()
        {
            try
            {
                ErrorMessage = string.Empty;
                Definitions.Clear();

                var definitions = await _dictionary.GetWordDefinitionsAsync(SearchWord);

                if (definitions != null && definitions.Any())
                {
                    foreach (var definition in definitions)
                    {
                        Definitions.Add(definition);
                    }
                }
                else
                {
                    ErrorMessage = $"No definitions found for the word: {SearchWord}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error fetching definitions: {ex.Message}";
            }
        }

        private void PlayAudio()
        {
            string? audioUrl = Definitions.FirstOrDefault()?.Phonetics?.FirstOrDefault(s => !string.IsNullOrEmpty(s.Audio))?.Audio;
            if (string.IsNullOrWhiteSpace(audioUrl))
            {
                ErrorMessage = "No audio available for this phonetic.";
                return;
            }

            try
            {
                if (audioUrl.StartsWith("//"))
                {
                    audioUrl = "https:" + audioUrl;
                }

                _mediaPlayer.Open(new Uri(audioUrl, UriKind.Absolute));
                _mediaPlayer.Play();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Could not play audio: {ex.Message}";
            }
        }
    }
        
}
