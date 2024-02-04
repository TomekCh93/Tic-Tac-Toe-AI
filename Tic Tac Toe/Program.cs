using System;
using System.Threading;

namespace Tic_Tac_Toe
{
    class Program
    {
        static void Main(string[] args)
        {
            var xWins = 0;
            var oWins = 0;
            var draws = 0;

            var playerX = new SmartComputerPlayer('X');
            var playerY = new HumanPlayer('O'); //select an opponent: smart, random or a human.
            var gameStatus = ' ';
            var turn = true;

            for (int i = 0; i < 20; i++)
            {
                while (gameStatus == ' ')
                {
                    Game.DrawStats(turn);
                    Game.Render();
                    turn = Game.MakeMove(turn, playerX, playerY);
                    Global.moves++;
                    gameStatus = Game.Evaluate();
                }

                if (gameStatus == 'X')
                {
                    Console.WriteLine(Game.PrintResults(gameStatus));
                    Thread.Sleep(5000);
                    xWins++;
                }
                else if (gameStatus == 'O')
                {
                    Console.WriteLine(Game.PrintResults(gameStatus));
                    Thread.Sleep(5000);
                    oWins++;
                }
                else
                {
                    Console.WriteLine(Game.PrintResults(gameStatus));
                    Thread.Sleep(5000);
                    draws++;
                }

                Global.moves = 0;
                Global.board = Game.FillBoard();
                gameStatus = ' ';
            }
            Console.WriteLine(Game.PrintResults(gameStatus));
            Console.WriteLine($"oWins : {oWins}, xWins : {xWins} , draws : {draws}");
        }
    }
}