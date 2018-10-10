using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IShortCommandBL : IBaseBL
    {

        /// <summary>
        /// Creates a new Short with the entered data
        /// </summary>
        /// <param name="command"></param>
        ShortViewModel CreateShort(CreateShortCommand command);

        /// <summary>
        /// Updates the entered Short
        /// </summary>
        /// <param name="command"></param>
        ShortViewModel UpdateShort(UpdateShortsCommand command);

        /// <summary>
        /// Deletes Shorts by the entered IDs
        /// </summary>
        /// <param name="ShortIds"></param>
        ShortViewModel DeleteShort(DeleteShortsCommand command);

        void UpdateJsonDataAllShorts();
        void UploadSharePicturesAllShorts();
    }
}
