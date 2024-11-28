using Moq;
using Observer.Controllers;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using SingleLog.Interfaces;

namespace ObserverApi.Controllers.Subscription
{
    public class DependenceInjectionsTests
    {
        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_sucesso()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act
            var controller = new SubscriptionController(subscriptionServiceMock.Object, singleLogMock.Object);

            // Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_subscriptionService()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new SubscriptionController(null!, singleLogMock.Object));
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_singleLog()
        {
            // Arrange
            var subscriptionServiceMock = new Mock<ISubscriptionServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new SubscriptionController(subscriptionServiceMock.Object, null!));
        }
    }
}