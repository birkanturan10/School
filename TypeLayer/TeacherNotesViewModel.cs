using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLayer
{
	public class TeacherNotesViewModel
	{
		public IEnumerable<Notes> Note { get; set; }
		public IEnumerable<Students> Student { get; set; }
	}
}
