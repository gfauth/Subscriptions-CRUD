using System.Net;
using Moq;
using Observer.Controllers;
using Observer.Data.Entities;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using ObserverApiTest.Data;
using SingleLog.Interfaces;

namespace Controllers.User
{
    public class CreateTests
    {
        [Fact]
        public async Task Deve_criar_novo_usuario_com_sucesso()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.UsefulUserRequest();

            userServiceMock.Setup(mock => mock.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserCreate(userRequest);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.IsType<Users>(values.Data!);
            Assert.Equal(1, ((Users)values.Data!).Id);
            Assert.Equal(HttpStatusCode.Created, values.ResponseCode);
        }

        [Fact]
        public async Task Deve_tentar_criar_usuario_e_receber_erro_de_validacao_de_senha()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.PasswordUserRequest();

            userServiceMock.Setup(mock => mock.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserCreate(userRequest);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Password precisa conter ao menos 8 dígitos, conter números e caracteres especiais para o usuário.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_usuario_e_receber_erro_de_validacao_de_login()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.LoginUserRequest();

            userServiceMock.Setup(mock => mock.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserCreate(userRequest);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Login precisa conter ao menos 5 dígitos para o usuário.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_usuario_e_receber_erro_de_validacao_de_data_de_aniversario()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.BurthdateUserRequest();

            userServiceMock.Setup(mock => mock.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserCreate(userRequest);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe uma data de nascimento válida para o usuário. Apenas maiores de 18 anos.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_usuario_e_receber_erro_de_validacao_de_sobrenome()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.LastNameUserRequest();

            userServiceMock.Setup(mock => mock.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserCreate(userRequest);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um sobrenome válido para o usuário.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_usuario_e_receber_erro_de_validacao_de_nome()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.NameUserRequest();

            userServiceMock.Setup(mock => mock.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.SuccessCreateUserResponse(userRequest));

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserCreate(userRequest);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Null(values.Data);
            Assert.Equal(HttpStatusCode.BadRequest, values.ResponseCode);
            Assert.Equal("Informe um nome válido para o usuário.", values.Details);
        }

        [Fact]
        public async Task Deve_tentar_criar_usuario_e_receber_erro_interno_do_servico()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.UsefulUserRequest();

            userServiceMock.Setup(mock => mock.CreateUser(It.IsAny<UserRequest>()))
                .ReturnsAsync(FakeData.InternalServerErrorUserResponse());

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserCreate(userRequest);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }

        [Fact]
        public async Task Deve_tentar_criar_usuario_e_receber_excecao_do_servico()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            var userRequest = FakeData.UsefulUserRequest();

            userServiceMock.Setup(mock => mock.CreateUser(It.IsAny<UserRequest>())).Throws<Exception>();

            singleLogMock.Setup(mock => mock.WriteLogAsync(It.IsAny<LogModel>()));
            singleLogMock.Setup(mock => mock.CreateBaseLogAsync()).ReturnsAsync(new LogModel());
            singleLogMock.Setup(mock => mock.GetBaseLogAsync()).ReturnsAsync(new LogModel());

            var controller = new UserController(userServiceMock.Object, singleLogMock.Object);

            // Act
            var result = await controller.UserCreate(userRequest);

            // Assert
            var values = (ResponseEnvelope)((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result!).Value!;

            Assert.NotNull(values);
            Assert.Equal(HttpStatusCode.InternalServerError, values.ResponseCode);
            Assert.Equal("Ocorreu um erro durante a execução da requisição.", values.Details);

        }
    }
}