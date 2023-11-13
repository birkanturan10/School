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
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int StudentID { get; set; }

		[DisplayName("Ad-Soyad")]
		public string NameSurname { get; set; }

        public string TCKimlikNo { get; set; }

        public string Password { get; set; }
	}
}
