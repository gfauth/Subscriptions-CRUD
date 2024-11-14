using Newtonsoft.Json;
using Observer.Domain.Models.Errors;
using Observer.Domain.Models.Responses;
using Observer.Domain.Validators;
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
            ProductsValidator validationRules = new ProductsValidator();
            var result = validationRules.Validate(this);

            return result.IsValid ? new ResponseEnvelope(HttpStatusCode.Continue) :
                ProductResponseErrors.ProductValidationErrorMessage(result.Errors.FirstOrDefault()!.ErrorMessage);
        }
    }
}
