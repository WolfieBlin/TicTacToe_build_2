using System;
using System.Collections.Generic;

namespace TicTacToe_build_2
{
    public class MinMax : Game
    {
        public int Minimax(string[] reboard, bool player) {
           // iter++;
           //var score = new List<int>();
            var array = Avail(reboard);
            if (WinningMinMax(reboard, true)) {
                //score.Add(-10);
                return -10;
            } else if (WinningMinMax(reboard, false)) {
              // score.Add(10);
              return 10;
            } else if (array.Length == 0) {
               // score.Add(0);
               return 0;
            }

            var moves = new List<int>();
            var movesscore = new List<int>();
            for (var i = 0; i < array.Length; i++)
            {
                int movescore;
                int move = Convert.ToInt32(array[i]);
                //var movescore = new List<int>();
                
                if (player)
                {
                    reboard[Convert.ToInt32(array[i]) - 1] = "X";
                }
                else
                {
                    reboard[Convert.ToInt32(array[i]) - 1] = "O";
                }

                if (!player) {
                    var g = Minimax(reboard, true);
                    movescore = g;
                } else {
                    var g = Minimax(reboard, false);
                    movescore = g;
                }
                reboard[Convert.ToInt32(array[i]) - 1] = move.ToString();
                moves.Add(move);
                movesscore.Add(movescore);
            }

            int bestMove = 0;
            if (player == false) {
                var bestScore = -10000;
                for (var i = 0; i < moves.Count; i++) {
                    if (movesscore[i] > bestScore) {
                        bestScore = movesscore[i];
                        bestMove = i;
                    }
                }
            } else {
                var bestScore = 10000;
                for (var i = 0; i < moves.Count; i++) {
                    if (movesscore[i] < bestScore) {
                        bestScore = movesscore[i];
                        bestMove = i;
                    }
                }
            }
            return moves[bestMove];
        }
    }
}