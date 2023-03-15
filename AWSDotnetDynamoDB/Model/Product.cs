using AWSDotnetDynamoDB.Model.Common;

namespace AWSDotnetDynamoDB.Model
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
