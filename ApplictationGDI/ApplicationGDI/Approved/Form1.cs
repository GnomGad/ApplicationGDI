using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using App = ApplicationGDI.Source.App;

namespace ApplicationGDI
{
    public partial class Form1 : Form
    {
        private App m_App = new App();
        private List<Image> m_images;
        private PictureBox m_pictureBox;
        private List<PictureBox> m_imagesPanel;
        private int m_count;

        public Form1()
        {
            InitializeComponent();
            Build();
        }
        void Build()
        {
            m_count = 0;
            m_imagesPanel = new List<PictureBox>();
        }
        //Блок добавлений в панель пикчер боксов
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (panel1.Controls.Count > 0)
                    RemoveLeftPanel();
                m_images = m_App.GetPicterBox(m_App.GetPathFile());
                AddPicture();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }
        /// <summary>
        /// Метод для директории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(panel1.Controls.Count>0)
                    RemoveLeftPanel();
                m_images = m_App.GetPicterBox(m_App.GetPathDirectory());
                AddPicture();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* Сделать нормальный ивент, что бы загружать в PictureBox
        */
        void AddPicture()
        {
           // panel1.Controls.Clear();
           // m_count = 0;
            foreach (Image image in m_images)
            {
                m_pictureBox = new PictureBox();
                m_pictureBox.Size = new Size(256, 256);
                m_pictureBox.Image = image;
                m_pictureBox.BackColor = Color.Gainsboro;
                m_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                m_pictureBox.Click += PictureBox_Click;
                m_pictureBox.Location = new Point(0, 256 * m_count);
                m_count++;
                panel1.Controls.Add(m_pictureBox);
            }
        }
        void PictureBox_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            PictureBox pb = sender as PictureBox;
            pictureBox1.Image = pb.Image;
        }
        void RemoveAllPictures()
        {
            pictureBox1.Image = null;
            RemoveLeftPanel();
        }
        void RemoveLeftPanel()
        {
            m_count = 0;
            m_imagesPanel = new List<PictureBox>();
            panel1.Controls.Clear();
        }

        //Блок сохранений
        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_App.SaveImage(pictureBox1.Image);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            m_App.SaveImage(pictureBox1.Image);
        }

        //Блок удаления
        private void removeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllPictures();
        }
    }
}
