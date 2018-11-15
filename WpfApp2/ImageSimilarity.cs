using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStitcher
{
    /// <summary>
    /// Represents a class describing the similarity of two pictures.
    /// </summary>
    class ImageSimilarity
    {
        /// <summary>
        /// Gets or sets the similarity percent.
        /// </summary>
        public double SimilarityPercent { get; set; }

        /// <summary>
        /// Gets or sets the overlap area relative to the first image.
        /// </summary>
        public Rectangle OverlapArea { get; set; }
    }
}
