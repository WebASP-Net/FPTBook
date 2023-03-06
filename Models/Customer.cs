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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cus_id { get; set; }
        [Required]// không đc rỗng thông báo mặc định
        [StringLength(50)] // không quá 50 kí tự
        public string cus_name { get; set; }
        //[Required(ErrorMessage ="errrrrrrr")]
        public DateTime cus_birthday { get; set; }
        public string cus_gender { get; set; }
        public string cus_address { get; set; }
    }
}