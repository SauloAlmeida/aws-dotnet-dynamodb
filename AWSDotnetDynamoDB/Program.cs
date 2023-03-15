using Amazon.DynamoDBv2;
using Amazon.Runtime;
using AWSDotnetDynamoDB.Data;
using AWSDotnetDynamoDB.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>(s =>
        {
            var credentials = new BasicAWSCredentials("", "");

            return new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast2);
        });

        builder.Services.AddSingleton<IProductRepository, ProductRepository>();

        var app = builder.Build();

        app.MapGet("/", () => Results.Ok("Hello, World!"));

        app.MapPost("/products", async ([FromBody] ProductInput input, [FromServices] IProductRepository repository) =>
        {
            bool created = await repository.InsertAsync(input.ToModel());

            if (created) return Results.NoContent();

            return Results.BadRequest("Sorry, I was unable to create it!");
        });

        app.MapGet("/products/{id}", async ([FromRoute] Guid id, [FromServices] IProductRepository repository) => Results.Ok(await repository.GetAsync(id)));

        app.Run();
    }
}