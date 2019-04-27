using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
namespace ApplicationGDI.Source.Structs
{
    struct BitMapsAndName
    {
        public List<Image> Images;
        public List<string> Paths;

        public BitMapsAndName(List<Image> Images,List<string> Paths)
        {
            this.Images = Images;
            for(int i =0;i<Paths.Count;i++)
            {
                Paths[i] =Path.GetFileName(Paths[i]);
            }
            this.Paths = Paths;
        }

        public BitMapsAndName(List<Image> Images, string path)
        {
            this.Images = Images;
            Paths = new List<string>();
            foreach (string p in Directory.GetFiles(path))
            {
                if (!Directory.Exists(p) && Path.HasExtension(p) && (Path.GetExtension(p) == ".png" || Path.GetExtension(p) == ".jpg" || Path.GetExtension(p) == ".bmp"))
                {
                    Paths.Add(Path.GetFileName(p));
                }
            }
        }
    }
}
