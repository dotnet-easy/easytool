using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace EasyTool
{
    public class ImgUtil
    {
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="width">缩放后的宽度</param>
        /// <param name="height">缩放后的高度</param>
        /// <returns>缩放后的图片</returns>
        public static Image ResizeImage(Image img, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.DrawImage(img, 0, 0, width, height);
            graphics.Dispose();
            return bmp;
        }

        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="x">剪裁起始横坐标</param>
        /// <param name="y">剪裁起始纵坐标</param>
        /// <param name="width">剪裁宽度</param>
        /// <param name="height">剪裁高度</param>
        /// <returns>剪裁后的图片</returns>
        public static Image CropImage(Image img, int x, int y, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.DrawImage(img, new Rectangle(0, 0, width, height), x, y, width, height, GraphicsUnit.Pixel);
            graphics.Dispose();
            return bmp;
        }

        /// <summary>
        /// 将图片转换为指定格式
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="format">目标格式</param>
        /// <returns>转换后的图片</returns>
        public static Image ConvertImageFormat(Image img, ImageFormat format)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, format);
            Image convertedImg = Image.FromStream(ms);
            ms.Dispose();
            return convertedImg;
        }

        /// <summary>
        /// 将彩色图片转换为黑白图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <returns>转换后的图片</returns>
        public static Image ConvertToBlackAndWhite(Image img)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix cm = new ColorMatrix(new float[][]{
                new float[]{0.299f, 0.299f, 0.299f, 0, 0},
                new float[]{0.587f, 0.587f, 0.587f, 0, 0},
                new float[]{0.114f, 0.114f, 0.114f, 0, 0},
                new float[]{0, 0,0, 1, 0},
                new float[]{0, 0, 0, 0, 1}
            });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, attributes);
            graphics.Dispose();
            return bmp;
        }

        /// <summary>
        /// 在图片上添加文字水印
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="text">水印文字</param>
        /// <param name="font">水印字体</param>
        /// <param name="brush">水印笔刷</param>
        /// <param name="x">水印起始横坐标</param>
        /// <param name="y">水印起始纵坐标</param>
        /// <returns>添加水印后的图片</returns>
        public static Image AddTextWatermark(Image img, string text, Font font, Brush brush, int x, int y)
        {
            Bitmap bmp = new Bitmap(img);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.DrawString(text, font, brush, new PointF(x, y));
            graphics.Dispose();
            return bmp;
        }

        /// <summary>
        /// 在图片上添加图片水印
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="watermark">水印图片</param>
        /// <param name="opacity">水印透明度</param>
        /// <param name="x">水印起始横坐标</param>
        /// <param name="y">水印起始纵坐标</param>
        /// <returns>添加水印后的图片</returns>
        public static Image AddImageWatermark(Image img, Image watermark, float opacity, int x, int y)
        {
            Bitmap bmp = new Bitmap(img);
            Graphics graphics = Graphics.FromImage(bmp);
            ImageAttributes attributes = new ImageAttributes();
            ColorMatrix cm = new ColorMatrix(new float[][]{
            new float[]{1, 0, 0, 0, 0},
            new float[]{0, 1, 0, 0, 0},
            new float[]{0, 0, 1, 0, 0},
            new float[]{0, 0, 0, opacity, 0},
            new float[]{0, 0, 0, 0, 1}
        });
            attributes.SetColorMatrix(cm);
            graphics.DrawImage(watermark, new Rectangle(x, y, watermark.Width, watermark.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, attributes);
            graphics.Dispose();
            return bmp;
        }

        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="angle">旋转角度</param>
        /// <returns>旋转后的图片</returns>
        public static Image RotateImage(Image img, float angle)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.TranslateTransform(bmp.Width / 2, bmp.Height / 2);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform((-img.Width / 2), (-img.Height / 2));
            graphics.DrawImage(img, new Point(0, 0));
            graphics.Dispose();
            return bmp;
        }

        /// <summary>
        /// 水平翻转图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <returns>翻转后的图片</returns>
        public static Image FlipImageHorizontally(Image img)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.Width, 0, -bmp.Width, bmp.Height, GraphicsUnit.Pixel);
            graphics.Dispose();
            return bmp;
        }
    }
}
