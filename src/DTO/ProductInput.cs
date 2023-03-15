using AWSDotnetDynamoDB.Model;

namespace AWSDotnetDynamoDB.DTO
{
    public class ProductInput
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product ToModel() => new()
        {
            Id = System.Guid.NewGuid(),
            Name = Name,
            Price = Price,
        };
    }
}