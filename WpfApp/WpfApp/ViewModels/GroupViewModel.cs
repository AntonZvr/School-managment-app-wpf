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
using WpfApp.Repositories;

namespace WpfApp.ViewModels
{
    public class GroupViewModel : INotifyPropertyChanged
    {
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

        public GroupViewModel()
        {
            LoadAllGroups();
            TeacherRepository = new TeacherRepository();
            LoadAllTeachers();
        }

        public GroupViewModel(int courseId)
        {
            LoadGroups(courseId);
            TeacherRepository = new TeacherRepository();
        }

        private void LoadGroups(int courseId)
        {
            Groups = _groupRepository.GetGroups(courseId);
        }

        public void LoadAllGroups() 
        {
            Groups = _groupRepository.GetAllGroups();
        }

        public event Action GroupAdded = delegate { };

        public bool TryAddGroup(int courseId, string groupName)
        {
            CourseModel course = _groupRepository.FindCourseById(courseId);
            if (course == null)
            {
                return false;
            }
            _groupRepository.CreateGroup(groupName, course.COURSE_ID);

            // Notify UI
            GroupAdded.Invoke();

            return true;

        }

        public void ChangeGroupName(int groupId, string newGroupName)
        {
            _groupRepository.ChangeGroupName(groupId, newGroupName);
        }

        public bool DeleteGroup(int groupId)
        {
            bool hasStudents = _groupRepository.HasStudents(groupId);
            if (hasStudents)
            {
                return false;
            }
            else
            {
                _groupRepository.DeleteGroup(groupId);
                return true;
            }
        }

        private ObservableCollection<TeacherModel> _teachers;
        public ObservableCollection<TeacherModel> Teachers
        {
            get { return _teachers; }
            set
            {
                _teachers = value;
                OnPropertyChanged();
            }
        }

        public void LoadAllTeachers()
        {
            Teachers = TeacherRepository.GetAllTeachers();
        }

        public TeacherRepository TeacherRepository { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ExportGroup(int groupId, string filePath)
        {
            _groupRepository.ExportStudents(groupId, filePath);
        }

        public void ImportGroup(int groupId, string filePath)
        {
            _groupRepository.ImportStudents(groupId, filePath);
        }

    }

}
