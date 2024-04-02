namespace IntegrationTests
{
    public abstract class IntegrationTest
    {
        protected readonly IntegrationTestWebApplicationFactory _factory;
        protected readonly HttpClient _client;

        public IntegrationTest()
        {
            _factory = new IntegrationTestWebApplicationFactory();
            _client = _factory.CreateClient();
        }

    }
}
