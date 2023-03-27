namespace ProductInventory.IntegrationsTests;

using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Product.Inventory.Controllers;
using TestContainers.Container.Abstractions.Hosting;
using TestContainers.Container.Database.MsSql;
using VerifyXunit;
using Xunit;
using TestContainers.Container.Database.MySql;

[UsesVerify]
public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<ProductTestDatabase>
{
    private const string ProductsApi = "/api/products";
    private readonly HttpClient _client;
    public ProductsControllerTests(WebApplicationFactory<Program> applicationInMemoryFactory, ProductTestDatabase productTestDatabase)
    {
        var configurationValues = new Dictionary<string, string>
        {
            { "ConnectionStrings:Products", productTestDatabase.ConnectionString!}
        };
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configurationValues)
            .Build();
        
        _client = applicationInMemoryFactory.WithWebHostBuilder(builder =>
        {
            builder.UseConfiguration(configuration);
        }).CreateClient();
    }

    [Theory]
    [ClassData(typeof(GetProductTestCases))]
    public async Task GetProducts_ShouldReturnListOfProducts(IReadOnlyCollection<Product> products)
    {
        // Arrange
        await CreateProduct(products);

        // Act
        var response = await _client.GetAsync(ProductsApi);

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<List<Product>>();
        await Verify(result);
    }

    private async Task CreateProduct(IReadOnlyCollection<Product> products)
    {
        foreach (var product in products)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, ProductsApi);
            request.Content = requestContent;

            await _client.SendAsync(request);
        }
    }
}

public class GetProductTestCases : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { 
            new List<Product> { 
                new Product { Name = "Product 1", Description = "Description 1", Price = 300 }, 
                new Product { Name = "Product 2", Description = "Description 2", Price = 450 },
                new Product { Name = "Product 3", Description = "Description 3", Price = 600 }, 
                new Product { Name = "Product 4", Description = "Description 4", Price = 999 }
            } };
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


public sealed class ProductTestDatabase : IAsyncLifetime
{
    private MsSqlContainer? _container;
    internal string? ConnectionString;

    public async Task InitializeAsync()
    {
        _container = new ContainerBuilder<MsSqlContainer?>()
            .ConfigureContainer((context, container) =>
            {
                container!.Password = "testpassfdsfdsf@323423AAAword";
                container.Username = "testuser";
                container.DatabaseName = "testdb";
            })
            .Build();

        await _container!.StartAsync();
        
        ConnectionString = _container.GetConnectionString();
    }

    public async Task DisposeAsync() => await _container!.StopAsync();
}