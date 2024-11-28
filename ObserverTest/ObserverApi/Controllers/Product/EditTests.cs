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

namespace ObserverApi.Controllers.Product
{
    public class EditTests
    {
        [Theory]
        [InlineData(1)]
        public async Task Deve_editar_novo_produto_com_sucesso(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.UsefulProductRequest();

            productServiceMock.Setup(mock => mock.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessEditProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductEdit(productId, productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.IsType<Products>(values.Data!);
            Assert.Equal(1, ((Products)values.Data!).Id);
            Assert.Equal(HttpStatusCode.OK, values.ResponseCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_produto_e_receber_erro_de_validacao_de_categoria(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.CategoryProductRequest();

            productServiceMock.Setup(mock => mock.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessEditProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductEdit(productId, productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe uma categoria válida para o produto.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_produto_e_receber_erro_de_validacao_de_descricao(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.DescriptionProductRequest();

            productServiceMock.Setup(mock => mock.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessEditProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductEdit(productId, productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Description precisa conter ao menos 3 dígitos.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_produto_e_receber_erro_de_validacao_de_data_de_estoque(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.StockProductRequest();

            productServiceMock.Setup(mock => mock.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessEditProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductEdit(productId, productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Não é possível cadastrar um produto com estoque inferior a 1 ou superior a 9999.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_produto_e_receber_erro_de_validacao_de_nome(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.NameProductRequest();

            productServiceMock.Setup(mock => mock.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessEditProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductEdit(productId, productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um nome válido para o produto.", values.Details);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Deve_tentar_editar_produto_e_receber_erro_de_validacao_de_identificador(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.NameProductRequest();

            productServiceMock.Setup(mock => mock.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessEditProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductEdit(productId, productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um 'productId' válido para a requisição.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_produto_e_receber_erro_interno_do_servico(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.UsefulProductRequest();

            productServiceMock.Setup(mock => mock.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.NotFoundErrorProductResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductEdit(productId, productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.NotFound, values.ResponseCode);
            Assert.Equal("Nenhum produto encontrado. O identificador informado não resultou em dados nesta ação.", values.Details);

        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_e_receber_excecao_do_servico(int productId)
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.UsefulProductRequest();

            productServiceMock.Setup(mock => mock.UpdateProduct(It.IsAny<int>(), It.IsAny<ProductRequest>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductEdit(productId, productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }
    }
}