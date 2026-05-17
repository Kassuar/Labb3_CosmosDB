using Microsoft.Azure.Cosmos;

namespace Labb3_CosmosDB.Data
{
    public class CosmosContext
    {
        private readonly string EndPoint = "https://localhost:8081";

        private readonly string DataBaseName = "CrmDb";

        private readonly string ContainerName = "Customer";

        public CosmosClient Client { get;}

        public Container Container { get;}

        public CosmosContext()
        {
            Client= new CosmosClient(EndPoint, "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            Container = Client.GetContainer(DataBaseName, ContainerName);
            
        }
    }
}
