using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    }
}
