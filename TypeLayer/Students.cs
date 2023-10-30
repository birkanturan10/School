using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLayer
{
    public class Students
    {
        [ForeignKey("Note")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int StudentID { get; set; }

        public int? NoteID { get; set; }

        public int? LessonID { get; set; }

		[DisplayName("Ad-Soyad")]
		public string NameSurname { get; set; }

        public string TCKimlikNo { get; set; }

        public string Password { get; set; }

        public Notes Note { get; set; }

		public Lessons Lesson { get; set; }
	}
}
