using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStitcher
{
    /// <summary>
    /// Represents a provider for <see cref="IUncompressedImage"/> instances.
    /// </summary>
    interface IUncompressedImageProvider
    {
        /// <summary>
        /// Gets an <see cref="IUncompressedImage"/> array representation of all the images in a directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        Task<IUncompressedImage[]> GetUncompressedImages(string directory);

        /// <summary>
        /// Gets an <see cref="IUncompressedImage" /> array representation of all the images in a directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="resizeWidth">The width to which the image should be resized.</param>
        /// <param name="resizeHeight">The height to which the image should be resized.</param>
        /// <returns></returns>
        Task<IUncompressedImage[]> GetUncompressedImages(string directory, int resizeWidth, int resizeHeight);
    }
}
