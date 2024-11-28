using System.Net;
using DomainLibrary.Entities;
using DomainLibrary.Interfaces.Services;
using DomainLibrary.Models.LogModels;
using DomainLibrary.Models.Requests;
using DomainLibrary.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Observer.Controllers;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace ObserverApi.Controllers.User
{
    public class EditTests
    {
        [Theory]
        [InlineData(1)]
        public async Task Deve_editar_novo_usuario_com_sucesso(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.UsefulUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessEditUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.IsType<Users>(values.Data!);
            Assert.Equal(1, ((Users)values.Data!).Id);
            Assert.Equal(HttpStatusCode.OK, values.ResponseCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_usuario_e_receber_erro_de_validacao_de_senha(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.PasswordUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessEditUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Password precisa conter ao menos 8 dígitos, conter números e caracteres especiais para o usuário.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_usuario_e_receber_erro_de_validacao_de_login(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.LoginUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessEditUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Login precisa conter ao menos 5 dígitos para o usuário.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_usuario_e_receber_erro_de_validacao_de_data_de_aniversario(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.BurthdateUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessEditUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe uma data de nascimento válida para o usuário. Apenas maiores de 18 anos.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_usuario_e_receber_erro_de_validacao_de_sobrenome(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.LastNameUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessEditUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um sobrenome válido para o usuário.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_usuario_e_receber_erro_de_validacao_de_nome(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.NameUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessEditUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um nome válido para o usuário.", values.Details);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Deve_tentar_editar_usuario_e_receber_erro_de_validacao_de_identificador(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.NameUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessEditUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um 'userId' válido para a requisição.", values.Details);
        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_usuario_e_receber_erro_interno_do_servico(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.UsefulUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.NotFoundErrorUserResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.NotFound, values.ResponseCode);
            Assert.Equal("Nenhum usuário encontrado. O identificador informado não resultou em dados nesta ação.", values.Details);

        }

        [Theory]
        [InlineData(1)]
        public async Task Deve_tentar_editar_e_receber_excecao_do_servico(int userId)
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.UsefulUserRequest();

            userServiceMock.Setup(mock => mock.UpdateUser(It.IsAny<int>(), It.IsAny<UserRequest>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserEdit(userId, userRequest);

            // Assert
            var values = (ResponseEnvelope)((ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }
    }
}