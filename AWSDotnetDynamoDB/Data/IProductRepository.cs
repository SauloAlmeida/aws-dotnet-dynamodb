using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using AWSDotnetDynamoDB.DTO;
using AWSDotnetDynamoDB.Model;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace AWSDotnetDynamoDB.Data
{
    public interface IProductRepository
    {
        Task<bool> InsertAsync(Product product);
        Task<ProductOutput> GetAsync(Guid id);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly IAmazonDynamoDB _db;
        private readonly string _tableName = "products";

        public ProductRepository(IAmazonDynamoDB db)
        {
            _db = db;
        }

        public async Task<ProductOutput> GetAsync(Guid id)
        {
            var request = new GetItemRequest()
            {
                TableName = _tableName,
                Key = new System.Collections.Generic.Dictionary<string, AttributeValue>()
                {
                    { "pk", new AttributeValue{ S = id.ToString() } },
                    { "sk", new AttributeValue{ S = id.ToString() } }
                }
            };

            var response = await _db.GetItemAsync(request);

            if (response.HttpStatusCode is not System.Net.HttpStatusCode.OK)
                return null;

            var document = Document.FromAttributeMap(response.Item);

            return JsonSerializer.Deserialize<ProductOutput>(document);
        }

        public async Task<bool> InsertAsync(Product product)
        {
            var productAsJson = JsonSerializer.Serialize(product);
            var productAsAttribute = Document.FromJson(productAsJson).ToAttributeMap();

            var request = new PutItemRequest()
            {
                TableName = _tableName,
                Item = productAsAttribute
            };

            var response = await _db.PutItemAsync(request);

            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
