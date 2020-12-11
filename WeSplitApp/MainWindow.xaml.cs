using System;
using System.Collections.Generic;
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
using WeSplitApp.Models;
using WeSplitApp.View;
using WeSplitApp.View.AddScreen;

namespace WeSplitApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var list = TripDAO.GetAll();
            list.ForEach(e => Debug.WriteLine(e));
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            switch (index)
            {
                case 0:
                    if (MainGrid == null)
                    {
                        return;
                    }
                    //HomeScreen
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new HomeScreen());
                    break;
                case 1:
                    MainGrid.Children.Clear();
                    //SearchScreen
                    //MainGrid.Children.Add(new DetailScreen());
                    break;
                case 2:
                    MainGrid.Children.Clear();
                    //AddScreen
                    MainGrid.Children.Add(new AddScreen());
                    break;
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
