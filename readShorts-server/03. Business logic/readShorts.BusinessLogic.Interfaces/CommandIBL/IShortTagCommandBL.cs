using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IShortTagCommandBL : IBaseBL
    {
        /// <summary>
        /// Creates a new Short with the entered data
        /// </summary>
        /// <param name="command"></param>
        ShortTagViewModel Create(CreateShortTagCommand command);
    }
}