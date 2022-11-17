using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisClient
{
    internal class Figure //данные о фигурках, на сервер?
    {
        public int x;
        public int y;
        public int[,] matrix;
        public int sizeMatrix;

        public int[,] stickFigure = new int[4, 4]{
            {0,0,1,0},
            {0,0,1,0},
            {0,0,1,0},
            {0,0,1,0},
        };

        public int[,] zFigure = new int[3, 3]{
            {0,2,0},
            {0,2,2},
            {0,0,2},
        };

        public int[,] revzFigure = new int[3, 3]{
            {0,6,0},
            {6,6,0},
            {6,0,0},
        };

        public int[,] tFigure = new int[3, 3]{
            {0,0,0},
            {3,3,3},
            {0,3,0},
        };

        public int[,] lFigure = new int[3, 3]{
            {4,0,0},
            {4,0,0},
            {4,4,0},
        };

        public int[,] revlFigure = new int[3, 3]{
            {0,0,7},
            {0,0,7},
            {0,7,7},
        };

        public int[,] cubeFigure = new int[2, 2]{
            {5,5},
            {5,5},
        };

        public Figure(int _x, int _y)
        {
            x = _x;
            y = _y;
            FigureGen();
        }
        public void FigureGen() //рандомизатор фигурок, на сервер
        {
            Random r = new Random();
            sizeMatrix = 3;
            switch (r.Next(1, 8))
            {
                case (1):
                    sizeMatrix = 4;
                    matrix = stickFigure;
                    break;
                case (2):
                    matrix = zFigure;
                    break;
                case (3):
                    matrix = tFigure;
                    break;
                case (4):
                    matrix = lFigure;
                    break;
                case (5):
                    sizeMatrix = 2;
                    matrix = cubeFigure;
                    break;
                case (6):
                    matrix = revzFigure;
                    break;
                case (7):
                    matrix = revlFigure;
                    break;

            }
        }
        public void Down() //движения, на сервер
        {
            y++;
        }        
    }
}
