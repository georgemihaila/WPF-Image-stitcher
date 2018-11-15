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
    /// <seealso cref="WPFStitcher.ISimilarityFinder" />
    class SimilarityFinder : ISimilarityFinder
    {
        /// <summary>
        /// Finds the similarity between two images.
        /// </summary>
        /// <param name="image1">The first image.</param>
        /// <param name="image2">The second image.</param>
        /// <returns>
        /// An <see cref="ImageSimilarity" /> instance describing the similarities between the two pictures.
        /// </returns>
        public Task<ImageSimilarity> FindSimilarity(IUncompressedImage image1, IUncompressedImage image2)
        {
            int initw1 = image1.Width, inith1 = image1.Height, initw2 = image2.Width, inith2 = image2.Height;
            ImageSimilarity result = new ImageSimilarity();
            int resW = 10;
            int resH = 10;
            image1 = ((UncompressedImage)image1).Resize(resW, resH);
            image2 = ((UncompressedImage)image2).Resize(resW, resH);
            int[,] sims = new int[resW, resH];
            int[] minIndex = new int[2];
            int minVal = int.MaxValue;
            for (int x = 0; x < resW; x++)
            {
                for (int y = 0; y < resH; y++)
                {
                    Pixel res = (Pixel)image1.Pixels[x, y] - (Pixel)image1.Pixels[x, y];
                    sims[x, y] = res.R + res.G + res.B;
                    result.SimilarityPercent += sims[x, y];
                    if (sims[x, y] < minVal)
                    {
                        minVal = sims[x, y];
                        minIndex[0] = x;
                        minIndex[1] = y;
                    }
                }
            }
            result.SimilarityPercent *= 100;
            result.SimilarityPercent /= resW * resH;
            result.OverlapArea = new System.Drawing.Rectangle(initw1 * minIndex[0] / resW, initw2 * minIndex[1] / resH, initw1 - initw1 * minIndex[0] / resW, inith1 - initw2 * minIndex[1] / resH);
            return Task.FromResult(result);
        }
    }
}
