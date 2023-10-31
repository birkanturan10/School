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

        public List<Students> Student { get; set; }

        public List<StudentsNotesLessons> StudentNoteLesson { get; set; }
    }

    public class StudentsNotesLessons
    {
        public Students Student { get; set; }
        public Notes Notes { get; set; }
        public Lessons Lesson { get; set; }
    }
}
