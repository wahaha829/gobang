using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace gobang
{
    class chess : PictureBox // 棋子繼承自PictureBox
    {
        public int chess_w = 36;
        public chess(int x, int y)
        {
            this.BackColor = Color.Transparent;
            this.Location = new Point(x - chess_w/2, y - chess_w/2);
            this.Size = new Size(chess_w, chess_w);

        }

    }
}
