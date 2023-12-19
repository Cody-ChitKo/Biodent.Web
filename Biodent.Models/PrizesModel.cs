using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class PrizesModel
    {
        public int PrizeId { get; set; }
        public int PackageId {  get; set; }
        public int UsersId { get; set; }
        public int GiftCardId { get; set; }
        public string GiftCardCode { get; set; }
        public string GiftCardName { get; set; }
        public string GiftCardImage { get; set; }
        public DateTime WithdrawDate { get; set; }
        public bool IsWithdraw { get; set; }
        public string IsWithdrawString { get;set; }
        public bool CanWithdraw { get; set; }
        public string CanWithdrawString { get; set; }
        public decimal PKAmount { get; set; }
        public decimal UseAmount { get; set; }
        public bool IsActive { get;set; }

    }
}
