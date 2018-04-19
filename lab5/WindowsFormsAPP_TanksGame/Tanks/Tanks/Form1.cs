using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tanks.BuisnessLogic;
using Tanks.BuisnessLogic.Concrete;

namespace Tanks
{
    public partial class Form1 : Form
    {
        private GameController gmCtrl;

        private Bitmap backBuffer;
        private Graphics backBufferGraphics;

        public Form1()
        {
            InitializeComponent();

            gmCtrl = new GameController();
            DoubleBuffered = true;

            backBuffer = new Bitmap(
                mainDrawContext.Width,
                mainDrawContext.Height,
                mainDrawContext.CreateGraphics());
            backBufferGraphics = Graphics.FromImage(backBuffer);
            
            gmCtrl.mainGraphics = backBufferGraphics;
            gmCtrl.mainKeyboardCtrl = this;

            var tank = GameObject.Instantiate(new Tank(
                GameController.rootPath + "imgs\\TankPlayer.png",
                speed: new PointF(0.05f, 0.05f),
                isAI: false));

            var tank2 = GameObject.Instantiate(new Tank(
                GameController.rootPath + "imgs\\TankEnemy.png",
                new PointF(0.01f, 0.01f),
                true));
            tank.transform.position = new PointF(120, 50);
            tank2.transform.position = new PointF(100, 100);

            GameObject.Instantiate(new Border(
                new PointF(0, -100),
                new PointF(mainDrawContext.Width, 0)));

            GameObject.Instantiate(new Border(
                new PointF(-100, 0),
                new PointF(0, mainDrawContext.Height)));

            GameObject.Instantiate(new Border(
                new PointF(mainDrawContext.Width, 0),
                new PointF(mainDrawContext.Width, mainDrawContext.Height + 10)));

            GameObject.Instantiate(new Border(
                new PointF(0, mainDrawContext.Height),
                new PointF(mainDrawContext.Width + 1, mainDrawContext.Height)));
                
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gmCtrl.Update();
            Repaint();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Repaint()
        {
            backBufferGraphics.Clear(Color.White);
            gmCtrl.Paint();
            mainDrawContext.CreateGraphics().DrawImage(backBuffer, new Point(0, 0));
        }
    }
}
