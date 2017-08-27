using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Core.Images
{
    public class ImageArea
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public static ImageArea CreateFromImageAsSquare(Image originalImage)
        {
            var minWidthHeight = Math.Min(originalImage.Height, originalImage.Width);
            var xPosition = (int)Math.Floor((originalImage.Width - minWidthHeight) / 2d); 
            var yPosition = (int)Math.Floor((originalImage.Width - minWidthHeight) / 2d);

            return new ImageArea
            {
                X = xPosition,
                Y = yPosition,
                Width = minWidthHeight,
                Height = minWidthHeight,
            };
        }
    }
}
