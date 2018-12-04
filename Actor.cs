using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PacCatherine
{
    public class Actor
    {
        private int x;
        private int y;
        private int dx;
        private int dy;
        private int speed;

        public Actor(int iX, int iY, int sp)
        {
            reset(iX, iY, sp);
        }

        public void reset(int iX, int iY, int sp)
        {
            x = iX;
            y = iY;
            speed = sp;
            dx = 0;
            dy = 0;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public void setX(int xx)
        {
            x = xx;
        }

        public void setY(int yy)
        {
            y = yy;
        }

        public int getDX()
        {
            return dx;
        }

        public int getDY()
        {
            return dy;
        }

        public void setDX(int dxx)
        {
            dx = dxx;
        }

        public void setDY(int dyy)
        {
            dy = dyy;
        }

        public void updatePosition()
        {
            x = x + dx;
            y = y + dy;
        }

        public void moveRight()
        {
            dx = speed;
            dy = 0;
        }

        public void moveUp()
        {
            dx = 0;
            dy = -speed;
        }

        public void moveLeft()
        {
            dx = -speed;
            dy = 0;
        }

        public void moveDown()
        {
            dx = 0;
            dy = speed;
        }

        public int getDirection()
        {
            int d;
            if (dx > 0) d = 2;
            else if (dy > 0) d = 3;
            else if (dx < 0) d = 4;
            else
                d = 1;
            return d;
        }
    }
}