using Moq;
using Observer.Data.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Services;
using SingleLog.Interfaces;

namespace ObserverApi.Services.Subscription
{
    public class DependenceInjectionsTests
    {
        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_sucesso()
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act
            var service = new SubscriptionServices(subscriptionRepositoryMock.Object, singleLogMock.Object);

            // Assert
            Assert.NotNull(service);
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_subscriptionService()
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new SubscriptionServices(null!, singleLogMock.Object));
        }

        [Fact]
        public void Deve_receber_injecoes_de_dependencia_com_erro_no_singleLog()
        {
            // Arrange
            var subscriptionRepositoryMock = new Mock<ISubscriptionRepository>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new SubscriptionServices(subscriptionRepositoryMock.Object, null!));
        }
    }
}
