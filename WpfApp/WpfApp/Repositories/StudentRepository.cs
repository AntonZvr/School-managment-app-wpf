using System.Collections.ObjectModel;
using System.Linq;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public class StudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository()
        {
            _context = new AppDbContext();
        }

        public ObservableCollection<StudentModel> GetStudents(int groupId)
        {
            return new ObservableCollection<StudentModel>(_context.Students.Where(s => s.GROUP_ID == groupId).ToList());
        }

        public ObservableCollection<StudentModel> GetAllStudents()
        {
            return new ObservableCollection<StudentModel>(_context.Students.ToList());
        }

       

        public void ChangeStudentName(int studentId, string newFirstName, string newLastname)
        {
            StudentModel student = _context.Students.FirstOrDefault(s => s.STUDENT_ID == studentId);
            if (student != null)
            {
                student.FIRST_NAME = newFirstName;
                student.LAST_NAME = newLastname;             
                _context.SaveChanges();
            }
        }
    }
}
