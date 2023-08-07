using System;
using System.Collections.Generic;
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

        public CourseViewModel()
        {
            LoadCourses();
        }

        private void LoadCourses()
        {
            Courses = _context.Courses.ToList();
            Console.WriteLine(Courses);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
