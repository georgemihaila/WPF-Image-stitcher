using Microsoft.JScript;
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
    /// <seealso cref="WPFStitcher.IImageStitcher" />
    class ImageStitcher<T> : IImageStitcher<T> where T: ISimilarityFinder, new()
    {
        /// <summary>
        /// Gets or sets the similarity finder.
        /// </summary>
        T SimilarityFinder { get; set; }

        /// <summary>
        /// Stitches the specified images.
        /// </summary>
        /// <param name="images">The images to be stitched.</param>
        /// <returns>
        /// An <see cref="IUncompressedImage" /> instance representing the result of the stitch.
        /// </returns>
        public async Task<IUncompressedImage> Stitch(IUncompressedImage[] images)
        {
            List<IUncompressedImage> imgs = images.ToList();
            IUncompressedImage result = new UncompressedImage(0, 0);
            while (imgs.Count > 0)
            {
                var res = FindMostSimilar(imgs[0], imgs.ToArray()).Result;
                await Stitch(ref result, imgs[0], res.Item1, res.Item2);
                imgs.Remove(imgs[0]);
                imgs.Remove(res.Item1);
            }
            return result;
        }

        /// <summary>
        /// Stitches two images to a source image.
        /// </summary>
        /// <param name="source">The source image.</param>
        /// <param name="image1">The first image.</param>
        /// <param name="image2">The second image.</param>
        /// <param name="similarity">The similarity between the two images.</param>
        private Task Stitch(ref IUncompressedImage source, IUncompressedImage image1, IUncompressedImage image2, ImageSimilarity similarity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds the most similar image that can be stitched against a source image.
        /// </summary>
        /// <param name="souce">The souce image.</param>
        /// <param name="potentials">The potential images. If the original image is contained in this collection, it is removed.</param>
        private Task<Tuple<IUncompressedImage, ImageSimilarity>> FindMostSimilar(IUncompressedImage source, IUncompressedImage[] potentials) 
            => Task.FromResult(potentials.ToList().Select(o => new Tuple<IUncompressedImage, ImageSimilarity>(o, SimilarityFinder.FindSimilarity(source, o).Result)).First());

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

                }
                this.SimilarityFinder = default;
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
