using System.Collections.Generic;

namespace readShorts.Models.ViewModels
{
    public abstract class BaseViewModel
    {
        public BaseViewModel()
        {
            Messages = new List<Message>();
        }

        public IEnumerable<Message> Messages { get; set; }
    }
}