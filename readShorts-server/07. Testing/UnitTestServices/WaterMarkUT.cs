using Microsoft.VisualStudio.TestTools.UnitTesting;
using readShorts.BusinessLogic;
using System.IO;

namespace UnitTestServices
{
    [TestClass]
    public class WaterMarkUT
    {
        [TestMethod]
        public void TestCreatingShareImage()
        {
            WatermarkBL bl = new WatermarkBL();

            string backgroundPhotoFullPath = @"E:\readShorts\readShorts.Server\03. Business logic\readShorts.BusinessLogic\ServiceBL\background_photo.jpg";
            string watermarkPhotoFullPath = @"E:\readShorts\readShorts.Server\03. Business logic\readShorts.BusinessLogic\ServiceBL\watermark.png";
            string text = "To be or not to be\r\nThis is the question";
            string tempDestinationPhotofullPath = @"E:\readShorts\readShorts.Server\03. Business logic\readShorts.BusinessLogic\ServiceBL\ShareShortPhoto.jpg";
            bl.Main(backgroundPhotoFullPath, watermarkPhotoFullPath, text, tempDestinationPhotofullPath);
        }
    }
}