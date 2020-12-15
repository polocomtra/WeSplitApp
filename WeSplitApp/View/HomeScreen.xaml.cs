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
using WeSplitApp.Converter;
using WeSplitApp.Models;

namespace WeSplitApp.View
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        List<Trip> data = new List<Trip>();
        BindingList<Trip> completedJourneyObj = new BindingList<Trip>();
        BindingList<Trip> currentJourneyObj = new BindingList<Trip>();
        private List<string> Tags = new List<string>()
            {"Tất cả", "Tên Chuyến Đi", "Địa Điểm", "Tên Thành Viên" };

        public HomeScreen()
        {
            InitializeComponent();

            SearchComboBox.ItemsSource = Tags;
            completedJourney.ItemsSource = completedJourneyObj;
            currentJourney.ItemsSource = currentJourneyObj;

            data = TripDAO.GetAll();
            Debug.WriteLine(data);
            ReloadData();
        }

        private void ReloadData()
        {
            completedJourneyObj.Clear();
            currentJourneyObj.Clear();
            foreach (var i in data)
            {

                if (i.Status == 0)
                {
                    completedJourneyObj.Add(i);
                }
                else if (i.Status == 1)
                {
                    currentJourneyObj.Add(i);
                }
            }

            allJourney.ItemsSource = data;
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Search();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string key = VNCharacterUtils.RemoveAccent(SearchTextBox.Text).ToLower();
            Debug.WriteLine(key);
            int sb = SearchComboBox.SelectedIndex;
            Debug.WriteLine($"{sb} {key}");
            SearchBy searchBy = (sb == 0) ? SearchAll
                                    : (sb == 1) ? SearchByName
                                        : (sb == 2) ? SearchByPlace : (SearchBy)SearchByMember;
            var all = TripDAO.GetAll();
            data = all.Where(e => searchBy(e, key)).ToList();
            ReloadData();
        }

        private delegate bool SearchBy(Trip trip, string key);

        private bool SearchAll(Trip trip, string key)
        {
            var result = SearchByName(trip, key) || SearchByPlace(trip, key) || SearchByMember(trip, key);
            return result;
        }

        private bool SearchByName(Trip trip, string key)
        {
            var result = StringContaints(trip.Name, key);
            return result;
        }

        private bool SearchByPlace(Trip trip, string key)
        {
            var result = StringContaints(trip.Place, key);
            return result;
        }

        private bool SearchByMember(Trip trip, string key)
        {
            var result = trip.Members.Any(e => StringContaints(e.Name, key));
            return result;
        }

        private bool StringContaints(string text, string key)
        {
            var result = VNCharacterUtils.RemoveAccent(text).ToLower().Contains(key);
            return result;
        }

    }
}
