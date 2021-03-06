﻿using TypiconOnline.AppServices.Interfaces;
using TypiconOnline.AppServices.Jobs;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Tests.Common
{
    public class DataQueryProcessorFactory
    {
        private static IQueryProcessor queryProcessor;
        public static IQueryProcessor Instance
        {
            get
            {
                if (queryProcessor == null)
                {
                    queryProcessor = Create();
                }

                return queryProcessor;
            }
        }


        public static IQueryProcessor Create() => Create(TypiconDbContextFactory.Create());

        public static IQueryProcessor Create(TypiconDBContext dbContext)
        {
            var container = new SimpleInjector.Container();

            container.Register<ITypiconSerializer, TypiconSerializer>();
            container.RegisterSingleton<IJobRepository>(() => new JobRepository());

            container.Register(typeof(IQueryHandler<,>), typeof(QueryProcessor).Assembly, typeof(TypiconEntityModel).Assembly);
            container.Register<IQueryProcessor, QueryProcessor>();

            container.RegisterInstance(dbContext);

            var processor = container.GetInstance<IQueryProcessor>();

            return processor;
        }

        //public static IDataQueryProcessor Create() => Create(UnitOfWorkFactory.Create());

        //public static IDataQueryProcessor Create(IUnitOfWork unitOfWork)
        //{
        //    var container = new SimpleInjector.Container();

        //    container.Register<ITypiconSerializer, TypiconSerializer>();

        //    container.RegisterTypiconQueryClasses();

        //    container.RegisterInstance(unitOfWork);

        //    var processor = container.GetInstance<IDataQueryProcessor>();

        //    return processor;
        //}
    }
}
