using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Shen.ChineseChess.Server
{
    public partial class MainForm : Form
    {
        ChessServer server = new ChessServer();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            server.Initialize(1888);
            btnStart.Enabled = false;
            timer1.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lstRooms.DataSource = null;
            lstRooms.DataSource = server.RoomService.GetRooms();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btnStart_Click(null, null);
        }

    }
}
