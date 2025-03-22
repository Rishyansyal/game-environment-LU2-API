using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Repositories;
using Microsoft.Extensions.Logging;

namespace WebApi.Tests.Controllers
{
    [TestClass]
    public class EnvironmentControllerTest
    {
        [TestMethod]
        public async Task Add_AddEnvironmentToUser_ReturnsCreatedAtAction()
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
    }
    [TestClass]
    public class Create2dObjectTest
    {
        [TestMethod]
        public async Task Add_AddObjectToEnvironment_ReturnsCreatedAtAction()
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
}
