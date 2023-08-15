using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.DAL
{
    public class TeacherModel
    {
        [Key]
        public int Teacher_Id { get; set; }
        [ForeignKey("Group")]
        public int Group_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public GroupModel Group { get; set; }

    }
}
