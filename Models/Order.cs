using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
	public class Order
	{
				[Key]
		public int order_id { get; set; }

		[Required(ErrorMessage = "Country Empty")]
		[StringLength(maximumLength:200,ErrorMessage ="Length must be less than 200 characters")]
		public string country { get; set; }

		[Required(ErrorMessage = "First Name Empty")]
		[StringLength(maximumLength:20,ErrorMessage ="Length must be less than 20 characters")]
		public string cus_first_name { get; set; }

		[Required(ErrorMessage = "Last Name Empty")]
		[StringLength(maximumLength:20,ErrorMessage ="Length must be less than 20 characters")]
		public string cus_last_name { get; set; }

		[Required(ErrorMessage = "Address Empty")]
		[StringLength(maximumLength:100,ErrorMessage ="Length must be less than 20 characters")]
		public string cus_address { get; set; }

		[Required(ErrorMessage = "City Empty")]
		[StringLength(maximumLength:50,ErrorMessage ="Length must be less than 20 characters")]
		public string cus_city { get; set; }

		[Required(ErrorMessage = "Phone Empty")]
		[StringLength(maximumLength:30,ErrorMessage ="Length must be less than 20 characters")]		
		public string cus_phone { get; set; }
		
		[Required(ErrorMessage = "Email Empty")]
		[StringLength(maximumLength:150,ErrorMessage ="Length must be less than 20 characters")]			
		public string cus_email { get; set; }
		

		[DataType(DataType.Date)]
		public DateTime OrderDate { get; set; }
		public virtual ICollection<Customer>? Customers { get; set; }
	
	}
}
