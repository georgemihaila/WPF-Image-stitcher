using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Compression;

namespace WPFStitcher
{
    public partial class MainWindow : Window
    {
        private readonly string _LocalTemporaryDirectoryPath = "tmp";

        public MainWindow()
        {
            InitializeComponent();
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            LayoutRoot.Loaded += (ls, le) => LayoutRoot.SizeChanged += (lsr, ler) => 
            {
                StitchResult_ScrollViewer.Width = ler.NewSize.Width;
                StitchResult_ScrollViewer.Height = ler.NewSize.Height - StitchResult_ScrollViewer.Margin.Top;
                Stitch_Button.Margin = new Thickness(ler.NewSize.Width - Stitch_Button.Width - 10, Stitch_Button.Margin.Top, 0, 0);
            };
        }

        /// <summary>Handles the DispatcherUnhandledException event of the Current control.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Threading.DispatcherUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.ToString());
        }

        /// <summary>Occurs when the user intents to choose a .zip image package file.</summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void PackagePick_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Title = "Choose package",
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false,
                Filter = "Archive files (.zip)|*.zip"
            };
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                string filePath = openFileDialog.FileName;
                if (Directory.Exists(_LocalTemporaryDirectoryPath))
                    Directory.Delete(_LocalTemporaryDirectoryPath, true);
                Directory.CreateDirectory(_LocalTemporaryDirectoryPath);
                ZipFile.ExtractToDirectory(filePath, _LocalTemporaryDirectoryPath);
                PackagePick_TextBlock.Text = openFileDialog.SafeFileName;
                if (!Stitch_Button.IsEnabled)
                    Stitch_Button.IsEnabled = true;
            }
        }

        /// <summary>
        /// Stitches 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void Stitch_Button_Click(object sender, RoutedEventArgs e)
        {
            Stitch_Button.IsEnabled = false;
            try
            {
                IUncompressedImageProvider imageProvider = new UncompressedImageProvider();
                IUncompressedImage[] uncompressedImages = await imageProvider.GetUncompressedImages(_LocalTemporaryDirectoryPath, 800, 600);
                IImageStitcher<SimilarityFinder> stitcher = new ImageStitcher<SimilarityFinder>();
                IUncompressedImage result = await stitcher.Stitch(uncompressedImages);

                this.StitchedImage.Source = result.ToBitmap().ToImageSource();
                this.StitchedImage.Width = result.Width;
            }
            catch
            {
                throw;
            }
            finally
            {
                Stitch_Button.IsEnabled = true;
            }
        }
    }
}
