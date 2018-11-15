using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFStitcher
{
    /// <summary>
    /// Represents extension methods.
    /// </summary>
    static class Extensions
    {
        //If you get 'dllimport unknown'-, then add 'using System.Runtime.InteropServices;'
        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        /// <summary>
        /// Performs a conversion from <see cref="Bitmap"/> to <see cref="ImageSource"/>.
        /// </summary>
        public static ImageSource ToImageSource(this Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            try
            {
                return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally { DeleteObject(handle); }
        }

        /// <summary>
        /// Performs a conversion from <see cref="IUncompressedImage"/> to <see cref="Bitmap"/>.
        /// </summary>
        public static Bitmap ToBitmap(this IUncompressedImage image)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    Pixel p = ((Pixel)image.Pixels[x, y]) ?? new Pixel(0, 0, 0);
                    bmp.SetPixel(x, y, (System.Drawing.Color)p);
                }
            return bmp;
        }
        
        /// <summary>
        /// Resizes a bitmap.
        /// </summary>
        public static Bitmap Resize(this Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        /// <summary>
        /// Performs a conversion from <see cref="Bitmap"/> to <see cref="UncompressedImage"/>.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        /// <returns></returns>
        public static UncompressedImage ToUncompressedImage(this Bitmap bitmap)
        {
            UncompressedImage image = new UncompressedImage(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    image.Pixels[x, y] = new Pixel(bitmap.GetPixel(x, y));
            return image;
        }

        /// <summary>
        /// Resizes the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public static UncompressedImage Resize(this UncompressedImage source, int width, int height) => source.ToBitmap().Resize(width, height).ToUncompressedImage();
    }
}
