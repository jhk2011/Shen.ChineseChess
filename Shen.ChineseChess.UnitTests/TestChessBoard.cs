using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shen.ChineseChess.UnitTests {
    [TestClass]
    public class TestChessBoard {

        [TestMethod]
        public void TestBing() {
            ChessBoard board = new ChessBoard();

            board.Grid[4, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);

            var p = board.GetMovePlaces(new Point(4, 3));

            Assert.AreEqual(p.Count, 1);

            board.Grid[4, 5] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);


            p = board.GetMovePlaces(new Point(4, 5));

            Assert.AreEqual(p.Count, 3);

            board.Grid[0, 5] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);

            p = board.GetMovePlaces(new Point(0, 5));

            Assert.AreEqual(p.Count, 2);

        }

        [TestMethod]
        public void TestGezi() {
            ChessBoard board = new ChessBoard();
            board.Initialize();
            int n = board.CalculateCount(new Point(0, 0), new Point(8, 0));
            Assert.AreEqual(n, 7);

            n = board.CalculateCount(new Point(4, 0), new Point(4, 9));
            Assert.AreEqual(n, 2);


            n = board.CalculateCount(new Point(0, 0), new Point(0, 4));
            Assert.AreEqual(n, 1);

        }

        [TestMethod]
        public void TestJu() {
            ChessBoard board = new ChessBoard();
            board.Grid[4, 3] = new Chessman(ChessmanType.Ju, ChessmanColor.Red);
            var p = board.GetMovePlaces(new Point(4, 3));
            //毫无阻挡的情况下应为17个可放位置
            Assert.AreEqual(p.Count, 17);

            board.Grid[5, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);

            p = board.GetMovePlaces(new Point(4, 3));
            Assert.AreEqual(p.Count, 17 - 4);

            board.Grid[3, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);
            p = board.GetMovePlaces(new Point(4, 3));
            Assert.AreEqual(p.Count, 17 - 4 - 3);

        }

        [TestMethod]
        public void TestMa() {
            ChessBoard board = new ChessBoard();
            board.Grid[4, 3] = new Chessman(ChessmanType.Ma, ChessmanColor.Red);
            var p = board.GetMovePlaces(new Point(4, 3));
            Assert.AreEqual(p.Count, 8);

            board.Grid[5, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);

            p = board.GetMovePlaces(new Point(4, 3));

            Assert.AreEqual(p.Count, 6);

        }

        [TestMethod]
        public void TestPao() {
            ChessBoard board = new ChessBoard();
            board.Grid[4, 3] = new Chessman(ChessmanType.Pao, ChessmanColor.Red);
            var p = board.GetMovePlaces(new Point(4, 3));
            Assert.AreEqual(p.Count, 17);

            board.Grid[5, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);
            p = board.GetMovePlaces(new Point(4, 3));
            Assert.AreEqual(p.Count, 13);

            board.Grid[6, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);
            p = board.GetMovePlaces(new Point(4, 3));
            Assert.AreEqual(p.Count, 14);
        }

        [TestMethod]
        public void TestJiang() {
            ChessBoard board = new ChessBoard();
            board.Grid[4, 0] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);

            var p = board.GetMovePlaces(new Point(4, 0));

            Assert.AreEqual(p.Count, 3);


            board = new ChessBoard();
            board.Grid[3, 0] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);

            p = board.GetMovePlaces(new Point(3, 0));

            Assert.AreEqual(p.Count, 2);


            board = new ChessBoard();
            board.Grid[4, 1] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);
            board.Grid[4, 9] = new Chessman(ChessmanType.Jiang, ChessmanColor.Black);

            p = board.GetMovePlaces(new Point(4, 1));

            Assert.AreEqual(p.Count,5);

            board = new ChessBoard();
            board.Grid[4, 1] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);

            board.Grid[4, 5] = new Chessman(ChessmanType.Pao, ChessmanColor.Red);

            board.Grid[4, 8] = new Chessman(ChessmanType.Jiang, ChessmanColor.Black);

            p = board.GetMovePlaces(new Point(4, 8));

            Assert.AreEqual(p.Count, 4);


            board = new ChessBoard();

            board.Initialize();
            board.Grid[4, 3] = null;
            board.Grid[4, 6] = null;

            p = board.GetMovePlaces(new Point(4, 9));

            Assert.AreEqual(p.Count, 2);
        }


        [TestMethod]
        public void TestShi() {
            ChessBoard board = new ChessBoard();
            board.Grid[3, 0] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);

            var p = board.GetMovePlaces(new Point(3, 0));

            Assert.AreEqual(p.Count, 1);


            board = new ChessBoard();
            board.Grid[4, 1] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);

            p = board.GetMovePlaces(new Point(4, 1));

            Assert.AreEqual(p.Count, 4);

        }

        [TestMethod]
        public void TestXiang() {
            ChessBoard board = new ChessBoard();
            board.Grid[2, 0] = new Chessman(ChessmanType.Xiang, ChessmanColor.Red);
            var p = board.GetMovePlaces(new Point(2, 0));
            Assert.AreEqual(p.Count, 2);

            board = new ChessBoard();
            board.Grid[4, 2] = new Chessman(ChessmanType.Xiang, ChessmanColor.Red);
            p = board.GetMovePlaces(new Point(4, 2));
            Assert.AreEqual(p.Count, 4);
        }
    }
}
