using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shen.ChineseChess.UnitTests {

    public class Columns {
        public const int LeftJu = 0;
        public const int LeftMa = 1;
        public const int LeftXiang = 2;
        public const int LeftShi = 3;
        public const int Jiang = 4;
        public const int RightJu = 8;
        public const int RightMa = 7;
        public const int RightXiang = 6;
        public const int RightShi = 5;
    }

    public class Rows {
        public const int RBottom = 0;
        public const int R2 = 1;
        public const int R3 = 2;
        public const int RBing = 3;
        public const int RRiver = 4;

        public const int BBottom = 9;
        public const int B2 = 8;
        public const int B3 = 7;
        public const int BBing = 6;
        public const int BRiver = 5;
    }


    [TestClass]
    public class TestChessBoard {

        [TestMethod]
        public void TestBing() {
            Chess board = new Chess();

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
        public void TestInterval() {
            Chess board = new Chess();
            board.Initialize();
            int n = board.GetInterval(new Point(0, 0), new Point(8, 0));
            Assert.AreEqual(n, 7);

            n = board.GetInterval(new Point(4, 0), new Point(4, 9));
            Assert.AreEqual(n, 2);


            n = board.GetInterval(new Point(0, 0), new Point(0, 4));
            Assert.AreEqual(n, 1);

        }

        [TestMethod]
        public void TestJu() {
            Chess board = new Chess();
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
            Chess board = new Chess();
            board.Grid[4, 3] = new Chessman(ChessmanType.Ma, ChessmanColor.Red);
            var p = board.GetMovePlaces(new Point(4, 3));
            Assert.AreEqual(p.Count, 8);

            board.Grid[5, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);

            p = board.GetMovePlaces(new Point(4, 3));

            Assert.AreEqual(p.Count, 6);

        }

        [TestMethod]
        public void TestPao() {
            Chess board = new Chess();
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
            Chess board = new Chess();
            board.Grid[4, 0] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);

            var p = board.GetMovePlaces(new Point(4, 0));

            Assert.AreEqual(p.Count, 3);


            board = new Chess();
            board.Grid[3, 0] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);

            p = board.GetMovePlaces(new Point(3, 0));

            Assert.AreEqual(p.Count, 2);


            board = new Chess();
            board.Grid[4, 1] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);
            board.Grid[4, 9] = new Chessman(ChessmanType.Jiang, ChessmanColor.Black);

            p = board.GetMovePlaces(new Point(4, 1));

            Assert.AreEqual(p.Count, 5);

            board = new Chess();
            board.Grid[4, 1] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);

            board.Grid[4, 5] = new Chessman(ChessmanType.Pao, ChessmanColor.Red);

            board.Grid[4, 8] = new Chessman(ChessmanType.Jiang, ChessmanColor.Black);

            p = board.GetMovePlaces(new Point(4, 8));

            Assert.AreEqual(p.Count, 4);


            board = new Chess();

            board.Initialize();
            board.Grid[4, 3] = null;
            board.Grid[4, 6] = null;

            p = board.GetMovePlaces(new Point(4, 9));

            Assert.AreEqual(p.Count, 2);
        }


        [TestMethod]
        public void TestShi() {
            Chess board = new Chess();
            board.Grid[3, 0] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);

            var p = board.GetMovePlaces(new Point(3, 0));

            Assert.AreEqual(p.Count, 1);


            board = new Chess();
            board.Grid[4, 1] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);

            p = board.GetMovePlaces(new Point(4, 1));

            Assert.AreEqual(p.Count, 4);

        }

        [TestMethod]
        public void TestXiang() {
            Chess board = new Chess();
            board.Grid[2, 0] = new Chessman(ChessmanType.Xiang, ChessmanColor.Red);
            var p = board.GetMovePlaces(new Point(2, 0));
            Assert.AreEqual(p.Count, 2);

            board = new Chess();
            board.Grid[4, 2] = new Chessman(ChessmanType.Xiang, ChessmanColor.Red);
            p = board.GetMovePlaces(new Point(4, 2));
            Assert.AreEqual(p.Count, 4);
        }

        [TestMethod]
        public void TestCheck() {
            Chess board = new Chess();

            board.Initialize();


            Assert.AreEqual(board.Check(ChessmanColor.Black), false);
            Assert.AreEqual(board.Check(ChessmanColor.Red), false);

            board = new Chess();

            board.Grid[Columns.Jiang, Rows.RBottom] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);
            board.Grid[Columns.Jiang, Rows.R2] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);

            Assert.AreEqual(board.Check(ChessmanColor.Black), true);


            board.Grid[Columns.LeftShi, Rows.RBottom] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);
            board.Grid[Columns.RightShi, Rows.RBottom] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);

            Assert.AreEqual(board.Check(ChessmanColor.Black), true);
        }


        [TestMethod]
        public void TestCheckmate() {
            Chess board = new Chess();

            board.Initialize();


            Assert.AreEqual(board.Checkmate(ChessmanColor.Black), false);
            Assert.AreEqual(board.Checkmate(ChessmanColor.Red), false);

            board = new Chess();

            //重炮

            board.Grid[Columns.Jiang, Rows.RBottom] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);
            board.Grid[Columns.LeftShi, Rows.RBottom] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);
            board.Grid[Columns.RightShi, Rows.RBottom] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);


            board.Grid[Columns.Jiang, Rows.B3] = new Chessman(ChessmanType.Pao, ChessmanColor.Black);
            board.Grid[Columns.Jiang, Rows.RRiver] = new Chessman(ChessmanType.Pao, ChessmanColor.Black);
            
            Assert.AreEqual(board.Checkmate(ChessmanColor.Black), true);


            board.Grid[Columns.LeftJu, Rows.BRiver] = new Chessman(ChessmanType.Pao, ChessmanColor.Red);

            Assert.AreEqual(board.Checkmate(ChessmanColor.Black), false);

            //无法移动

            board = new Chess();

            board.Grid[Columns.LeftShi, Rows.RBottom] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);
            board.Grid[Columns.Jiang, Rows.R2] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);

            Assert.AreEqual(board.Checkmate(ChessmanColor.Black), true);


            board = new Chess();

            board.Grid[Columns.LeftShi, Rows.RBottom] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);
            board.Grid[Columns.Jiang, Rows.R2] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);
            board.Grid[Columns.Jiang, Rows.RBottom] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);


            board.Grid[Columns.RightShi, Rows.BBottom] = new Chessman(ChessmanType.Ju, ChessmanColor.Black);
            board.Grid[Columns.Jiang, Rows.BBottom] = new Chessman(ChessmanType.Jiang, ChessmanColor.Black);

            Assert.AreEqual(board.Checkmate(ChessmanColor.Black), true);
        }
    }
}
