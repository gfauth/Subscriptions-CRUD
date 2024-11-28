using System.Diagnostics.CodeAnalysis;
using DomainLibrary.Models.Requests;

namespace DomainLibrary.Entities
{
    [ExcludeFromCodeCoverage]
    /// <summary>
    /// Class of data for table User into database.
    /// </summary>
    public record Users
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string Document { get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Users() { }

        /// <summary>
        /// UserData Constructor for use when a new user will be inserte into database.
        /// </summary>
        /// <param name="request">Object UserRequest who become from requester.</param>
        public Users(UserRequest request)
        {
            Name = request.Name;
            LastName = request.LastName;
            Birthdate = request.Birthdate;
            Document = request.Document;
            Login = request.Login;
            Password = request.Password;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// UserData Constructor for use when neet to edit an user data into database but using a self object based.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <param name="request">Object UserRequest who become from database.</param>
        public Users(int userId, Users request)
        {
            Id = userId;
            Name = request.Name;
            LastName = request.LastName;
            Birthdate = request.Birthdate;
            Document = request.Document;
            Login = request.Login;
            Password = request.Password;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// UserData Constructor for use when neet to edit an user data into database.
        /// </summary>
        /// <param name="userId">User identification.</param>
        /// <param name="request">Object UserRequest who become from requester.</param>
        public Users(int userId, UserRequest request)
        {
            Id = userId;
            Name = request.Name;
            LastName = request.LastName;
            Birthdate = request.Birthdate;
            Document = request.Document;
            Login = request.Login;
            Password = request.Password;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}