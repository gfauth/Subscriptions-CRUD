using Observer.Data.Entities;
using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using Observer.Domain.ResponsesEnvelope;
using System.Net;

namespace ObserverTest.Data
{
    internal static class FakeData
    {
        internal static UserRequest UsefulUserRequest() => 
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), "01234567890", "tester", "D3f4u1t0aaa");

        internal static UserRequest PasswordUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), string.Empty, "tester", "12345");

        internal static UserRequest LoginUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), string.Empty, "usr", "D3f4u1t0aaaa");

        internal static UserRequest BurthdateUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2020, 5, 15), string.Empty, "tester", "D3f4u1t0aaa");

        internal static UserRequest LastNameUserRequest() =>
            new UserRequest("Fulano", "Bu", new DateTime(2000, 5, 15), string.Empty, "tester", "D3f4u1t0aaa");

        internal static UserRequest NameUserRequest() =>
            new UserRequest("Ba", "Blevers", new DateTime(2000, 5, 15), string.Empty, "tester", "D3f4u1t0aaa");

        internal static IResponse<ResponseEnvelope> SuccessCreateUserResponse(UserRequest user) => 
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.Created, $"Usuário {user.Name} criado com sucesso."!, 
                    new Users(1, user))
            );

        internal static IResponse<ResponseEnvelope> SuccessRetrieveUserResponse(UserRequest user) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.OK, "Usuário recuperado com sucesso."!, 
                    new Users(1, user))
            );
    }
}