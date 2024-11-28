using System.Diagnostics.CodeAnalysis;
using DomainLibrary.Models.Requests;

namespace DomainLibrary.Entities
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Class of data for table Product into database.
    /// </summary>
    public record Products
    {
        public int Id { get; }
        public string Name { get; }
        public string Category { get; }
        public string Description { get; }
        public int Stock { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Products() { }

        /// <summary>
        /// ProductData Constructor for use when a new product will be inserte into database.
        /// </summary>
        /// <param name="request">Object ProductRequest who become from requester.</param>
        public Products(ProductRequest request)
        {
            Name = request.Name;
            Category = request.Category;
            Description = request.Description;
            Stock = request.Stock;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// ProductData Constructor for use when need to edit an product data into database.
        /// </summary>
        /// <param name="productId">Product identification.</param>
        /// <param name="request">Object Products who become from database.</param>
        public Products(int productId, Products request)
        {
            Id = productId;
            Name = request.Name;
            Category = request.Category;
            Description = request.Description;
            Stock = request.Stock;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// ProductData Constructor for use when need to edit an product data into database.
        /// </summary>
        /// <param name="productId">Product identification.</param>
        /// <param name="request">Object ProductRequest who become from requester.</param>
        public Products(int productId, ProductRequest request)
        {
            Id = productId;
            Name = request.Name;
            Category = request.Category;
            Description = request.Description;
            Stock = request.Stock;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}