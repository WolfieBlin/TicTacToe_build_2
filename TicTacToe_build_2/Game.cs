using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace TicTacToe_build_2
{
    public class Game
    {
        public string[] _board;
        public bool _playerTurn;
        private readonly Random _random = new Random();
        private bool _winning;
        private bool _draw;
        private bool _continue = true;
        public string[] _reboard;
        /*public string pcPlayer = "X";
        public string aiPlayer = "X";*/
        
        //až se v tom budeš někdy hrabat tak nezapomeň že to jde na chuja a ani
        //teď nevíš jak to je s tou výhrou kdy je X a kdy O tak bacha kryple
        public Game()
        {
            Start();
        }

        public int Minimax(string[] reboard, bool player) {
           // iter++;
           //var score = new List<int>();
            var array = Avail(reboard);
            if (Winning(reboard, true)) {
                //score.Add(-10);
                return -10;
            } else if (Winning(reboard, false)) {
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
                //var movescore = new List<int>();
                var move = Convert.ToInt32(array[i]);
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
        
        private void Move(int element, bool player)
        {
            if (_board[element] != "X" && _board[element] != "O")
            {
                if (player)
                {
                    _board[element] = "X";
                    Winning(_board, _playerTurn);
                    Draw(_board);
                    _playerTurn = false;
                }
                else
                {
                    _board[element] = "O";
                    Winning(_board, _playerTurn);
                    Draw(_board);
                    _playerTurn = true;
                }

                
                Print(_board);
            }
        }

        private void Draw(string[] board)
        {
            if (board[0] != "1" && board[1] != "2" && board[2] != "3" && board[3]
                != "4" && board[4] != "5" && board[5] != "6" && board[6] != "7"
                && board[7] != "8" && board[8] != "9")
            {
                _draw = true;
            }
          
        }
        private void Reset()
        {
            _board = new [] {"1","2","3","4","5","6","7","8","9"};
            _winning = false;
            _draw = false;
            
            if (_random.Next(0,2) == 1)
            {
                _playerTurn = true;
            }
            else
            {
                _playerTurn = false;
            }
        }

        public string[] Avail(string[] reboard)
        {
            var availMoves = reboard.Where(s => s !="X" && s != "O").ToArray();
            return availMoves;
        }

        public bool Winning(string[] board, bool player)
        {
            string playerXO;
            /*if (player == "X")
            {
                this.pokus = "O";
            }
            else
            {
                this.pokus = "X";
            }*/
            if (player)
            {
                playerXO = "X";
            }
            else
            {
                playerXO = "O";
            }
            
            if ((board[0] == playerXO && board[1] == playerXO && board[2] == playerXO) ||
                (board[3] == playerXO && board[4] == playerXO && board[5] == playerXO) ||
                (board[6] == playerXO && board[7] == playerXO && board[8] == playerXO) ||
                (board[0] == playerXO && board[3] == playerXO && board[6] == playerXO) ||
                (board[1] == playerXO && board[4] == playerXO && board[7] == playerXO) ||
                (board[2] == playerXO && board[5] == playerXO && board[8] == playerXO) ||
                (board[0] == playerXO && board[4] == playerXO && board[8] == playerXO) ||
                (board[2] == playerXO && board[4] == playerXO && board[6] == playerXO))
            {
                
                _winning = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Print(string[] board)
        {
            Console.Clear();
            
            Console.WriteLine("Vítejte v TicTacToe, kde hrajete proti počítači.");
            Console.WriteLine("Hrajete za křížek: X a potítač za kolečko: O");
            
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[0], board[1], board[2]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[3], board[4], board[5]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[6], board[7], board[8]);
            Console.WriteLine("     |     |      ");
        }
//
        private int HracTurn()
        {
            var choose = Convert.ToInt32(Console.ReadLine()) - 1;
            return choose;
        }

        private int AiTurn()
        {
            var choose = _random.Next(0, Avail(_board).Length);
            return Convert.ToInt32(Avail(_board)[choose]) - 1;
        }

        private void Start()
        {
            while (_continue)
            {
                Reset();
                Print(_board);
                
                while (!_draw && !_winning)
                {
                    if (_playerTurn)
                    {
                        Move(HracTurn(), _playerTurn);
                    }
                    else
                    {
                        Move(AiTurn(), _playerTurn);
                    }
                }

                if (_winning)
                {
                    if (!_playerTurn)
                    {
                        Console.WriteLine("Vyhrál: X");
                    }
                    else
                    {
                        Console.WriteLine("Vyhrál: O");
                    }
                }
                else if (_draw)
                {
                    Console.WriteLine("It's a draw!");
                }


                Console.WriteLine("Chcete hrát znovu? y/n");
                if (Console.ReadLine() != "y")
                {
                    _continue = false;
                }
            }
        }
    }
}