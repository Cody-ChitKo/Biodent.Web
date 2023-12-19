using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class ServiceModel
    {
        public int ServiceId { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceType { get; set; }

        public string ServiceHeader { get; set; }
        public string ServiceDescription { get; set; }
        public decimal ServicePrice { get; set; }
        public IFormFile? ServiceImage { get; set; }
        public string? ImagePathUrl { get; set; }
        //public string ServiceImage { get; set; }
        public bool IsActive { get; set; }
        public int? UsersId { get; set; }
        public string InvNo { get; set; }
        public List<ServiceTypeModel> ServiceTypes { get; set; }
    }
}
