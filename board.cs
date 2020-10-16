using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace gobang
{
    class board
    //棋盤座標
    {

        public int origin_x = 45, origin_y = 45;
        public int grid_width = 45;
        public int index_row, index_col ;
        public int near = 8;
        public List<int> axis_x = new List<int>();
        public List<int> axis_y = new List<int>();
        
        public board()
        {
            //建立交叉點座標List
            for (int i=0; i<=8; i++)
            {
                axis_x.Add(origin_x + grid_width * i);
                axis_y.Add(origin_y + grid_width * i);
            }
        }
        public bool isCursor_near(int x, int y)
        // 找出離游標最近的交叉點，判斷游標是不是在交叉點附近
        {
            index_row = -1; 
            index_col = -1;
            for (int i = 0; i <= 8; i++)
            {
                if (x >= axis_x[i] - near && x <= axis_x[i] + near)
                {
                    index_col = i; // 先判斷x軸，是的話紀錄交叉點index_col(xy軸跟行列相反)
                    break;
                }
            }
            for (int j = 0; j <= 8; j++)
            {
                if (y >= axis_y[j] - near && y <= axis_y[j] + near)
                {
                    index_row = j; // 判斷y軸，是的話紀錄交叉點index_row
                    break;
                }
            }
            if (index_row != -1 && index_col != -1)
                return true; //x,y都在交叉點上，回傳true
            else
                return false;
        }
        public int move_torightx()
        {
            // 回傳最近的交叉點上x座標值
            return axis_x[index_col];
        }
        public int move_torighty()
        {
            // 回傳最近的交叉點上y座標值
            return axis_y[index_row];
        }

        
    }

}
