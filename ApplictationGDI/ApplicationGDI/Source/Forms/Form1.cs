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
using BitMapsAndName = ApplicationGDI.Source.Structs.BitMapsAndName;
using System.IO;

namespace ApplicationGDI.Source.Forms
{
    public partial class Form1 : Form
    {
        private App m_App = new App();
        private List<Image> m_images;
        private PictureBox m_pictureBox;
        private List<PictureBox> m_imagesPanel;
        private int m_count;
        private int m_selectedPictureBoxClick = default(int);
        private int m_selectedPictureBoxDoubleClick = default(int);
        private bool m_batch = false;
        private string m_batchDirectory = "";
        private string m_privateCopyRight = "Привет";

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
                string tmp = m_App.GetPathFile();
                m_images = m_App.GetPicterBox(tmp);
                AddPicture();
                AddElementsInDataGridView(BitMapsAndName(m_images, tmp));
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
                string tmp = m_App.GetPathDirectory();
                m_images = m_App.GetPicterBox(tmp);
                AddPicture();
               AddElementsInDataGridView(BitMapsAndName(m_images, tmp));
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /* 
        */
        void AddPicture()
        {

            panel1.Controls.Clear();
            m_count = 0;
            foreach (Image image in m_images)
            {
                m_pictureBox = new PictureBox();
                m_pictureBox.Size = new Size(256, 256);
                m_pictureBox.Image = image;
                m_pictureBox.BackColor = Color.Gainsboro;
                m_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                m_pictureBox.MouseDoubleClick += PictureBox_MouseDoubleClick;
                m_pictureBox.Click += M_pictureBox_Click;
                m_pictureBox.Location = new Point(0, 256 * m_count);
                m_pictureBox.Parent = panel1;
                m_count++;
                panel1.Controls.Add(m_pictureBox);
            }
            
        }

        private void M_pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pb = sender as PictureBox;
            m_selectedPictureBoxClick = ((pb.Location.Y-panel1.Controls[0].Location.Y) / 256) + 1;
        }

