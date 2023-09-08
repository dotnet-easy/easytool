using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Text;

namespace EasyTool.ImageCategory
{
    public static class ImgExtension
    {
        /// <summary>
        /// 缩放图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="width">缩放后的宽度</param>
        /// <param name="height">缩放后的高度</param>
        /// <returns>缩放后的图片</returns>
        public static Image ResizeImage(this Image img, int width, int height) => ImgUtil.ResizeImage(img, width, height);

        /// <summary>
        /// 剪裁图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="x">剪裁起始横坐标</param>
        /// <param name="y">剪裁起始纵坐标</param>
        /// <param name="width">剪裁宽度</param>
        /// <param name="height">剪裁高度</param>
        /// <returns>剪裁后的图片</returns>
        public static Image CropImage(this Image img, int x, int y, int width, int height) => ImgUtil.CropImage(img, x, y, width, height);

        /// <summary>
        /// 将图片转换为指定格式
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="format">目标格式</param>
        /// <returns>转换后的图片</returns>
        public static Image ConvertImageFormat(this Image img, ImageFormat format) => ImgUtil.ConvertImageFormat(img, format);

        /// <summary>
        /// 将彩色图片转换为黑白图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <returns>转换后的图片</returns>
        public static Image ConvertToBlackAndWhite(this Image img) => ImgUtil.ConvertToBlackAndWhite(img);

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
        public static Image AddTextWatermark(this Image img, string text, Font font, Brush brush, int x, int y) => ImgUtil.AddTextWatermark(img, text, font, brush, x, y);

        /// <summary>
        /// 在图片上添加图片水印
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="watermark">水印图片</param>
        /// <param name="opacity">水印透明度</param>
        /// <param name="x">水印起始横坐标</param>
        /// <param name="y">水印起始纵坐标</param>
        /// <returns>添加水印后的图片</returns>
        public static Image AddImageWatermark(this Image img, Image watermark, float opacity, int x, int y) => ImgUtil.AddImageWatermark(img, watermark, opacity, x, y);

        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <param name="angle">旋转角度</param>
        /// <returns>旋转后的图片</returns>
        public static Image RotateImage(this Image img, float angle) => ImgUtil.RotateImage(img, angle);

        /// <summary>
        /// 水平翻转图片
        /// </summary>
        /// <param name="img">原始图片</param>
        /// <returns>翻转后的图片</returns>
        public static Image FlipImageHorizontally(this Image img) => ImgUtil.FlipImageHorizontally(img);
    }
}
