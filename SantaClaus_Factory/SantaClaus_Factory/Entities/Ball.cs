using SantaClaus_Factory.Abstractions;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SantaClaus_Factory.Entities
{
    public class Ball:Toy
    {
        //lehet, hogy csak az override rész kell ide?
        public Ball()
        {
            AutoSize = false;
            Width = 50;
            Height = 50;
            Paint += Ball_Paint;
        }

        private void Ball_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }

        public void MoveBall()
        {
            Left += 1;
        }



    }
}
