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
    public class StudentViewModel : INotifyPropertyChanged
    {
        private readonly StudentRepository _studentRepository = new StudentRepository();

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

        public StudentViewModel(int groupId)
        {
            LoadStudents(groupId);
        }

        private void LoadStudents(int groupId)
        {
            Students = _studentRepository.GetStudents(groupId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
