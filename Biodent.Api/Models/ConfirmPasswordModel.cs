namespace Biodent.Api.Models
{
    public class ConfirmPasswordModel
    {
        public string CheckPassword { get; set; }
        public bool Status {  get; set; }
        public string Message { get; set; }
    }
}
