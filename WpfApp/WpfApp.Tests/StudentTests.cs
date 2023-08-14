using Microsoft.EntityFrameworkCore;
using System.IO.Abstractions;
using WpfApp.DAL;
using WpfApp.Repositories;

namespace WpfApp
{

    [TestClass]
    public class StudentRepositoryTest
    {
        private StudentRepository repository;
        private AppDbContext context;
        private IFileSystem fileSystem;

        [TestInitialize]
        public void TestInitialize()
        {
            // Using InMemory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new AppDbContext(options);
            context.Database.EnsureCreated();

            repository = new StudentRepository(context, fileSystem);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllStudentsTest()
        {
            // Arrange
            var student1 = new StudentModel { FIRST_NAME = "John", LAST_NAME = "Doe" };
            var student2 = new StudentModel { FIRST_NAME = "Jane", LAST_NAME = "Doe" };
            context.Students.Add(student1);
            context.Students.Add(student2);
            context.SaveChanges();

            // Act
            var result = repository.GetAllStudents();

            // Assert
            Assert.AreEqual(2, result.Count);

            var resultStudent1 = result[0];
            var resultStudent2 = result[1];

            Assert.AreEqual("John", resultStudent1.FIRST_NAME);
            Assert.AreEqual("Doe", resultStudent1.LAST_NAME);
            Assert.AreEqual("Jane", resultStudent2.FIRST_NAME);
            Assert.AreEqual("Doe", resultStudent2.LAST_NAME);
        }

        [TestMethod]
        public void GetStudentsTest()
        {
            // Arrange
            var student1 = new StudentModel { GROUP_ID = 1, FIRST_NAME = "John", LAST_NAME = "Doe" };
            var student2 = new StudentModel { GROUP_ID = 2, FIRST_NAME = "Jane", LAST_NAME = "Doe" };
            context.Students.Add(student1);
            context.Students.Add(student2);
            context.SaveChanges();

            // Act
            var result = repository.GetStudents(1);

            // Assert
            Assert.AreEqual(1, result.Count);
            var resultStudent1 = result[0];
            Assert.AreEqual("John", resultStudent1.FIRST_NAME);
            Assert.AreEqual("Doe", resultStudent1.LAST_NAME);
            Assert.AreEqual(1, resultStudent1.GROUP_ID);
        }

        [TestMethod]
        public void FindGroupByIdTest()
        {
            // Arrange
            var group1 = new GroupModel { NAME = "Test Group 1", COURSE_ID = 1 };
            context.Groups.Add(group1);
            context.SaveChanges();

            // Act
            var result = repository.FindGroupById(group1.GROUP_ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Group 1", result.NAME);
        }

        [TestMethod]
        public void ChangeStudentNameTest()
        {
            // Arrange
            var student1 = new StudentModel { FIRST_NAME = "John", LAST_NAME = "Doe" };
            context.Students.Add(student1);
            context.SaveChanges();

            // Act
            repository.ChangeStudentName(student1.STUDENT_ID, "Jane", "Smith");
            var result = context.Students.Where(s => s.STUDENT_ID == student1.STUDENT_ID).FirstOrDefault();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Jane", result.FIRST_NAME);
            Assert.AreEqual("Smith", result.LAST_NAME);
        }

        [TestMethod]
        public void DeleteGroupTest()
        {
            // Arrange
            var student1 = new StudentModel { FIRST_NAME = "John", LAST_NAME = "Doe" };
            context.Students.Add(student1);
            context.SaveChanges();

            // Act
            repository.DeleteGroup(student1.STUDENT_ID);
            var result = context.Students.Where(s => s.STUDENT_ID == student1.STUDENT_ID).FirstOrDefault();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateStudentTest()
        {
            // Arrange
            var student1 = new StudentModel { FIRST_NAME = "John", LAST_NAME = "Doe", GROUP_ID = 1 };

            // Act
            repository.CreateStudent(student1.FIRST_NAME, student1.LAST_NAME, student1.GROUP_ID);
            var result = context.Students.Where(s => s.FIRST_NAME == "John" && s.LAST_NAME == "Doe").FirstOrDefault();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(student1.FIRST_NAME, result.FIRST_NAME);
            Assert.AreEqual(student1.LAST_NAME, result.LAST_NAME);
            Assert.AreEqual(student1.GROUP_ID, result.GROUP_ID);
        }

    }
}