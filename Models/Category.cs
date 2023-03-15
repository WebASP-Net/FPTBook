using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBook.Models
{
    public class Category
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Cat_Id { get; set; }
		[StringLength(maximumLength:50,ErrorMessage ="Length must be less than 50 characters")]
		public string Cat_Name { get; set;}
		[StringLength(maximumLength:1000,ErrorMessage ="Length must be less than 1000 characters")]
		public string Cat_Des { get; set; }
		public virtual ICollection<Product>? Products { get; set;}
    }
}