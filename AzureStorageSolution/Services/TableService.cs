using Azure;
using Azure.Data.Tables;
using static AzureStorageSolution.Models.EntityClass;

public class TableService
{
    private readonly TableClient _customerTableClient;
    private readonly TableClient _productTableClient;

    public TableService(string storageAccountConnectionString)
    {
        var serviceClient = new TableServiceClient(storageAccountConnectionString);

        // Initialize table clients for Customers and Products
        _customerTableClient = serviceClient.GetTableClient(tableName: "CustomerProfiles");
        _productTableClient = serviceClient.GetTableClient(tableName: "Products");

        // Create the tables if they don't exist
        _customerTableClient.CreateIfNotExists();
        _productTableClient.CreateIfNotExists();
    }

    public async Task AddCustomerProfileAsync(CustomerProfile customerProfile)
    {
        await _customerTableClient.AddEntityAsync(customerProfile);
    }

    public async Task AddProductAsync(Product product)
    {
        await _productTableClient.AddEntityAsync(product);
    }

    public async Task<CustomerProfile> GetCustomerProfileAsync(string partitionKey, string rowKey)
    {
        var customer = await _customerTableClient.GetEntityAsync<CustomerProfile>(partitionKey, rowKey);
        return customer.Value;
    }

    public async Task<Product> GetProductAsync(string partitionKey, string rowKey)
    {
        var product = await _productTableClient.GetEntityAsync<Product>(partitionKey, rowKey);
        return product.Value;
    }

    public async Task UpdateCustomerProfileAsync(CustomerProfile customerProfile)
    {
        await _customerTableClient.UpdateEntityAsync(customerProfile, ETag.All);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productTableClient.UpdateEntityAsync(product, ETag.All);
    }

    public async Task DeleteCustomerProfileAsync(string partitionKey, string rowKey)
    {
        await _customerTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }

    public async Task DeleteProductAsync(string partitionKey, string rowKey)
    {
        await _productTableClient.DeleteEntityAsync(partitionKey, rowKey);
    }
}
