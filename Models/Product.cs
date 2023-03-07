using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FPTBook.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { set; get; }
        public string Name { set; get; }
        public int Quantity { get; set; }
        public string Description { set; get; }
        public decimal Price { set; get; }
        public string? ProductImage { set; get; }
        [NotMapped]
        public IFormFile FileImage { set; get; }
        public int Cat_Id { get; set; }
        [ForeignKey("Cat_Id")]
        public virtual Category? Category { get; set; }
    }
}