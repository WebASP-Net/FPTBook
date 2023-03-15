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

        [Required(ErrorMessage = "Name Empty")]
        [StringLength(maximumLength:50,ErrorMessage ="Length must be less than 50 characters")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Quantity Empty")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Description Empty")]
        [StringLength(maximumLength:1000,ErrorMessage ="Length must be less than 1000 characters")]
        public string Description { set; get; }

        [Required(ErrorMessage = "Price Empty")]
        public float Price { set; get; }
        public string? ProductImage { set; get; }
        [NotMapped]
        public IFormFile FileImage { set; get; }

        [Required(ErrorMessage = "Category Empty")]
        public int Cat_Id { get; set; }
        [ForeignKey("Cat_Id")]
        public virtual Category? Category { get; set; }
    }
}