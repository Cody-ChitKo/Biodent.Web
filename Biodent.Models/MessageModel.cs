using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
	public class MessageModel
	{
		public int MessageId { get; set; }
		public int? ToUsersId { get; set; }
		public int? FromUsersId { get; set; }
		public string? UserName { get; set; }
		public string MessageText { get; set; }
		public DateTime TextDate { get; set; }

		public List<UsersModel> userList { get; set; }
    }
}
