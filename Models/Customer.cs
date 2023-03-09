using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBook.Models
{
    public class Customer
    {
             [Key] //Create PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
        public int Cus_Id { get; set; }
        [Required(ErrorMessage = "Id Empty")]
        [StringLength(50)]
        public string Cus_Name { get; set; }
		[Required(ErrorMessage = "Name Empty")]
		public DateTime Cus_Birthday { get; set; }
		[Required(ErrorMessage = "Birthday Empty")]
		public string Cus_Gender { get; set; }
		[Required(ErrorMessage = "Choose Gender")]
		public string Cus_Address { get; set; }
        public virtual Order? Order { get; set; }
    }
}