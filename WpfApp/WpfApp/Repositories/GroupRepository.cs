using CsvHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public class GroupRepository
    {
        private readonly AppDbContext _context;
        public GroupRepository()
        {
            _context = new AppDbContext();
        }
        public ObservableCollection<GroupModel> GetGroups(int courseId)
        {
            return new ObservableCollection<GroupModel>(_context.Groups.Where(g => g.COURSE_ID == courseId).ToList());
        }

        public ObservableCollection<GroupModel> GetAllGroups() 
        {
            return new ObservableCollection<GroupModel>(_context.Groups.ToList());
        }

        public void CreateGroup(string groupName, int courseId)
        {          
            GroupModel newGroup = new GroupModel
            {
                NAME = groupName, 
                COURSE_ID = courseId            
            };

            _context.Groups.Add(newGroup);   
            _context.SaveChanges();
        }

        public void ChangeGroupName(int groupId, string newGroupName)
        {
            GroupModel group = _context.Groups.FirstOrDefault(g => g.GROUP_ID == groupId);
            if (group != null)
            {
                group.NAME = newGroupName;
                _context.SaveChanges();
            }
        }

        public bool HasStudents(int groupId)
        {
            return _context.Students.Any(s => s.GROUP_ID == groupId);
        }

        public void DeleteGroup(int groupId)
        {
            GroupModel group = _context.Groups.FirstOrDefault(g => g.GROUP_ID == groupId);
            if (group != null)
            {
                _context.Groups.Remove(group);
                _context.SaveChanges();
            }
        }

        public CourseModel FindCourseById(int courseId)
        {
            return _context.Courses.FirstOrDefault(c => c.COURSE_ID == courseId);
        }

        public void ExportStudents(int groupId, string filePath)
        {
            var students = _context.Students.Where(s => s.GROUP_ID == groupId).ToList();

            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(students.Select(s => new { s.STUDENT_ID, s.GROUP_ID, s.FIRST_NAME, s.LAST_NAME }));
            }
        }

        public void ImportStudents(int groupId, string filePath)
        {
            // Clear the group first
            var studentsInGroup = _context.Students.Where(s => s.GROUP_ID == groupId);
            _context.Students.RemoveRange(studentsInGroup);

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
                foreach (var record in records)
                {
                    var student = new StudentModel
                    {                      
                        GROUP_ID = groupId,
                        FIRST_NAME = record.FIRST_NAME,
                        LAST_NAME = record.LAST_NAME                      
                    };
                    _context.Students.Add(student);
                    _context.SaveChanges();
                }
            }
            _context.SaveChanges();
        }


    }
}
