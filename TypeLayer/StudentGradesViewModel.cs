using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLayer
{
    public class StudentGradesViewModel
    {
        public List<NotesWithLessonsViewModel> NotesWithLessons { get; set; }
    }

    public class NotesWithLessonsViewModel
    {
        public int NoteID { get; set; }
        public int StudentID { get; set; }
        public string LessonName { get; set; }
        public int FirstExam { get; set; }
        public int SecondExam { get; set; }
        public int Project { get; set; }
        public int AverageNote { get; set; }
        public string DidItPass { get; set; }
    }

}
