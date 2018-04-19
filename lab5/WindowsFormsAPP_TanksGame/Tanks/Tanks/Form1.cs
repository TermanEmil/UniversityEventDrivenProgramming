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
                new PointF(0.01f, 0.01f),
                false));

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
