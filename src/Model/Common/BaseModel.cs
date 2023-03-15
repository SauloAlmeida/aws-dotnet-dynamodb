using System;
using System.Text.Json.Serialization;

namespace AWSDotnetDynamoDB.Model.Common
{
    public abstract class BaseModel
    {
        [JsonPropertyName("pk")]
        public string Pk => Id.ToString();

        [JsonPropertyName("sk")]
        public string Sk => Id.ToString();

        public Guid Id { get; init; } = default!;
    }
}
