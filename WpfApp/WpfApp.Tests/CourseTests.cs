using Microsoft.EntityFrameworkCore;
using System.IO.Abstractions;
using WpfApp.DAL;
using WpfApp.Repositories;

namespace WpfApp
{
    [TestClass]
    public class CourseRepositoryTest
    {
        private CourseRepository repository;
        private AppDbContext context;
        private IFileSystem fileSystem;

        [TestInitialize]
        public void TestInitialize()
        {
            // Set up InMemory Database for each test
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new AppDbContext(options);
            context.Database.EnsureCreated();

            repository = new CourseRepository(context, fileSystem);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            context.Database.EnsureDeleted(); // Clean up InMemory Db after each test 
        }

        [TestMethod]
        public void GetCoursesTest()
        {
            // Arrange
            var course1 = new CourseModel { NAME = "Intro to C#", DESCRIPTION = "desc1" };
            var course2 = new CourseModel { NAME = "Advanced Java", DESCRIPTION = "desc2" };

            context.Courses.AddRange(course1, course2);
            context.SaveChanges();

            // Act
            var courses = repository.GetCourses();

            // Assert
            Assert.AreEqual(2, courses.Count);
            CollectionAssert.Contains(courses, course1);
            CollectionAssert.Contains(courses, course2);
        }
    }
}