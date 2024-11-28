using System.Net;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces.Services;
using DomainLibrary.Models.LogModels;
using DomainLibrary.Models.Requests;
using DomainLibrary.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Observer.Controllers;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace ObserverApi.Controllers.Subscription
{
    public class CreateTests
    {
        [Fact]
        public async Task Deve_criar_nova_subscricao_com_sucesso()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var subscriptionRequest = FakeData.UsefulSubscriptionRequest();

            subscriptionServiceMock.Setup(mock => mock.CreateSubscription(It.IsAny<SubscriptionRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateSubscriptionResponse(1, subscriptionRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.SubscriptionCreate(subscriptionRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.IsType<Subscriptions>(values.Data!);
            Assert.Equal(1, ((Subscriptions)values.Data!).Id);
            Assert.Equal(HttpStatusCode.Created, values.ResponseCode);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 0)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(-1, -1)]
        public async Task Deve_tentar_criar_subscricao_e_receber_erro_de_validacao_de_identificadores(int productId, int userId)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var subscriptionRequest = new SubscriptionRequest(productId, userId);

            subscriptionServiceMock.Setup(mock => mock.CreateSubscription(It.IsAny<SubscriptionRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateSubscriptionResponse(1, subscriptionRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.SubscriptionCreate(subscriptionRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um productId ou userId válido para a subscrição.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_subscricao_e_receber_erro_interno_do_servico()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var subscriptionRequest = FakeData.UsefulSubscriptionRequest();

            subscriptionServiceMock.Setup(mock => mock.CreateSubscription(It.IsAny<SubscriptionRequest>()))
                .ReturnsAsync(FakeData.InternalServerErrorSubscriptionResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.SubscriptionCreate(subscriptionRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }

        [Fact]
        public async Task Deve_tentar_criar_subscricao_e_receber_excecao_do_servico()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var subscriptionRequest = FakeData.UsefulSubscriptionRequest();

            subscriptionServiceMock.Setup(mock => mock.CreateSubscription(It.IsAny<SubscriptionRequest>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.SubscriptionCreate(subscriptionRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);
        }
    }
}