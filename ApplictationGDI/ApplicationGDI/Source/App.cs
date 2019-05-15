using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Text;

namespace ApplicationGDI.Source
{
    /// <summary>
    /// Этот класс реализует то, что будет потом использовать для своей работы From1 и остальные классы, если такие будут
    /// </summary>
    public class App
    {
        /*
         * 
         * 
         */
         /// <summary>
         /// Возвращает выбранный путь или null
         /// </summary>
         /// <returns></returns>
        public string GetPathFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DirectoryInfo d = new DirectoryInfo(@"..\..\Source\Images");
            if (d.Exists)
                openFileDialog.InitialDirectory = d.FullName;
            openFileDialog.Filter = "Image files (*.jpg,*.png)|*.jpg;*.png|Bitmap files (*.bmp)|*.bmp";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == "")
                return default(string);
            else
                return openFileDialog.FileName;
        }
        /// <summary>
        /// Возвращает выбранный путь или null
        /// </summary>
        /// <returns></returns>
        public string GetPathDirectory()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            if (folderBrowserDialog.SelectedPath == "")
                return default(string);
            else
                return folderBrowserDialog.SelectedPath;
        }
        /// <summary>
        /// Возвращает картинки
        /// </summary>
        /// <param name="path">путь откуда брать картинки</param>
        /// <returns></returns>
        public List<Image> GetPicterBox(string path)
        {
            if (path == null || path == "")
                throw new Exception("Указанный путь равен null либо его вообще нет");
            else if (File.Exists(path))
            {
                List<Image> image = new List<Image>();
                image.Add(Image.FromFile(path));
                return image;
            }
            else if (Directory.Exists(path))
            {
                List<Image> image = new List<Image>();
                foreach (string p in Directory.GetFiles(path))
                {
                    if(!Directory.Exists(p) && Path.HasExtension(p) && (Path.GetExtension(p) == ".png"|| Path.GetExtension(p) == ".jpg"|| Path.GetExtension(p) == ".bmp"))
                    {
                        image.Add(Image.FromFile(p));
                    }
                }
                return image;
            }
            else
                throw new Exception("По пути нет данных");
        }
        /// <summary>
        /// Сохранить картинку
        /// </summary>
        /// <param name="image">передать картинку на сохранение</param>
        public void SaveImage(Image image)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Image files (*.jpg,*.png)|*.jpg;*.png|Bitmap files (*.bmp)|*.bmp";
            save.ShowDialog();
            if (image == null || save.FileName == null || save.FileName == "")
                return;
            image.Save(save.FileName);
        }
        /// <summary>
        /// Выведет строку
        /// </summary>
        /// <param name="image"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="brush"></param>
        /// <param name="pointF"></param>
        /// <returns></returns>
        public string SaveImage(Image image,string text,Font font,Brush brush, PointF pointF)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Image files (*.jpg,*.png)|*.jpg;*.png|Bitmap files (*.bmp)|*.bmp";
            save.ShowDialog();
            if (image == null || save.FileName == null || save.FileName == "")
                return null;
            Graphics g = Graphics.FromImage(image);
            pointF.X = -1 * pointF.X / 2f + pointF.X;
            pointF.Y = -1 * pointF.Y / 4f + pointF.Y;
            g.DrawString(text, font,brush, pointF);
            image.Save(save.FileName);
            return save.FileName;
        }
        public string batch_SaveImage(string path,Image image, string text, Font font, Brush brush, PointF pointF)
        {
            if (image == null || path == null || path == "")
                return null;
            Graphics g = Graphics.FromImage(image);
            pointF.X = -1 * pointF.X / 2f + pointF.X;
            pointF.Y = -1 * pointF.Y / 4f + pointF.Y;
            g.DrawString(text, font, brush, pointF);
            if (File.Exists(path))
                File.Delete(path);
            image.Save(path);
            return path;
        }

    }
}
