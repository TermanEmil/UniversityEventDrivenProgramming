using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic
{
    public class Mesh : Property
    {
        public delegate void Draw(Graphics graphics);

        public Image Image { get; set; }
        public Draw draw;
        public Graphics DrawContext { get; set; }
        
        public Mesh(
            GameObject gmObj,
            string spritePath,
            Graphics drawContext) : base(gmObj)
        {
            Image = Image.FromFile(spritePath);
            DrawContext = drawContext;
        }

        public Mesh(GameObject gmObj, Graphics drawContext) : base(gmObj)
        {
            DrawContext = drawContext;
        }

        public override void Paint()
        {
            base.Paint();
            if (Image != null)
            {
                DrawContext.DrawImage(
                    RotateImage(Image, MyTransform.rotation),
                    MyTransform.position.X,
                    MyTransform.position.Y,
                    MyTransform.scale.X * Image.Width,
                    MyTransform.scale.Y * Image.Height);
            }

            if (draw != null)
                draw.Invoke(DrawContext);
        }

        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }
    }
}
