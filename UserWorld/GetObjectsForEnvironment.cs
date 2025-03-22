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
}
