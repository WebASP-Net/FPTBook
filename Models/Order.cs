using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
	public class Order
	{
				[Key]
		public int order_id { get; set; }

		[Required(ErrorMessage = "Country Empty")]
		public string country { get; set; }

		[Required(ErrorMessage = "First Name Empty")]
		public string cus_first_name { get; set; }

		[Required(ErrorMessage = "Last Name Empty")]
		public string cus_last_name { get; set; }

		[Required(ErrorMessage = "Address Empty")]
		public string cus_address { get; set; }

		[Required(ErrorMessage = "City Empty")]
		public string cus_city { get; set; }

		[Required(ErrorMessage = "Phone Empty")]
		public string cus_phone { get; set; }
		
		[Required(ErrorMessage = "Email Empty")]			
		public string cus_email { get; set; }
		[DataType(DataType.Date)]
		public DateTime OrderDate { get; set; }
		public virtual ICollection<Customer>? Customers { get; set; }
	
	}
}
