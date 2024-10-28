using Observer.Presentation.Models.Requests;

namespace Observer.Data.Entities
{
    /// <summary>
    /// Class of data for table Product into database.
    /// </summary>
    public record Products
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public int Stock { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

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