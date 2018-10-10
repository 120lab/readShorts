using AutoMapper;

namespace readShorts.BusinessLogic.Mappers
{
    using Framework.Core.Interfaces.Mappers;

    public class DomainToViewModelMappingProfile : Profile, IDomainToViewModelMappingProfile
    {
        public new string ProfileName
        {
            get
            {
                return "DomainToViewModelMappingProfile";
            }
        }

        protected override void Configure()
        {
            // Lookup
            Mapper.CreateMap<Entities.LOOKUP.LookupBase, Models.LOOKUP.LookupBase>();

            // dbo
            Mapper.CreateMap<Entities.dbo.Audit, Models.dbo.Audit>();
            Mapper.CreateMap<Entities.dbo.UserAccount, Models.dbo.UserAccount>();
            Mapper.CreateMap<Entities.dbo.Short, Models.dbo.Short>();
            Mapper.CreateMap<Entities.dbo.ShortUserAccount, Models.dbo.ShortUserAccount>();
            Mapper.CreateMap<Entities.dbo.Ad, Models.dbo.Ad>();
            Mapper.CreateMap<Entities.dbo.EventUserAccount, Models.dbo.EventUserAccount>();
            Mapper.CreateMap<Entities.dbo.ShortTag, Models.dbo.ShortTag>();
            Mapper.CreateMap<Entities.dbo.UserAccount, Models.Commands.CreateUserCommand>();
            Mapper.CreateMap<Entities.dbo.UserAccount, Models.Commands.UpdateUsersCommand>();
        }
    }
}