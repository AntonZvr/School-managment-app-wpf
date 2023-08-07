using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp.DAL
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-L4JH3JT\SQLEXPRESS;Database=task6DB;Trusted_Connection=True;TrustServerCertificate=True");           
        }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<GroupModel> Groups { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<TeacherModel> Teachers { get; set; }
    }

}
