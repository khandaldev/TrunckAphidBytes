using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace AphidBytes.Core.Images
{
    public class ImageUtility
    {
        public static ILog Log = LogManager.GetLogger(typeof(ImageUtility).Name);
        public const int DefaultProfilePictureWidth = 100; 
        public const int DefaultProfilePictureHeight = 100;
        public const int DefaultImageEncodingQuality = 95;
        public const string DefaultProfilePicturePath = "nopic.jpg";

        public static void ResizeSelectedAreaToJpeg(Stream inputStream, Stream outputStream)
        {
            ResizeSelectedAreaToJpeg(inputStream, outputStream, DefaultImageEncodingQuality, DefaultProfilePictureWidth, DefaultProfilePictureHeight);
        }

        public static void ResizeSelectedAreaToJpeg(Stream inputStream, Stream outputStream, int quality, int destinationWidth, int destinationHeight)
        {
            var sw = new Stopwatch();
            sw.Start();

            using (var originalImage = Image.FromStream(inputStream))
            {
                var area = ImageArea.CreateFromImageAsSquare(originalImage);
                var newImage = MakeThumbnail(originalImage, area, destinationWidth, destinationHeight);
                OutputImageToStream(newImage, outputStream, quality);
            }

            sw.Stop();
            Log.Debug("Image resizing and cropping took " + sw.ElapsedMilliseconds + "ms");
        }

        private static Image MakeThumbnail(Image sourceImage, ImageArea sourceArea, int destinationWidth, int destinationHeight)
        {

            var thumbnail = new Bitmap(destinationWidth, destinationHeight);
            var graphics = Graphics.FromImage(thumbnail);

            SetEncodingConfiguration(graphics);

            graphics.DrawImage(sourceImage, new Rectangle(0, 0, destinationWidth, destinationHeight),
                                        sourceArea.X, sourceArea.Y, sourceArea.Width, sourceArea.Height, GraphicsUnit.Pixel);

            return thumbnail;
        }

        private static void SetEncodingConfiguration(Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
        }

        private static void OutputImageToStream(Image image, Stream outputStream, int quality)
        {
            ImageCodecInfo codecEncoder = GetEncoder("image/jpeg");
            EncoderParameters encodeParams = new EncoderParameters(1);
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encodeParams.Param[0] = qualityParam;
            image.Save(outputStream, codecEncoder, encodeParams);
            image.Dispose();
        }

        private static ImageCodecInfo GetEncoder(string mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().Where(c => c.MimeType == mimeType).FirstOrDefault();
        }
    }

}
