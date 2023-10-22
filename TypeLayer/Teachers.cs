using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLayer
{
    public class Teachers
    {
        [ForeignKey("Lesson")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherID { get; set; }

        public int? LessonID { get; set; }

        public string NameSurname { get; set; }

        public string TCKimlikNo { get; set; }

        public string Password { get; set; }

        public Lessons Lesson { get; set; }
    }
}
