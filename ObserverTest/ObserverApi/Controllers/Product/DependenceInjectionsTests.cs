using Moq;
using Observer.Controllers;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using SingleLog.Interfaces;

namespace ObserverApi.Controllers.Product
{
    public class DependenceInjectionsTests
    {
        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_sucesso()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act
            var controller = new ProductController(productServiceMock.Object, singleLogMock.Object);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_productService()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new ProductController(null!, singleLogMock.Object));
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_singleLog()
        {
            // Arrange
            var productServiceMock = new Mock<IProductServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new ProductController(productServiceMock.Object, null!));
        }
    }
}