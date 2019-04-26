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
        App m_App = new App();
        public Form1()
        {
            InitializeComponent();
            Build();
        }
        void Build()
        {
            List<Image> images = m_App.GetPicterBox(m_App.GetPathDirectory());
            m_App.SaveImage(images[0]);

        }
    }
}
