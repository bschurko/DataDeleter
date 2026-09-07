using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DataDeleter.ViewModel
{
    public partial class ImageViewModel : ObservableObject
    {
        [ObservableProperty]
        private BitmapImage? _imageSource;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [ObservableProperty]
        private bool _hasError;

        public ImageViewModel(string imagePath)
        {
            LoadImageSafe(imagePath);
        }

        private void LoadImageSafe(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                ErrorMessage = "File does not exist or path is invalid.";
                HasError = true;
                return;
            }

            try
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                // OnLoad cache option ensures the file stream releases immediately and decodes on load
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(filePath, UriKind.Absolute);
                bitmap.EndInit();
                bitmap.Freeze(); // Freezes bitmap for thread-safe cross-thread UI consumption

                ImageSource = bitmap;
                HasError = false;
            }
            catch (Exception ex) when (ex is NotSupportedException || ex is FileFormatException || ex is Exception)
            {
                // Prevents crash when opening non-image files (e.g. .txt, .pdf, corrupt headers)
                ErrorMessage = $"Unable to load image: The file format is invalid or unsupported.\n({Path.GetFileName(filePath)})";
                HasError = true;
            }
        }
    }
}
