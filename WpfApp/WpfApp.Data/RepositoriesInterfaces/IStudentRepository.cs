using System.Collections.ObjectModel;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public interface IStudentRepository
    {
        ObservableCollection<StudentModel> GetStudents(int groupId);
        ObservableCollection<StudentModel> GetAllStudents();
        GroupModel FindGroupById(int groupId);
        void ChangeStudentName(int studentId, string newFirstName, string newLastName);
        void DeleteGroup(int studentId);
        void CreateStudent(string studentFirstName, string studentLastName, int groupId);
    }
}
