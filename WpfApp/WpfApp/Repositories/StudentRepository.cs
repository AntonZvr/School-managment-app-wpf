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

        public GroupModel FindGroupById(int groupId)
        {
            return _context.Groups.FirstOrDefault(c => c.GROUP_ID == groupId);
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

        public void DeleteGroup(int studentId)
        {
            StudentModel student = _context.Students.FirstOrDefault(s => s.STUDENT_ID == studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        public void CreateStudent(string studentFirstName, string studentLastName, int groupId)
        {
            StudentModel newStudent = new StudentModel
            {
                GROUP_ID = groupId,
                FIRST_NAME = studentFirstName,
                LAST_NAME = studentLastName
            };

            _context.Students.Add(newStudent);
            _context.SaveChanges();
        }
    }
}
