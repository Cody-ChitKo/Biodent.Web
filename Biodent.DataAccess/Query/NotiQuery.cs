using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class NotiQuery
    {
        private string query;
        public string Insert()
        {
            query = "INSERT tbl_notification(UsersID, NotiDate, NotiTitleEng, NotiTitleMyan, NotiType, NotiMsgEng, NotiMsgMyan, IsSeen, IsActive) ";
            query += "VALUES(@UsersID, @NotiDate, @NotiTitleEng, @NotiTitleMyan, @NotiType, @NotiMsgEng, @NotiMsgMyan, @IsSeen, 1);";
            return query;
        }
        public string GetNotis()
        {
            query = "SELECT * FROM tbl_notification WHERE UsersID = @UsersID";
            return query;
        }
    }
}
