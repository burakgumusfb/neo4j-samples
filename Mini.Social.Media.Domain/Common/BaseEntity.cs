using System;

namespace Mini.Social.Media.Domain.Common
{
	public class BaseEntity
	{
		public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModifyDate { get; set; }
        public int? ModifyUserId { get; set; }

		public DateTime DeleteDate { get; set; }
		public int? DeleteUserId { get; set; }
		public bool IsDeleted { get; set; }
	}
}

