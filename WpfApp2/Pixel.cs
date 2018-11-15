using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFStitcher
{
    class Pixel : IPixel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pixel"/> class.
        /// </summary>
        public Pixel() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Pixel" /> class.
        /// </summary>
        /// <param name="r">The red channel value.</param>
        /// <param name="g">The green channel value.</param>
        /// <param name="b">The blue channel value.</param>
        public Pixel(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pixel"/> class.
        /// </summary>
        /// <param name="source">The source <see cref="Color"/>.</param>
        public Pixel(Color source)
        {
            this.R = source.R;
            this.G = source.G;
            this.B = source.B;
        }

        /// <summary>
        /// Gets or sets the red channel value.
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Gets or sets the green channel value.
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// Gets or sets the green channel value.
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Pixel"/> to <see cref="Color"/>.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator Color(Pixel p) => Color.FromArgb(255,p.R, p.G, p.B);

        /// <summary>
        /// Implements the operator -.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static Pixel operator -(Pixel p1, Pixel p2) => new Pixel((byte)(p1.R - p2.R), (byte)(p1.G - p2.G), (byte)(p1.B - p2.B));
    }
}
