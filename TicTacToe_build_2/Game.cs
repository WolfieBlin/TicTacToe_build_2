using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace TicTacToe_build_2
{
    public class Game
    {
        private string[] _board;
        private string _playerTurn;
        private readonly Random _random = new Random();
        private bool _winning;
        private bool _draw;
        private string pokus;
        private bool pokracovat = true;
        
        public Game()
        {
            Start();
        }

        private void Move(int element, string player)
        {
            if (_board[element] != "X" && _board[element] != "O")
            {
                if (player == "X")
                {
                    _board[element] = "X";
                    if (Winning(_board, _playerTurn)|| Draw(_board))
                    {
                        pokracovat = true;
                    }
                    _playerTurn = "O";
                }
                else
                {
                    _board[element] = "O";
                    if (Winning(_board, _playerTurn) || Draw(_board))
                    {
                        pokracovat = true;
                    }
                    _playerTurn = "X";
                }

                
                Print(_board);
            }
        }

        private bool Draw(string[] board)
        {
            if (board[0] != "1" && board[1] != "2" && board[2] != "3" && board[3]
                != "4" && board[4] != "5" && board[5] != "6" && board[6] != "7"
                && board[7] != "8" && board[8] != "9")
            {
                _draw = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        private void Reset()
        {
            _board = new [] {"1","2","3","4","5","6","7","8","9"};
            if (_random.Next(0,2) == 1)
            {
                _playerTurn = "X";
            }
            else
            {
                _playerTurn = "O";
            }
        }

        private string[] Avail()
        {
            var availMoves = _board.Where(s => s !="X" && s != "O").ToArray();
            return availMoves;
        }

        private bool Winning(string[] board, string player)
        {
            /*if (player == "X")
            {
                this.pokus = "O";
            }
            else
            {
                this.pokus = "X";
            }*/
            if ((board[0] == player && board[1] == player && board[2] == player) ||
                (board[3] == player && board[4] == player && board[5] == player) ||
                (board[6] == player && board[7] == player && board[8] == player) ||
                (board[0] == player && board[3] == player && board[6] == player) ||
                (board[1] == player && board[4] == player && board[7] == player) ||
                (board[2] == player && board[5] == player && board[8] == player) ||
                (board[0] == player && board[4] == player && board[8] == player) ||
                (board[2] == player && board[4] == player && board[6] == player))
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

        private int HracTurn()
        {
            var choose = Convert.ToInt32(Console.ReadLine()) - 1;
            return choose;
        }

        private int AiTurn()
        {
            var choose = _random.Next(0, Avail().Length);
            return Convert.ToInt32(Avail()[choose]) - 1;
        }

        private void Start()
        {
            while (true)
            {
                Reset();
                Print(_board);
                
                while (!_draw && !_winning)
                {
                    if (_playerTurn == "X")
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
                    if (_playerTurn == "O")
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
                    break;
                }
            }
        }
    }
}