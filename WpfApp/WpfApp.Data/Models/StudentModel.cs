using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.DAL
{
   public class StudentModel
    {
        [Key]
        public int STUDENT_ID { get; set; }
        [ForeignKey("Group")]
        public int GROUP_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }

        public GroupModel Group { get; set; }

    }
}
