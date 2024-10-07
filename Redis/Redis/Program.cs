using StackExchange.Redis;
using Redis.Models;
using Redis;

var builder = WebApplication.CreateBuilder(args);

// Redis baðlantýsý
var redisConnection = ConnectionMultiplexer.Connect("localhost:6379");
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

// GraphQL yapýlandýrmasý
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddType<ProductType>();

builder.Services.AddControllers(); // Controller desteði ekleyin

var app = builder.Build();

app.UseHttpsRedirection();


// API ve GraphQL endpoint'lerini tanýmlama
app.MapControllers();
app.MapGraphQL("/graphql"); // GraphQL sorgularýný bu endpoint üzerinden çalýþtýracaðýz.

app.Run();
