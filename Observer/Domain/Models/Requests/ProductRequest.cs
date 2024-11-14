using Newtonsoft.Json;
using Observer.Domain.Models.Errors;
using Observer.Domain.Models.Responses;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;

namespace Observer.Domain.Models.Requests
{
    /// <summary>
    /// Request record for use on ProductController.
    /// </summary>
    public record ProductRequest
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="stock"></param>
        /// <param name="category"></param>
        public ProductRequest(string name, string description, int stock, string? category)
        {
            Name = name;
            Stock = stock;
            Description = description;
            Category = category is null ? "None" : category;
        }

        /// <summary>
        /// Product name.
        /// </summary>
        /// <example>Estaringo Lopingo</example>
        [Required]
        public string Name { get; }

        /// <summary>
        /// Product's category.
        /// </summary>
        /// <example>Blevers</example>
        [Required]
        public string Category { get; }

        /// <summary>
        /// Product's description.
        /// </summary>
        /// <example>Blevers and blever for blevers.</example>
        public string Description { get; }

        /// <summary>
        /// Product's stock count.
        /// </summary>
        /// <example>Blevers</example>
        [Required]
        public int Stock { get; }

        /// <summary>
        /// Validate product dada.
        /// </summary>
        /// <returns>Object ResponseEnvelope.</returns>
        public ResponseEnvelope IsValid()
        {
            if (Name is null || Name.Equals(string.Empty) || Name.Length <= 2)
                return ProductResponseErrors.ProductValidationErrorMessage("Informe um nome válido para o produto.");

            if (Category is null || Category.Equals(string.Empty) || Category.Length <= 2)
                return ProductResponseErrors.ProductValidationErrorMessage("Informe uma categoria válida para o produto.");

            if (string.IsNullOrEmpty(Description) || Description.Length < 4)
                return ProductResponseErrors.ProductValidationErrorMessage("Description precisa conter ao menos 5 dígitos.");

            var regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (Stock <= 0)
                return ProductResponseErrors.ProductValidationErrorMessage("Stock precisa conter ao menos 1 item para o produto.");

            return new ResponseEnvelope(HttpStatusCode.Continue);
        }
    }
}
