using DataDeleter.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataDeleter.Views
{
    /// <summary>
    /// Interaction logic for PopupImage.xaml
    /// </summary>
    public partial class PopupImage : Window
    {
        public PopupImage(string imagePath)
        {
            InitializeComponent();
            DataContext = new ImageViewModel(imagePath);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
