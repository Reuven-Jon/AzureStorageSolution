using Azure.Data.Tables;
using Azure;
using System;

namespace AzureStorageSolution.Models
{
    public class EntityClass {

        // Model for Customer Profile
        public class CustomerProfile : ITableEntity
        {
            // PartitionKey could be something like "Customer" or a geographic region
            public string PartitionKey { get; set; }

            // RowKey could be the customer ID
            public string RowKey { get; set; }

            // Required by ITableEntity
            public DateTimeOffset? Timestamp { get; set; }

            // Required by ITableEntity
            public ETag ETag { get; set; }

            // Custom properties for your customer profile
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }

        // Model for Product
        public class Product : ITableEntity
        {
            // PartitionKey could be "Product" or a product category
            public string PartitionKey { get; set; }

            // RowKey could be the product ID
            public string RowKey { get; set; }

            // Required by ITableEntity
            public DateTimeOffset? Timestamp { get; set; }

            // Required by ITableEntity
            public ETag ETag { get; set; }

            // Custom properties for your product
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }
            public int StockQuantity { get; set; }
        }
    }
}