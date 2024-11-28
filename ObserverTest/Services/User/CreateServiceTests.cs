using System.Net;
using Moq;
using Observer.Data.Entities;
using Observer.Data.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Services;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace Services.User
{
    public class CreateServiceTests
    {
        [Fact]
        public async void Deve_criar_nova_produto_com_sucesso()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulProductRequest();

            productRepositoryMock.Setup(x => x.InsertProduct(It.IsAny<Products>())).ReturnsAsync(FakeData.SuccessCreateProductServiceResponse(requestData));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.CreateProduct(requestData);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(HttpStatusCode.Created, result.Data.ResponseCode);
        }

        [Fact]
        public async void Deve_tentar_criar_nova_produto_e_retornar_erro_CreateProductError()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulProductRequest();

            productRepositoryMock.Setup(x => x.InsertProduct(It.IsAny<Products>())).ReturnsAsync(FakeData.NoDataFoundCreateProductServiceResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.CreateProduct(requestData);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.InternalServerError, result.Data.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a criação do produto.", result.Data.Details);
        }

        [Fact]
        public async void Deve_tentar_criar_nova_produto_e_receber_excecao_do_banco_de_dados()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var requestData = FakeData.UsefulProductRequest();

            productRepositoryMock.Setup(x => x.InsertProduct(It.IsAny<Products>())).Throws(new Exception("Erro de conexão com o banco de dados"));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.CreateProduct(requestData));
        }
    }
}