using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface IUserQueryBL : IBaseBL
    {
        Int64? GetUser(string userName);
        /// <summary>
        /// Return all users in the system
        /// </summary>
        /// <returns></returns>
        UserViewModel GetUsers();

        //UserViewModel GetUsersWithProfiles();

        /// <summary>
        /// Returns the entered user's data
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        UserViewModel GetUser(UserQuery query);

    }
}
