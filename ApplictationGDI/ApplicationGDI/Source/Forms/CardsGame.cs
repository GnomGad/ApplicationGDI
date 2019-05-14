using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using ApplicationGDI.Source.CardsGame;

namespace ApplicationGDI.Source.Forms
{
    public partial class CardsGame : Form
    {
        List<Bitmap> m_bitmaps;
        int m_decks;
        Graphics m_gc;
        Bitmap m_talon;

        Card[] m_Cards;
        int m_IndexSelectedCard;

        bool k1 = false;
        public CardsGame()
        {
            DoubleBuffered = true;
            InitializeComponent();
            m_IndexSelectedCard = -1;
            SetBasic();
            
        }
        void Setm_Cards()
        {
            m_Cards = new Card[36];
            for(int i =0;i<36;i++)
                m_Cards[i] = new Card(m_bitmaps[i], new Size(120, 200), new Point(0, 0));
        }
        void SetBasic()
        {
            Setm_gc();
            Setm_bitmaps();
            Setm_talon();
            Setm_Decks();
            Setm_Cards();

        }
        void Setm_gc()
        {
            m_gc = CreateGraphics();
            m_gc.CompositingMode = CompositingMode.SourceCopy;
            m_gc.CompositingQuality = CompositingQuality.HighQuality;
            m_gc.InterpolationMode = InterpolationMode.HighQualityBicubic;
            m_gc.SmoothingMode = SmoothingMode.HighQuality;
            m_gc.PixelOffsetMode = PixelOffsetMode.HighQuality;

        }
        void Setm_bitmaps()
        {
            AppCardsGame appCardsGame = new AppCardsGame();
            m_bitmaps = appCardsGame.GetBitmaps(Program.PathImages + @"\Cards");
        }
        void Setm_Decks()
        {
            m_decks = 36;
        }
        void Setm_talon()
        {
            m_talon = new Bitmap(Image.FromFile(Program.PathImages + @"\Cards\talon.png"));
        }
        void FillRectangleGradient(Color c1,Color c2)
        {
            Point startGradient = new Point(0, 0);
            Point endGradient = new Point(ClientRectangle.Width, ClientRectangle.Height);
            LinearGradientBrush linear = new LinearGradientBrush(startGradient,endGradient, c1, c2);
            m_gc.FillRectangle(linear, ClientRectangle);
        }
        Bitmap DriweDeck()//Рисует деку
        {
            Bitmap b = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics gr = Graphics.FromImage(b);
            for (int i = 0; i < m_decks; i++)
                gr.DrawImage(m_talon, i * 0.2f, 0, 120, 200);
            return b;
        }
        Bitmap SetGradient()//рисует задник
        {
            Bitmap bm = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics gr = Graphics.FromImage(bm);
            gr.FillRectangle(new LinearGradientBrush(new Point(0, 0), new Point(ClientRectangle.Width, ClientRectangle.Height), Color.Orange, Color.Black), ClientRectangle);
            return bm;
        }
        Bitmap RedriweCards()//Перерисовывает карты на столе
        {
            Bitmap b = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            Graphics gr = Graphics.FromImage(b);
            for(int i =0;i<36;i++)
            {
                if (!m_Cards[i].IsTalon && !m_Cards[i].IsActive)
                {
                    gr.RotateTransform(m_Cards[i].Rotate);
                    gr.DrawImage(m_Cards[i].Image, m_Cards[i].New.X, m_Cards[i].New.Y, m_Cards[i].SizeImage.Width, m_Cards[i].SizeImage.Height);
                    gr.RotateTransform(0);
                }
            }

            return b;
        }

        private void CardsGame_MouseUp(object sender, MouseEventArgs e)
        {
            if (k1 == true)
            {
                m_Cards[m_IndexSelectedCard].Move(new Point(e.X, e.Y));
                k1 = !k1;
                m_Cards[m_IndexSelectedCard].IsActive = false;
                Random r = new Random();
                m_Cards[m_IndexSelectedCard].Rotate = r.Next(-10, 10);
                return;
            }
            if (e.X <= 100 && e.Y <= 200)
            {
                k1 = !k1;
                m_decks--;
                m_IndexSelectedCard++;
                if(m_IndexSelectedCard==36)
                {
                    m_IndexSelectedCard = 0;
                }
                if (m_decks == 0)
                    m_decks = 36;
                m_Cards[m_IndexSelectedCard].IsTalon = false;
                m_Cards[m_IndexSelectedCard].IsActive = true;
               
            }
            
        }

        private void CardsGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (k1 == true )
            {
                m_Cards[m_IndexSelectedCard].Move(new Point(e.X, e.Y));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Setm_gc();
            Bitmap k = SetGradient();
            Graphics g = Graphics.FromImage(k);
            g.DrawImage(DriweDeck(),0,0);
            g.DrawImage(RedriweCards(), 0, 0);

            if (k1 == true)
            {
                m_Cards[m_IndexSelectedCard].Draw(m_gc, k);
            }
            else
            {
                m_gc.DrawImage(k, 0, 0); 
            }

        }

        
    }
}
