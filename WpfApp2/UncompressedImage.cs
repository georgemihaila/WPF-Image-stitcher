using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStitcher
{
    /// <summary>
    /// Represents an uncompressed image.
    /// </summary>
    /// <seealso cref="WPFStitcher.IUncompressedImage" />
    class UncompressedImage : IUncompressedImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UncompressedImage"/> class.
        /// </summary>
        /// <param name="width">The width of the image.</param>
        /// <param name="height">The height of the image.</param>
        public UncompressedImage(int width, int height)
        {
            this.Pixels = new IPixel[width, height];
        }

        /// <summary>
        /// Gets or sets the pixels of the image.
        /// </summary>
        public IPixel[,] Pixels { get; set; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public int Width => Pixels.GetLength(0);

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public int Height => Pixels.GetLength(1);

        /// <summary>
        /// Loads a file from local storage.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <returns>
        /// An instance of the <see cref="IUncompressedImage&gt;" /> class.
        /// </returns>
        public static Task<IUncompressedImage> FromFile(string filename)
        {
            Bitmap bitmap = (Bitmap)Image.FromFile(filename);
            UncompressedImage image = new UncompressedImage(bitmap.Width, bitmap.Height);
            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    image.Pixels[x, y] = new Pixel(bitmap.GetPixel(x, y));
            return Task.FromResult((IUncompressedImage)image);
        }

        /// <summary>
        /// Loads a file from local storage.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <param name="resizeWidth">The width to which the image should be resized.</param>
        /// <param name="resizeHeight">The height to which the image should be resized.</param>
        /// <returns></returns>
        public static Task<IUncompressedImage> FromFile(string filename, int resizeWidth, int resizeHeight) => Task.FromResult((IUncompressedImage)((Bitmap)Image.FromFile(filename)).Resize(resizeWidth, resizeHeight).ToUncompressedImage());

        #region IDisposable Support

        private bool disposedValue = false;

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Pixels = null;
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
