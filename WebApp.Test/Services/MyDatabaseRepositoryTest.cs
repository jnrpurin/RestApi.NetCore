using WebApp.Domain.Models;
using WebApp.Infra.Context;
using WebApp.Infra.Repository;
using Microsoft.Extensions.Configuration;
using Moq;

namespace WebApp.Test.Services
{
    [TestClass]
    [TestCategory("ClientsTest")]
    public class MyDatabaseRepositoryTest
    {
        private readonly Mock<IMyDatabaseRepository> MockMyDatabaseRepo;
        private readonly Mock<Infra.Repository.DapperRepositoryBase<ClientInfo>> MockDapperRepositoryBase;
        private readonly Mock<MyDatabaseContext> MockContext;
        private readonly IConfiguration MockConfiguration;

        public MyDatabaseRepositoryTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"ConnectionString", "MockDBTest"},
                {"SectionName:SomeKey", "SectionValue"}
            };

            MockConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            MockContext = new Mock<MyDatabaseContext>(MockConfiguration);

            MockMyDatabaseRepo = new Mock<IMyDatabaseRepository>(MockBehavior.Strict);
            MockDapperRepositoryBase = new Mock<DapperRepositoryBase<ClientInfo>>(MockContext);

        }

        [TestMethod]
        public void ReturnClientBySpecificName()
        {
            var nameParam = "John";

            var newClient = new ClientInfo() { FirstName = nameParam };
            IEnumerable<ClientInfo> expectedReturn = new[] { newClient };
            MockMyDatabaseRepo.Setup(r => r.GetClientInfoList(nameParam)).Returns(() => expectedReturn);

            var resultToTest = MockMyDatabaseRepo.Object.GetClientInfoList(nameParam);
            Assert.AreEqual(expectedReturn, resultToTest);
        }
    }
}
