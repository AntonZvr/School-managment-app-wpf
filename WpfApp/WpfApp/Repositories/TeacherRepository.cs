using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.DAL;

namespace WpfApp.Repositories
{
    public class TeacherRepository
    {
        private readonly AppDbContext _context;
        public TeacherRepository()
        {
            _context = new AppDbContext();
        }

        public ObservableCollection<TeacherModel> GetAllTeachers()
        {
            return new ObservableCollection<TeacherModel>(_context.Teachers.ToList());
        }

        public ObservableCollection<TeacherModel> Teachers
        {
            get
            {
                return new ObservableCollection<TeacherModel>(_context.Teachers.ToList());
            }
        }

        public bool ChangeTeacherGroup(int teacherId, int groupId)
        {
            TeacherModel teacher = _context.Teachers.FirstOrDefault(t => t.Teacher_Id == teacherId);
            if (teacher != null)
            {
                // Count how many teachers belong to the current group of the teacher
                int numberTeachersInGroup = _context.Teachers.Count(t => t.Group_Id == teacher.Group_Id);
                if (numberTeachersInGroup <= 1)
                {
                    throw new InvalidOperationException("Cannot change the group of a teacher who is the only one in their group.");
                }

                // Check if the new group ID exists
                GroupModel group = _context.Groups.FirstOrDefault(g => g.GROUP_ID == groupId);
                if (group != null)
                {
                    // Manually change the GroupId property of the teacher
                    teacher.Group_Id = groupId;
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
