﻿using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
	public class Order
	{
				[Key]
		public int order_id { get; set; }
		public string country { get; set; }
		public string cus_first_name { get; set; }
		public string cus_last_name { get; set; }
		public string cus_address { get; set; }
		public string cus_city { get; set; }
		public string cus_phone { get; set; }
		public string cus_email { get; set; }

		[DataType(DataType.Date)]
		public DateTime OrderDate { get; set; }
		[DataType(DataType.Date)]
		public DateTime DeliveryDate { get; set; }
		public virtual ICollection<Customer>? Customers { get; set; }
	
	}
}
