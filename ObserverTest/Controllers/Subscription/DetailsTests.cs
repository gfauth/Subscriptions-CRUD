using System.Net;
using Moq;
using Observer.Controllers;
using Observer.Data.Entities;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Responses;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace Controllers.Subscription
{
    public class DetailsTests
    {
        [Theory]
        [InlineData(1)]
        public async Task Deve_realizar_uma_busca_de_subscricao_com_sucesso(int subscriptionId)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            subscriptionServiceMock.Setup(mock => mock.RetrieveSubscription(It.IsAny<int>()))
                .ReturnsAsync(FakeData.SuccessRetrieveSubscriptionResponse(FakeData.UsefulSubscriptionRequest()));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.SubscriptionDetails(subscriptionId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.IsType<Subscriptions>(values.Data!);
            Assert.Equal(subscriptionId, ((Subscriptions)values.Data!).Id);
            Assert.Equal(HttpStatusCode.OK, values.ResponseCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Deve_tentar_buscar_subscricao_com_utilizando_parametros_fora_do_esperado_e_receber_erro(int subscriptionId)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.SubscriptionDetails(subscriptionId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal("Informe um 'subscriptionId' válido para a requisição.", values.Details);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
        }


        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_buscar_subscricao_e_receber_erro_do_servico(int subscriptionId)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            subscriptionServiceMock.Setup(mock => mock.RetrieveSubscription(It.IsAny<int>()))
                .ReturnsAsync(FakeData.NotFoundErrorSubscriptionResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.SubscriptionDetails(subscriptionId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.NotFound, values.ResponseCode);
            Assert.Equal("Nenhuma subscrição encontrada. O identificador informado não resultou em dados nesta ação.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_buscar_subscricao_e_receber_excecao_do_servico(int subscriptionId)
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            subscriptionServiceMock.Setup(mock => mock.RetrieveSubscription(It.IsAny<int>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.SubscriptionDetails(subscriptionId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }
    }
}