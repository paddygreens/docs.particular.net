﻿using System;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using System.Threading.Tasks;

namespace SqlPersistence_1.UsingSaga
{

    #region SqlPersistenceSagaWithCorrelationAndTransitional

    [SqlSaga(
        CorrelationProperty = nameof(SagaData.CorrelationProperty),
        TransitionalCorrelationProperty = nameof(SagaData.TransitionalCorrelationProperty)
    )]
    public class SagaWithCorrelationAndTransitional :
        SqlSaga<SagaWithCorrelationAndTransitional.SagaData>,
        IAmStartedByMessages<StartSagaMessage>
    {
        protected override void ConfigureMapping(MessagePropertyMapper<SagaData> mapper)
        {
            mapper.MapMessage<StartSagaMessage>(_ => _.CorrelationProperty);
        }

        public Task Handle(StartSagaMessage message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }

        public class SagaData :
            ContainSagaData
        {
            public Guid CorrelationProperty { get; set; }
            public Guid TransitionalCorrelationProperty { get; set; }
        }
    }

    #endregion
}