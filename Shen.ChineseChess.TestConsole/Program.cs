using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            long m1 = GC.GetTotalMemory(true);

            ChessBoard board = new ChessBoard();
            board.Initialize();

            long m2 = GC.GetTotalMemory(true);

            Console.WriteLine(m2-m1);

            //var p = board.GetMovePlaces(new Point(3,4));

            //Console.WriteLine(board);
        }
    }
}
