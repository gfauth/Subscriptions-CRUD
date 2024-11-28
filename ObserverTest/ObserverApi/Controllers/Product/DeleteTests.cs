using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Observer.Controllers;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Responses;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace ObserverApi.Controllers.Product
{
    public class DeleteTests
    {
        [Theory]
        [InlineData(1)]
        public async Task Deve_deletar_novo_produto_com_sucesso(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productServiceMock.Setup(mock => mock.DeleteProduct(It.IsAny<int>())).ReturnsAsync(FakeData.SuccessDeleteProductResponse(productId));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductDelete(productId);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.OK, values.ResponseCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Deve_tentar_deletar_produto_e_receber_erro_de_validacao_de_identificador(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productServiceMock.Setup(mock => mock.DeleteProduct(It.IsAny<int>())).ReturnsAsync(FakeData.NotFoundErrorProductResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductDelete(productId);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um 'productId' válido para a requisição.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_deletar_produto_e_receber_erro_interno_do_servico(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productServiceMock.Setup(mock => mock.DeleteProduct(It.IsAny<int>())).ReturnsAsync(FakeData.NotFoundErrorProductResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductDelete(productId);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.NotFound, values.ResponseCode);
            Assert.Equal("Nenhum produto encontrado. O identificador informado não resultou em dados nesta ação.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_deletar_produto_e_receber_excecao_do_servico(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productServiceMock.Setup(mock => mock.DeleteProduct(It.IsAny<int>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductDelete(productId);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);
        }
    }
}