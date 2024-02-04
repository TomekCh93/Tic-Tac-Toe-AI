using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe
{
    public class Game
    {
        public static void Render()
        {
            Console.Clear();
            Console.WriteLine($" |  1  |  2  |  3  |  "); ;

            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i + 1}|  ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(Global.board[i, j]);
                    Console.Write($"  |  ");
                }

                Console.WriteLine();
            }
        }

        public static char Evaluate()
        {
            for (int row = 0; row < Global.board.GetLength(0); row++)
            {
                if (AllEqual(Global.board[row, 0], Global.board[row, 1], Global.board[row, 2]) && Global.board[row, 0] != ' ')
                {
                    return Global.board[row, 0];
                }

            }
            for (int col = 0; col < Global.board.GetLength(0); col++)
            {
                if (AllEqual(Global.board[0, col], Global.board[1, col], Global.board[2, col]) && Global.board[0, col] != ' ')
                {
                    return Global.board[0, col];
                }

            }

            var diagonal1 = new List<char>();
            var diagonal2 = new List<char>();

            for (int i = 0; i < Global.board.GetLength(0); i++)
            {
                for (int j = 0; j < Global.board.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        diagonal1.Add(Global.board[i, j]);
                    }

                    if ((i + j) == (Global.board.GetLength(0) - 1))
                    {
                        diagonal2.Add(Global.board[i, j]);
                    }

                }
                if (diagonal1.Distinct().Count() == 1 && diagonal1.Count >= 3)
                {
                    return diagonal1[0];
                }
                if (diagonal2.Distinct().Count() == 1 && diagonal2.Count >= 3)
                {
                    return diagonal2[0];
                }
            }

            if (Global.moves >= 9)
            {
                return 'D';
            }
            else
            {
                return ' ';
            }
        }

        public static string PrintResults(char status)
        {
            Game.Render();
            return (status == ' ' || status == 'D') ? "Draw" : (status == 'X') ? "player X wins" : "player O wins";
        }
        public static bool MakeMove(bool turn, Player playerOne, Player playerTwo)
        {
            if (turn)
            {
                playerOne.GetMove();

            }
            else
            {
                playerTwo.GetMove();

            }
            return turn ? false : true;
        }
        public static char[,] FillBoard()
        {
            var res = new char[3, 3];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    res[i, j] = ' ';
                }
            }

            return res;
        }
        public static bool CheckIfFieldNotOccupied((int, int) values)
        {
            if (Global.board[values.Item1 - 1, values.Item2 - 1] != ' ')
            {
                return false;
            }
            return true;
        }
        public static void FillWithMove((int, int) values, char letter)
        {
            Global.board[values.Item1 - 1, values.Item2 - 1] = letter;
            return;
        }

        public static void DrawStats(bool turn)
        {
            if (turn)
            {
                Console.WriteLine("Ruch gracza X");
            }
            else
            {
                Console.WriteLine("Ruch gracza O");
            }
        }

        public static bool AllEqual<T>(params T[] values)
        {
            if (values == null || values.Length == 0)
            {
                return false;
            }
            return values.Distinct().Count() == 1;
        }
    }
}