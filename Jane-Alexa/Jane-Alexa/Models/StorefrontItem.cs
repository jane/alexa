using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jane.Alexa.Models
{
	public class StorefrontItem
	{
		public long DealId { get; set; }
		public bool IsDeliveredElectronically { get; set; }
		public bool IsEndingSoon { get; set; }
		public bool IsNew { get; set; }
		public bool IsSoldOut { get; set; }
		public long LikeCount { get; set; }
		public double Price { get; set; }
		public long Quantity { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
	}
}
