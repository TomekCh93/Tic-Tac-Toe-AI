using System;
using System.Collections.Generic;
using System.Text;

namespace Tic_Tac_Toe
{
    public static class Global
    {
        public static char[,] board = Game.FillBoard();
        public static Random rnd = new Random();
        public static int moves = 0;

    }
}