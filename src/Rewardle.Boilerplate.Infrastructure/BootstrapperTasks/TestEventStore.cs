using System;
using AcklenAvenue.EventSourcing.Postgres;
using AcklenAvenue.EventSourcing.Serializer.JsonNet;

namespace Rewardle.Boilerplate.Infrastructure.BootstrapperTasks
{
    public class TestEventStore : PostgresEventStore<Guid>
    {
        readonly JsonEventConverter _serializer;
        public TestEventStore(string connectionString, string tableName) : base(connectionString, tableName)
        {
            _serializer = new JsonEventConverter();
        }

        public override object DeserializeEvent(JsonEvent eventJson)
        {
            return _serializer.GetEvent(eventJson);
        }
    }
}