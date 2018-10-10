using AutoMapper;
using Framework.Core.Interfaces.Log;
using Framework.DataAccess.Interfaces;
using readShorts.BusinessLogic.Interfaces;
using readShorts.BusinessLogic.Mappers;
using System;
using System.Web.Script.Serialization;

namespace readShorts.BusinessLogic
{
    public abstract class BaseBL : IBaseBL
    {
        //[NonSerialized]
        //protected internal readonly ILoggerService _loggerService;
        //[NonSerialized]
        //protected internal readonly IUnitOfWork _unitOfWork;
        //[NonSerialized]
        //protected internal readonly IDatabaseFactory _dbFactory;

        [ScriptIgnore(ApplyToOverrides = true)]
        protected IUnitOfWork UnitOfWork { get; private set; }

        //[ScriptIgnore(ApplyToOverrides = true)]
        //protected IDatabaseFactory DatabaseFactory { get; private set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        protected ILoggerService LoggerService { get; private set; }

        protected readonly Object thisLock = new Object();

        public BaseBL(IUnitOfWork unitOfWork, ILoggerService loggerService)
        {
            //_dbFactory = dbFactory;
            //_unitOfWork = unitOfWork;
            //_loggerService = loggerService;

            //this.DatabaseFactory = dbFactory;
            this.UnitOfWork = unitOfWork;
            this.LoggerService = loggerService;

            Mapper.Initialize(mapper =>
            {
                mapper.AddProfile<ViewModelToDomainMappingProfile>();
                mapper.AddProfile<DomainToViewModelMappingProfile>();
            });
        }

        /// <summary>
        /// Use autoMapper to map from one class to another
        /// </summary>
        /// <typeparam name="VSource">The class to convert from</typeparam>
        /// <typeparam name="TResult">The class to convert to</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public TResult Map<VSource, TResult>(VSource source)
            where TResult : class
            where VSource : class
        {
            var result = Mapper.Map<VSource, TResult>(source);
            return result;
            //return null;
        }
    }
}