using System.Net;
using Moq;
using Observer.Controllers;
using Observer.Data.Entities;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Responses;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace Controllers.User
{
    public class DetailsTests
    {
        [Theory]
        [InlineData(1)]
        public async Task Deve_realizar_uma_busca_de_usuario_com_sucesso(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            userServiceMock.Setup(mock => mock.RetrieveUser(It.IsAny<int>()))
                .ReturnsAsync(FakeData.SuccessRetrieveUserResponse(FakeData.UsefulUserRequest()));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserDetails(userId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.IsType<Users>(values.Data!);
            Assert.Equal(userId, ((Users)values.Data!).Id);
            Assert.Equal(HttpStatusCode.OK, values.ResponseCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Deve_tentar_buscar_usuario_com_utilizando_parametros_fora_do_esperado_e_receber_erro(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserDetails(userId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal("Informe um 'userId' válido para a requisição.", values.Details);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
        }


        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_buscar_usuario_e_receber_erro_do_servico(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            userServiceMock.Setup(mock => mock.RetrieveUser(It.IsAny<int>()))
                .ReturnsAsync(FakeData.NotFoundErrorUserResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserDetails(userId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.NotFound, values.ResponseCode);
            Assert.Equal("Nenhum usuário encontrado. O identificador informado não resultou em dados nesta ação.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_buscar_usuario_e_receber_excecao_do_servico(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            userServiceMock.Setup(mock => mock.RetrieveUser(It.IsAny<int>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserDetails(userId);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }
    }
}