using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WeSplitApp.View.AddScreen
{
    /// <summary>
    /// Interaction logic for AddMembers.xaml
    /// </summary>
    public partial class AddMembers : UserControl
    {
        public BindingList<string> Members { get; set; } = new BindingList<string>();
        public String TripImageName;

        public AddMembers()
        {
            InitializeComponent();
            string defaultImageName = AppDomain.CurrentDomain.BaseDirectory + "Images/beach.png";
            LoadImage(defaultImageName);
        }

        private void MemberRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string name = button.DataContext as string;
                Members.Remove(name);
            }
        }

        private void MemberAddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = MemberNameTextBox.Text;
            MemberNameTextBox.Text = "";
            if (!string.IsNullOrEmpty(name) && Members.IndexOf(name) < 0)
            {
                Members.Add(name);
            }
        }

        public void Clear()
        {
            NameTextBox.Text = "";
            PlaceTextBox.Text = "";
            MemberNameTextBox.Text = "";
            Members.Clear();
        }

        private void LoadImage(string imagename)
        {
            TripImage.Source = new BitmapImage(new Uri(imagename, UriKind.Absolute));
        }

        private void EditImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = ".png";
            dialog.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif"
                                + "|PNG Portable Network Graphics (*.png)|*.png"
                                + "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif"
                                + "|BMP Windows Bitmap (*.bmp)|*.bmp"
                                + "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff"
                                + "|GIF Graphics Interchange Format (*.gif)|*.gif";
            if (true == dialog.ShowDialog())
            {
                TripImageName = dialog.FileName;
                LoadImage(TripImageName);
            }
        }
    }
}
