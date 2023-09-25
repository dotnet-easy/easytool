using System.Drawing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyTool.CoreTests.ImageCategory
{
    [TestClass]
    public class ImageUtilTests
    {
        /// <summary>
        /// 图像分割方法测试
        /// 测试用到的ori和mask在测试类旁边的Reources中
        /// </summary>
        [TestMethod]
        public void MaskImageTest()
        {
            Image ori = new Bitmap(@"C:\Desktop\ori.jpg");
            Image mask = new Bitmap(@"C:\Desktop\mask.jpg");

            Console.WriteLine($"ori-width:{ori.Width} | ori-height:{ori.Height}");
            Console.WriteLine($"mask-width:{mask.Width} | mask-height:{mask.Height}");

            Image result = ImgUtil.MaskImage(mask, ori);

            Console.WriteLine($"result-width:{mask.Width} | result-height:{mask.Height}");
            result.Save(@"C:\Desktop\result.jpg");
        }

        [TestMethod]
        public void ResizeImageTest()
        {
            Console.WriteLine("Hello World");
        }

        [TestMethod]
        public void CropImageTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ConvertImageFormatTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ConvertToBlackAndWhiteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void AddTextWatermarkTest()
        {
            Assert.Fail();
        }
    }
}
