using DomainLibrary.Interfaces.Services;
using DomainLibrary.Models.LogModels;
using Moq;
using Observer.Controllers;
using SingleLog.Interfaces;

namespace ObserverApi.Controllers.User
{
    public class DependenceInjectionsTests
    {
        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_sucesso()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act
            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_userService()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UserController(null!, singleLogMock.Object));
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_singleLog()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UserController(userServiceMock.Object, null!));
        }
    }
}