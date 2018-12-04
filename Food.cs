using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacCatherine
{
    public class Food
    {
        private int x;
        private int y;
        private int point;
        private bool active;

        public Food(int iX, int iY, int p)
        {
            reset(iX, iY, p);
            active = true;
        }

        public void reset(int iX, int iY, int p)
        {
            x = iX;
            y = iY;
            point = p;
        }

        public int getPoint()
        {
            return point;
        }

        public bool isHot()
        {
            return active;
        }

        public void deactivate()
        {
            active = false;
        }

        public void drawFood(Graphics myG, Brush b)
        {
            if (active)
            {
                Point[] myShape = new Point[4];

                myShape[0] = new Point(x + 5, y + 10);
                myShape[1] = new Point(x + 10, y + 5);
                myShape[2] = new Point(x + 15, y + 10);
                myShape[3] = new Point(x + 10, y + 15);
                
                myG.FillPolygon(b, myShape);
            }
        }

        public bool collideWithPacMan(PacMan pm)
        {
            int px = pm.getX();
            int py = pm.getY();
            bool collide = false;
            if ((px >= (x - 20)) & (px <= (x + 20)) & (py >= (y - 20)) & (py <= (y + 20)))
                collide = true;
            return collide;
        }

        public void setX(int xx)
        {
            x = xx;
        }

        public void setY(int yy)
        {
            y = yy;
        }

    }
}