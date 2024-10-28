using Observer.Data.Entities;

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

        ///// <summary>
        ///// Constructor for user data response envelope.
        ///// </summary>
        ///// <param name="user">Object Users.</param>
        //public UsersEnvelope(Users user)
        //{
        //    Id = user.Id;
        //    Name = user.Name;
        //    LastName = user.LastName;
        //    Birthdate = user.Birthdate;
        //    Document = user.Document;
        //    Login = user.Login;
        //    CreatedAt = user.CreatedAt;
        //    UpdatedAt = user.UpdatedAt;
        //}

        ///// <summary>
        ///// Constructor for user data response envelope with id before creation.
        ///// </summary>
        ///// <param name="user">Object Users.</param>
        ///// <param name="userId">New user identification.</param>
        //public UsersEnvelope(int userId, Users user)
        //{
        //    Id = userId;
        //    Name = user.Name;
        //    LastName = user.LastName;
        //    Birthdate = user.Birthdate;
        //    Document = user.Document;
        //    Login = user.Login;
        //    CreatedAt = user.CreatedAt;
        //    UpdatedAt = user.UpdatedAt;
        //}
    }
}
