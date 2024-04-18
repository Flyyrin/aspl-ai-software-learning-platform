using Business_Logic_Layer.Models;
using System.Globalization;

namespace Presentation_Layer.Models
{
    public class ChatDataModel
    {
        public string Course { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
}
