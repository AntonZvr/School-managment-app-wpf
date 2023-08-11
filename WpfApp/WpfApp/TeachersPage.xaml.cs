using System;
using System.Collections.Generic;
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
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : Page
    {
        public TeachersPage()
        {
            InitializeComponent();
            DataContext = new TeacherViewModel();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is TeacherViewModel viewModel)
            {
                int groupId = 0; // Default value for groupId
                string studentFirstName = "Default First Name"; // Default value
                string studentLastName = "Default Last Name";

                if (!string.IsNullOrEmpty(GroupIdTextBox.Text))
                {
                    groupId = int.Parse(GroupIdTextBox.Text);
                }

                if (!string.IsNullOrEmpty(TeacherFirstNameTextBox.Text))
                {
                    studentFirstName = TeacherFirstNameTextBox.Text;
                }

                if (!string.IsNullOrEmpty(TeacherLastNameTextBox.Text))
                {
                    studentLastName = TeacherLastNameTextBox.Text;
                }

                if (!viewModel.TryAddTeacher(groupId, studentFirstName, studentLastName))
                {
                    MessageBox.Show("Group not found");
                }
                else
                {
                    viewModel.LoadAllTeachers();
                    GroupIdTextBox.Clear();
                    TeacherFirstNameTextBox.Clear();
                    TeacherLastNameTextBox.Clear();
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int teacherId)
            {
                TeacherNewFirstNameTextBox.Text = "New First Name" + teacherId;
                TeacherNewLastNameTextBox.Text = "New Last Name" + teacherId;
                SaveButton.Tag = teacherId;
                TeacherNewFirstNameTextBox.Focus();
                TeacherNewLastNameTextBox.Focus();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is TeacherViewModel viewModel && sender is Button button && button.Tag is int teacherId)
            {
                viewModel.ChangeTeacherName(teacherId, TeacherNewFirstNameTextBox.Text, TeacherNewLastNameTextBox.Text);
                TeacherNewFirstNameTextBox.Clear();
                TeacherNewLastNameTextBox.Clear();
                TeacherNewFirstNameTextBox.Tag = null;
                TeacherNewLastNameTextBox.Tag = null;
                viewModel.LoadAllTeachers();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int teacherId)
            {
                if (DataContext is TeacherViewModel viewModel)
                {
                    try
                    {
                        viewModel.DeleteTeacher(teacherId);
                        viewModel.LoadAllTeachers();
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Show the message of the exception to the user.
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
