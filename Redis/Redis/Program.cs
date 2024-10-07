using StackExchange.Redis;
using Redis.Models;
using Redis;

var builder = WebApplication.CreateBuilder(args);

// Redis ba�lant�s�
var redisConnection = ConnectionMultiplexer.Connect("localhost:6379");
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

// GraphQL yap�land�rmas�
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<ProductType>();

builder.Services.AddControllers(); // Controller deste�i ekleyin

var app = builder.Build();

app.UseHttpsRedirection();


// API ve GraphQL endpoint'lerini tan�mlama
app.MapControllers();
app.MapGraphQL("/graphql"); // GraphQL sorgular�n� bu endpoint �zerinden �al��t�raca��z.

app.Run();
