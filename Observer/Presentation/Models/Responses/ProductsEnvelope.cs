namespace Observer.Presentation.Models.Responses
{
    /// <summary>
    /// User data response envelope.
    /// </summary>
    public record ProductsEnvelope
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public int Stock { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public ProductsEnvelope()
        {
            
        }
    }
}