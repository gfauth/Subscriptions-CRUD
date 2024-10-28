using Observer.Data.Entities;
using Observer.Presentation.Models.Requests;
using Observer.Presentation.Models.Responses;
using System.Net;

namespace ObserverTest.Data
{
    internal static class FakeData
    {
        internal static UserRequest UsefulUserRequest() => 
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), "01234567890", "tester", "D3f4u1t0");

        internal static UserRequest PasswordUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), string.Empty, "tester", "12345");

        internal static UserRequest LoginUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), string.Empty, "usr", "D3f4u1t0");

        internal static UserRequest BurthdateUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2020, 5, 15), string.Empty, "tester", "D3f4u1t0");

        internal static UserRequest LastNameUserRequest() =>
            new UserRequest("Fulano", "Bu", new DateTime(2000, 5, 15), string.Empty, "tester", "D3f4u1t0");

        internal static UserRequest NameUserRequest() =>
            new UserRequest("Ba", "Blevers", new DateTime(2000, 5, 15), string.Empty, "tester", "D3f4u1t0");

        internal static UserResponse SuccessCreateUserResponse(UserRequest user) => 
            new(HttpStatusCode.Created, $"Usuário {user.Name} criado com sucesso."!, 
                new UsersEnvelope(1, 
                    new Users(user)));

        internal static UserResponse SuccessRetrieveUserResponse(UserRequest user) =>
            new(HttpStatusCode.OK, "Usuário recuperado com sucesso."!, new UsersEnvelope(1, new Users(user)));
    }
}