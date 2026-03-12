using Spelling.Commands;
using Spelling.Sentences;
using System;
using System.Speech.Synthesis;
using System.Windows.Input;

namespace Spelling.ViewModels
{
    public class SpellingViewModel : BaseViewModel
    {
        private readonly ISentenceGetter _sentenceGetter;
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();

        private string _sentence;
        private string _text = string.Empty;
        private string _message = string.Empty;

        public SpellingViewModel()
        {
            _sentenceGetter = new SentenceFromTextFile();
            _sentence = _sentenceGetter.GetSentence();

            _synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.NotSet);
            _synth.Rate = 0;
            _synth.Volume = 100;

            Check = new RelayCommand(CheckSentence);
            Reveal = new RelayCommand(RevealSentence);
            Next = new RelayCommand(NextSentence);
            Read = new RelayCommand(ReadSentence);
        }

        public string Sentence
        {
            get => _sentence;
            set
            {
                _sentence = value;
                OnPropertyChanged(nameof(Sentence));
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand Reveal { get; }
        public ICommand Check { get; }
        public ICommand Next { get; }
        public ICommand Read { get; }

        private void CheckSentence()
        {
            if (string.IsNullOrWhiteSpace(Text))
            {
                Message = "Please type the sentence first.";
                return;
            }

            var expectedWords = Sentence.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var typedWords = Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int minLength = Math.Min(expectedWords.Length, typedWords.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (!expectedWords[i].Equals(typedWords[i], StringComparison.OrdinalIgnoreCase))
                {
                    Message = $"Word {i + 1} is incorrect. Expected: \"{expectedWords[i]}\"";
                    return;
                }
            }

            if (typedWords.Length < expectedWords.Length)
            {
                Message = "You are missing some words.";
                return;
            }

            if (typedWords.Length > expectedWords.Length)
            {
                Message = "You typed extra words.";
                return;
            }

            Message = "Correct. Click Next for another sentence.";
        }

        private void ReadSentence()
        {
            try
            {
                _synth.SpeakAsyncCancelAll();
                _synth.SpeakAsync(Sentence);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        private void NextSentence()
        {
            Sentence = _sentenceGetter.GetSentence();
            Text = string.Empty;
            Message = string.Empty;
            ReadSentence();
        }

        private void RevealSentence()
        {
            Message = Sentence;
        }
    }
}