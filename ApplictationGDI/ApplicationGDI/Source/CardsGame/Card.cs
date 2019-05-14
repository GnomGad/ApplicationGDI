using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace ApplicationGDI.Source.CardsGame
{
    class Card
    {
        public Bitmap Image { get; set; }
        public Size SizeImage { get; set; }
        /// <summary>
        /// Старые сохранять
        /// </summary>
        public Point Previous { get; set; }
        /// <summary>
        /// По новым координатам выводить
        /// </summary>
        public Point New { get; set; }
        public bool IsTalon { get; set; }
        public bool IsActive { get; set; }
        public int Rotate { get; set; }

        public Card(Bitmap Image,Size SizeImage,Point Previous)
        {
            this.Image = Image;
            this.SizeImage = SizeImage;
            this.Previous = Previous;
            New = Previous;
            IsTalon = true;
            IsActive = false;
            Rotate = 0;
        }

        public void Move(Point New)
        {
            Previous = this.New;
            this.New = New;
        }
        public void Draw(Graphics gc,Bitmap b)
        {
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(Image, New.X, New.Y, SizeImage.Width,SizeImage.Height);
            gc.DrawImage(b,0,0);
           
        }

        

    }
}
