using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HangmanKata.UI.Models
{
    public class Hangman
    {
        private string _secretWord;
        private int _incorrectGuesses;

        public Hangman(string secretWord)
        {
            _secretWord = secretWord.ToUpper();

        }

        public Hangman(string secretWord, int incorrectGuesses)
        {
            _secretWord = secretWord;
            _incorrectGuesses = incorrectGuesses;
            InProgress = true;
        }

        public bool InProgress { get; private set; }

        public bool Guess(string letter)
        {
            if(!Regex.IsMatch(letter, "[A-Za-z]"))
            {
                return false;
            }

            if (letter.Length > 1)
            {
                return false;
            }

            return true;
        }
    }
}
