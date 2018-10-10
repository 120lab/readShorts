using readShorts.Models.Commands;
using readShorts.Models.ViewModels;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IUserCommandBL : IBaseBL
    {
        
        /// <summary>
        /// Creates a new user with the entered data
        /// </summary>
        /// <param name="command"></param>
        UserViewModel CreateUser(CreateUserCommand command);

        /// <summary>
        /// Updates the entered user
        /// </summary>
        /// <param name="command"></param>
        UserViewModel UpdateUser(UpdateUsersCommand command);

        /// <summary>
        /// Deletes users by the entered IDs
        /// </summary>
        /// <param name="userIds"></param>
        UserViewModel DeleteUser(DeleteUsersCommand command);
    }
}
