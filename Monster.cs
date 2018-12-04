using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacCatherine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Drawing;

    public class Monster : Actor
    {
        private Random rdm;
        private bool active;

        public Monster(int iX, int iY, int sp) : base(iX, iY, sp)
        {
            rdm = new Random(iX * iY + DateTime.Now.Millisecond);
            active = true;
        }

        public bool isHot()
        {
            return active;
        }

        public void deactivate()
        {
            active = false;
        }

        public void drawActor(Graphics myG, Brush b)
        {
            if (active)
            {
                int x = base.getX();
                int y = base.getY();
                Point[] myShape = new Point[11];
                myShape[0] = new Point(x, y + 5);
                myShape[1] = new Point(x + 5, y);
                myShape[2] = new Point(x + 15, y);
                myShape[3] = new Point(x + 20, y + 5);
                myShape[4] = new Point(x + 20, y + 15);
                myShape[5] = new Point(x + 15, y + 20);
                myShape[6] = new Point(x + 13, y + 15);
                myShape[7] = new Point(x + 10, y + 20);
                myShape[8] = new Point(x + 8, y + 15);
                myShape[9] = new Point(x + 5, y + 20);
                myShape[10] = new Point(x, y + 15);

                myG.FillPolygon(b, myShape);
            }
        }

        public bool collideWithPacMan(PacMan pm)
        {
            int mx = base.getX();
            int my = base.getY();
            int px = pm.getX();
            int py = pm.getY();
            bool collide = false;
            if (active)
                if ((px >= (mx - 20)) & (px <= (mx + 20)) & (py >= (my - 20)) & (py <= (my + 20)))
                    collide = true;
            return collide;
        }

        public void handleBorderCollision(int scX, int scY)
        {
            if (active)
            {
                int x = base.getX();
                int y = base.getY();
                int dx = base.getDX();
                int dy = base.getDY();
                if (x >= (scX - 20) || (y <= 0) || y >= (scY - 20) || (x <= 0))
                {
                    base.setDX(-dx);
                    base.setDY(-dy);
                    updatePosition();
                }
            }
        }

        public void moveHunting(PacMan pm)
        {
            if (active)
            {
                int mx = base.getX();
                int my = base.getY();
                int px = pm.getX();
                int py = pm.getY();
                int dir = rdm.Next(1, 7);
                if (dir == 1) base.moveUp();
                else if (dir == 2) base.moveRight();
                else if (dir == 3) base.moveDown();
                else if (dir == 4) base.moveLeft();
                else if (dir == 5)
                {
                    if (px < mx) base.moveLeft();
                    else base.moveRight();
                }
                else
                {
                    if (py < my) base.moveUp();
                    else base.moveDown();
                }
            }
        }
    }
}
