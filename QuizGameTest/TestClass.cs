using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using QuizGame;

namespace QuizGameTest
{
   [TestFixture]
    public class TestClass
    {

         Player player = new Player();
       
        [Test]
        public void readQuestionsTest()
        {
            player.name = "TestPlayer";
            Game game = new Game(player);
            game.questionQuantity = 2;
            game.readQuestions();
            Assert.That(game.questions, Is.Not.Empty);
        }
        [Test]
        public void DisplayMenuTest()
        {
            player.name = "TestPlayer";
            Game game = new Game(player);
            game.questionQuantity = 2;
            Mock<IGame> fooMock = new Mock<IGame>();
            fooMock.Setup(f => f.DisplayMenu(player)).Equals("");
        }
        [Test]
        public void DisplayQuestion()
        {
            player.name = "TestPlayer";
            Game game = new Game(player);
            game.questionQuantity = 2;
            game.readQuestions();
            Mock<IGame> fooMock = new Mock<IGame>();
            fooMock.Setup(f => f.DisplayQuestion(1)).Returns(1);
        }

        [Test]
        public void CheckAnswerTest()
        {
            player.name = "TestPlayer";
            Game game = new Game(player);
            game.questionQuantity = 2;
            string[] distr = new string[] { "1890", "1765", "1492", "1200"};
            Question q1 = new Question("En que anno se conquisto America?", 3, distr, 2);
            string[] distr2 = new string[] { "4", "5", "56", "22" };
            Question q2 = new Question("Cuanto es 2+2?", 1, distr2, 2);
            string[] distr3 = new string[] { "4", "5", "56", "22" };
            Question q3 = new Question("Cual es la capital de CR?", 1, distr2, 2);
            List<Question> listq = new List<Question>();
            listq.Add(q1);
            listq.Add(q2);
            game.questions = listq;
            bool expResult = game.CheckAnswer(game.round, 3);
            Assert.That(expResult, Is.True);
        }

        [Test]
        public void AddQuestionTest()
        {
            player.name = "TestPlayer";
            Game game = new Game(player);
            game.questionQuantity = 2;
            string[] distr = new string[] { "1890", "1765", "1492", "1200" };
            Question q1 = new Question("En que anno se conquisto America?", 3, distr, 2);
            bool expResult = game.AddQuestion(q1);
            Assert.That(expResult, Is.True);
        }
        [Test]
        public void RemoveQuestionTest()
        {
            player.name = "TestPlayer";
            Game game = new Game(player);
            game.questionQuantity = 2;
            bool expResult = game.RemoveQuestion(1);
            Assert.That(expResult, Is.True);
        }
        [Test]
        public void EditQuestionTest()
        {
            player.name = "TestPlayer";
            Game game = new Game(player);
            game.questionQuantity = 2;
            bool expResult = game.EditQuestion(1);
            Assert.That(expResult, Is.True);
        }
        [Test]
        public void IncrementDificultyTest()
        {
            player.name = "TestPlayer";
            Game game = new Game(player);
            game.questionQuantity = 2;
            int prevDiff = game.gameDifficulty;
            game.IncrementDificulty();
            Assert.That(game.gameDifficulty, Is.EqualTo(prevDiff+1));

        }
    }
}
