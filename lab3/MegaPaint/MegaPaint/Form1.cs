using MegaPaint.Draw;
using MegaPaint.Draw.DrawAction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace MegaPaint
{
    public partial class Form1 : Form
    {
        private IDrawAction currentDrawAction = null;
        public List<PaintEventData> DrawEvents = new List<PaintEventData>();

        public GroupBox BezierCtrl { get { return bezierCtrl; } }
        public RadioButton RadioBtn1 { get { return point1; } }
        public RadioButton RadioBtn2 { get { return point2; } }
        public Button BezierDoneBtn { get { return button1; } }
        public bool BezierIsDone = false;

        private int movements = 0;

        private float zoom = 1;
        private float deltaX = 0, deltaY = 0;
        private const float zoomSpeed = 0.01f;

        public PictureBox MainImage
        {
            get { return pictureBox1; }
        }


        private int lastInsertedWidth = 1;
        public int InsertedWidth
        {
            get
            {
                int.TryParse(textBox4.Text, out lastInsertedWidth);
                return lastInsertedWidth;
            }
        }

        private Color lastInsertedColor = Color.Black;
        public Color InsertedColor
        {
            get
            {
                var r = lastInsertedColor.R;
                var g = lastInsertedColor.G;
                var b = lastInsertedColor.B;

                byte.TryParse(textBox1.Text, out r);
                byte.TryParse(textBox2.Text, out g);
                byte.TryParse(textBox3.Text, out b);
                lastInsertedColor = Color.FromArgb(r, g, b);
                return lastInsertedColor;
            }
        }     

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += imagebox1_MouseWheel;

        }

        private void SetCurrentDrawing(IDrawAction drawAction)
        {
            if (currentDrawAction != null)
                currentDrawAction.OnDestroy();
            drawAction.form1 = this;
            currentDrawAction = drawAction;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var drawEvent in DrawEvents)
                drawEvent.Draw(e, 1, deltaX, deltaY);

            if (currentDrawAction != null)
                currentDrawAction.TempPaint(e);

            
        }

        private Point GetRelativeCursorPoint(Control targetCtrl)
        {
            var point = PointToClient(Cursor.Position);
            point.X -= targetCtrl.Location.X;
            point.Y -= targetCtrl.Location.Y;
            
            return point;
        }

        private void drawLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrentDrawing(new DrawActionLine());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentDrawAction != null)
                currentDrawAction.OnMouseDown(e);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (currentDrawAction != null)
                currentDrawAction.OnMouseUp(e);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentDrawAction != null)
                currentDrawAction.OnMouseMove(e);
        }

        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrentDrawing(new DrawActionEllipse());
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrentDrawing(new DrawActionRect());

        }

        private void pieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrentDrawing(new DrawActionPie());
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void bezieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var drawer = new DrawBezier();
            SetCurrentDrawing(drawer);
            button1.Click += drawer.OnBezierDone;
        }

        public void AddPaintAction(PaintEventData paintEventData)
        {
            DrawEvents.Add(paintEventData);

            movements++;
            var item = new ListViewItem()
            {
                Text = movements + ") " + currentDrawAction.ToString(),
                Tag = paintEventData,
                ForeColor = paintEventData.color
            };
            
            listView1.Items.Add(item);
        }

        public void RefreshImg()
        {
            pictureBox1.Refresh();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            RemoveSelectedItemsFromListView();
        }

        private void RemoveSelectedItemsFromListView()
        {
            var selected = listView1.SelectedItems;
            foreach (ListViewItem item in selected)
            {
                listView1.Items.Remove(item);
                DrawEvents.Remove(item.Tag as PaintEventData);
            }
            pictureBox1.Refresh();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back)
            {
                RemoveSelectedItemsFromListView();
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetCurrentDrawing(new DrawActionFill());
        }

        private void imagebox1_MouseWheel(object sender, MouseEventArgs e)
        {
            zoom += Math.Sign(e.Delta) * zoomSpeed;
            pictureBox1.Refresh();
            
        }
    }
}
