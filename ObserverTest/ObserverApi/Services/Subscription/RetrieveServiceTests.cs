using System.Net;
using DomainLibrary.Interfaces.Repositories;
using DomainLibrary.Models.LogModels;
using Moq;
using Observer.Services;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace ObserverApi.Services.Subscription
{
    public class RetrieveServiceTests
    {
        [Theory]
        [InlineData(1)]
        public async void Deve_realizar_busca_de_subscricao_com_sucesso(int subscriptionId)
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            subscriptionRepositoryMock.Setup(x => x.SelectOneSubscription(It.IsAny<int>()))
                .ReturnsAsync(FakeData.SuccessRetrieveSubscriptionServiceResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.RetrieveSubscription(subscriptionId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, result.Data.ResponseCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Deve_tentar_realizar_busca_de_subscricao_e_retornar_erro_notFound(int subscriptionId)
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            subscriptionRepositoryMock.Setup(x => x.SelectOneSubscription(It.IsAny<int>()))
                .ReturnsAsync(FakeData.NotFoundRetrieveSubscriptionServiceResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.RetrieveSubscription(subscriptionId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.Data.ResponseCode);
            Assert.Equal("Nenhuma subscrição encontrada. O identificador informado não resultou em dados nesta ação.", result.Data.Details);
        }

        [Theory]
        [InlineData(1)]
        public async void Deve_tentar_realizar_busca_de_subscricao_e_receber_excecao_do_banco_de_dados(int subscriptionId)
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            subscriptionRepositoryMock.Setup(x => x.SelectOneSubscription(It.IsAny<int>())).Throws(new Exception("Erro de conexão com o banco de dados"));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.RetrieveSubscription(subscriptionId));
        }
    }
}