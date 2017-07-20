﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess {
    /// <summary>
    /// 表示一个象棋游戏房间
    /// </summary>
    [Serializable]
    public class ChessRoom {
        public int Id { get; set; }

        [NonSerialized]
        private ChessBoard _chessBoard = new ChessBoard();

        /// <summary>
        /// 此房间的棋盘
        /// </summary>
        public ChessBoard ChessBoard
        {
            get { return _chessBoard; }
            set { _chessBoard = value; }
        }

        private ChessPlayerCollection _players = new ChessPlayerCollection();

        /// <summary>
        /// 此房间的玩家
        /// </summary>
        public ChessPlayerCollection Players
        {
            get { return _players; }
            set { _players = value; }
        }

        /// <summary>
        /// 是否全部准备
        /// </summary>
        public bool IsReady { get { return _players.Count == 2 && _players.All(x => x.IsReady == true); } }

        public override string ToString() {
            return String.Format("玩家数量{0},全部准备{1}", _players.Count, IsReady);
        }

    }
}
