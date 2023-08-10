using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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

        private StudentModel _selectedStudent;
        public StudentModel SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
               
                GroupID = _selectedStudent?.GROUP_ID ?? 0;
                FirstName = _selectedStudent?.FIRST_NAME;
                LastName = _selectedStudent?.LAST_NAME;
            }
        }

        private int _groupID;
        public int GroupID
        {
            get { return _groupID; }
            set
            {
                _groupID = value;
                OnPropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public StudentViewModel(int groupId)
        {
            LoadStudents(groupId);

            // Initialize the command with the update action
           
        }

        public StudentViewModel() 
        {
            LoadAllStudents();
        }

        private void LoadStudents(int groupId)
        {
            Students = _studentRepository.GetStudents(groupId);
        }

        public void LoadAllStudents()
        {
            Students = _studentRepository.GetAllStudents();
        }

        

        public void ChangeStudentName(int studentId, string newFirstName, string newLastName)
        {
            _studentRepository.ChangeStudentName(studentId, newFirstName, newLastName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
