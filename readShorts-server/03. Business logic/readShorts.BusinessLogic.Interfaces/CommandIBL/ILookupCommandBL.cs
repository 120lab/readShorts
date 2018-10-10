using readShorts.Models.Commands;
using readShorts.Models.LOOKUP;
using readShorts.Models.ViewModels;

namespace readShorts.BusinessLogic.Interfaces
{
    public interface ILookupCommandBL : IBaseBL
    {
        LookupViewModel Create(CreateLookupCommand command);
    }
}