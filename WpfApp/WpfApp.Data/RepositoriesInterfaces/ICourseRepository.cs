using System.Collections.Generic;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public interface ICourseRepository
    {
        List<CourseModel> GetCourses();
    }
}
