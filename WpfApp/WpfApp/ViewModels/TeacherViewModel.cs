using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp.DAL;
using WpfApp.Repositories;

namespace WpfApp.ViewModels
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        private readonly TeacherRepository _teacherRepository = new TeacherRepository();
        private ObservableCollection<TeacherModel> _teachers;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TeacherModel> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                RaisePropertyChanged(nameof(Teachers));
            }
        }

        public TeacherViewModel()
        {
            LoadAllTeachers();
        }

        public void LoadAllTeachers()
        {
            Teachers = _teacherRepository.GetAllTeachers();
        }
    }
}
