using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLayer
{
    public class StudentGradesViewModel
    {
        public List<Lessons> Lesson { get; set; }

        public List<Notes> Note { get; set; }

        public List<LessonsNotes> LessonNote { get; set; }
    }

    public class LessonsNotes
    {
        public Notes Notes { get; set; }

        public Lessons Lesson { get; set; }
    }
}
