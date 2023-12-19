using Biodent.Models;

namespace Biodent.Web.Models
{
    public class ChatingModel
    {
        public ChatingModel()
        {
            this.message = new MessageModel();
        }
        public List<MessageModel> Messages { get; set; }
        public MessageModel message { get; set; }
    }
}
