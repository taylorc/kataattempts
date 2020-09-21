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
            hangman.InProgress.Should().Be(true);
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
    }
}
