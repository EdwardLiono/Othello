using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}
		Button[,] board = new Button[8, 8];
		int[,] map = new int[8, 8];
        int end;
		private void Form1_Load(object sender, EventArgs e)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					board[i, j] = new Button();
					board[i, j].Location = new Point(i*50,j*50);
					board[i, j].Size = new Size(50, 50);
					board[i, j].Name = "b" + i.ToString() + j.ToString();
					board[i, j].Click += new EventHandler(buttonpress);
					board[i, j].BackColor = Color.Gray;
                    board[i, j].ForeColor = Color.Red;
					this.Controls.Add(board[i, j]);
				}
			}
			board[3, 3].BackColor = Color.Black;
			board[4, 4].BackColor = Color.Black;
			board[3, 4].BackColor = Color.White;
			board[4, 3].BackColor = Color.White;
			button1.Tag = "White";
			button1.BackColor = Color.White;
            checker();
		}

		void buttonpress(object sender, EventArgs e)
		{
			Button now = (Button)sender;
            int i, j;
            i = Convert.ToInt32(now.Name[1]);
            j = Convert.ToInt32(now.Name[2]);
            i -= 48;
            j -= 48;
            checker();

            if (map[i, j] != 0 && board[i,j].BackColor==Color.Gray)
            {

                move(i, j);
                if (button1.Tag == "Black")
                {
                    button1.Tag = "White";
                }
                else
                {
                    button1.Tag = "Black";
                }
                checker();
                int asd = 0;
                for (int x = 0; x < 8; x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                       if(map[x,y] != 0)
                       {
                            asd = 1;
                       }
                    }
                }
                if(asd != 1)
                {
                    if (button1.Tag == "Black")
                    {
                        button1.Tag = "White";
                    }
                    else
                    {
                        button1.Tag = "Black";
                    }
                }
                else
                {
                    checker();
                    if (button1.Tag == "Black")
                    {
                        button1.BackColor = Color.Black;
                    }
                    else
                    {
                        button1.BackColor = Color.White;
                    }
                }
            }
            if (button1.BackColor == Color.Black)
            {
                //AI();
            }
            endcheck();
        }
        void endcheck()
        {
            checker();
            int echeck=0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] != 0)
                    {
                        echeck++;
                    }
                }
            }
            if (button1.BackColor == Color.White)
            {
                button1.Tag = "Black";
                checker();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (map[i, j] != 0)
                        {
                            echeck++;
                        }
                    }
                }
                button1.Tag = "White";
            }
            else
            {
                button1.Tag = "White";
                checker();
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (map[i, j] != 0)
                        {
                            echeck++;
                        }
                    }
                }
                button1.Tag = "Black";
            }
            checker();
            if (echeck == 0)
            {
                if (Convert.ToInt32(label3.Text) > Convert.ToInt32(label4.Text))
                {
                    MessageBox.Show("White win");
                }
                else if(Convert.ToInt32(label3.Text) < Convert.ToInt32(label4.Text))
                {
                    MessageBox.Show("Black win");
                }
                else
                {
                    MessageBox.Show("Draw");
                }
            }
        }
		void checker()
		{
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    map[i, j] = 0;
                }
            }
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (board[i, j].BackColor == Color.Gray)
					{
                        for (int x = 0; x < 8; x++)
                        {
                            int a, b, heu, i2, j2;
                            a = b = heu = 0;
                            i2 = i;
                            j2 = j;
                            if(x == 0|| x == 1||x==2)
                            {
                                a = 1;
                            }

                            if (x == 4 || x == 5 || x == 6)
                            {
                                a = -1;
                            }
                            if (x == 2 || x == 3 || x == 4)
                            {
                                b = 1;
                            }
                            if (x == 0 || x == 6 || x == 7)
                            {
                                b = -1;
                            }
                            while (true)
                            {
                                
                                i2 = i2 + a;
                                j2 = j2 + b;
                                if (i2 == 8 || i2 == -1 || j2 == 8 || j2 == -1||board[i2,j2].BackColor==Color.Gray)
                                {
                                    break;
                                }
                                else if(button1.Tag == "Black")
                                {
                                    if(board[i2,j2].BackColor == Color.White)
                                    {
                                        heu = heu + 1;
                                    }
                                    if (board[i2, j2].BackColor == Color.Black)
                                    {
                                        map[i, j] = map[i,j]+heu;
                                        break;
                                    }
                                }
                                else if (button1.Tag == "White")
                                {
                                    if (board[i2, j2].BackColor == Color.Black)
                                    {
                                        heu = heu + 1;
                                    }
                                    if (board[i2, j2].BackColor == Color.White)
                                    {
                                        map[i, j] = map[i, j] + heu;
                                        break;
                                    }
                                }
                            }
                            if (board[i, j].BackColor != Color.Gray)
                            {
                                map[i, j] = 0;
                            }
                        }
					}
                    board[i, j].Text = map[i, j].ToString();
				}
			}
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {

                }
            }
            int W, B;
            W = 0;
            B = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board[x, y].BackColor == Color.White)
                    {
                        W++;
                    }
                    else if (board[x,y].BackColor == Color.Black)
                    {
                        B++;
                    }
                }
            }
            label3.Text = W.ToString();
            label4.Text = B.ToString();
            
		}

        void move(int i, int j)
        {
            for (int x = 0; x < 8; x++)
            {
                int a, b, heu, i2, j2;
                a = b = heu = 0;
                i2 = i;
                j2 = j;
                if (x == 0 || x == 1 || x == 2)
                {
                    a = 1;
                }

                if (x == 4 || x == 5 || x == 6)
                {
                    a = -1;
                }
                if (x == 2 || x == 3 || x == 4)
                {
                    b = 1;
                }
                if (x == 0 || x == 6 || x == 7)
                {
                    b = -1;
                }
                bool end=false;
                while (true)
                {
                    if (end == true)
                    {
                        break;
                    }
                    i2 = i2 + a;
                    j2 = j2 + b;
                    if (i2 == 8 || i2 == -1 || j2 == 8 || j2 == -1 || board[i2, j2].BackColor == Color.Gray)
                    {
                        break;
                    }
                    else if (button1.Tag == "Black")
                    {

                        if (board[i2, j2].BackColor == Color.Black)
                        {
                            while (true)
                            {
                                i2 = i2 - a;
                                j2 = j2 - b;
                                if (i2 == i && j2 == j)
                                {
                                    board[i2, j2].BackColor = Color.Black;
                                    end = true;
                                    break;
                                }
                                else
                                {
                                    board[i2, j2].BackColor = Color.Black;
                                }
                            }
                        }
                    }
                    else if (button1.Tag == "White")
                    {

                        if (board[i2, j2].BackColor == Color.White)
                        {
                            while (true)
                            {
                                i2 = i2 - a;
                                j2 = j2 - b;
                                if (i2 == i && j2 == j)
                                {
                                    board[i2, j2].BackColor = Color.White;
                                    end = true;
                                    break;
                                }
                                else
                                {
                                    board[i2, j2].BackColor = Color.White;
                                }
                            }
                        }
                    }
                }
            }
        }
        
        void AI()
        {
            checker();
            int x, y;
            x = y = 0;
            if (board[2, 2].BackColor == Color.Gray || board[2, 5].BackColor == Color.Gray || board[5, 5].BackColor == Color.Gray || board[5, 2].BackColor == Color.Gray)
            {
                for (int i = 2; i < 6; i++)
                {
                    for (int j = 2; j < 6; j++)
                    {
                        map[i, j] = map[i, j] * 2;
                    }
                }

                map[2, 2] = map[2, 2] * 100;
                map[2, 5] = map[2, 5] * 100;
                map[5, 2] = map[5, 2] * 100;
                map[5, 5] = map[5, 5] * 100;
            }
            else if (64 - (Convert.ToInt32(label3.Text) + Convert.ToInt32(label4.Text)) > 15) {
                map[0, 0] = map[0, 0] * 100;
                map[0, 7] = map[0, 7] * 100;
                map[7, 0] = map[7, 0] * 100;
                map[7, 7] = map[7, 7] * 100;
            }


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] > map[x, y])
                    {
                        x = i;
                        y = j;
                    }
                }
            }
            board[x, y].PerformClick();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

