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
    public class TeacherRepository : ITeacherRepository
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

        public GroupModel FindGroupById(int groupId)
        {
            return _context.Groups.FirstOrDefault(c => c.GROUP_ID == groupId);
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

        public void ChangeTeacherName(int teacherId, string newFirstName, string newLastname)
        {
            TeacherModel teacher = _context.Teachers.FirstOrDefault(t => t.Teacher_Id == teacherId);
            if (teacher != null)
            {
                teacher.FirstName = newFirstName;
                teacher.LastName = newLastname;
                _context.SaveChanges();
            }
        }

        public void CreateTeacher(string teacherFirstName, string teacherLastName, int groupId)
        {
            TeacherModel newTeacher = new TeacherModel
            {
                Group_Id = groupId,
                FirstName = teacherFirstName,
                LastName = teacherLastName
            };

            _context.Teachers.Add(newTeacher);
            _context.SaveChanges();
        }

        public bool DeleteTeacher(int teacherId)
        {
            TeacherModel teacher = _context.Teachers.FirstOrDefault(t => t.Teacher_Id == teacherId);
            if (teacher != null)
            {
                // Count how many teachers belong to the current group of the teacher
                int numberTeachersInGroup = _context.Teachers.Count(t => t.Group_Id == teacher.Group_Id);
                if (numberTeachersInGroup <= 1)
                {
                    throw new InvalidOperationException("Cannot delete a teacher who is the only one in their group.");
                }

                _context.Teachers.Remove(teacher);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

    }
}
