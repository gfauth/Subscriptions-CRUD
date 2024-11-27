using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Observer.Controllers;
using Observer.Data.Entities;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace Controllers.Product
{
    public class CreateTests
    {
        [Fact]
        public async Task Deve_criar_novo_produto_com_sucesso()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.UsefulProductRequest();

            productServiceMock.Setup(mock => mock.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductCreate(productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.IsType<Products>(values.Data!);
            Assert.Equal(1, ((Products)values.Data!).Id);
            Assert.Equal(HttpStatusCode.Created, values.ResponseCode);
        }

        [Fact]
        public async Task Deve_tentar_criar_produto_e_receber_erro_de_validacao_de_categoria()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.CategoryProductRequest();

            productServiceMock.Setup(mock => mock.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductCreate(productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe uma categoria válida para o produto.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_produto_e_receber_erro_de_validacao_de_descricao()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.DescriptionProductRequest();

            productServiceMock.Setup(mock => mock.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductCreate(productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Description precisa conter ao menos 3 dígitos.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_produto_e_receber_erro_de_validacao_de_estoque()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.StockProductRequest();

            productServiceMock.Setup(mock => mock.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductCreate(productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Não é possível cadastrar um produto com estoque inferior a 1 ou superior a 9999.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_produto_e_receber_erro_de_validacao_de_nome()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.NameProductRequest();

            productServiceMock.Setup(mock => mock.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateProductResponse(productRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductCreate(productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um nome válido para o produto.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_produto_e_receber_erro_interno_do_servico()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.UsefulProductRequest();

            productServiceMock.Setup(mock => mock.CreateProduct(It.IsAny<ProductRequest>()))
                .ReturnsAsync(FakeData.InternalServerErrorProductResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductCreate(productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }

        [Fact]
        public async Task Deve_tentar_criar_produto_e_receber_excecao_do_servico()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var productRequest = FakeData.UsefulProductRequest();

            productServiceMock.Setup(mock => mock.CreateProduct(It.IsAny<ProductRequest>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.ProductCreate(productRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);
        }
    }
}