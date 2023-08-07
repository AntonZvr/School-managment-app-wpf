using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfApp.DAL
{
    public class CourseModel
    {
        [Key]
        public int COURSE_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }

        public virtual ICollection<GroupModel> Groups { get; set; }
    }
}
