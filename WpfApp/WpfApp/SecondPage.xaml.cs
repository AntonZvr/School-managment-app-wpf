using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Repositories;
using WpfApp.ViewModels;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for SecondPage.xaml
    /// </summary>
    public partial class SecondPage : Page
    {
        public SecondPage()
        {
            InitializeComponent();
            this.DataContext = new GroupViewModel();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is GroupViewModel viewModel)
            {
                int courseId = 0; // Default value for courseId
                string groupName = "Default name"; // Default value for groupName

                if (!string.IsNullOrEmpty(CourseIdTextBox.Text))
                {
                    courseId = int.Parse(CourseIdTextBox.Text);
                }

                if (!string.IsNullOrEmpty(GroupNameTextBox.Text))
                {
                    groupName = GroupNameTextBox.Text;
                }

                if (!viewModel.TryAddGroup(courseId, groupName))
                {
                    MessageBox.Show("Course not found");
                }
                else
                {
                    viewModel.LoadAllGroups();
                    CourseIdTextBox.Clear();
                    GroupNameTextBox.Clear();
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int groupId)
            {
                GroupNewNameTextBox.Text = "New " + groupId;
                SaveButton.Tag = groupId;
                GroupNewNameTextBox.Focus();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GroupViewModel viewModel && sender is Button button && button.Tag is int groupId)
            {
                viewModel.ChangeGroupName(groupId, GroupNewNameTextBox.Text);
                GroupNewNameTextBox.Clear();
                GroupNewNameTextBox.Tag = null;
                viewModel.LoadAllGroups();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int groupId)
            {
                if (DataContext is GroupViewModel viewModel)
                {
                    bool deleted = viewModel.DeleteGroup(groupId);
                    if (!deleted)
                    {
                        MessageBox.Show("Group has associated students, cannot be deleted.");
                    }
                    else 
                    {
                        viewModel.LoadAllGroups();                   
                    }
                }
            }
        }

        private void EditTeacherGroup_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is int teacherId)
            {
                // Get the parent StackPanel
                var stackPanel = (StackPanel)button.Parent;

                // Find the TextBox in the StackPanel
                var textBox = (TextBox)stackPanel.Children.OfType<TextBox>().First();

                // Get the new group ID from the TextBox
                int groupId = 0; // Default value for groupId
                int.TryParse(textBox.Text, out groupId);

                if (DataContext is GroupViewModel viewModel)
                {
                    try
                    {
                        if (viewModel.TeacherRepository.ChangeTeacherGroup(teacherId, groupId))
                        {
                            MessageBox.Show("Changed!");
                            textBox.Clear();
                            viewModel.LoadAllTeachers();
                        }
                        else
                        {
                            MessageBox.Show("No group with this group id!");
                            textBox.Clear();
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Show the message of the exception to the user. This will be 
                        // "Cannot change the group of a teacher who is the only one in their group."
                        MessageBox.Show(ex.Message);
                        textBox.Clear();
                    }
                }
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GroupViewModel viewModel)
            {
                if (sender is Button button && button.Tag is int groupId)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filePath = saveFileDialog.FileName;

                        viewModel.ExportGroup(groupId, filePath);
                    }
                }
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GroupViewModel viewModel)
            {
                if (sender is Button button && button.Tag is int groupId)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        string filePath = openFileDialog.FileName;

                        try
                        {
                            viewModel.ImportGroup(groupId, filePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred while importing students: {ex.Message}");
                        }
                    }
                }
            }
        }

        private void ExportGroupDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GroupViewModel viewModel)
            {
                if (sender is Button button && button.Tag is int groupId)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "DOCX files (*.docx)|*.docx";                

                    var group = viewModel.FindGroupById(groupId);
                    var course = viewModel.GetCourseById(group.COURSE_ID);           

                    string defaultFileName = course.NAME + "_" + group.NAME + ".docx";
                    saveFileDialog.FileName = defaultFileName;

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filePath = saveFileDialog.FileName;

                        viewModel.ExportGroupDetailsToDocx(groupId, filePath);
                    }
                }
            }
        }

        private void ExportPDFGroupDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is GroupViewModel viewModel)
            {
                if (sender is Button button && button.Tag is int groupId)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

                    var group = viewModel.FindGroupById(groupId);
                    var course = viewModel.GetCourseById(group.COURSE_ID);

                    string defaultFileName = course.NAME + "_" + group.NAME + ".pdf";
                    saveFileDialog.FileName = defaultFileName;

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filePath = saveFileDialog.FileName;

                        viewModel.ExportGroupDetailsToPdf(groupId, filePath);
                    }
                }
            }
        }
    }
}
