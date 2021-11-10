using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;

//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;


namespace My_Math
{
    class OperationsWithImage
    {
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*делает скриншот*/
        public static Bitmap Capture_Screen()
        {
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int screenX = Screen.PrimaryScreen.Bounds.X;
            int screenY = Screen.PrimaryScreen.Bounds.Y;

            Size boundsSize = Screen.PrimaryScreen.Bounds.Size;
            //Инициализирует новый экземпляр класса System.Drawing.Bitmap заданными значениями размера и формата.        
            Bitmap picture = new Bitmap(
                screenWidth,        //Ширина в пикселях нового изображения System.Drawing.Bitmap.       
                screenHeight,       //Высота в пикселях нового изображения System.Drawing.Bitmap.
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);  //Указываем, что форматом отводится 32 бита на пиксель: по 8 бит на красный,зеленый и синий каналы, а также альфа-канал.

            //Создаем новый объект System.Drawing.Graphics из рисунка picture, с новым объектом System.Drawing.Graphics для указанного объекта.
            Graphics graphics = Graphics.FromImage(picture);
            //Выполняем передачу данных о цвете, соответствующих прямоугольной области пикселей, блоками битов с экрана на 
            //поверхность рисования объекта System.Drawing.Graphics.        
            graphics.CopyFromScreen(
                screenX,    //Координата X точки в верхнем левом углу исходного прямоугольника.                       
                screenY,    //Координата Y точки в верхнем левом углу исходного прямоугольника.                       
                0,          //Координата X точки в верхнем левом углу конечного прямоугольника.                 
                0,          //Координата Y точки в верхнем левом углу конечного прямоугольника.                  
                boundsSize, //Размер передаваемой области.   
                CopyPixelOperation.SourceCopy);//Область источника копируется прямо в область назначения.

            return picture; //Возвращаем полученное изображение
        }

        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*копирует прямоугольник с картинки*/
        public static Bitmap Get_Image_Region(Bitmap srcImage, Rectangle srcRegion)
        {
            Bitmap destImage = new Bitmap(srcRegion.Width, srcRegion.Height);
            using (Graphics gr = Graphics.FromImage(destImage))
            {
                Rectangle destRegion = new Rectangle(0, 0, srcRegion.Width, srcRegion.Height);
                gr.DrawImage(srcImage, destRegion, srcRegion, GraphicsUnit.Pixel);
            }
            return destImage;
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*преобразует картинку в монохром черно-белый*/
        public static Bitmap Region_Bitmap_To_BlackWhite_Monochrome(Bitmap srcImage, Rectangle srcRegion, int black_white_coeff)
        {
            Bitmap image = srcImage.Clone(srcRegion, srcImage.PixelFormat);
            //Bitmap image = Get_Image_Region(srcImage, srcRegion);
            int destination_height = 18;
            if (image.Height < destination_height)
            {
                double dest_Percent = (double)destination_height / (double)image.Height;
                image = new Bitmap(image, (int)(image.Width * dest_Percent), (int)(image.Height * dest_Percent));
            }

            image = Bitmap_To_BlackWhite_Monochrome(image, black_white_coeff);

            Bitmap bitmap_end = OperationsWithImage.Сreate_Filled_Bitmap(8, image.Height);
            Rectangle location_end = OperationsWithImage.Search_Bitmap(bitmap_end, image, 0.1);
            Rectangle text_rectandle;
            if (location_end.Width != 0)
            {
                text_rectandle = new Rectangle(0, 0, location_end.X, image.Height);
                image = image.Clone(text_rectandle, image.PixelFormat);
            }
            return image;

        }
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*##################################################################################################################*/
            /*преобразует картинку в монохром черно-белый*/
            public static Bitmap Bitmap_To_BlackWhite_Monochrome(Bitmap bmpImg, int black_white_coeff)
        {
            Color color = new Color();
            try
            {
                unsafe
                {
                    for (int j = 0; j < bmpImg.Height; j++)
                    {
                        for (int i = 0; i < bmpImg.Width; i++)
                        {
                            color = bmpImg.GetPixel(i, j);
                            int K = ((color.R + color.G + color.B) / 3);
                            //int K = (int)(0.11f * color.R + 0.59f * color.G + 0.3f * color.B);
                            bmpImg.SetPixel(i, j, (K <= black_white_coeff ? Color.Black : Color.White));
                        }
                    }
                }
                //for (int j = 0; j < result.Height; j++)
                //{
                //    for (int i = 0; i < result.Width; i++)
                //    {
                //        color = result.GetPixel(i, j);
                //        result.SetPixel(i, j, (color.ToArgb() == Color.White.ToArgb() ? Color.Black : Color.White));
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return bmpImg;
        }

        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        static public Bitmap Сreate_Filled_Bitmap(int x, int y)
        {
            Bitmap bmp = new Bitmap(x, y);
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle ImageSize = new Rectangle(0, 0, x, y);
                graph.FillRectangle(Brushes.Black, ImageSize);
            }
            return bmp;
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*возврашает текст на картинке*/
        public String Get_Text_From_Bitmap(Bitmap srcImage, Rectangle srcRegion, string whitelist, int black_white_coeff)
        {
            if (srcImage.Width==0) return "Error";
            Bitmap image = Region_Bitmap_To_BlackWhite_Monochrome(srcImage, srcRegion, black_white_coeff);
            List<tessnet2.Word> result;
            try
            {
                tessnet2.Tesseract ocr = new tessnet2.Tesseract();
                ocr.SetVariable("tessedit_char_whitelist", whitelist);
                //ocr.SetVariable("tessedit_char_blacklist", "1234567890."); 
                //нужно проверить наличие этой папки, если ее нет то крушиться приложение
                //не взирая на try catch
                var dir = System.IO.Directory.GetCurrentDirectory();
                string ocrPath = dir + @"\tessdata";
                //Console.WriteLine("DIR:" + dir);
                bool exists = System.IO.Directory.Exists(ocrPath);
                if (!exists)
                    return "NO FOLDER tessdata";

                ocr.Init(ocrPath, "eng", false);
                result = ocr.DoOCR(image, Rectangle.Empty);
            }
            catch (Exception)
            {
                return "Error";
            }

            foreach (tessnet2.Word word in result)
            {
                //MessageBox.Show(word.Confidence.ToString());
                //MessageBox.Show(word.Text);
                return word.Text;
            }




            return "Error";
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        // Palette - палитра Cl - цвет, который требуется найти. Возвращаемое значение - индекс цвета в палитре
        public int Find_Color_In_Palette(Color[] Palette, Color Cl, int num)
        {
            int i, fi, best_color = -1, f_min = 6000000;
            for (i = 0; i < num; i++)
            {
                fi = 30 * (int)Math.Pow((Palette[i].R - Cl.R), 2) +
                     59 * (int)Math.Pow((Palette[i].G - Cl.G), 2) +
                     11 * (int)Math.Pow((Palette[i].B - Cl.B), 2);
                if (fi < f_min) { best_color = i; f_min = fi; }
            }
            return (best_color);
        }
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        /*##################################################################################################################*/
        static public Rectangle Search_Bitmap(Bitmap smallBmp, Bitmap bigBmp, double tolerance)
        {
            BitmapData smallData =
              smallBmp.LockBits(new Rectangle(0, 0, smallBmp.Width, smallBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapData bigData =
              bigBmp.LockBits(new Rectangle(0, 0, bigBmp.Width, bigBmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            int smallStride = smallData.Stride;
            int bigStride = bigData.Stride;

            int bigWidth = bigBmp.Width;
            int bigHeight = bigBmp.Height - smallBmp.Height + 1;
            int smallWidth = smallBmp.Width * 3;
            int smallHeight = smallBmp.Height;

            Rectangle location = Rectangle.Empty;
            int margin = Convert.ToInt32(255.0 * tolerance);

            unsafe
            {
                byte* pSmall = (byte*)(void*)smallData.Scan0;
                byte* pBig = (byte*)(void*)bigData.Scan0;

                int smallOffset = smallStride - smallBmp.Width * 3;
                int bigOffset = bigStride - bigBmp.Width * 3;

                bool matchFound = true;

                for (int y = 0; y < bigHeight; y++)
                {
                    for (int x = 0; x < bigWidth; x++)
                    {
                        byte* pBigBackup = pBig;
                        byte* pSmallBackup = pSmall;

                        //Look for the small picture.
                        for (int i = 0; i < smallHeight; i++)
                        {
                            int j = 0;
                            matchFound = true;
                            for (j = 0; j < smallWidth; j++)
                            {
                                //With tolerance: pSmall value should be between margins.
                                int inf = pBig[0] - margin;
                                int sup = pBig[0] + margin;
                                if (sup < pSmall[0] || inf > pSmall[0])
                                {
                                    matchFound = false;
                                    break;
                                }

                                pBig++;
                                pSmall++;
                            }

                            if (!matchFound) break;

                            //We restore the pointers.
                            pSmall = pSmallBackup;
                            pBig = pBigBackup;

                            //Next rows of the small and big pictures.
                            pSmall += smallStride * (1 + i);
                            pBig += bigStride * (1 + i);
                        }

                        //If match found, we return.
                        if (matchFound)
                        {
                            location.X = x;
                            location.Y = y;
                            location.Width = smallBmp.Width;
                            location.Height = smallBmp.Height;
                            break;
                        }
                        //If no match found, we restore the pointers and continue.
                        else
                        {
                            pBig = pBigBackup;
                            pSmall = pSmallBackup;
                            pBig += 3;
                        }
                    }

                    if (matchFound) break;

                    pBig += bigOffset;
                }
            }

            bigBmp.UnlockBits(bigData);
            smallBmp.UnlockBits(smallData);

            return location;
        }
    }
}
