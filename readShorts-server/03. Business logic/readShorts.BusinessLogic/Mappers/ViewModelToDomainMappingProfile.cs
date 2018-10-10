using AutoMapper;

namespace readShorts.BusinessLogic.Mappers
{
    using Framework.Core.Interfaces.Mappers;

    public class ViewModelToDomainMappingProfile : Profile, IViewModelToDomainMappingProfile
    {
        public new string ProfileName
        {
            get
            {
                return "ViewModelToDomainMappingProfile";
            }
        }

        protected override void Configure()
        {
            // Lookup
            //Mapper.CreateMap<Entities.LOOKUP.City, Models.LOOKUP.City>();
            //Mapper.CreateMap<Entities.LOOKUP.City, Models.LOOKUP.BaseLookup>();
            Mapper.CreateMap<Models.LOOKUP.LookupBase, Entities.LOOKUP.LookupBase>();

            // dbo
            Mapper.CreateMap<Models.dbo.Audit, Entities.dbo.Audit>();
            Mapper.CreateMap<Models.dbo.UserAccount, Entities.dbo.UserAccount>();
            Mapper.CreateMap<Models.dbo.Short, Entities.dbo.Short>();
            Mapper.CreateMap<Models.dbo.ShortUserAccount, Entities.dbo.ShortUserAccount>();
            Mapper.CreateMap<Models.dbo.Ad, Entities.dbo.Ad>();
            Mapper.CreateMap<Models.dbo.EventUserAccount, Entities.dbo.EventUserAccount>();
            Mapper.CreateMap<Models.dbo.ShortTag, Entities.dbo.ShortTag>();
            Mapper.CreateMap<Models.Commands.CreateUserCommand, Entities.dbo.UserAccount>();
            Mapper.CreateMap<Models.Commands.UpdateUsersCommand, Entities.dbo.UserAccount>();
        }
    }
}