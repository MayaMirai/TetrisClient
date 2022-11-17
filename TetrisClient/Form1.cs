using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Schema;
using Microsoft.Win32;

namespace TetrisClient
{
    public partial class Form1 : Form
    {
        Figure currentFigure;
        int size, width, lenght, pos;
        int[,] map = new int[20, 10];
        int cleared;
        int score;
        int timeElapsed;
        DateTime date1 = new DateTime(0, 0);

        public Form1()
        {
            InitializeComponent();
            timer2.Start();
            Init();
        }

        public void Init()
        {
            size = 25;
            width = 10; lenght = 20;
            pos = 25;
            cleared= 0;
            score = 0;
            currentFigure = new Figure(3, 0);
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 500;
            timer.Tick += new EventHandler(update);
            timer.Enabled = true;
            timer.Start();
            

            label1.Text = "Счёт: " + score;
            
            Invalidate();

        }
        public void DrawMap(Graphics e)
        {
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] == 1)
                    {
                        e.FillRectangle(Brushes.Cyan, new Rectangle(pos + j * size + 1, pos + i * size + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 2)
                    {
                        e.FillRectangle(Brushes.Green, new Rectangle(pos + j * size + 1, pos + i * size + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 3)
                    {
                        e.FillRectangle(Brushes.Purple, new Rectangle(pos + j * size + 1, pos + i * size + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 4)
                    {
                        e.FillRectangle(Brushes.Orange, new Rectangle(pos + j * size + 1, pos + i * size + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 5)
                    {
                        e.FillRectangle(Brushes.Yellow, new Rectangle(pos + j * size + 1, pos + i * size + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 6)
                    {
                        e.FillRectangle(Brushes.Red, new Rectangle(pos + j * size + 1, pos + i * size + 1, size - 1, size - 1));
                    }
                    if (map[i, j] == 7)
                    {
                        e.FillRectangle(Brushes.Blue, new Rectangle(pos + j * size + 1, pos + i * size + 1, size - 1, size - 1));
                    }
                }
            }
        }

        public void DrawGrid(Graphics g)
        {
            for(int i = 0; i <= lenght; i++)
            {
                g.DrawLine(Pens.Black, new Point(pos, pos + i * size), new Point(pos + width * size, pos + i * size));
            }

            for (int i = 0; i <= width; i++)
            {
                g.DrawLine(Pens.Black, new Point(pos + i * size, pos), new Point(pos + i * size, pos + lenght * size));
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void update(object sender, EventArgs e)
        {
            Erase();
            if (!Collide())
            { 
                currentFigure.Down();
            } else
            {
                Merge();
                currentFigure = new Figure(3, 0);
            }
            Merge();
            Invalidate();
            
        }

        public void Merge()
        {
            for(int i=currentFigure.y; i < currentFigure.y + currentFigure.sizeMatrix; i++)
            {
                for (int j = currentFigure.x; j < currentFigure.x + currentFigure.sizeMatrix; j++)
                {
                    if (currentFigure.matrix[i - currentFigure.y, j - currentFigure.x] != 0)
                        map[i,j] = currentFigure.matrix[i - currentFigure.y,j - currentFigure.x];
                }
            }
        }
        public void Erase()
        {
            for (int i = currentFigure.y; i < currentFigure.y + currentFigure.sizeMatrix; i++)
            {
                for (int j = currentFigure.x; j < currentFigure.x + currentFigure.sizeMatrix; j++)
                {
                    if (i >= 0 && j >= 0 && i < lenght && j < width)
                    {
                        if (currentFigure.matrix[i - currentFigure.y, j - currentFigure.x] != 0)
                        map[i, j] = 0;
                    }
                    
                }
            }
        }

        

        public bool Collide()
        {
            for (int i = currentFigure.y + currentFigure.sizeMatrix - 1; i >= currentFigure.y; i--)
            {
                for (int j = currentFigure.x; j < currentFigure.x + currentFigure.sizeMatrix; j++)
                {
                    if (currentFigure.matrix[i - currentFigure.y, j - currentFigure.x] != 0)
                    {
                        if (i + 1 == lenght)
                            return true;
                        if (map[i + 1, j] != 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e) // на сервер
        {
            timeElapsed++;
            label2.Text = "Время: " + timeElapsed.ToString();
        }

        public void Clear() //всё на сервер или только подсчёт 
        {
            int counter = 0;
            int cCleared = 0;
            for(int i = 0; i < lenght; i++ )
            {
                counter = 0;
                for (int j = 0; j < width; j++)
                {
                    if (map[i, j] != 0)
                    counter++;
                }
                if (counter == width)
                {
                    cCleared++;
                    for(int k = i; k >= 0; k--)
                    {
                        for (int n = 0; n < width; n++)
                        {
                            map[k + 1, n] = map[k, n];
                        }
                    }
                }
            }
            for(int i = 0; i < cCleared; i++) 
            {
                score += 100 * (i+1);
            }
            label1.Text = "Счет: " + score;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
            DrawMap(e.Graphics);
        }
    }
}
