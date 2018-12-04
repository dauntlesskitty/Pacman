using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace PacCatherine
{
    public partial class MainForm : Form
    {
        private const int MAXMONSTER = 3;
        private const int MAXLIFE = 3;
        private const int MAXFOOD = 20;
        private const int MAXSUPERFOOD = 4;
        private int totFood, totSuperFood;
        private Graphics myG;
        private int scX = 400;
        private int scY = 400;
        private PacMan cat;        // cat is the Pac-Man
        private Monster[] mon = new Monster[MAXMONSTER];
        private Food[] food = new Food[MAXFOOD];
        private SuperFood[] SuperFood = new SuperFood[MAXSUPERFOOD];
        private int gameMode = 0;
        private int score;
        private SoundPlayer soundEat, soundDie, soundIntro, soundWin;
        private int life = MAXLIFE;
        private Random rdm;

        public MainForm()
        {
            InitializeComponent();
            rdm = new Random(DateTime.Now.Millisecond);
            soundEat = new SoundPlayer("eatfood.wav");
            soundDie = new SoundPlayer("pacdie.wav");
            soundIntro = new SoundPlayer("intro.wav");
            soundWin = new SoundPlayer("pacwin.wav");
            soundIntro.Play();
            resetGame();
        }

        public void resetGame()
        {
            cat = new PacMan(scX / 2, scY - 40, 5);
            mon[0] = new Monster(170, 160, 6);
            mon[1] = new Monster(210, 160, 6);
            mon[2] = new Monster(170, 200, 6);
            for (int k = 0; k < MAXFOOD; k++)
            {
                int initX = rdm.Next(1, 19) * 20;
                int initY = rdm.Next(1, 19) * 20;
                food[k] = new Food(initX, initY, 10); //10 is points
            }
            for (int k = 0; k < MAXSUPERFOOD; k++)
            {
                int SFpts = 50;
                SuperFood[0] = new SuperFood(50, 50, SFpts); 
                SuperFood[1] = new SuperFood(350, 50, SFpts); 
                SuperFood[2] = new SuperFood(50, 350, SFpts); 
                SuperFood[3] = new SuperFood(350, 350, SFpts); 
            }
            score = 0;
            life = MAXLIFE;
            totFood = MAXFOOD;
            totSuperFood = MAXSUPERFOOD;
        }

        public void resetPosition()
        {
            cat = new PacMan(scX / 2, scY - 40, 5);
            mon[0].setX(170);
            mon[0].setY(170);
            mon[1].setX(210);
            mon[1].setY(170);
            mon[2].setX(170);
            mon[2].setY(210);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            myG = Graphics.FromHwnd(this.Handle);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if ((gameMode == 0) || (gameMode == 2))
            {
                tmrStart.Enabled = true;
                btnStart.Text = "Pause";
                gameMode = 1;
            }
            else if (gameMode == 1)
            {
                tmrStart.Enabled = false;
                btnStart.Text = "Resume";
                gameMode = 2;
            }
            else if ((gameMode == 3) || (gameMode == 4))
            {
                btnStart.Text = "Start";
                gameMode = 0;
                resetGame();
            }
            else if (gameMode == 5)
            {
                btnStart.Text = "Start";
                gameMode = 0;
                resetPosition();
            }
        }

        private void tmrStart_Tick(object sender, EventArgs e)
        {
            for (int k = 0; k < MAXMONSTER; k++)
                mon[k].moveHunting(cat);
            cat.updatePosition();
            for (int k = 0; k < MAXMONSTER; k++)
            {
                mon[k].updatePosition();
                mon[k].handleBorderCollision(scX, scY);
            }
            if (cat.isBorderCollision(scX, scY))
            {
                cat.setDX(0);
                cat.setDY(0);
            }
            for (int k = 0; k < MAXMONSTER; k++)
            {
                if (mon[k].isHot())
                {
                    if (mon[k].collideWithPacMan(cat))
                        gameOver();
                }
            }
            if (gameMode == 1)
            {
                for (int k = 0; k < MAXFOOD; k++)
                {
                    if (food[k].isHot())
                    {
                        if (food[k].collideWithPacMan(cat))
                        {
                            food[k].deactivate();
                            score = score + food[k].getPoint();
                            totFood--;
                            soundEat.Play();
                        }
                    }
                }
                for (int k = 0; k < MAXSUPERFOOD; k++)
                {
                    if (SuperFood[k].isHot())
                    {
                        if (SuperFood[k].collideWithPacMan(cat))
                        {
                            SuperFood[k].deactivate();
                            score = score + SuperFood[k].getPoint();
                            totSuperFood--;
                            soundEat.Play();
                        }
                    }
                }
                if (totFood == 0 && totSuperFood ==0)
                {
                    gameWin();
                }
            }
            Refresh();
        }

        public void gameOver()
        {
            soundDie.Play();
            tmrStart.Enabled = false;
            life--;
            if (life == 0)
                gameMode = 3;
            else
                gameMode = 5;
            btnStart.Text = "Restart";
        }

        public void gameWin()
        {
            tmrStart.Enabled = false;
            gameMode = 4;
            btnStart.Text = "Restart";
            soundWin.Play();
        }

        public void drawLife(Graphics myG)
        {
            for (int k = 0; k < life; k++)
                cat.drawLife(myG, Brushes.LightCyan, 300,0);
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            myG.FillRectangle(Brushes.Black, 0, 0, scX, scY);
            for (int k = 0; k < MAXFOOD; k++)
                food[k].drawFood(myG, Brushes.LightYellow);
            for (int k = 0; k < MAXSUPERFOOD; k++)
                SuperFood[k].drawFood(myG, Brushes.Blue);
            cat.drawActor(myG, Brushes.Yellow);
            for (int k = 0; k < MAXMONSTER; k++)
                mon[0].drawActor(myG, Brushes.Red); //Blinky the red ghost
                mon[1].drawActor(myG, Brushes.Purple); //Sue MS pacman
                mon[2].drawActor(myG, Brushes.Cyan); //Inky
            if (gameMode == 0)
            {
                myG.DrawString("Press START to EAT!",
                    new Font("Times", 18, FontStyle.Bold), Brushes.LimeGreen, 100, 250);
            }
            if (gameMode == 2)
            {
                myG.DrawString("P A U S E",
                    new Font("Times", 24), Brushes.LimeGreen, 120, 200);
            }
            if (gameMode == 3)
            {
                myG.DrawString("G A M E   O V E R",
                    new Font("Times", 20, FontStyle.Bold), Brushes.PaleVioletRed, 90, 200);
            }
            if (gameMode == 4)
            {
                myG.DrawString("Y O U   W I N",
                    new Font("Times", 20, FontStyle.Bold), Brushes.LimeGreen, 110, 200);
            }
            if (gameMode == 5)
            {
                //drawLife(myG);
                myG.DrawString("YOU HAVE " + life + " LIFE LEFT!",
                    new Font("Times", 20), Brushes.LimeGreen, 60, 200);
            }
            myG.DrawString("L I F E : " + life, 
                new Font("Arial", 12, FontStyle.Bold), Brushes.LimeGreen, 300, 0);
            myG.DrawString("S C O R E : " + score,
                new Font("Arial", 12, FontStyle.Bold), Brushes.LightCyan, 0, 0);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            cat.moveRight();
        }

        private void tmrIntro_Tick(object sender, EventArgs e)
        {
            btnStart.Visible = true;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            cat.moveUp();
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            cat.moveLeft();
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            cat.moveDown();
        }
    }
}