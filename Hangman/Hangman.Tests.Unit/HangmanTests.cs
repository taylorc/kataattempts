using FluentAssertions;
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

        [Fact]
        public void GuessShouldAcceptALetter()
        {
            var secretWord = "secRetWord";
            var incorrectGuesses = 5;

            /*
             var action = () => classToTest.MethodToTest();
    action.Should().Throw<InvalidOperationException>();
             */
            var hangman = new HangmanKata.UI.Models.Hangman(secretWord, incorrectGuesses);

            hangman.Guess("q").Should().BeTrue();
            
        }


    }
}
