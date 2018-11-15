using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStitcher
{
    /// <summary>
    /// Represents an image stitcher.
    /// </summary>
    interface IImageStitcher<T> : IDisposable where T: ISimilarityFinder
    {
        /// <summary>
        /// Stitches the specified images.
        /// </summary>
        /// <param name="images">The images to be stitched.</param>
        /// <returns>An <see cref="IUncompressedImage"/> instance representing the result of the stitch.</returns>
        Task<IUncompressedImage> Stitch(IUncompressedImage[] images);
    }
}
