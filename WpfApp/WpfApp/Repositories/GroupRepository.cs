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

        public void CreateGroup(string groupName)
        {          
            GroupModel newGroup = new GroupModel
            {
                NAME = groupName                
            };

            _context.Groups.Add(newGroup);   
            _context.SaveChanges();
        }
    }
}
