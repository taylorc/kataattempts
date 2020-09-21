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
        private int _correctGuesses;
        private List<string> IncorrectGuesses;
        public GameState GameState { get; set; }

        public Hangman(string secretWord, int incorrectAllowedGuesses)
        {
            _secretWord = secretWord.ToUpper();
            _incorrectAllowedGuesses = incorrectAllowedGuesses;
            _correctGuesses = 0;
            IncorrectGuesses = new List<string>();
            GameState = GameState.InProgress;

        }

        public HangmanResult Guess(string letter)
        {
            HangmanResult result = new HangmanResult();

            if (GameState == GameState.Lost || GameState == GameState.Won)
            {
                return result;
            }

            result = IsAValidLetter(letter, result);
            result = IsAValidLength(letter, result);

            if (IncorrectGuesses.FirstOrDefault(x => x.Equals(letter.ToUpper())) != null)
            {
                _incorrectAllowedGuesses--;
                return new DuplicateHangmanGuessResult();
            }

            result = IsACorrectGuess(letter, result);
            IsGameLost();

            _correctGuesses++;
            IsGameWon();

            return result;
        }

        private void IsGameWon()
        {
            if (_correctGuesses == _secretWord.Length)
                GameState = GameState.Won;
        }

        private void IsGameLost()
        {
            if (IncorrectGuesses.Count() > _incorrectAllowedGuesses)
            {
                GameState = GameState.Lost;
            }
        }

        private HangmanResult IsACorrectGuess(string letter, HangmanResult result)
        {
            if (!_secretWord.Contains(letter.ToUpper()))
            {
                _incorrectAllowedGuesses--;
                IncorrectGuesses.Add(letter.ToUpper());
                result = new IncorrectHangmanResult();
            }

            return result;
        }

        private HangmanResult IsAValidLength(string letter, HangmanResult result)
        {
            if (letter.Length > 1)
            {
                _incorrectAllowedGuesses--;
                result = new IncorrectHangmanResult();
            }

            return result;
        }

        private HangmanResult IsAValidLetter(string letter, HangmanResult result)
        {
            if (!Regex.IsMatch(letter, "[A-Za-z]"))
            {
                _incorrectAllowedGuesses--;
                result = new IncorrectHangmanResult();
            }

            return result;
        }
    }


    public class HangmanResult
    {
        
    }

    public class IncorrectHangmanResult: HangmanResult
    {
    }

    public class DuplicateHangmanGuessResult: HangmanResult
    {
    }

    public enum GameState
    {
        Won, Lost, InProgress
    }
}
