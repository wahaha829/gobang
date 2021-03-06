﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gobang
{
    public partial class Form1 : Form
    {
        public bool turn_to_black = true; // 黑子先下
        board board1 = new board(); // 棋盤座標值物件
        board_array[,] Board_array = new board_array[9, 9]; // 棋盤index陣列
        public int chess_linked; // 棋子相連個數
        public bool isGameover = false; // 是否五子已相連

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            create_board_array();
        }
        
        


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //放棋子
            if (board1.isCursor_near(e.X, e.Y))
            // 游標在交叉點附近才能放棋子
            {
                if(isGameover != true)
                {
                // 非五子相連遊戲結束時才給放棋子
                    if (turn_to_black == true)
                    {
                        //在交叉點上放黑棋
                        this.Controls.Add(new black_chess(board1.move_torightx(), board1.move_torighty()));
                        Board_array[board1.index_row, board1.index_col].chess_type = 1;
                        Board_array[board1.index_row, board1.index_col].isplaced = true;
                        if (isfive(board1.index_row, board1.index_col))
                        {
                            MessageBox.Show("黑子贏");
                            isGameover = true;
                            label6.Text = "遊戲結束";
                        }
                        else
                        {
                            pictureBox1.Image = Properties.Resources.white;
                            turn_to_black = false;
                            label5.Text = "白子下";
                        }
                    }
                    else
                    {
                        //在交叉點上放白棋
                        this.Controls.Add(new white_chess(board1.move_torightx(), board1.move_torighty()));
                        Board_array[board1.index_row, board1.index_col].chess_type = 0;
                        Board_array[board1.index_row, board1.index_col].isplaced = true;
                        if (isfive(board1.index_row, board1.index_col))
                        {
                            MessageBox.Show("白子贏");
                            isGameover = true;
                            label6.Text = "遊戲結束";
                        }
                        else
                        {
                            turn_to_black = true;
                            pictureBox1.Image = Properties.Resources.black;
                            label5.Text = "黑子下";
                        }   
                    }
                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // 滑鼠移動的時候就會呼叫這個方法，e.X,e.Y是滑鼠座標(內建屬性)
            if (board1.isCursor_near(e.X, e.Y))
            {
                this.Cursor = Cursors.Hand;  // 游標移到交叉點附近就變換圖案
            }
            else
                this.Cursor = Cursors.Default;
            // 印出座標、index值
            label1.Text = "index_row : " + board1.index_row;
            label2.Text = "index_col : " + board1.index_col;
            label3.Text = "x座標值 : " + e.X;
            label4.Text = "y座標值 : " + e.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            restart();
        }

        public void create_board_array()
        {
            //建立棋盤index陣列
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    Board_array[i, j] = new board_array();
                }
            }
        }
        public void restart()
        {
            // 初始化
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    Board_array[i, j].chess_type = -1;
                    Board_array[i, j].isplaced = false;
                }
            }
            label5.Text = "黑子下";
            label6.Text = "";
            pictureBox1.Image = Properties.Resources.black;
            isGameover = false;
            turn_to_black = true;

            // 移除已放置的棋子pictureBox
            //新增一個控制項基底類別的list，篩選出this.Controls裡的chess，Add到List
            List<Control> chess_list = new List<Control>();
            foreach (Control ctr in this.Controls)
            {
                if (ctr is chess)
                {
                    chess_list.Add(ctr);
                }
            }
            //從this.Controls把剛剛篩選出來的chess移除
            for (int i = 0; i < chess_list.Count; i++)
            {
                this.Controls.Remove(chess_list[i]);
            }
        }


        public bool isfive(int row, int col)
        //依據放下棋子的index值，檢查四個方向是否有五子相連
        {
            int[] row_array = new int[] {-1, -1, 0, 1}; //row的 北,東北,東,東南
            int[] col_array = new int[] { 0,  1, 1, 1}; //col的 北,東北,東,東南

            for (int j = 0; j <= 3; j++) // 0-3代表4種方向
            {
                chess_linked = 1;
                for (int i = 1; i <= 4; i++) // 1-4正向檢查4顆棋子
                {
                    try
                    {
                        if (Board_array[row, col].chess_type == Board_array[row + i * row_array[j], col + i * col_array[j]].chess_type)
                        {
                            chess_linked++; // 棋子類型相同就+1
                        }
                        else
                            break;
                    }
                    catch
                    {
                        break;
                    }
                }
                for (int i = 1; i <= 4; i++) // 1-4反向檢查4顆棋子
                {
                    try
                    {
                        if (Board_array[row, col].chess_type == Board_array[row - i * row_array[j], col - i * col_array[j]].chess_type)
                        {
                            chess_linked++;
                        }
                        else
                            break;
                    }
                    catch
                    {
                        break;
                    }
                }
                if (chess_linked >= 5)
                    return true; 
            }
            return false;
        }

        /*
        public bool isfive()
        {
            // 逐行逐列檢查整個陣列判斷是否有五顆棋子相連，可行但太沒效率
            // 判斷橫向
            for(int i=0; i<=8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    chess_linked = 1;
                    for (int k=1; k<=4; k++)
                    {
                        try
                        {
                            if (Board_array[i, j].isplaced == true &&
                                Board_array[i, j + k].isplaced == true &&
                                Board_array[i, j].chess_type == Board_array[i, j + k].chess_type)
                                chess_linked++;
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break; //跳過index out of range
                        }
                    }
                    if (chess_linked == 5)
                        return true;
                }
            }
            // 判斷縱向
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    chess_linked = 1;
                    for (int k = 1; k <= 4; k++)
                    {
                        try
                        {
                            if (Board_array[i, j].isplaced == true &&
                                Board_array[i + k, j].isplaced == true &&
                                Board_array[i, j].chess_type == Board_array[i + k, j].chess_type)
                                chess_linked++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break; //跳過index out of range
                        }
                    }
                    if (chess_linked == 5)
                        return true;
                }
            }
            // 判斷斜向\
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    chess_linked = 1;
                    for (int k = 1; k <= 4; k++)
                    {
                        try
                        {
                            if (Board_array[i, j].isplaced == true &&
                                Board_array[i+k, j+k].isplaced == true &&
                                Board_array[i, j].chess_type == Board_array[i+k, j+k].chess_type)
                                chess_linked++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break; //跳過index out of range
                        }
                    }
                    if (chess_linked == 5)
                        return true;
                }
            }
            // 判斷斜向/
            for (int i = 0; i <= 8; i++)
            {
                for (int j = 0; j <= 8; j++)
                {
                    chess_linked = 1;
                    for (int k = 1; k <= 4; k++)
                    {
                        try
                        {
                            if (Board_array[i, j].isplaced == true &&
                                Board_array[i+k, j-k].isplaced == true &&
                                Board_array[i, j].chess_type == Board_array[i+k, j-k].chess_type)
                                chess_linked++;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            break; //跳過index out of range
                        }
                    }
                    if (chess_linked == 5)
                        return true;
                }
            }
            return false;
        }
        */

    }
}
