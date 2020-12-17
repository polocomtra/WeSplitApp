using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using WeSplitApp.Models;

namespace WeSplitApp.View
{
    /// <summary>
    /// Interaction logic for DetailScreen.xaml
    /// </summary>
    public partial class DetailScreen : Window
    {
        public Trip SelectedTrip { get; set; }

        public DetailScreen()
        {
            InitializeComponent();
        }

        public DetailScreen(Trip trip)
        {
            InitializeComponent();
            SelectedTrip = trip;
            Debug.WriteLine(SelectedTrip);
            TripLabel.Content = SelectedTrip.Name;
            ImagesListView.ItemsSource = SelectedTrip.Images;
            MembersListView.ItemsSource = SelectedTrip.Members;
        }

        private void AddTripImageButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.DefaultExt = ".png";
            dialog.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif"
                                + "|PNG Portable Network Graphics (*.png)|*.png"
                                + "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif"
                                + "|BMP Windows Bitmap (*.bmp)|*.bmp"
                                + "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff"
                                + "|GIF Graphics Interchange Format (*.gif)|*.gif";
            if (true == dialog.ShowDialog())
            {
                foreach (string FileName in dialog.FileNames)
                {
                    string imageName = System.IO.Path.GetFileName(FileName);
                    string imageRP = "Images/" + imageName;
                    string imagePath = AppDomain.CurrentDomain.BaseDirectory + imageRP;
                    var prefix = 0;
                    while (File.Exists(imagePath))
                    {
                        prefix += 1;
                        imageRP = "Images/i" + prefix + imageName;
                        imagePath = AppDomain.CurrentDomain.BaseDirectory + imageRP;
                    }
                    File.Copy(FileName, imagePath);
                    SelectedTrip.Images.Add(imageRP);
                }

                TripDAO.Update(SelectedTrip);
                ImagesListView.Items.Refresh();
            }
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {
            Member mem = new Member();
            mem.Name = MemberNameTextBox.Text;
            mem.Expenses = new List<TripExpense>();
            MemberNameTextBox.Text = "";
            if (!string.IsNullOrEmpty(mem.Name) && !SelectedTrip.Members.Exists(x => x.Name.Equals(mem.Name)))
            {
                SelectedTrip.Members.Add(mem);
            }

            TripDAO.Update(SelectedTrip);
            MembersListView.Items.Refresh();
        }

        private void Member_Click(object sender, MouseButtonEventArgs e)
        {
            if (MembersListView.SelectedIndex >=0 ) {
                ExpensesListView.ItemsSource = SelectedTrip.Members[MembersListView.SelectedIndex].Expenses;
                AddExpense.Visibility = Visibility.Visible;
            }
        }

        private void AddExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            if (MembersListView.SelectedIndex >= 0)
            {
                Debug.WriteLine(SelectedTrip.Members[MembersListView.SelectedIndex].ToString());
                TripExpense expense = new TripExpense();
                expense.Description = ExpenseDescriptionTextBox.Text;
                string ct = ExpenseCostTextBox.Text.Replace(" ", string.Empty);
                double cost = 0;
                if (double.TryParse(ct, out cost) && cost > 0)
                {
                    expense.Cost = cost;
                    SelectedTrip.Members[MembersListView.SelectedIndex].Expenses.Add(expense);
                    ExpenseDescriptionTextBox.Text = "";
                    ExpenseCostTextBox.Text = "";
                    TripDAO.Update(SelectedTrip);
                    ExpensesListView.Items.Refresh();
                }
                else
                {
                    ShowErrorDialog("Chi phí là một số lớn hơn không    ");
                }
            }
        }
        private void ShowErrorDialog(string error)
        {
            MessageBox.Show(error, "Thông tin không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (e.Delta < 0)
            {
                scrollViewer.LineRight();
            }
            else
            {
                scrollViewer.LineLeft();
            }
            e.Handled = true;
        }
    }
}
