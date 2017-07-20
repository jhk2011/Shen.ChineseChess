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


    class SessionService {

        ChessServer server;
        public SessionService(ChessServer server) {
            this.server = server;
        }

        public ChessSession Find(string id) {
            if (!server.Sessions.Contains(id)) return null;

            return server.Sessions[id] as ChessSession;
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

            room.ChessBoard.ChessBoardChanged += () => OnChessBoardChanged();

            room.Players.Add(player);

            room.ChessBoard.Update();
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

        }

        public event Action ChessBoardChanged;

        public ChessBoard GetChessBoard() {
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

                    //OnRoomChanged(room, (p) => {
                    //    sessionServive.Find(p.Id).ChessService.OnChessBoardChanged();
                    //});

                    //OnChessBoardChanged(room);
                }
            }
            return room.Players[0] == player ? ChessmanColor.Black : ChessmanColor.Red;
        }

        //void OnRoomChanged(ChessRoom room, Action<ChessPlayer> action) {
        //    foreach (var player in room.Players) {
        //        action(player);
        //    }
        //}

        internal void OnChessBoardChanged() {
            ChessBoardChanged.Invoke();
        }


    }
}
