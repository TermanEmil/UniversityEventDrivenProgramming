using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic
{
    public class Mesh : Property
    {
        public Image Image { get; set; }
        public Graphics DrawContext { get; set; }

        public Mesh(
            GameObject gmObj,
            string spritePath,
            Graphics drawContext) : base(gmObj)
        {
            Image = Image.FromFile(spritePath);
            DrawContext = drawContext;
        }

        public override void Paint()
        {
            base.Paint();
            //DrawContext.DrawLine(new Pen(Color.Black), 0, 0, 300, 300);
            
            DrawContext.DrawImage(
                Image,
                MyTransform.position.X,
                MyTransform.position.Y,
                MyTransform.scale.X * Image.Width,
                MyTransform.scale.Y * Image.Height);
        }
    }
}
