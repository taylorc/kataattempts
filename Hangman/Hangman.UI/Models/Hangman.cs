using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HangmanKata.UI.Models
{
    public class Hangman
    {
        private string _secretWord;
        private int _incorrectAllowedGuesses;
        private List<string> IncorrectGuesses;

        public Hangman(string secretWord, int incorrectAllowedGuesses)
        {
            _secretWord = secretWord.ToUpper();
            _incorrectAllowedGuesses = incorrectAllowedGuesses;
            InProgress = true;
            IncorrectGuesses = new List<string>();
        }

        public bool InProgress { get; private set; }

        public HangmanResult Guess(string letter)
        {
            if(!Regex.IsMatch(letter, "[A-Za-z]"))
            {
                return new IncorrectHangmanResult();
            }

            if (letter.Length > 1)
            {
                return new IncorrectHangmanResult();
            }

            if (IncorrectGuesses.FirstOrDefault(x => x.Equals(letter.ToUpper()))!=null){
                return new DuplicateHangmanGuessResult();
            }

            if (!_secretWord.Contains(letter.ToUpper()))
            {
                IncorrectGuesses.Add(letter.ToUpper());
                return new IncorrectHangmanResult();
            }

            return new HangmanResult();
        }
    }

    public class HangmanResult
    {
    }

    public class IncorrectHangmanResult : HangmanResult
    {
    }

    public class DuplicateHangmanGuessResult: HangmanResult
    {
    }
}
