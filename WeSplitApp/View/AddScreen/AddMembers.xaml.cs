using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WeSplitApp.View.AddScreen
{
    /// <summary>
    /// Interaction logic for AddMembers.xaml
    /// </summary>
    public partial class AddMembers : UserControl
    {
        public BindingList<string> Members { get; set; } = new BindingList<string>();
        public AddMembers()
        {
            InitializeComponent();
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
    }
}
