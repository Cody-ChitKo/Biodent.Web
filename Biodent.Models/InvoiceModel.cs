using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodent.Models
{
    public class InvoiceModel
    {
        public string InvoiceID { get; set; }
        public string InvNo { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime InvDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "You need to add Patient Name")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "You need to add Patient Age")]
        public int? P_Age { get; set; }

        [Required(ErrorMessage = "You need to add Patient Gender")]
        public string Gender { get; set; }
        public string TeethShade { get; set; }
        public int UsersID { get; set; }
        public string UsersName { get; set; }
        public string DoctorName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal Discount { get; set; }
        public int RemainingDay { get; set; }
        public string Remark { get; set; }
        public string Materials { get; set; }
        public string FilePath { get; set; }

        public IFormFile ToothSimple { get; set; }
        public string ToothSimpleFile { get; set; }

        public int ApproveUserID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ApproveDate { get; set; }
        public string OrderStatus { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DeliverdDate { get; set; }
        public bool IsActive { get; set; }

        public List<UsersModel> Users { get; set; }
        public List<DoctorModel> Doctors { get; set; }
        public List<ProthesisModel> ProthesisLiv { get; set; }
        public List<SubProthesisModel> SubProthesisLiv { get;set; }
        public List<InvoiceDetailModel> invDetail { get; set; }
    }
}
