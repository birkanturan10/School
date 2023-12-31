﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLayer
{
	public class TeacherNotesViewModel
	{
		public List<Notes> Note { get; set; }
		public List<Students> Student { get; set; }

		public List<StudentNotes> StudentNotesList { get; set; }
	}

	public class StudentNotes
	{
		public Students Student { get; set; }

		public Notes Notes { get; set; }
	}
}
