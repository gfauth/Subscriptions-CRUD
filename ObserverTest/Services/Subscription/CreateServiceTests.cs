using System.Net;
using Moq;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Services;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace Services.Subscription
{
    public class CreateServiceTests
    {
        [Fact]
        public async void Deve_criar_nova_subscricao_com_sucesso()
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulSubscriptionRequest();

            subscriptionRepositoryMock.Setup(x => x.InsertSubscription(It.IsAny<Subscriptions>()))
                .ReturnsAsync(FakeData.SuccessCreateSubscriptionServiceResponse(requestData));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.CreateSubscription(requestData);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, result.Data.ResponseCode);
        }

        [Fact]
        public async void Deve_tentar_criar_nova_subscricao_e_retornar_erro_CreateSubscriptionError()
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulSubscriptionRequest();

            subscriptionRepositoryMock.Setup(x => x.InsertSubscription(It.IsAny<Subscriptions>()))
                .ReturnsAsync(FakeData.NoDataFoundCreateSubscriptionServiceResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.CreateSubscription(requestData);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.Data.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a criação da subscrição.", result.Data.Details);
        }

        [Fact]
        public async void Deve_tentar_criar_nova_subscricao_e_receber_excecao_do_banco_de_dados()
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulSubscriptionRequest();

            subscriptionRepositoryMock.Setup(x => x.InsertSubscription(It.IsAny<Subscriptions>())).Throws(new Exception("Erro de conexão com o banco de dados"));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.CreateSubscription(requestData));
        }
    }
}
