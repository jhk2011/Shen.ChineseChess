using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Shen.ChineseChess.Client {
    public partial class RoomForm : Form {
        ChessClient client = new ChessClient();

        public RoomForm() {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e) {
            await client.ConnectAsync("192.168.3.110", 1888);

            Console.WriteLine("Connected");

            if (string.IsNullOrEmpty(txtName.Text)) {
                txtName.Text =( Guid.NewGuid() + "n").Substring(0,6);
            }

            client.ChessService.Login(txtName.Text);

            lstRooms.DataSource = client.ChessService.GetRooms();
        }

        private void btnEnter_Click(object sender, EventArgs e) {

            var room = lstRooms.SelectedItem as ChessRoom;

            MainForm frm = new MainForm {
                rid = room.Id,
                client = client
            };

            frm.Show();
        }
    }
}
