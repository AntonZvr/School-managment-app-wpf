using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;
        private IFileSystem _fileSystem;
        public CourseRepository() : this(new AppDbContext(), new FileSystem()) { }
        public CourseRepository(AppDbContext dbContext, IFileSystem fileSystem)
        {
            _context = dbContext;
            _fileSystem = fileSystem;
        }
        public List<CourseModel> GetCourses()
        {
            return _context.Courses.ToList();
        }
    }
}
