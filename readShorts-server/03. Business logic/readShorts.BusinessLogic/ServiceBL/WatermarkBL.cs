using Framework.Core.Utils;
using readShorts.DataAccess.Storage;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace readShorts.BusinessLogic.ServiceBL
{
    public class WatermarkBL
    {
        //private void Main(string backgroundPhotoFullPath, string watermarkPhotoFullPath, string text, string tempDestinationPhotofullPath)
        //{
        //    Image imgPhoto = null;
        //    //create a image object containing the text/watermark
        //    using (MemoryStream fs = StorageUtil.Download(watermarkPhotoFullPath, StorageUtil.CONST_SHARE_PICTURE_PATH))
        //    {
        //        imgPhoto = Image.FromStream(fs);
        //    }

        //    if (imgPhoto == null)
        //    {
        //        throw new FileNotFoundException(string.Format("file : {0} from container : {1}", watermarkPhotoFullPath, StorageUtil.CONST_SHARE_PICTURE_PATH));
        //    }

        //    int phWidth = imgPhoto.Width;
        //    int phHeight = imgPhoto.Height;

        //    //create a image object for the back ground of the whole picture
        //    Image imgWatermark = null;
        //    using (MemoryStream fs = StorageUtil.Download(backgroundPhotoFullPath, StorageUtil.CONST_SHARE_PICTURE_PATH))
        //    {
        //        imgWatermark = new Bitmap(fs);
        //    }

        //    if (imgWatermark == null)
        //    {
        //        throw new FileNotFoundException(string.Format("file : {0} from container : {1}", backgroundPhotoFullPath, StorageUtil.CONST_SHARE_PICTURE_PATH));
        //    }

        //    int wmWidth = imgWatermark.Width;
        //    int wmHeight = imgWatermark.Height;

        //    //create a Bitmap the Size of the original photograph
        //    //Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
        //    Bitmap bmPhoto = new Bitmap(wmWidth, wmHeight, PixelFormat.Format24bppRgb);

        //    bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        //    //load the Bitmap into a Graphics object
        //    Graphics grPhoto = Graphics.FromImage(bmPhoto);

        //    //------------------------------------------------------------
        //    //Step #1 - Insert Copyright message
        //    //------------------------------------------------------------

        //    InsertMessage(text, imgPhoto, phWidth, phHeight, grPhoto);

        //    //------------------------------------------------------------
        //    //Step #2 - Insert Watermark image
        //    //------------------------------------------------------------

        //    //Create a Bitmap based on the previously modified photograph Bitmap
        //    Bitmap bmWatermark = new Bitmap(bmPhoto);
        //    bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
        //    //Load this Bitmap into a new Graphic Object
        //    Graphics grWatermark = Graphics.FromImage(bmWatermark);

        //    InserWatermarkImage(wmWidth, wmHeight, imgWatermark, phWidth, phHeight, grWatermark);

        //    //Replace the original photgraphs bitmap with the new Bitmap
        //    imgPhoto = bmWatermark;
        //    grPhoto.Dispose();
        //    grWatermark.Dispose();

        //    //save new image to file system.
        //    StorageUtil.Upload(
        //        ConvertUtil.ImageToByteArr(imgPhoto),
        //        tempDestinationPhotofullPath,
        //        StorageUtil.CONST_SHARE_PICTURE_PATH,
        //        "application/octet-stream",
        //        string.Format("attachment; filename=\"{0}\"", tempDestinationPhotofullPath));

        //    imgPhoto.Dispose();
        //    imgWatermark.Dispose();
        //}

        public static void Execute(string watermarkPhotoFullPath, string text, string writer, string tempDestinationPhotofullPath)
        {
            Image imgPhoto = null;
            //create a image object containing the text/watermark
            using (MemoryStream fs = StorageUtil.Download(watermarkPhotoFullPath, StorageUtil.CONST_SHARE_PICTURE_PATH))
            {
                imgPhoto = Image.FromStream(fs);
            }

            if (imgPhoto == null)
            {
                throw new FileNotFoundException(string.Format("file : {0} from container : {1}", watermarkPhotoFullPath, StorageUtil.CONST_SHARE_PICTURE_PATH));
            }

            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph
            //Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            //------------------------------------------------------------
            //Step #1 - Insert Copyright message
            //------------------------------------------------------------

            InsertMessage(text, writer, imgPhoto, phWidth, phHeight, grPhoto);

            //------------------------------------------------------------
            //Step #2 - Insert Watermark image
            //------------------------------------------------------------

            //Create a Bitmap based on the previously modified photograph Bitmap
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //Load this Bitmap into a new Graphic Object
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            Color backColor = bmWatermark.GetPixel(0, 0);
            //bmWatermark.MakeTransparent(backColor);

            //grWatermark.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //CreateTransparentText(imgPhoto, grWatermark);

            //Replace the original photgraphs bitmap with the new Bitmap
            imgPhoto = bmWatermark;

            grPhoto.Dispose();
            grWatermark.Dispose();

            //save new image to file system.
            StorageUtil.Upload(
                ConvertUtil.ImageToByteArr(imgPhoto),
                tempDestinationPhotofullPath,
                StorageUtil.CONST_SHARE_PICTURE_PATH,
                "application/octet-stream",
                string.Format("attachment; filename=\"{0}\"", tempDestinationPhotofullPath));

            imgPhoto.Dispose();
        }

        private static void CreateTransparentText(Image imgWatermark, Graphics grWatermark)
        {

            //To achieve a transulcent watermark we will apply (2) color
            //manipulations by defineing a ImageAttributes object and
            //seting (2) of its properties.
            ImageAttributes imageAttributes = new ImageAttributes();

            //The first step in manipulating the watermark image is to replace
            //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
            //to do this we will use a Colormap and use this to define a RemapTable
            ColorMap colorMap = new ColorMap();

            //My watermark was defined with a background of 100% Green this will
            //be the color we search for and replace with transparency
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            //The second color manipulation is used to change the opacity of the
            //watermark.  This is done by applying a 5x5 matrix that contains the
            //coordinates for the RGBA space.  By setting the 3rd row and 3rd column
            //to 0.3f we achive a level of opacity
            float[][] colorMatrixElements = {
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            //For this example we will place the watermark in the upper right
            //hand corner of the photograph. offset down 10 pixels and to the
            //left 10 pixles

            int xPosOfWm = 0;
            int yPosOfWm = 0;

            grWatermark.DrawImage(imgWatermark,
                new Rectangle(xPosOfWm, yPosOfWm, imgWatermark.Width, imgWatermark.Height),  //Set the detination Position
                0,                  // x-coordinate of the portion of the source image to draw.
                0,                  // y-coordinate of the portion of the source image to draw.
                imgWatermark.Width,            // Watermark Width
                imgWatermark.Height,           // Watermark Height
                GraphicsUnit.Pixel, // Unit of measurment
                imageAttributes);   //ImageAttributes Object
        }

        private void InserWatermarkImage(int phWidth, int phHeight, Image imgWatermark, int wmWidth, int wmHeight, Graphics grWatermark)
        {
            //To achieve a transulcent watermark we will apply (2) color
            //manipulations by defineing a ImageAttributes object and
            //seting (2) of its properties.
            ImageAttributes imageAttributes = new ImageAttributes();

            //The first step in manipulating the watermark image is to replace
            //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
            //to do this we will use a Colormap and use this to define a RemapTable
            ColorMap colorMap = new ColorMap();

            //My watermark was defined with a background of 100% Green this will
            //be the color we search for and replace with transparency
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            //The second color manipulation is used to change the opacity of the
            //watermark.  This is done by applying a 5x5 matrix that contains the
            //coordinates for the RGBA space.  By setting the 3rd row and 3rd column
            //to 0.3f we achive a level of opacity
            float[][] colorMatrixElements = {
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.5f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            //For this example we will place the watermark in the upper right
            //hand corner of the photograph. offset down 10 pixels and to the
            //left 10 pixles

            int xPosOfWm = ((phWidth - wmWidth) - 10);
            int yPosOfWm = 50;

            grWatermark.DrawImage(imgWatermark,
                new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),  //Set the detination Position
                0,                  // x-coordinate of the portion of the source image to draw.
                0,                  // y-coordinate of the portion of the source image to draw.
                wmWidth,            // Watermark Width
                wmHeight,           // Watermark Height
                GraphicsUnit.Pixel, // Unit of measurment
                imageAttributes);   //ImageAttributes Object
        }

        private static void InsertMessage(string text, string writer, Image imgPhoto, int phWidth, int phHeight, Graphics grPhoto)
        {
            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
                imgPhoto,                               // Photo Image object
                new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
                0,                                      // x-coordinate of the portion of the source image to draw.
                0,                                      // y-coordinate of the portion of the source image to draw.
                phWidth,                                // Width of the portion of the source image to draw.
                phHeight,                               // Height of the portion of the source image to draw.
                GraphicsUnit.Pixel);                    // Units of measure

            //-------------------------------------------------------
            //to maximize the size of the Copyright message we will
            //test multiple Font sizes to determine the largest posible
            //font we can use for the width of the Photograph
            //define an array of point sizes you would like to consider as possiblities
            //-------------------------------------------------------
            int[] sizes = new int[] { 26,25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4 };

            Font crFont = null;
            SizeF crSize = new SizeF();

            //Loop through the defined sizes checking the length of the Copyright string
            //If its length in pixles is less then the image width choose this Font size.
            for (int i = 0; i < sizes.GetLength(0); i++)
            {
                //set a Font object to Arial (i)pt, Bold
                crFont = new Font("Arial", sizes[i], FontStyle.Regular);
                //Measure the Copyright string in this Font
                crSize = grPhoto.MeasureString(text, crFont);

                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //Since all photographs will have varying heights, determine a
            //position 5% from the bottom of the image
            //int yPixlesFromBottom = (int)(phHeight * .05);
            int yPixlesFromBottom = (int)(phHeight * .50);

            //Now that we have a point size use the Copyrights string height
            //to determine a y-coordinate to draw the string of the photograph
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2)-20);

            //Determine its x-coordinate by calculating the center of the width of the image
            float xCenterOfImg = (phWidth / 2);

            //Define the text layout by setting the text alignment to centered
            StringFormat StrFormat = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            StrFormat.Alignment = StringAlignment.Center;

            //define a Brush which is semi trasparent black (Alpha set to 153)
            //SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(250, 0, 0, 0));
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

            //Draw the Copyright string
            //grPhoto.DrawString(text,                 //string of text
            //    crFont,                                   //font
            //    semiTransBrush2,                           //Brush
            //    new PointF(xCenterOfImg + 1, yPosFromBottom + 1),  //Position
            //    StrFormat);

            //define a Brush which is semi trasparent white (Alpha set to 153)
            //SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 0, 0, 0));
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(255, 255, 255, 255));

            //Draw the Copyright string a second time to create a shadow effect
            //Make sure to move this text 1 pixel to the right and down 1 pixel
            grPhoto.DrawString(text,                 //string of text
                crFont,                                   //font
                semiTransBrush,                           //Brush
                new PointF(xCenterOfImg, yPosFromBottom),  //Position
                StrFormat);                               //Text alignment

            //Font writerByFont = new Font("arial", 14, FontStyle.Italic);

            //grPhoto.DrawString("by ",      //string of text
            //writerByFont,                                   //font
            //semiTransBrush,                           //Brush
            //new PointF((int)(xCenterOfImg * 1.3), phHeight - (int)(phHeight * 0.13)),  //Position
            //StrFormat);                               //Text alignment

            Font writerFont = new Font("Arial", 12, FontStyle.Italic | FontStyle.Bold);

            grPhoto.DrawString(writer,      //string of text
            writerFont,                                   //font
            semiTransBrush,                           //Brush
            new PointF((int)(xCenterOfImg), (int)(10.4)),  //Position
            StrFormat);                               //Text alignment
        }
    }
}