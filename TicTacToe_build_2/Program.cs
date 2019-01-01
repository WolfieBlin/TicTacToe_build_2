using System;

namespace TicTacToe_build_2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var game = new Game();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}