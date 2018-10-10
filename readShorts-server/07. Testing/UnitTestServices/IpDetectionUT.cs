using Microsoft.VisualStudio.TestTools.UnitTesting;
using readShorts.BusinessLogic;
using System.IO;

namespace UnitTestServices
{
    [TestClass]
    public class IpDetectionUT
    {
        [TestMethod]
        public void TestIsIsraelIP_IsraelValid()
        {
            Assert.IsTrue(IpDetectionBL.IsIsraelIP("194.56.215.218"));
        }

        [TestMethod]
        public void TestIsIsraelIP_InvalidIP()
        {
            Assert.IsFalse(IpDetectionBL.IsIsraelIP("194.tt.215.218"));
        }

        [TestMethod]
        public void TestIsIsraelIP_USAIP()
        {
            Assert.IsFalse(IpDetectionBL.IsIsraelIP("216.253.255.255"));
        }
    }
}