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
        public ProgressBar HPBar { get { return playerHPBar; } }
        public Label PlayerPoints { get { return playerPointsLabel; } }

        private GameController gmCtrl;

        private Bitmap backBuffer;
        private Graphics backBufferGraphics;
        private Graphics mainDrawContextGraphics;
        
        public Form1()
        {
            InitializeComponent();

            gmCtrl = new GameController(this);

            // Graphics
            DoubleBuffered = true;
            mainDrawContextGraphics = mainDrawContext.CreateGraphics();
            backBuffer = new Bitmap(
                mainDrawContext.Width,
                mainDrawContext.Height,
                mainDrawContextGraphics);
            backBufferGraphics = Graphics.FromImage(backBuffer);        
            gmCtrl.mainGraphics = backBufferGraphics;

            // Init tanks
            var playerTank = GameObject.Instantiate(new Tank(isAI: false));
            gmCtrl.player = new Player(playerTank, this);

            var enemy1 = GameObject.Instantiate(new Tank(isAI: true));
            gmCtrl.enemyTanks.Add(enemy1);

            // Init enemies events
            gmCtrl.enemyTanks.ForEach(x => gmCtrl.player.ConfigTankEvents(x));

            // Tank pos
            playerTank.transform.position = new PointF(120, 50);
            enemy1.transform.position = new PointF(100, 100);

            // Init map
            GameObject.Instantiate(new Map(mainDrawContext.Width, mainDrawContext.Height));

            // Init timer
            timer1.Start();
            gmCtrl.Start();
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
            mainDrawContextGraphics.DrawImage(backBuffer, new Point(0, 0));
        }
    }
}
