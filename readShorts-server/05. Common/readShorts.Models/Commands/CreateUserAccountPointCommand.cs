using Framework.Core.Interfaces.CQRS;
using readShorts.Models.dbo;
using System;

namespace readShorts.Models.Commands
{
    public sealed class CreateUserAccountPointCommand : ICommand
    {
        public Int64? UserAccountKey { get; set; }
        public string UserName { get; set; }
        public Models.Enums.LULUPointTypeEnum LUPointTypeKey { get; set; }
    }
}