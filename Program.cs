using System;

namespace PokerAnalyzer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
                new Game().start();
        }
    }
}