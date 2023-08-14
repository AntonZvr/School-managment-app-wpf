using CommunityToolkit.Mvvm.Input;
using System;
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

        private readonly GroupRepository _groupRepository = new GroupRepository();

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
                OnPropertyChanged();
            }
        }

        public void LoadAllGroups()
        {
            Groups = new ObservableCollection<GroupModel>(_groupRepository.GetAllGroups());
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
        }

        public StudentViewModel() 
        {
            LoadAllGroups();
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

        public void DeleteGroup(int studentId)
        {
            _studentRepository.DeleteGroup(studentId);
        }

        public event Action StudentAdded = delegate { };

        public bool TryAddStudent(int groupId, string studentFirstName, string studentLastName)
        {
            GroupModel group = _studentRepository.FindGroupById(groupId);
            if (group == null)
            {
                return false;
            }
            _studentRepository.CreateStudent(studentFirstName, studentLastName, group.GROUP_ID);

            // Notify UI
            StudentAdded.Invoke();

            return true;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
