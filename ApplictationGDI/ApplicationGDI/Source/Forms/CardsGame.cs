using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        Card newCard;

        bool k1 = false;
        public CardsGame()
        {
            InitializeComponent();
            SetBasic();
            newCard = new Card(m_bitmaps[0], new Size(120, 200), new Point(500, 500));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoubleBuffered = true;
            SetGradient();

            TestDraw2();
            TestDraw();
            TestDraw3();
        }
        void SetBasic()
        {
            Setm_gc();
            Setm_bitmaps();
            Setm_talon();
            Setm_Decks();
            
        }
        void Setm_gc()
        {
            m_gc = CreateGraphics();
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
        void SetGradient()
        {
            FillRectangleGradient(Color.Black, Color.Red);
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
        void TestDraw()
        {
            for (int i = 0; i < m_decks; i++)
                m_gc.DrawImage(m_talon, i * 0.2f, 0, 120, 200);
            int count = 0;
            for (int i = 0; i < m_decks / 4; i++)
            {
                for (int k = 0; k < m_decks / 9; k++)
                {
                    m_gc.DrawImage(m_bitmaps[count++], 200 + i * 50, k * 120, 120, 200);
                }
            }
        }

        void TestDraw2()
        {
            Graphics gc = CreateGraphics();
            gc.DrawImage(m_talon, 800, 300, 120, 200);
            
        }

        void TestDraw3()
        {
            
        }

        private void CardsGame_Resize(object sender, EventArgs e)
        {
            Setm_gc();
            SetGradient();
            TestDraw2();
            TestDraw();
        }

        private void CardsGame_ResizeEnd(object sender, EventArgs e)
        {
            Setm_gc();
            SetGradient();
            TestDraw2();
            TestDraw();
        }


        // --------------------------------все фигня, над чинить---------------------------------------

        private void CardsGame_Click(object sender, EventArgs e)
        {

            MouseEventArgs k = (MouseEventArgs)e;
            if (k.X <= 200 && k.Y <= 200)
                k1 = true;
            
        }

        private void CardsGame_MouseUp(object sender, MouseEventArgs e)
        {
            if (k1 == true)
            {
                newCard.Move(new Point(e.X, e.Y));
                newCard.Draw(m_gc);
                
            }
            k1 = !k1;
        }

        private void CardsGame_MouseMove(object sender, MouseEventArgs e)
        {
            if (k1 == true)
            {
                button1_Click(sender, new EventArgs());
               
                newCard.Move(new Point(e.X, e.Y));
                newCard.Draw(m_gc);
            }
        }

        // --------------------------------весь отмеченный блок---------------------------------------
    }
}
