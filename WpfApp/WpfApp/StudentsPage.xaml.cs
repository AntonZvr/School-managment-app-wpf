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

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is StudentViewModel viewModel)
            {
                int groupId = 0; // Default value for groupId
                string studentFirstName = "Default First Name"; // Default value
                string studentLastName = "Default Last Name";

                if (!string.IsNullOrEmpty(GroupIdTextBox.Text))
                {
                    groupId = int.Parse(GroupIdTextBox.Text);
                }

                if (!string.IsNullOrEmpty(StudentFirstNameTextBox.Text))
                {
                    studentFirstName = StudentFirstNameTextBox.Text;
                }

                if (!string.IsNullOrEmpty(StudentLastNameTextBox.Text))
                {
                    studentLastName = StudentLastNameTextBox.Text;
                }

                if (!viewModel.TryAddStudent(groupId, studentFirstName, studentLastName))
                {
                    MessageBox.Show("Group not found");
                }
                else
                {
                    viewModel.LoadAllStudents();
                    GroupIdTextBox.Clear();
                    StudentFirstNameTextBox.Clear();
                    StudentLastNameTextBox.Clear();
                }
            }
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int studentId)
            {
                if (DataContext is StudentViewModel viewModel)
                {
                   viewModel.DeleteGroup(studentId); 
                   viewModel.LoadAllStudents();                   
                }
            }
        }
    }
}
