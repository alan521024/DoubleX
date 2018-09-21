using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.DrawingCore;
using System.DrawingCore.Imaging;
using System.DrawingCore.Drawing2D;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// 图片辅助操作
    /// </summary>
    public static class ImageHelper
    {
        #region 属性变量

        #endregion

        #region 获取内容

        #endregion

        #region 扩展方法

        #endregion

        #region 验证判断

        #endregion

        #region 辅助操作(GetByXXX,GetToXXX,GetByXXXXToXXX,SetXXX,......)

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="path">原图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public static string GetToThumbnailBase64(string path, int width = 0, int height = 0, int flag = 100)
        {
            Bitmap img = GetToThumbnailBitmap(path, width, height, flag);
            if (img != null)
            {
                return GetByBitmapToBase64(img);
            }
            return string.Empty;
        }

        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="path">原图片</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="flag">压缩质量 1-100</param>
        /// <returns></returns>
        public static Bitmap GetToThumbnailBitmap(string path, int width = 0, int height = 0, int flag = 100)
        {
            Image sourceImg = Image.FromFile(path);
            ImageFormat tFormat = sourceImg.RawFormat;
            int sW = 0, sH = 0;

            //按比例缩放
            Size sourceSize = new Size(sourceImg.Width, sourceImg.Height);

            //根据压缩质量自动宽高
            if (width == 0)
            {
                width = sourceSize.Width / (100 / flag);
            }
            if (height == 0)
            {
                height = sourceSize.Height / (100 / flag);
            }

            if (sourceSize.Width > height || sourceSize.Width > width)
            {
                if ((sourceSize.Width * height) > (sourceSize.Height * width))
                {
                    sW = width;
                    sH = (width * sourceSize.Height) / sourceSize.Width;
                }
                else
                {
                    sH = height;
                    sW = (sourceSize.Width * height) / sourceSize.Height;
                }
            }
            else
            {
                sW = sourceSize.Width;
                sH = sourceSize.Height;
            }

            Bitmap targetImg = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(targetImg);
            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(sourceImg, new Rectangle((width - sW) / 2, (height - sH) / 2, sW, sH), 0, 0, sourceImg.Width, sourceImg.Height, GraphicsUnit.Pixel);
            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.DrawingCore.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;
            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    targetImg.Save(ms, jpegICIinfo, ep);
                    return new Bitmap(ms);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                sourceImg.Dispose();
                targetImg.Dispose();
            }
        }

        /// <summary>
        /// 获取Base64字符串根据Bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetByBitmapToBase64(Bitmap bitmap, ImageFormat format = null)
        {
            if (bitmap == null)
            {
                return string.Empty;
            }

            if (format == null)
            {
                format = ImageFormat.Png;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, format);
                byte[] bytes = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(bytes, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(bytes);
            }
        }

        /// <summary>
        /// 获取Base64字符串根据Bitmap
        /// </summary>
        /// <param name="base64"></param>
        /// <returns></returns>
        public static Bitmap GetByBase64ToBitmap(string base64, ImageFormat format = null)
        {
            if (string.IsNullOrWhiteSpace(base64))
            {
                return null;
            }
            if (format == null)
            {
                format = ImageFormat.Png;
            }
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Bitmap bmp = new Bitmap(ms);
                    bmp.Save(ms, format);
                    ms.Close();
                    return bmp;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 生成内容图片(eg:验证码)
        /// </summary>
        public static byte[] GetContentGraphic(string content, int width = 0, int height = 30, float emSize = 13)
        {
            Bitmap image = new Bitmap(width == 0 ? (int)Math.Ceiling(content.Length * 16.0) : width, height);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //随机生成器
                Random random = new Random();


                //内容块大小
                float sizeWidth = (width - 10) / content.Length;
                float sizeHeight = (height - 10) / 2;

                //内容文字大小
                Font font = new Font("Arial", emSize, (FontStyle.Bold | FontStyle.Italic));
                float[] fontSizes = new float[content.Length];
                for (var i = 0; i < content.Length; i++)
                {
                    SizeF sizef = g.MeasureString(content[i].ToString(), font);
                    if ((sizeWidth - 2) / sizef.Width < 1)
                    {
                        fontSizes[i] = emSize * ((sizeWidth - 2) / sizef.Width);
                    }
                    else
                    {
                        fontSizes[i] = emSize;
                    }
                }

                //清空背景
                g.Clear(Color.White);

                //画干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    //绘制一条连接由坐标对指定的两个点的线条。 
                    g.DrawLine(new Pen(Color.Silver), x1, x2, y1, y2);
                }

                //内容居中
                StringFormat stringFormat = new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                for (var i = 0; i < content.Length; i++)
                {
                    Font currentFont = new Font("Arial", fontSizes[i], (FontStyle.Bold | FontStyle.Italic));
                    g.DrawString(content[i].ToString(), currentFont, brush, 5 + (i * sizeWidth), 5 + sizeHeight - (currentFont.GetHeight() / 2));
                }

                //指定像素的颜色
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //图片边框
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                //保存返回
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion
    }
}
