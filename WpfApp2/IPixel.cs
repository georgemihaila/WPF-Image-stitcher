using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStitcher
{
    /// <summary>
    /// Represents a pixel.
    /// </summary>
    interface IPixel
    {
        /// <summary>
        /// Gets or sets the red channel value.
        /// </summary>
        byte R { get; set; }

        /// <summary>
        /// Gets or sets the green channel value.
        /// </summary>
        byte G { get; set; }

        /// <summary>
        /// Gets or sets the blue channel value.
        /// </summary>
        byte B { get; set; }
    }
}
