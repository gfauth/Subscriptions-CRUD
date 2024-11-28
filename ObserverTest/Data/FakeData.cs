using Observer.Data.Entities;
using Observer.Domain.Models.Errors;
using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using Observer.Domain.ResponsesEnvelope;
using System.Net;

namespace ObserverApiTest.Data
{
    internal static class FakeData
    {
        internal static UserRequest UsefulUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), "01234567890", "tester", "D3f4u1t0aa@");

        internal static UserRequest PasswordUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), string.Empty, "tester", "12345");

        internal static UserRequest LoginUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2000, 5, 15), string.Empty, "usr", "D3f4u1t0aaa@");

        internal static UserRequest BurthdateUserRequest() =>
            new UserRequest("Fulano", "Blevers", new DateTime(2020, 5, 15), string.Empty, "tester", "D3f4u1t0aa@");

        internal static UserRequest LastNameUserRequest() =>
            new UserRequest("Fulano", "Bu", new DateTime(2000, 5, 15), string.Empty, "tester", "D3f4u1t0aa@");

        internal static UserRequest NameUserRequest() =>
            new UserRequest("Ba", "Blevers", new DateTime(2000, 5, 15), string.Empty, "tester", "D3f4u1t0aa@");

        internal static ProductRequest UsefulProductRequest() => new ProductRequest("New Toy", "Blevers toy blevers", 15, "Toys");

        internal static ProductRequest NameProductRequest() => new ProductRequest(string.Empty, "Blevers toy blevers", 15, "Toys");

        internal static ProductRequest DescriptionProductRequest() => new ProductRequest("New Toy", string.Empty, 15, "Toys");

        internal static ProductRequest StockProductRequest() => new ProductRequest("New Toy", "Blevers toy blevers", -15, "Toys");

        internal static ProductRequest CategoryProductRequest() => new ProductRequest("New Toy", "Blevers toy blevers", 15, "2");
        
        internal static SubscriptionRequest UsefulSubscriptionRequest() => new SubscriptionRequest(1, 1);

        internal static IResponse<ResponseEnvelope> SuccessCreateUserResponse(UserRequest user) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.Created, $"Usuário {user.Name} criado com sucesso."!,
                    new Users(1, user))
            );

        internal static IResponse<ResponseEnvelope> SuccessEditUserResponse(UserRequest user) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.OK, $"Dados do usuário {user.Name} foram alterados com sucesso."!,
                    new Users(1, user))
            );

        internal static IResponse<ResponseEnvelope> SuccessRetrieveUserResponse(UserRequest user) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.OK, "Usuário recuperado com sucesso."!,
                    new Users(1, user))
            );

        internal static IResponse<ResponseEnvelope> SuccessDeleteUserResponse(int userId) =>
            new ResponseOk<ResponseEnvelope>(new ResponseEnvelope(HttpStatusCode.OK, $"Usuário {userId} foi deletado com sucesso."!));

        internal static IResponse<ResponseEnvelope> SuccessCreateProductResponse(ProductRequest product) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.Created, $"Produto {product.Name} criado com sucesso."!,
                    new Products(1, product))
            );

        internal static IResponse<ResponseEnvelope> SuccessEditProductResponse(ProductRequest product) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.OK, $"Dados do produto {product.Name} foram alterados com sucesso."!,
                    new Products(1, product))
            );

        internal static IResponse<ResponseEnvelope> SuccessRetrieveProductResponse(ProductRequest product) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.OK, "Produto recuperado com sucesso."!,
                    new Products(1, product))
            );

        internal static IResponse<ResponseEnvelope> SuccessDeleteProductResponse(int productId) =>
            new ResponseOk<ResponseEnvelope>(new ResponseEnvelope(HttpStatusCode.OK, $"Produto {productId} foi deletado com sucesso."!));

        internal static IResponse<ResponseEnvelope> SuccessCreateSubscriptionResponse(int subscriptionId, SubscriptionRequest subscriptionRequest) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.Created, $"Subscrição {subscriptionId} criada com sucesso."!,
                    new Subscriptions(subscriptionId, subscriptionRequest))
            );

        internal static IResponse<ResponseEnvelope> SuccessRetrieveSubscriptionResponse(SubscriptionRequest subscription) =>
            new ResponseOk<ResponseEnvelope>(
                new ResponseEnvelope(HttpStatusCode.OK, "Subscrição recuperada com sucesso."!,
                    new Subscriptions(1, subscription))
            );

        internal static IResponse<ResponseEnvelope> SuccessDeleteSubscriptionResponse(int subscriptionId) =>
            new ResponseOk<ResponseEnvelope>(new ResponseEnvelope(HttpStatusCode.OK, $"Subscrição {subscriptionId} foi deletada com sucesso."!));

        internal static IResponse<ResponseEnvelope> InternalServerErrorUserResponse() =>
            new ResponseError<ResponseEnvelope>(UserResponseErrors.InternalServerError);

        internal static IResponse<ResponseEnvelope> NotFoundErrorUserResponse() =>
            new ResponseError<ResponseEnvelope>(UserResponseErrors.UserNotFound);

        internal static IResponse<ResponseEnvelope> InternalServerErrorProductResponse() =>
            new ResponseError<ResponseEnvelope>(ProductResponseErrors.InternalServerError);

        internal static IResponse<ResponseEnvelope> NotFoundErrorProductResponse() =>
            new ResponseError<ResponseEnvelope>(ProductResponseErrors.ProductNotFound);

        internal static IResponse<ResponseEnvelope> InternalServerErrorSubscriptionResponse() =>
            new ResponseError<ResponseEnvelope>(SubscriptionResponseErrors.InternalServerError);

        internal static IResponse<ResponseEnvelope> NotFoundErrorSubscriptionResponse() =>
            new ResponseError<ResponseEnvelope>(SubscriptionResponseErrors.SubscriptionNotFound);



        internal static IResponse<Subscriptions> SuccessCreateSubscriptionServiceResponse(SubscriptionRequest subscriptionRequest) =>
            new ResponseOk<Subscriptions>(new Subscriptions(1, subscriptionRequest));

        internal static IResponse<Subscriptions> SuccessRetrieveSubscriptionServiceResponse() =>
            new ResponseOk<Subscriptions>(new Subscriptions(1, new SubscriptionRequest(1, 1)));

        internal static IResponse<Subscriptions> NoDataFoundCreateSubscriptionServiceResponse() => new ResponseError<Subscriptions>("No data found.");

        internal static IResponse<Subscriptions> NotFoundRetrieveSubscriptionServiceResponse() => 
            new ResponseError<Subscriptions>("Nenhuma subscrição encontrada. O identificador informado não resultou em dados nesta ação.");



        internal static IResponse<Products> SuccessCreateProductServiceResponse(ProductRequest productRequest) =>
            new ResponseOk<Products>(new Products(1, productRequest));

        internal static IResponse<Products> SuccessRetrieveProductServiceResponse() =>
            new ResponseOk<Products>(new Products());

        internal static IResponse<Products> NoDataFoundCreateProductServiceResponse() => new ResponseError<Products>("No data found.");

        internal static IResponse<Products> NotFoundRetrieveProductServiceResponse() =>
            new ResponseError<Products>("Nenhum produto encontrado. O identificador informado não resultou em dados nesta ação.");
    }
}