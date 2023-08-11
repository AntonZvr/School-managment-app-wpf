using System;
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

        public void ChangeTeacherName(int teacherId, string newFirstName, string newLastName)
        {
            _teacherRepository.ChangeTeacherName(teacherId, newFirstName, newLastName);
        }

        public event Action TeacherAdded = delegate { };

        public bool TryAddTeacher(int groupId, string teacherFirstName, string teacherLastName)
        {
            GroupModel group = _teacherRepository.FindGroupById(groupId);
            if (group == null)
            {
                return false;
            }
            _teacherRepository.CreateTeacher(teacherFirstName, teacherLastName, group.GROUP_ID);

            // Notify UI
            TeacherAdded.Invoke();

            return true;

        }

        public void DeleteTeacher(int teacherId)
        {
            _teacherRepository.DeleteTeacher(teacherId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
