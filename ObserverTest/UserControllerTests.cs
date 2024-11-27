using Moq;
using Observer.Controllers;
using Observer.Data.Entities;
using Observer.Domain.Interfaces;
using Observer.Domain.Models.LogModels;
using Observer.Domain.Models.Requests;
using Observer.Domain.Models.Responses;
using ObserverTest.Data;
using SingleLog.Interfaces;
using System.Net;

namespace ObserverTest
{
    public class UserControllerTests
    {
        [Fact]
        public void Will_receive_dependeces_into_constructor_successful()
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
        public void Will_receive_userService_dependece_null_constructor_and_throw_exception()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UserController(null!, singleLogMock.Object));
        }

        [Fact]
        public void Will_receive_singleLog_dependece_null_constructor_and_throw_exception()
        {
            // Arrange
            var userServiceMock = new Mock<IUserServices>();
            var singleLogMock = new Mock<ISingletonLogger<LogModel>>();

            // Act and Assert
            Assert.Throws<ArgumentNullException>(() => new UserController(userServiceMock.Object, null!));
        }

        [Fact]
        public async Task Will_create_new_user_successful()
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
        public async Task Will_create_new_user_and_got_password_validation_error()
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
        public async Task Will_create_new_user_and_got_login_validation_error()
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
        public async Task Will_create_new_user_and_got_bithdate_validation_error()
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
        public async Task Will_create_new_user_and_got_lastname_validation_error()
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
        public async Task Will_create_new_user_and_got_name_validation_error()
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

        [Theory]
        [InlineData(1)]
        public async Task Will_retrieve_existent_user_successful(int userId)
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
            Assert.Equal(1, ((Users)values.Data!).Id);
            Assert.Equal(HttpStatusCode.OK, values.ResponseCode);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Will_try_retrieve_existent_user_with_id_zero_or_negatrive(int userId)
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
    }
}