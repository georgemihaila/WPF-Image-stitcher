using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStitcher
{
    /// <summary>
    /// Represents a similarity finder.
    /// </summary>
    interface ISimilarityFinder
    {
        /// <summary>
        /// Finds the similarity between two images.
        /// </summary>
        /// <param name="image1">The first image.</param>
        /// <param name="image2">The second image.</param>
        /// <returns>An <see cref="ImageSimilarity"/> instance describing the similarities between the two pictures.</returns>
        Task<ImageSimilarity> FindSimilarity(IUncompressedImage image1, IUncompressedImage image2);
    }
}
