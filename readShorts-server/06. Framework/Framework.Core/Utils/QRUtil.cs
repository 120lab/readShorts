//using MessagingToolkit.QRCode.Codec;
//using MessagingToolkit.QRCode.Codec.Data;
//using System.Drawing;
//using System.IO;

//namespace Framework.Core.Utils
//{
//    public static class QRUtil
//    {
//        private const string CONST_FILE_PATH = "QR_Fund_{0}_MemberID_{1}.jpg";
//        private const string CONST_URI_PATH = "http://qr20160524122643.azurewebsites.net/api/Accept/{0}";

//        public static Bitmap Encoder(long id)
//        {
//            string uri = string.Format(CONST_URI_PATH, id);
//            QRCodeEncoder encoder = new QRCodeEncoder();
//            Bitmap img = encoder.Encode(uri);
//            return img;
//        }

//        public static void Decoder(int id)
//        {
//            QRCodeDecoder decoder = new QRCodeDecoder();
//            Bitmap img = new Bitmap(string.Format(CONST_FILE_PATH, 18, id));
//            string url = decoder.Decode(new QRCodeBitmapImage(img));
//        }
//    }
//}