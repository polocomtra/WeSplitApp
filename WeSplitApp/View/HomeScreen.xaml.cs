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

namespace WeSplitApp.View
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : UserControl
    {
        List<Trip> data = new List<Trip>();
        List<Trip> completedJourneyObj = new List<Trip>();
        List<Trip> currentJourneyObj = new List<Trip>();
        public HomeScreen()
        {
            InitializeComponent();
            data = TripDAO.GetAll();
            Debug.WriteLine(data);
            foreach (var i in data)
            {

                if (i.Status == 0)
                {
                    completedJourneyObj.Add(i);
                }else if (i.Status == 1)
                {
                    currentJourneyObj.Add(i);
                }
            }
            allJourney.ItemsSource = data;
            completedJourney.ItemsSource = completedJourneyObj;
            currentJourney.ItemsSource = currentJourneyObj;
            
        }
        
    }
}
