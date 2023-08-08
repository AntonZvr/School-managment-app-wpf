using System.Windows;
using System.Windows.Controls;
using WpfApp.ViewModels;

namespace WpfApp
{
    public partial class MainWindow : Page
    {
        private CourseViewModel _courseViewModel;
        private GroupViewModel _groupViewModel;
        private StudentViewModel _studentViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _courseViewModel = new CourseViewModel();
            CourseListView.DataContext = _courseViewModel;

            _courseViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "SelectedCourse")
                {
                    _groupViewModel = new GroupViewModel(_courseViewModel.SelectedCourse.COURSE_ID);
                    GroupListView.DataContext = _groupViewModel;

                    _groupViewModel.PropertyChanged += (sender, args) =>
                    {
                        if (args.PropertyName == "SelectedGroup")
                        {
                            _studentViewModel = new StudentViewModel(_groupViewModel.SelectedGroup.GROUP_ID);
                            StudentListView.DataContext = _studentViewModel;
                        }
                    };
                }
            };
        }
    }
}
