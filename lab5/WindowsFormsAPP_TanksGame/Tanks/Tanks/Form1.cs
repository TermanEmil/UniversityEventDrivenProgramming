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
        public readonly string rootPath = "..\\..\\";

        private GameController gmCtrl;

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;
          
            gmCtrl = new GameController(mainDrawContext);
            var tank = new Tank();
            var tankMesh = new Mesh(
                tank,
                rootPath + "imgs\\TankPlayer.png",
                mainDrawContext.CreateGraphics());

            var charCtrl = new CharacterController(
                tank,
                this)
            {
                Speed = new PointF(0.01f, 0.01f)
            };
            GameObject.Instantiate(tank);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            gmCtrl.Update();
            gmCtrl.Paint();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            gmCtrl.Paint();
        }
    }
}
