using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLayer
{
    public class Notes
    {
        [ForeignKey("Lesson")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteID { get; set; }

        public int LessonID { get; set; }

        public int FirstExam { get; set; }

        public int SecondExam { get; set; }

        public int Project { get; set; }

        public int AverageNote { get; set; }

        public string DidItPass { get; set; }

        public Lessons Lesson { get; set; }
    }
}
