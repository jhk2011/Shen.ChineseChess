using System;
using System.Collections.Generic;
using System.Linq;
using Dao.Net;

namespace Shen.ChineseChess.Server {

    class RoomService {

        ChessRoomCollection rooms;

        public RoomService() {
            rooms = new ChessRoomCollection();
            for (int i = 0; i < 10; i++) {
                rooms.Add(new ChessRoom {
                    Id = i
                });
            }
        }

        public ChessRoomCollection GetRooms() {
            return rooms;
        }

        public ChessRoom Find(int id) {
            return rooms.Where(x => x.Id == id).FirstOrDefault();
        }


    }

    class ChessService : IChessService {

        RoomService roomService;

        public ChessService(RoomService roomService) {
            this.roomService = roomService;
        }


        public ChessRoomCollection GetRooms() {
            return roomService.GetRooms();
        }


        public ChessSession Session { get { return SocketContext.Current.Session as ChessSession; } }

        public void Login(string name) {
            Session.Name = name;

            var player = new ChessPlayer { Name = name, Id = Session.Id };

            Session.Player = player;

            Session.ChessService = this;

        }

        public void Join(int rid) {

            var player = Session.Player;

            if (player == null) {
                throw new InvalidOperationException("未登录");
            }

            if (player.Number != null && player.Number != rid) {
                throw new InvalidOperationException("不能同时进入多个房间");
            }

            ChessRoom room = roomService.Find(rid);

            if (room == null) {
                throw new InvalidOperationException("房间不存在");
            }

            if (room.Players.Count >= 2) {
                throw new InvalidOperationException("房间已满");
            }

            player.Number = rid;

            room.RoomChanged += () => OnRoomChanged();
            room.ChessBoard.ChessBoardChanged += () => OnChessBoardChanged();

            if (room.Players.Count == 0) {
                player.Color = ChessmanColor.Red;
            } else {
                player.Color = room.Players[0].Color == ChessmanColor.Red ?
                    ChessmanColor.Black :
                    ChessmanColor.Red;
            }

            room.Players.Add(player);

            room.Update();
        }

        private void OnRoomChanged() {
            RoomChanged?.Invoke();
        }

        public void Leave() {

            var player = Session.Player;

            if (player == null) throw new InvalidOperationException("");

            var room = roomService.Find((int)player.Number);

            if (room == null) throw new InvalidOperationException("房间不存在");

            if (!room.Players.Contains(player)) throw new InvalidOperationException();

            player.Number = null;
            player.IsReady = false;

            room.Players.Remove(player);

            room.Update();
        }

        public event Action ChessBoardChanged;

        public event Action RoomChanged;

        public Chess GetChessBoard() {
            return roomService.Find((int)Session.Player.Number)?.ChessBoard;
        }

        public void Move(Point point, Point dest) {
            var player = Session.Player;
            if (player == null) return;
            if (player.Number == null) return;

            ChessRoom room = roomService.Find((int)player.Number);

            room.ChessBoard.Move(point, dest);

            //OnChessBoardChanged(room);
        }

        public ChessmanColor Ready() {

            var player = Session.Player;

            if (player == null) throw new InvalidOperationException("");

            var room = roomService.Find((int)player.Number);

            if (room == null) throw new InvalidOperationException("房间不存在");

            if (!room.Players.Contains(player)) throw new InvalidOperationException("不在此房间");

            if (!player.IsReady) {

                player.IsReady = true;

                if (room.IsReady) {

                    room.ChessBoard.Initialize();
                }
            }

            room.Update();


            return player.Color;

            //return room.Players[0] == player ? ChessmanColor.Red : ChessmanColor.Black;
        }


        internal void OnChessBoardChanged() {
            ChessBoardChanged.Invoke();
        }

        public ChessRoom GetRoom() {
            var id = Session.Player.Number;
            if (id == null) return null;
            return roomService.Find((int)id);
        }
    }
}
