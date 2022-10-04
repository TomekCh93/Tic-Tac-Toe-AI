using System;
using System.Collections.Generic;
using System.Threading;

namespace Tic_Tac_Toe
{


    public class Player
    {
        private char _letter;

        public char Letter
        {
            get
            {
                return _letter;
            }

            set
            {
                _letter = value;
            }
        }


        public Player(char letter)
        {
            _letter = letter;
        }

        public virtual void GetMove()
        {
            Console.WriteLine($"Turn of {this.Letter}. Specify row and then column without space.");
            var userInput = Console.ReadLine();
            var rowParse = 0;
            var colParse = 0;
            while (!CheckUserInput(userInput, out rowParse, out colParse))
            {

                Console.WriteLine("You specified the wrong value. Specify the row number first then the column.");
                userInput = Console.ReadLine();
            }

            Game.FillWithMove((rowParse, colParse), this.Letter);
            return;


        }

        private bool CheckUserInput(string userInput, out int rowParse, out int colParse)
        {

            try
            {

                rowParse = int.Parse(userInput[0].ToString());
                colParse = int.Parse(userInput[1].ToString());

                if (rowParse <= 0 || rowParse > 3 || colParse <= 0 || colParse > 3 || userInput.Length < 2)
                {

                    return false;
                }
            }
            catch (Exception)
            {
                rowParse = colParse = 0;
                return false;
            }


            return Game.CheckIfFieldNotOccupied((rowParse, colParse));
        }


    }

    public class HumanPlayer : Player
    {

        public HumanPlayer(char letter) : base(letter)
        {
        }


    }

    public class RandomComputerPlayer : Player
    {

        public RandomComputerPlayer(char letter) : base(letter)
        {
        }

        public override void GetMove()
        {
            var randomRow = 0;
            var randomCol = 0;
            var occupied = false;
            while (!occupied)
            {
                randomRow = Global.rnd.Next(1, 4);
                randomCol = Global.rnd.Next(1, 4);
                occupied = Game.CheckIfFieldNotOccupied((randomRow, randomCol));
            }

            Game.FillWithMove((randomRow, randomCol), this.Letter);
            return;
        }
    }

    public class SmartComputerPlayer : Player
    {

        public SmartComputerPlayer(char letter) : base(letter)
        {
        }

        public override void GetMove()
        {

            var bestScore = int.MinValue;

            (int, int) bestMove = (0, 0);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Global.board[i, j] == ' ')
                    {

                        Global.board[i, j] = this.Letter;
                        Global.moves++;
                        int score = Minimax(true, 0); // start with maximizing
                        Global.board[i, j] = ' ';
                        Global.moves--;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }

                    }
                }
            }
            //if (!Game.CheckIfFieldNotOccupied((bestMove.Item1+1, bestMove.Item2+1)))
            //{
            //    Console.WriteLine("We encountered an issue!");

            //    Thread.Sleep(133000);
            //}

            Game.FillWithMove((bestMove.Item1 + 1, bestMove.Item2 + 1), this.Letter);

            return;
        }

        private int Minimax(bool maximizingMove, int depth)
        {
            var result = Game.Evaluate();
            if (result != ' ')
            {
                if (result == 'D')
                {
                    return 0;
                }
                var score = result == this.Letter ? (10 - depth) : (-10 + depth);
                return score;
            }
            int bestScore;
            char letter;
            if (maximizingMove)
            {
                bestScore = int.MaxValue;
                letter = this.Letter == 'X' ? 'O' : 'X';
            }
            else
            {
                bestScore = int.MinValue;
                letter = this.Letter;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Global.board[i, j] == ' ')
                    {
                        Global.board[i, j] = letter;
                        Global.moves++;
                        int score = maximizingMove ? Minimax(false, depth + 1) : Minimax(true, depth + 1);
                        Global.board[i, j] = ' ';
                        Global.moves--;
                        bestScore = maximizingMove ? Math.Min(score, bestScore) : Math.Max(score, bestScore);

                    }
                }
            }
            return bestScore;

        }
    }

}