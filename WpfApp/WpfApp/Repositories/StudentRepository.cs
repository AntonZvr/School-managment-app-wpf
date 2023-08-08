using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
