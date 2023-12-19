using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.DataAccess.Query
{
    internal class GiftCardQuery
    {
        private string query = string.Empty;
        public string Insert()
        {
            query = "INSERT tbl_giftcard(PackageId, GiftCardCode, GiftCardName, GiftCardImage, GiftCardLevel, IsActive)";
            query += " VALUES(@PackageId, @GiftCardCode, @GiftCardName, @GiftCardImage, @GiftCardLevel, 1)";
            return query;
        }
        public string Update()
        {
            query = "UPDATE tbl_giftcard SET PackageId = @PackageId, GiftCardCode=@GiftCardCode, GiftCardName= @GiftCardName,";
            query += "GiftCardImage = @GiftCardImage, GiftCardLevel = @GiftCardLevel WHERE GiftCardId = @GiftCardId";
            return query;
        }
        public string Delete()
        {
            query = "UPDATE tbl_giftcard SET IsActive = 0 WHERE GiftCardId = @GiftCardId";
            return query;
        }
        public string Select(int GiftCardId)
        {
            if(GiftCardId == 0)
            {
                query = "SELECT tbl_giftcard.*, PackageName FROM tbl_giftcard ";
                query += " INNER JOIN tbl_package ON tbl_package.PackageId = tbl_giftcard.PackageId";
                query += " WHERE tbl_giftcard.IsActive = 1";
            }
            else
            {
                query = "SELECT tbl_giftcard.*, PackageName FROM tbl_giftcard ";
                query += " INNER JOIN tbl_package ON tbl_package.PackageId = tbl_giftcard.PackageId";
                query += " WHERE tbl_giftcard.IsActive = 1 AND tbl_giftcard.GiftCardId = @GiftCardId";
            }
            
            return query;
        }
    }
}
