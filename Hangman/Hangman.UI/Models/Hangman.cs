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

        private readonly RulesEngine _rulesEngine;

        public Hangman(string secretWord, int incorrectAllowedGuesses)
        {
            _secretWord = secretWord.ToUpper();
            _incorrectAllowedGuesses = incorrectAllowedGuesses;
            _correctGuesses = 0;
            IncorrectGuesses = new List<string>();
            GameState = GameState.InProgress;
            _rulesEngine = new RulesEngine(_secretWord);
        }

        public HangmanResult Guess(string letter)
        {
            HangmanResult result = new HangmanResult();

            if (GameState == GameState.Lost || GameState == GameState.Won)
            {
                return result;
            }

            if (!_rulesEngine.IsAValidLetter(letter))
            {
                _incorrectAllowedGuesses--;
                result = new IncorrectHangmanResult();
            }

            if (!_rulesEngine.IsLetterASingleOne(letter))
            {
                _incorrectAllowedGuesses--;
                result = new IncorrectHangmanResult();
            }

            if (_rulesEngine.HasLetterBeenGuessedPreviously(letter, IncorrectGuesses)){
                _incorrectAllowedGuesses--;
                return new DuplicateHangmanGuessResult();
            }

            if (!_rulesEngine.SecretWordContainsLetter(letter))
            {
                _incorrectAllowedGuesses--;
                IncorrectGuesses.Add(letter.ToUpper());
                result = new IncorrectHangmanResult();
            }

            if (IncorrectGuesses.Count() > _incorrectAllowedGuesses)
            {
                GameState = GameState.Lost;
            }

            _correctGuesses++;

            if (_correctGuesses == _secretWord.Length)
                GameState = GameState.Won;


            return result;
        }
    }

    public class RulesEngine
    {
        private string _secretWord;

        public RulesEngine(string secretWord)
        {
            _secretWord = secretWord;
        }

        internal bool HasLetterBeenGuessedPreviously(string letter, List<string> incorrectGuesses)
        {
            return incorrectGuesses.FirstOrDefault(x => x.Equals(letter.ToUpper())) != null;
        }

        internal bool IsAValidLetter(string letter)
        {
            return Regex.IsMatch(letter, "[A-Za-z]");
        }

        internal bool IsLetterASingleOne(string letter)
        {
            return letter.Length == 1;
        }

        internal bool SecretWordContainsLetter(string letter)
        {
            return _secretWord.Contains(letter, StringComparison.InvariantCultureIgnoreCase);
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
