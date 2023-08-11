using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        public CourseRepository()
        {
            _context = new AppDbContext();
        }
        public List<CourseModel> GetCourses()
        {
            return _context.Courses.ToList();
        }
    }
}
