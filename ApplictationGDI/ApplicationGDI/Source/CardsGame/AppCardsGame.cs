using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace ApplicationGDI.Source.CardsGame
{
    class AppCardsGame
    {
       public List<Bitmap> GetBitmaps(string path)
       {
            if (path == null || path == "")
                throw new Exception("Указанный путь равен null либо его вообще нет");
            else if (Directory.Exists(path))
            {
                List<Bitmap> image = new List<Bitmap>();
                foreach (string p in Directory.GetFiles(path))
                {
                    if (Path.GetFileNameWithoutExtension(p) != "talon" &&!Directory.Exists(p) && Path.HasExtension(p) && Path.GetExtension(p) == ".png")
                    {
                        
                        image.Add(new Bitmap(Image.FromFile(p)));
                    }
                }
                return image;
            }
            else
                throw new Exception("По пути нет данных");
            
       }
        
    }
}
