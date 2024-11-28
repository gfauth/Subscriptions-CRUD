using System.Net;
using Moq;
using Observer.Data.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.ResponsesEnvelope;
using Observer.Services;
using SingleLog.Interfaces;

namespace Services.User
{
    public class DeleteServiceTests
    {
        [Theory]
        [InlineData(1)]
        public async void Deve_deletar_produto_com_sucesso(int productId)
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productRepositoryMock.Setup(x => x.DeleteProduct(It.IsAny<int>())).ReturnsAsync(new ResponseOk<bool>(true));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.DeleteProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(HttpStatusCode.OK, result.Data.ResponseCode);
        }

        [Theory]
        [InlineData(1)]
        public async void Deve_tentar_deletar_produto_e_retornar_erro_notFound(int productId)
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productRepositoryMock.Setup(x => x.DeleteProduct(It.IsAny<int>())).ReturnsAsync(new ResponseError<bool>(false));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act
            var result = await service.DeleteProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(HttpStatusCode.NotFound, result.Data.ResponseCode);
            Assert.Equal("Nenhum produto encontrado. O identificador informado não resultou em dados nesta ação.", result.Data.Details);
        }

        [Theory]
        [InlineData(1)]
        public async void Deve_tentar_deletar_produto_e_receber_excecao_do_banco_de_dados(int productId)
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            productRepositoryMock.Setup(x => x.DeleteProduct(It.IsAny<int>())).Throws(new Exception("Erro de conexão com o banco de dados"));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<Exception>(async () => await service.DeleteProduct(productId));
        }
    }
}