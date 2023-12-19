using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class PackageModel
    {
        public int PackageId { get; set; }
        public string PackageName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public decimal PKPrice { get; set; }
        public decimal GetPKAmount { get; set; }
        public IFormFile PKImage { get; set; }
        public string? PKImageUrl { get; set; }
    }
}
