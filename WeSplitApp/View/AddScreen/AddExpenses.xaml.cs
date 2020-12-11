using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WeSplitApp.View.AddScreen
{
    /// <summary>
    /// Interaction logic for AddExpenses.xaml
    /// </summary>
    public partial class AddExpenses : UserControl
    {
        public class MbExpense
        {
            public string MemberName { get; set; }
            public string Description { get; set; }
            public double Cost { get; set; }
        }

        public BindingList<string> MemberNames { get; set; } = new BindingList<string>();
        public BindingList<MbExpense> Expenses { get; set; } = new BindingList<MbExpense>();

        public AddExpenses()
        {
            InitializeComponent();
        }

        private void ExpenseAddButton_Click(object sender, RoutedEventArgs e)
        {
            string memberName = MemberComboBox.SelectedItem as string;
            string description = ExpenseDesTextBox.Text;
            //ExpenseDesTextBox.Text
            if (string.IsNullOrEmpty(memberName))
            {
                ShowErrorDialog("Chọn thành viên thực hiện khoản chi");
            }
            else if (string.IsNullOrEmpty(description))
            {
                ShowErrorDialog("Điền thông tin khoản chi           ");
            }
            else
            {
                string ct = ExpenseCostTextBox.Text.Replace(" ", string.Empty);
                double cost = 0;
                if (double.TryParse(ct, out cost) && cost > 0)
                {
                    MbExpense expense = new MbExpense()
                    {
                        MemberName = memberName,
                        Description = description,
                        Cost = cost
                    };
                    Expenses.Add(expense);
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

        private void ExpenseRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                MbExpense expense = button.DataContext as MbExpense;
                Expenses.Remove(expense);
            }
        }

        public void Clear()
        {
            ExpenseDesTextBox.Text = "";
            ExpenseCostTextBox.Text = "";
            MemberNames.Clear();
            Expenses.Clear();
        }
    }
}
