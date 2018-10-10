using Framework.Core.Interfaces.CQRS;
using readShorts.BusinessLogic.Interfaces;
using readShorts.Models.Queries;
using readShorts.Models.ViewModels;
using System.Threading.Tasks;

namespace readShorts.Services.QueryHandlers
{
    public sealed class UserQueryHandler : ServiceBase, IQueryHandler<UserQuery, UserViewModel>
    {
        private readonly IUserQueryBL _userQueryBL;

        public UserQueryHandler(IUserQueryBL userBL)
        {
            _userQueryBL = userBL;
        }

        public UserViewModel Retrieve(UserQuery query)
        {
            if (!string.IsNullOrEmpty(query.UserName) || !string.IsNullOrEmpty(query.Identity) || !string.IsNullOrEmpty(query.Guid))
                return Task.Run(() => _userQueryBL.GetUser(query)).Result;

            return Task.Run(() => _userQueryBL.GetUsers()).Result;
        }
    }
}