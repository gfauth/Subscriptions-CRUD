namespace Observer.Presentation.Models.Responses
{
    /// <summary>
    /// User data response envelope.
    /// </summary>
    public record UsersEnvelope
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string Document { get; private set; }
        public string Login { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        public UsersEnvelope()
        {

        }
    }
}