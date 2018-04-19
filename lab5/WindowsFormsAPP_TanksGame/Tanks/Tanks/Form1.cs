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

            gmCtrl = new GameController(this);
            DoubleBuffered = true;

            backBuffer = new Bitmap(
                mainDrawContext.Width,
                mainDrawContext.Height,
                mainDrawContext.CreateGraphics());
            backBufferGraphics = Graphics.FromImage(backBuffer);
            
            gmCtrl.mainGraphics = backBufferGraphics;

            var player = GameObject.Instantiate(new Tank(
                GameSettings.GetPath(GameSettings.playerSprite),
                isAI: false));

            var enemy1 = GameObject.Instantiate(new Tank(
                GameSettings.GetPath(GameSettings.enemySprite),
                isAI: true));

            player.transform.position = new PointF(120, 50);
            enemy1.transform.position = new PointF(100, 100);

            GameObject.Instantiate(new Map(mainDrawContext.Width, mainDrawContext.Height));
                
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
