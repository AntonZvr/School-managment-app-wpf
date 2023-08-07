using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.DAL;

namespace WpfApp.DAL
{
    public class GroupModel
    {
        [ForeignKey("Course")]
        public int COURSE_ID { get; set; }
        [Key]
        public int GROUP_ID { get; set; }
        public string NAME { get; set; }

        public virtual ICollection<StudentModel> Students { get; set; }
    }
}
