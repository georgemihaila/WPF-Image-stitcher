using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStitcher
{
    /// <summary>
    /// Represents an uncompressed image.
    /// </summary>
    interface IUncompressedImage : IDisposable
    {
        /// <summary>
        /// Gets or sets the pixels of the image.
        /// </summary>
        IPixel[,] Pixels { get; set; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        int Height { get; }
    }
}
