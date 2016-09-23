using System;
using Rewardle.Boilerplate.Domain.Services;
using Rewardle.Boilerplate.Domain.UserRoot;
using Rewardle.Boilerplate.Infrastructure;
using Rewardle.Boilerplate.Infrastructure.BootstrapperTasks;
using Rewardle.Core.Domain;
using Rewardle.Core.Infrastructure;
using Rewardle.EventStoreUtilities;

namespace Rewardle.Boilerplate.AggregateSeeder
{
    class Program
    {
        const string StarbucksBusinessId = "90bfbeda-4cdb-429d-9c51-7f09d15ea59c";
        const string StarbucksKioskId = "90bfbeda-4cdb-429d-9c51-7f09d15ea60c";
        const string EspresoAmericanoBusinessId = "90bfbeda-4cdb-429d-9c51-7f09d15ea59b";
        const string EspresoAmericanoKioskId = "90bfbeda-4cdb-429d-9c51-7f09d15ea61b";

        static readonly AccountCodeCreator AccountGenerator =
            new AccountCodeCreator(new UniqueNumberGenerator(), new RandomNumberGenerator(), new RandomStringGenerator());

        static void Main(string[] args)
        {
            Console.WriteLine("Using config: " + Application.GetConfig("Environment"));

            new EventStoreRebuilder().WithTableName(EventStoreConfig.TableName)
                .WithConnectionString(EventStoreConfig.ConnectionString)
                .WithIdType(AggregateIdType.String)
                .RebuildTable()
                .SeedWith(
                    () =>
                    {
                        TestRepository repo = GetTestRepository();
                        SeedTest(repo);
                    }).Rebuild();
        }

        static void SeedTest(TestRepository repo)
        {
            TestAggregateRoot testAggregateRoot = new TestAggregateRoot(Guid.NewGuid(), "Test");
            repo.SaveChanges(testAggregateRoot).Wait();
        }

        static TestRepository GetTestRepository()
        {
            TestEventStore eventStore = GetEventStore();

            var repository = new TestRepository(eventStore, new NullBlingDispatcher());
            return repository;
        }

        static TestEventStore GetEventStore()
        {
            var eventStore =
                new TestEventStore(EventStoreConfig.ConnectionString,
                    EventStoreConfig.TableName);
            return eventStore;
        }
    }
}