        void PictureBox_MouseDoubleClick(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            PictureBox pb = sender as PictureBox;
            pictureBox1.Image = pb.Image;
            m_selectedPictureBoxDoubleClick = ((pb.Location.Y - panel1.Controls[0].Location.Y) / 256) + 1;
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
            RemoveDataGridView();
        }
        void RemoveDataGridView()
        {
            dataGridView1.Rows.Clear();
        }
        //починить
        void RemoveOneElementWithLeftPanel()
        {
            
            List<PictureBox> pb = new List<PictureBox>();
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                if (i == m_selectedPictureBoxClick - 1)
                    continue;
                pb.Add((PictureBox)panel1.Controls[i]);
            }
            m_count = 0;
            panel1.Controls.Clear();
            for (int i = 0; i < pb.Count; i++)
            {
                pb[i].Location = new Point(0, 256 * i);
                panel1.Controls.Add(pb[i]);
            }
        }
        void RefreshPanel()
        {
            m_count = panel1.Controls.Count;
            Panel p = new Panel();
            for (int i =0; i< panel1.Controls.Count;i++)
            {
                panel1.Controls[i].Location = new Point(0,256 * m_count);
                m_count++;
            }
            panel1.Controls.Clear();
        }
        void AddElementsInDataGridView(BitMapsAndName bit)
        {
            dataGridView1.RowCount = bit.Images.Count;
            for(int i =0;i< bit.Images.Count;i++)
            {
                dataGridView1.Rows[i].Cells[3].Value = null;
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
                dataGridView1.Rows[i].Cells[2].ReadOnly = true;
                dataGridView1.Rows[i].Cells[1].Value = bit.Images[i].Width;
                dataGridView1.Rows[i].Cells[2].Value = bit.Images[i].Height;
                dataGridView1.Rows[i].Cells[0].Value = bit.Paths[i];
            }
        }
        void DeleteElementInDataGridView(int index)
        {
            List<DataGridViewRow> zek = new List<DataGridViewRow>();
            for(int i =0;i< dataGridView1.RowCount;i++)
            {
                if(i!=index)
                zek.Add(dataGridView1.Rows[i]);
            }
            RemoveDataGridView();
            for (int i = 0; i < zek.Count; i++)
            {
                    dataGridView1.Rows.Add(zek[i]);
            }
            
        }
        void AddCopyRight()
        {
           PictureBox box = (PictureBox)panel1.Controls[m_selectedPictureBoxDoubleClick - 1];
           string nameFile = 
                m_App.SaveImage(
                box.Image,
                (string)dataGridView1.Rows[m_selectedPictureBoxDoubleClick - 1].Cells[3].Value, 
                new Font(FontFamily.Families[0], 120),
                Brushes.Red,
                new PointF((int)dataGridView1.Rows[m_selectedPictureBoxDoubleClick - 1].Cells[1].Value,
                (int) dataGridView1.Rows[m_selectedPictureBoxDoubleClick - 1].Cells[2].Value)
                );
            List<Image> images = m_App.GetPicterBox(nameFile);
            pictureBox1.Image = images[0];
            List<PictureBox> pb = new List<PictureBox>();
            for (int i =0; i< panel1.Controls.Count;i++)
            {
                pb.Add((PictureBox)panel1.Controls[i]);
            }
           pb[m_selectedPictureBoxDoubleClick-1].BackColor = Color.Green;
                
            for (int i = 0; i < pb.Count;i++)
            {
                panel1.Controls.Add(pb[i]);
            }
            m_images.Clear();


        }
        BitMapsAndName BitMapsAndName(List<Image> img,string str)
        {
            if (Path.HasExtension(str))
            {
                List<string> st = new List<string>();
                st.Add(str);
                return new BitMapsAndName(img, st);
            }
            else
                return new BitMapsAndName(img, str);
        }

        void BatchModeTrue()
        {
            button1.Click -= button1_Click;
            button1.Click += newButton1_Click;
        }
        void BatchModeFalse()
        {
            
            button1.Click -= newButton1_Click;
            button1.Click += button1_Click;
        }
        void SelectBatchMode()
        {
            m_batch = !m_batch;
            if (m_batch)
                BatchModeTrue();
            else
                BatchModeFalse();
        }

        void newButton1_Click(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)panel1.Controls[m_selectedPictureBoxDoubleClick - 1];
            string nameFile =
                 m_App.batch_SaveImage(
                 m_batchDirectory+"\\"+dataGridView1.Rows[m_selectedPictureBoxDoubleClick-1].Cells[0].Value,
                 box.Image,
                 m_privateCopyRight,
                 new Font(FontFamily.Families[0], 120),
                 Brushes.Red,
                 new PointF((int)dataGridView1.Rows[m_selectedPictureBoxDoubleClick - 1].Cells[1].Value,
                 (int)dataGridView1.Rows[m_selectedPictureBoxDoubleClick - 1].Cells[2].Value)
                 );
            dataGridView1.Rows[m_selectedPictureBoxDoubleClick - 1].Cells[3].Value = m_privateCopyRight;
            List<Image> images = m_App.GetPicterBox(nameFile);
            pictureBox1.Image = images[0];
            List<PictureBox> pb = new List<PictureBox>();
            for (int i = 0; i < panel1.Controls.Count; i++)
            {
                pb.Add((PictureBox)panel1.Controls[i]);
            }
            pb[m_selectedPictureBoxDoubleClick - 1].BackColor = Color.Green;

            for (int i = 0; i < pb.Count; i++)
            {
                panel1.Controls.Add(pb[i]);
            }
            m_images.Clear();
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyData == Keys.Delete && panel1.Controls.Count >0)
            {
                //MessageBox.Show("Меня еще не написали\r\n потом я смогу удалять");
                RemoveOneElementWithLeftPanel();
                DeleteElementInDataGridView(m_selectedPictureBoxClick-1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddCopyRight();
        }

        private void addCopyrigtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCopyRight();
        }

        private void batchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectBatchMode();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SelectBatchMode();
        }

        private void copyrightDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App j = new App();
            m_batchDirectory= j.GetPathDirectory();
        }

        private void copyrightTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}
