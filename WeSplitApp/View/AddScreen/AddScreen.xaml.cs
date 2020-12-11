using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WeSplitApp.Models;

namespace WeSplitApp.View.AddScreen
{
    /// <summary>
    /// Interaction logic for AddScreen.xaml
    /// </summary>
    public partial class AddScreen : UserControl
    {
        public AddScreen()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = AddMembers.NameTextBox.Text;
            string place = AddMembers.PlaceTextBox.Text;
            if (string.IsNullOrEmpty(name))
            {
                ShowErrorDialog("Điền tên chuyến đi                 ");
            }
            else if (string.IsNullOrEmpty(place))
            {
                ShowErrorDialog("Điền địa điểm chuyến đi            ");
            }
            else
            {
                Trip trip = new Trip()
                {
                    Name = name,
                    Place = place,
                };

                var members = AddMembers.Members.Select(n => new Member() { Name = n, Expenses = new List<TripExpense>() }).ToList();
                var mbExpenseList = AddExpenses.Expenses.GroupBy(mbe => mbe.MemberName);
                foreach (var mbGroup in mbExpenseList)
                {
                    var member = members.Find(m => m.Name.Equals(mbGroup.Key));
                    if (member != null)
                    {
                        member.Expenses = mbGroup.Select(mbe => new TripExpense() { Description = mbe.Description, Cost = mbe.Cost }).ToList();
                    }
                }
                trip.Members = members;
                if (TripDAO.Insert(trip))
                {
                    Clear();
                    MessageBox.Show($"Đã thêm chuyến đi {trip.Name}", "Thêm Chuyến Đi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Đã xảy ra lỗi không rõ!", "Thêm Chuyến Đi", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void Clear()
        {
            AddMembers.Clear();
            AddExpenses.Clear();
            HideUserControl(AddExpenses);
            ShowUserControl(AddMembers);
        }

        private void ShowErrorDialog(string error)
        {
            MessageBox.Show(error, "Thông tin không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void CancleButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //step = 0;
            BackButton.IsEnabled = false;
            NextButton.IsEnabled = true;
            HideUserControl(AddExpenses);
            ShowUserControl(AddMembers);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            //step = 1;
            BackButton.IsEnabled = true;
            NextButton.IsEnabled = false;
            ShowUserControl(AddExpenses);
            HideUserControl(AddMembers);

            AddExpenses.MemberNames.Clear();
            foreach (string name in AddMembers.Members)
            {
                AddExpenses.MemberNames.Add(name);
            }

            var expenseRemoveList = AddExpenses.Expenses.Where(exp => AddMembers.Members.IndexOf(exp.MemberName) < 0).ToList();
            expenseRemoveList.ForEach(expense => AddExpenses.Expenses.Remove(expense));

        }

        private void ShowUserControl(UserControl control)
        {
            control.Visibility = Visibility.Visible;
        }

        private void HideUserControl(UserControl control)
        {
            control.Visibility = Visibility.Collapsed;
        }
    }
}
