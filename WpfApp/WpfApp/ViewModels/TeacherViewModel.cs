using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DAL;
using WpfApp.Repositories;

namespace WpfApp.ViewModels
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        private readonly TeacherRepository _teacherRepository = new TeacherRepository();

        private ObservableCollection<TeacherModel> _teachers;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<TeacherModel> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged();
            }
        }

        public TeacherViewModel()
        {
            LoadAllTeachers();
        }

        private void LoadAllTeachers()
        {
            Teachers = _teacherRepository.GetAllTeachers();
        }
       
    }


}
