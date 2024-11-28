using System.Net;
using Moq;
using Observer.Data.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Services;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace ObserverApi.Services.User
{
    public class RetrieveServiceTests
    {
        [Theory]
        [InlineData(1)]
        public async void Deve_realizar_busca_de_produto_com_sucesso(int productId)
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productRepositoryMock.Setup(x => x.SelectProduct(It.IsAny<int>())).ReturnsAsync(FakeData.SuccessRetrieveProductServiceResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.RetrieveProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, result.Data.ResponseCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Deve_tentar_realizar_busca_de_produto_e_retornar_erro_notFound(int productId)
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productRepositoryMock.Setup(x => x.SelectProduct(It.IsAny<int>())).ReturnsAsync(FakeData.NotFoundRetrieveProductServiceResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.RetrieveProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.Data.ResponseCode);
            Assert.Equal("Nenhum produto encontrado. O identificador informado não resultou em dados nesta ação.", result.Data.Details);
        }

        [Theory]
        [InlineData(1)]
        public async void Deve_tentar_realizar_busca_de_produto_e_receber_excecao_do_banco_de_dados(int productId)
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productRepositoryMock.Setup(x => x.SelectProduct(It.IsAny<int>())).Throws(new Exception("Erro de conexão com o banco de dados"));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.RetrieveProduct(productId));
        }
    }
}