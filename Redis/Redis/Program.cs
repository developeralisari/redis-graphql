using StackExchange.Redis;
using Redis.Models;
using Redis;

var builder = WebApplication.CreateBuilder(args);

// Redis bağlantısı
var redisConnection = ConnectionMultiplexer.Connect("localhost:6379");
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

// GraphQL yapılandırması
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<ProductType>();

builder.Services.AddControllers(); // Controller desteği ekleyin

var app = builder.Build();

app.UseHttpsRedirection();


// API ve GraphQL endpoint'lerini tanımlama
app.MapControllers();
app.MapGraphQL("/graphql"); // GraphQL sorgularını bu endpoint üzerinden çalıştıracağız.

app.Run();
