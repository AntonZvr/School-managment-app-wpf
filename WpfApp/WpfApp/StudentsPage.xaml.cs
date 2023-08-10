using System.Windows;
using System.Windows.Controls;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : Page
    {
        public StudentsPage()
        {
            InitializeComponent();
            DataContext = new StudentViewModel();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int studentId)
            {
                StudentNewFirstNameTextBox.Text = "New First Name" + studentId;
                StudentNewLastNameTextBox.Text = "New Last Name" + studentId;
                SaveButton.Tag = studentId;
                StudentNewFirstNameTextBox.Focus();
                StudentNewLastNameTextBox.Focus();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is StudentViewModel viewModel && sender is Button button && button.Tag is int studentId)
            {
                viewModel.ChangeStudentName(studentId, StudentNewFirstNameTextBox.Text, StudentNewLastNameTextBox.Text);
                StudentNewFirstNameTextBox.Clear();
                StudentNewLastNameTextBox.Clear();
                StudentNewFirstNameTextBox.Tag = null;
                StudentNewLastNameTextBox.Tag = null;
                viewModel.LoadAllStudents();
            }
        }
    }
}
