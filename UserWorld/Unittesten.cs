using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace WebApi.Tests.Controllers
{


        [TestClass]
        public class EnvironmentControllerTest
        {
            [TestMethod] // wereld aanmaken
            public async Task Add_AddEnvironmentToUser_ReturnsCreatedAtAction() // Werled aanmaken
        {
                // Arrange
                var userId = Guid.NewGuid().ToString();
                var newEnvironment = new Environment2d { UserId = userId };
                var mockRepository = new Mock<IEnvironment2DRepository>();
                var mockLogger = new Mock<ILogger<EnvironmentObjectsController>>();
                var mockAuthenticationService = new Mock<IAuthenticationService>();
                mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);
                mockRepository.Setup(repo => repo.AddWorldAsync(newEnvironment)).Returns(Task.CompletedTask);
                var controller = new EnvironmentObjectsController(mockRepository.Object, mockLogger.Object, mockAuthenticationService.Object);

                // Act
                var result = await controller.Add(newEnvironment);

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
                var createdAtActionResult = result as CreatedAtActionResult;
                Assert.IsNotNull(createdAtActionResult);
                Assert.AreEqual(nameof(controller.Get), createdAtActionResult.ActionName);
            }

            [TestMethod]
            public async Task Get_ReturnsOkResult_WithListOfEnvironmentsForUser() // lijst met werelden voor user
        {
                // Arrange
                var userId = "testUser";
                var environments = new List<Environment2d>
            {
                new Environment2d { Id = 1, Name = "Environment1", UserId = userId },
                new Environment2d { Id = 2, Name = "Environment2", UserId = userId }
            };
                var mockRepository = new Mock<IEnvironment2DRepository>();
                var mockLogger = new Mock<ILogger<EnvironmentObjectsController>>();
                var mockAuthenticationService = new Mock<IAuthenticationService>();
                mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);
                mockRepository.Setup(repo => repo.GetAllEnvironment2DsAsync(userId)).ReturnsAsync(environments);
                var controller = new EnvironmentObjectsController(mockRepository.Object, mockLogger.Object, mockAuthenticationService.Object);

                // Act
                var result = await controller.Get();

                // Assert
                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                var okResult = result.Result as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.IsInstanceOfType(okResult.Value, typeof(IEnumerable<Environment2d>));
                var returnedEnvironments = okResult.Value as IEnumerable<Environment2d>;
                Assert.AreEqual(environments.Count, returnedEnvironments.Count());
                Assert.IsTrue(returnedEnvironments.All(e => e.UserId == userId));
            }

            [TestMethod]
            public async Task Get_ReturnsUnauthorized_WhenUserNotAuthenticated() // geen user ingelogd
        {
                // Arrange
                var mockRepository = new Mock<IEnvironment2DRepository>();
                var mockLogger = new Mock<ILogger<EnvironmentObjectsController>>();
                var mockAuthenticationService = new Mock<IAuthenticationService>();
                mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns((string)null);
                var controller = new EnvironmentObjectsController(mockRepository.Object, mockLogger.Object, mockAuthenticationService.Object);

                // Act
                var result = await controller.Get();

                // Assert
                Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
            }
        }
    



[TestClass]
    public class Create2dObjectTest
    {
        [TestMethod]
        public async Task Add_AddObjectToEnvironment_ReturnsCreatedAtAction() // object aanmaken in wereld
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var newObject = new Object2d { EnvironmentId = 1 };
            var mockRepository = new Mock<IObject2DRepository>();
            var mockLogger = new Mock<ILogger<Object2dController>>();
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            mockAuthenticationService.Setup(x => x.GetCurrentAuthenticatedUserId()).Returns(userId);
            mockRepository.Setup(repo => repo.AddObject2DAsync(newObject)).Returns(Task.CompletedTask);
            var controller = new Object2dController(mockRepository.Object, mockLogger.Object);

            // Act
            var result = await controller.Add(1, newObject);
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
            var createdAtActionResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdAtActionResult);
            Assert.AreEqual(nameof(controller.Get), createdAtActionResult.ActionName);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        }
    }
    [TestClass]
    public class AccountControllerTests 
    {
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly AccountController _controller;

        public AccountControllerTests() 
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            _controller = new AccountController(_userManagerMock.Object);
        }

        [TestMethod]
        public async Task Register_ReturnsOk_WhenRegistrationIsSuccessful()// can a user be created?
        {
            // Arrange
            var model = new RegisterModel
            {
                
                Email = "testuser@example.com",
                Password = "StrongPassword123!"
            };
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), model.Password))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.Register(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

    }
    }


    //[TestClass]
    //public class GetObjectsForEnvironmentTest
    //{
    //    [TestMethod]
    //    public async Task Get_ObjectsForEnvironment_ReturnsOk()
    //    {
    //        // Arrange
    //        var mockRepository = new Mock<IObject2DRepository>();
    //        var mockLogger = new Mock<ILogger<Object2dController>>();
    //        mockRepository.Setup(repo => repo.GetAllObject2DsAsync()).ReturnsAsync(new Object2d[] { });
    //        var controller = new Object2dController(mockRepository.Object, mockLogger.Object);
    //        // Act
    //        var result = await controller.Get(1);
    //        // Assert
    //        Assert.IsNotNull(result);
    //        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
    //    }
    //}

