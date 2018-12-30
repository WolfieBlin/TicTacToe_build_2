using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace TicTacToe_build_2
{
    public class Game
    {
        private string[] _board;
        private int _playerTurn;
        private readonly Random _random = new Random();
        private bool _winning;
        private bool _draw;
        
        public Game()
        {
            Start();
        }

        private void Move(int element, int player)
        {
            if (_board[element] != "X" && _board[element] != "O")
            {
                if (player == 1)
                {
                    _board[element] = "X";
                    _playerTurn = 2;
                }
                else
                {
                    _board[element] = "O";
                    _playerTurn = 1;
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
            _playerTurn = _random.Next(0,2);
        }

        private string[] Avail()
        {
            var availMoves = _board.Where(s => s !="X" && s != "O").ToArray();
            return availMoves;
        }

        private bool Winning(string[] board)
        {
            if ((board[0] == board[1] && board[1] == board[2]) ||
                (board[3] == board[4] && board[5] == board[4]) ||
                (board[6] == board[7] && board[8] == board[7]) ||
                (board[0] == board[3] && board[6] == board[3]) ||
                (board[1] == board[4] && board[7] == board[4]) ||
                (board[2] == board[5] && board[8] == board[5]) ||
                (board[0] == board[4] && board[8] == board[4]) ||
                (board[2] == board[4] && board[6] == board[4]))
            {
                _playerTurn = _playerTurn - 1;
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
                
                while (!Winning(_board) && !Draw(_board))
                {
                    if (_playerTurn == 1)
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
                    if (_playerTurn == 1)
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
                if (Console.ReadLine() == "y")
                {
                    Reset();
                    continue;
                }

                break;
            }
        }
    }
}