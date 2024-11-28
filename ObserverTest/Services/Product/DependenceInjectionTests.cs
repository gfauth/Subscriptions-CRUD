using Moq;
using Observer.Data.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Services;
using SingleLog.Interfaces;

namespace Services.Product
{
    public class DependenceInjectionsTests
    {
        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_sucesso()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act
            var service = new ProductServices(productRepositoryMock.Object, singleLogMock.Object);

            // Assert
            Assert.NotNull(service);
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_productService()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new ProductServices(null!, singleLogMock.Object));
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_singleLog()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new ProductServices(productRepositoryMock.Object, null!));
        }
    }
}
