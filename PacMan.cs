using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacCatherine
{
    public class PacMan : Actor
    {
        public PacMan(int iX, int iY, int sp) : base(iX, iY, sp)
        {

        }

        public void drawShip(Graphics myG)
        {
            int x = base.getX();
            int y = base.getY();
            myG.FillRectangle(Brushes.LightGreen, x, y, 20, 20);
        }

        public void drawActor(Graphics myG, Brush b)
        {
            int x = base.getX();
            int y = base.getY();
            Point[] myShape = new Point[9];

            if (base.getDirection() == 2)
            {
                myShape[0] = new Point(x + 20, y + 5);
                myShape[1] = new Point(x + 15, y);
                myShape[2] = new Point(x + 5, y);
                myShape[3] = new Point(x, y + 5);
                myShape[4] = new Point(x, y + 15);
                myShape[5] = new Point(x + 5, y + 20);
                myShape[6] = new Point(x + 15, y + 20);
                myShape[7] = new Point(x + 20, y + 15);
            }
            else if (base.getDirection() == 3)
            {
                myShape[0] = new Point(x + 15, y + 20);
                myShape[1] = new Point(x + 20, y + 15);
                myShape[2] = new Point(x + 20, y + 5);
                myShape[3] = new Point(x + 15, y);
                myShape[4] = new Point(x + 5, y);
                myShape[5] = new Point(x, y + 5);
                myShape[6] = new Point(x, y + 15);
                myShape[7] = new Point(x + 5, y + 20);
            }
            else if (base.getDirection() == 4)
            {
                myShape[0] = new Point(x, y + 5);
                myShape[1] = new Point(x + 5, y);
                myShape[2] = new Point(x + 15, y);
                myShape[3] = new Point(x + 20, y + 5);
                myShape[4] = new Point(x + 20, y + 15);
                myShape[5] = new Point(x + 15, y + 20);
                myShape[6] = new Point(x + 5, y + 20);
                myShape[7] = new Point(x, y + 15);
            }
            else
            {
                myShape[0] = new Point(x + 15, y);
                myShape[1] = new Point(x + 20, y + 5);
                myShape[2] = new Point(x + 20, y + 15);
                myShape[3] = new Point(x + 15, y + 20);
                myShape[4] = new Point(x + 5, y + 20);
                myShape[5] = new Point(x, y + 15);
                myShape[6] = new Point(x, y + 5);
                myShape[7] = new Point(x + 5, y);
            }

            myShape[8] = new Point(x + 10, y + 10);
            myG.FillPolygon(b, myShape);
        }

        public bool isBorderCollision(int scX, int scY)
        {
            int x = base.getX();
            int y = base.getY();
            bool collide = false;
            if (x >= (scX - 20) || (y <= 0) || y >= (scY - 20) || (x <= 0))
                collide = true;
            return collide;
        }

        public void drawLife(Graphics myG, Brush b, int ix, int iy)
        {
            int x = ix;
            int y = iy;
            Point[] myShape = new Point[9];

            myShape[0] = new Point(x + 9, y);
            myShape[1] = new Point(x + 12, y + 3);
            myShape[2] = new Point(x + 12, y + 9);
            myShape[3] = new Point(x + 9, y + 12);
            myShape[4] = new Point(x + 3, y + 12);
            myShape[5] = new Point(x, y + 9);
            myShape[6] = new Point(x, y + 3);
            myShape[7] = new Point(x + 3, y);

            myG.FillPolygon(b, myShape);
        }
    }
}