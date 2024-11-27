using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Observer.Controllers;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Responses;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace Controllers.User
{
    public class DeleteTests
    {
        [Theory]
        [InlineData(1)]
        public async Task Deve_deletar_novo_usuario_com_sucesso(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            userServiceMock.Setup(mock => mock.DeleteUser(It.IsAny<int>())).ReturnsAsync(FakeData.SuccessDeleteUserResponse(userId));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserDelete(userId);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.OK, values.ResponseCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Deve_tentar_deletar_usuario_e_receber_erro_de_validacao_de_identificador(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            userServiceMock.Setup(mock => mock.DeleteUser(It.IsAny<int>())).ReturnsAsync(FakeData.NotFoundErrorUserResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserDelete(userId);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um 'userId' válido para a requisição.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_deletar_usuario_e_receber_erro_interno_do_servico(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            userServiceMock.Setup(mock => mock.DeleteUser(It.IsAny<int>())).ReturnsAsync(FakeData.NotFoundErrorUserResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserDelete(userId);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.NotFound, values.ResponseCode);
            Assert.Equal("Nenhum usuário encontrado. O identificador informado não resultou em dados nesta ação.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_deletar_usuario_e_receber_excecao_do_servico(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            userServiceMock.Setup(mock => mock.DeleteUser(It.IsAny<int>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserDelete(userId);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);
        }
    }
}