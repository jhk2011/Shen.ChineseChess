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

            client.ChessService.Login("abc");
            lstRooms.DataSource = client.ChessService.GetRooms();
        }

        private void btnEnter_Click(object sender, EventArgs e) {

            int index = lstRooms.SelectedIndex;

           

            MainForm frm = new MainForm {
                rid=index,
                client = client
            };

            frm.Show();
        }
    }
}
