using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.DAL;

namespace WpfApp.ViewModels
{
    public class CourseViewModel : INotifyPropertyChanged
    {
        private readonly AppDbContext _context = new AppDbContext();

        private List<CourseModel> _courses;
        public List<CourseModel> Courses
        {
            get { return _courses; }
            set
            {
                _courses = value;
                OnPropertyChanged();
            }
        }

        private CourseModel _selectedCourse;
        public CourseModel SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                _selectedCourse = value;
                LoadGroups(_selectedCourse.COURSE_ID);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GroupModel> _groups;
        public ObservableCollection<GroupModel> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        private GroupModel _selectedGroup;
        public GroupModel SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                _selectedGroup = value;
                LoadStudents();
            }
        }

        private ObservableCollection<StudentModel> _students;
        public ObservableCollection<StudentModel> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private void LoadStudents()
        {
            if (SelectedGroup != null)
            {
                Students = new ObservableCollection<StudentModel>(_context.Students.Where(s => s.GROUP_ID == SelectedGroup.GROUP_ID).ToList());
            }
            else
            {
                Students = new ObservableCollection<StudentModel>();
            }
        }


        public CourseViewModel()
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            Courses = _context.Courses.ToList();
            Console.WriteLine(Courses);
        }

        private void LoadGroups(int courseId)
        {
            // load groups associated with the selected course
            Groups = new ObservableCollection<GroupModel>(_context.Groups.Where(g => g.COURSE_ID == courseId).ToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
