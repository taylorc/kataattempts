using FluentAssertions;
using HangmanKata.UI.Models;
using System;
using Xunit;

namespace Hangman.Tests.Unit
{
    public class HangmanTests
    {
        [Fact]
        public void ShouldReturnHangmanClass()
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);
            hangman.Should().NotBeNull();
        }
        
        [Fact]
        public void ShouldReturnTrueIfGameIsUnderway()
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);
            hangman.GameState.Should().Be(GameState.InProgress);
        }

        [Theory]
        [InlineData("o", typeof(HangmanResult))]
        [InlineData("t", typeof(HangmanResult))]
        [InlineData("R", typeof(HangmanResult))]
        [InlineData("e", typeof(HangmanResult))]
        [InlineData("!", typeof(IncorrectHangmanResult))]
        [InlineData("$", typeof(IncorrectHangmanResult))]
        [InlineData("vv", typeof(IncorrectHangmanResult))]
        [InlineData("8", typeof(IncorrectHangmanResult))]
        public void GuessShouldOnlyAcceptASingleLetter(string letter, Type result)
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);

            hangman.Guess(letter).Should().BeOfType(result);
            
        }

        [Theory]
        [InlineData("r", typeof(HangmanResult))]
        [InlineData("s", typeof(HangmanResult))]
        [InlineData("q", typeof(IncorrectHangmanResult))]
        [InlineData("a", typeof(IncorrectHangmanResult))]
        public void GuessShouldReturnIncorrectGuessResultIfLetterDoesNotExistInWord(string letter, Type result)
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);

            hangman.Guess(letter).Should().BeOfType(result);

        }

        [Theory]
        [InlineData("q", typeof(DuplicateHangmanGuessResult))]
        [InlineData("z", typeof(IncorrectHangmanResult))]
        public void GuessShouldReturnDuplicateGuessResultIfLetterDoesNotExistInWord(string letter, Type result)
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);
            hangman.Guess("q");

            hangman.Guess(letter).Should().BeOfType(result);

        }

        [Fact]
        public void ShouldReturnGameWonIfAllLettersGuessedCorrectly()
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);

            foreach (var letter in secretWord)
            {
                hangman.Guess(letter.ToString());
            }

            hangman.GameState.Should().Be(GameState.Won);
        }

        [Theory]
        [InlineData("s","e","c","r","e","t","w","o","r")]
        public void ShouldReturnGameInProgressIfAllLettersAreNotGuessedAsYes(params string[] values)
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);

            foreach (var letter in values)
            {
                hangman.Guess(letter.ToString());
            }

            hangman.GameState.Should().Be(GameState.InProgress);
        }

        [Theory]
        [InlineData("s", "e", "c", "r", "e", "t", "w", "o", "r", "d", "d")]
        public void ShouldReturnGameWonIfAllLettersAreGuessedButMoreLettersAreAdded(params string[] values)
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);

            foreach (var letter in values)
            {
                hangman.Guess(letter.ToString());
            }

            hangman.GameState.Should().Be(GameState.Won);
        }

        [Theory]
        [InlineData(1, "x","a")]
        [InlineData(2, "x","y", "a")]
        [InlineData(3, "x","a", "b")]
        public void ShouldReturnGameLostIfIncorrectGuessesAreExceeded(int incorrectGuesses, params string[] values)
        {
            var secretWord = "secRetWord";

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);

            foreach (var letter in values)
            {
                hangman.Guess(letter);
            }

            hangman.GameState.Should().Be(GameState.Lost);
        }

        [Theory]
        [InlineData(1, "x", "a", "y")]
        [InlineData(2, "x", "a", "y", "x", "a", "y")]
        public void ShouldReturnGameLostIfIncorrectGuessesAreExceededButMoreLettersAreAdded(int incorrectGuesses, params string[] values)
        {
            var secretWord = "secRetWord";

            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);

            foreach (var letter in values)
            {
                hangman.Guess(letter);
            }

            hangman.GameState.Should().Be(GameState.Lost);
        }
    }
}
