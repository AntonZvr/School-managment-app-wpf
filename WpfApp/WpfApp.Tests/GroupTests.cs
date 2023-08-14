using Microsoft.EntityFrameworkCore;
using WpfApp.DAL;
using WpfApp.Repositories;
using System.IO.Abstractions;
using Moq;
using System.Text;

namespace WpfApp
{
    [TestClass]
    public class GroupRepositoryTest
    {
        private GroupRepository repository;
        private AppDbContext context;
        private IFileSystem fileSystem;

        [TestInitialize]
        public void TestInitialize()
        {
            // Use InMemory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new AppDbContext(options);
            context.Database.EnsureCreated();

            repository = new GroupRepository(context, fileSystem);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllGroupsTest()
        {
            // Arrange
            var group1 = new GroupModel { NAME = "Test Group 1", COURSE_ID = 1 };
            var group2 = new GroupModel { NAME = "Test Group 2", COURSE_ID = 2 };

            context.Groups.Add(group1);
            context.Groups.Add(group2);
            context.SaveChanges();

            // Act
            var result = repository.GetAllGroups();

            // Assert
            Assert.AreEqual(2, result.Count);

            GroupModel resultGroup1 = result[0];
            GroupModel resultGroup2 = result[1];

            Assert.AreEqual("Test Group 1", resultGroup1.NAME);
            Assert.AreEqual(1, resultGroup1.COURSE_ID);
            Assert.AreEqual("Test Group 2", resultGroup2.NAME);
            Assert.AreEqual(2, resultGroup2.COURSE_ID);
        }

        [TestMethod]
        public void CreateGroupTest()
        {
            // Arrange
            int initialCount = context.Groups.Count();

            // Act
            repository.CreateGroup("Test group", 5);

            // Assert
            int finalCount = context.Groups.Count();
            Assert.AreEqual(initialCount + 1, finalCount);

            var addedGroup = context.Groups.Last();
            Assert.AreEqual("Test group", addedGroup.NAME);
            Assert.AreEqual(5, addedGroup.COURSE_ID);
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
        public void ChangeGroupNameTest()
        {
            // Arrange
            var group1 = new GroupModel { NAME = "Test Group 1", COURSE_ID = 1 };
            context.Groups.Add(group1);
            context.SaveChanges();

            // Act
            repository.ChangeGroupName(group1.GROUP_ID, "Changed Group Name");
            var result = repository.FindGroupById(group1.GROUP_ID);

            // Assert
            Assert.AreEqual("Changed Group Name", result.NAME);
        }

        [TestMethod]
        public void HasStudentsTest()
        {
            // Arrange
            var group1 = new GroupModel { NAME = "Test Group 1", COURSE_ID = 1 };
            var student1 = new StudentModel { GROUP_ID = 0, FIRST_NAME = "Test", LAST_NAME = "Student" };
            context.Groups.Add(group1);
            context.Students.Add(student1);
            context.SaveChanges();

            // Act
            var result = repository.HasStudents(0);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteGroupTest()
        {
            // Arrange
            var group1 = new GroupModel { NAME = "Test Group 1", COURSE_ID = 1 };
            context.Groups.Add(group1);
            context.SaveChanges();

            // Act
            repository.DeleteGroup(group1.GROUP_ID);
            var result = repository.FindGroupById(group1.GROUP_ID);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void FindCourseByIdTest()
        {
            // Arrange
            var course1 = new CourseModel { NAME = "Test Course", DESCRIPTION = "Desc" };
            context.Courses.Add(course1);
            context.SaveChanges();

            // Act
            var result = repository.FindCourseById(course1.COURSE_ID);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Course", result.NAME);
        }

    }
}