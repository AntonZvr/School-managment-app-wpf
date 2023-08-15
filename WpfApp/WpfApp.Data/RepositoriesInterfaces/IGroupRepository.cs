using System.Collections.ObjectModel;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public interface IGroupRepository
    {
        ObservableCollection<GroupModel> GetGroups(int courseId);
        ObservableCollection<GroupModel> GetAllGroups();
        GroupModel FindGroupById(int groupId);
        void CreateGroup(string groupName, int courseId);
        void ChangeGroupName(int groupId, string newGroupName);
        bool HasStudents(int groupId);
        void DeleteGroup(int groupId);
        CourseModel FindCourseById(int courseId);
        void ExportStudents(int groupId, string filePath);
        void ImportStudents(int groupId, string filePath);
        void ExportGroupToDocx(string folderPath, GroupModel group, CourseModel course, List<StudentModel> students);
        void ExportGroupToPdf(string folderPath, GroupModel group, CourseModel course, List<StudentModel> students);
    }
}
