using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WPFStitcher
{
    /// <summary>
    /// Represents a provider for <see cref="IUncompressedImage"/> instances.
    /// </summary>
    /// <seealso cref="WPFStitcher.IUncompressedImageProvider" />
    class UncompressedImageProvider : IUncompressedImageProvider
    {
        /// <summary>
        /// The allowed image file types.
        /// </summary>
        private readonly string[] _SupportedImageTypes = new string[] { "*.jpg", "*.png", "*.jpeg" }; 

        /// <summary>
        /// Gets an <see cref="IUncompressedImage" /> array representation of all the images in a directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        public async Task<IUncompressedImage[]> GetUncompressedImages(string directory)
        {
            List<string> imageNames = GetSupportedImagesFromDirectory(directory);
            List<IUncompressedImage> images = new List<IUncompressedImage>();
            List<Task<IUncompressedImage>> tasks = imageNames.Select(o => Task.Run(()=>UncompressedImage.FromFile(o))).ToList();
            while(tasks.Count > 0)
            {
                Task<IUncompressedImage> firstTask = (Task<IUncompressedImage>)await Task.WhenAny(tasks.ToArray());
                tasks.Remove(firstTask);
                images.Add(firstTask.Result);
            }
            return images.ToArray();
        }

        /// <summary>
        /// Gets an <see cref="IUncompressedImage" /> array representation of all the images in a directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="resizeWidth">The width to which the image should be resized.</param>
        /// <param name="resizeHeight">The height to which the image should be resized.</param>
        public async Task<IUncompressedImage[]> GetUncompressedImages(string directory, int resizeWidth, int resizeHeight)
        {
            List<string> imageNames = GetSupportedImagesFromDirectory(directory);
            List<IUncompressedImage> images = new List<IUncompressedImage>();
            List<Task<IUncompressedImage>> tasks = imageNames.Select(o => Task.Run(() => UncompressedImage.FromFile(o, resizeWidth, resizeHeight))).ToList();
            while (tasks.Count > 0)
            {
                Task<IUncompressedImage> firstTask = (Task<IUncompressedImage>)await Task.WhenAny(tasks.ToArray());
                tasks.Remove(firstTask);
                images.Add(firstTask.Result);
            }
            return images.ToArray();
        }

        /// <summary>
        /// Gets the names of the supported images from a directory.
        /// </summary>
        /// <param name="directory">The directory to be searched.</param>
        private List<string> GetSupportedImagesFromDirectory(string directory)
        {
            List<string> allImages = new List<string>();
            foreach (var fileType in _SupportedImageTypes)
            {
                string[] forType = Directory.GetFiles(directory, fileType, SearchOption.AllDirectories);
                foreach (string imageName in forType)
                    allImages.Add(imageName);
            }
            return allImages;
        }
    }
}
