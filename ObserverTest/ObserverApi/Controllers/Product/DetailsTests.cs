using System.Net;
using Moq;
using Observer.Controllers;
using Observer.Data.Entities;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Responses;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace ObserverApi.Controllers.Product
{
    public class DetailsTests
    {
        [Theory]
        [InlineData(1)]
        public async Task Deve_realizar_uma_busca_de_produto_com_sucesso(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productServiceMock.Setup(mock => mock.RetrieveProduct(It.IsAny<int>()))
                .ReturnsAsync(FakeData.SuccessRetrieveProductResponse(FakeData.UsefulProductRequest()));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductDetails(productId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.IsType<Products>(values.Data!);
            Assert.Equal(productId, ((Products)values.Data!).Id);
            Assert.Equal(HttpStatusCode.OK, values.ResponseCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Deve_tentar_buscar_produto_com_utilizando_parametros_fora_do_esperado_e_receber_erro(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductDetails(productId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal("Informe um 'productId' válido para a requisição.", values.Details);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
        }


        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_buscar_produto_e_receber_erro_do_servico(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productServiceMock.Setup(mock => mock.RetrieveProduct(It.IsAny<int>()))
                .ReturnsAsync(FakeData.NotFoundErrorProductResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductDetails(productId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.NotFound, values.ResponseCode);
            Assert.Equal("Nenhum produto encontrado. O identificador informado não resultou em dados nesta ação.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_buscar_produto_e_receber_excecao_do_servico(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productServiceMock.Setup(mock => mock.RetrieveProduct(It.IsAny<int>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductDetails(productId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }
    }
}