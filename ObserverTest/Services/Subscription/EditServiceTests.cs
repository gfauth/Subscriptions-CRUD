using System.Net;
using Moq;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.ResponsesEnvelope;
using Observer.Services;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace Services.Subscription
{
    public class EditServiceTests
    {
        [Theory]
        [InlineData(1)]
        public async void Deve_editar_subscricao_com_sucesso(int subscriptionId)
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulSubscriptionRequest();

            subscriptionRepositoryMock.Setup(x => x.UpdateSubscription(It.IsAny<Subscriptions>())).ReturnsAsync(new ResponseOk<bool>(true));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.UpdateSubscription(subscriptionId, requestData);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, result.Data.ResponseCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Deve_tentar_editar_subscricao_e_retornar_erro_notFound(int subscriptionId)
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulSubscriptionRequest();

            subscriptionRepositoryMock.Setup(x => x.UpdateSubscription(It.IsAny<Subscriptions>())).ReturnsAsync(new ResponseError<bool>(false));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.UpdateSubscription(subscriptionId, requestData);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.Data.ResponseCode);
            Assert.Equal("Nenhuma subscrição encontrada. O identificador informado não resultou em dados nesta ação.", result.Data.Details);
        }

        [Theory]
        [InlineData(1)]
        public async void Deve_tentar_editar_subscricao_e_recber_excecao_do_banco_de_dados(int subscriptionId)
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulSubscriptionRequest();

            subscriptionRepositoryMock.Setup(x => x.UpdateSubscription(It.IsAny<Subscriptions>())).Throws(new Exception("Erro de conexão com o banco de dados"));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.UpdateSubscription(subscriptionId, requestData));
        }
    }
}