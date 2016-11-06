using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame
{
   public class Player
    {
        public string name;
        public int score;

        public Player()
        {
        
        }

    }
    public class Question
    {
        public string question;
        public int answer;
        public string[] distractors;
        public int difficulty;

        public Question(string question, int answer, string[] distractors, int difficulty)
        {
            this.question = question;
            this.answer = answer;
            this.distractors = distractors;
            this.difficulty = difficulty;  
        }
    }
    public class FileReader
    {
        public int line;
        public int questionQuantity;
        public string path;

        public FileReader(string path, int qq)
        {
            this.path = path;
            this.questionQuantity = qq;
        }

        public List<Question> readFile(int gameDifficulty)
        {
            var reader = new StreamReader(File.OpenRead(path));
            int questAdded = 0;
            List<Question> questionsList = new List<Question>();
            string line;
            while (!reader.EndOfStream || questAdded < questionQuantity)
            {
                 line = reader.ReadLine();
               
                string[] values = line.Split(';');

                if((Convert.ToInt32(values[6]) == gameDifficulty))
                {
                    string[] distractors = new string[] { values[2], values[3], values[4], values[5] };
                    Question q = new Question(values[0], Convert.ToInt32(values[1]), distractors, Convert.ToInt32(values[6]));
                    questionsList.Add(q);
                    questAdded++;
                }
            }
            return questionsList;
        }

    }
    public interface IGame
    {
        void DisplayMenu(Player player);
        int DisplayQuestion(int i);
    }
    public class Game : IGame
    {
        public Player player;
        public List<Question> questions;
        public int round;
        public int questionQuantity;
        public int gameDifficulty;

        public Game(Player player)
        {
            this.player = player;
            this.round = 0;
            this.gameDifficulty = 1;
        }

        public void readQuestions()
        {
            FileReader fr = new FileReader(@"C:\Users\Denisse\Documents\2016\Aseguramiento\Proyectos\QuizGame\questions.csv", questionQuantity);
            questions = fr.readFile(gameDifficulty);
        }

       public void DisplayMenu(Player player)
        {
            Console.WriteLine();
            Console.WriteLine("----------Welcome to the Quiz Game "+player.name+"!---------------");
            Console.WriteLine();
            Console.WriteLine("Please type in the number of questions you want to answer: ");
        }

        public int DisplayQuestion(int i)
        {
                Console.WriteLine(i+1 +". "+questions[i].question);
                Console.WriteLine("1." + questions[i].distractors[0]);
                Console.WriteLine("2." + questions[i].distractors[1]);
                Console.WriteLine("3." + questions[i].distractors[2]);
                Console.WriteLine("4." + questions[i].distractors[3]);
                int z = Convert.ToInt32(Console.ReadLine());
                return z;
        }

        public bool CheckAnswer(int question, int answer)
        {
            if (questions[question].answer == answer) {
                player.score++;
                return true;
            }
            return false;
        }

        public bool AddQuestion(Question question)
        {
            //Some code to add question to the file
            return true;
        }

        public bool RemoveQuestion(int question)
        {
            //Some code to remove question from the file
            return true;
        }

        public bool EditQuestion(int question)
        {
            //Some code to edit question from the file
            return true;
        }

        public void IncrementDificulty()
        {
            //Some code to increment difficulty of the game
            gameDifficulty++;

        }
        static void Main(string[] args)
        {
            
            Player player1 = new Player();
            Console.WriteLine("What's your name? ");
            player1.name = Console.ReadLine();
            Game game1 = new Game(player1);
            game1.DisplayMenu(player1);
            game1.questionQuantity = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            
            game1.readQuestions();
            //string[] distr = new string[] { "1890", "1765", "1492", "1200"};
            //Question q1 = new Question("En que anno se conquisto America?", 3, distr, 2);
            //string[] distr2 = new string[] { "4", "5", "56", "22" };
            //Question q2 = new Question("Cuanto es 2+2?", 1, distr2, 2);
            //string[] distr3 = new string[] { "4", "5", "56", "22" };
            //Question q3 = new Question("Cual es la capital de CR?", 1, distr2, 2);
            //List<Question> listq = new List<Question>();
            //listq.Add(q1);
            //listq.Add(q2);
            //game1.questions = listq;

            while (game1.round < game1.questionQuantity)
            {
                int ans = game1.DisplayQuestion(game1.round);
                if (game1.CheckAnswer(game1.round,ans))
                {
                    Console.WriteLine();
                    Console.WriteLine("Correct!");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Wrong Answer :(");
                    Console.WriteLine("The answer is : " + game1.questions[game1.round].distractors[game1.questions[game1.round].answer -1]);
                    Console.WriteLine();
                }
                game1.round++;
            }

            Console.WriteLine();
            Console.WriteLine("Game Finished. Your score was: " + game1.player.score);

            Console.ReadKey(true);
        }
    }
}
