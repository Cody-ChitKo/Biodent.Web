using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class GiftCardModel
    {
        public int GiftCardId { get; set; }
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string GiftCardCode { get; set; }
        public string GiftCardName { get; set; }
        public IFormFile GiftCardImage { get; set; }
        public string GiftCardImageUrl { get; set; }
        public int? GiftCardLevel { get; set; }
        public bool IsActive { get; set; }
        public List<PackageModel> Packages { get; set; }
    }
}
