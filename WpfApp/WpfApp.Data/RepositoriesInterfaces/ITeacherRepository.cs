using System.Collections.ObjectModel;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public interface ITeacherRepository
    {
        ObservableCollection<TeacherModel> GetAllTeachers();
        GroupModel FindGroupById(int groupId);
        bool ChangeTeacherGroup(int teacherId, int groupId);
        void ChangeTeacherName(int teacherId, string newFirstName, string newLastName);
        void CreateTeacher(string teacherFirstName, string teacherLastName, int groupId);
        bool DeleteTeacher(int teacherId);
    }
}
