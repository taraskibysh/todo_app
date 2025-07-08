using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using todo.BLL.Services.Interfaces;
using todo.Contracts.DTOs.StepDTOs;
using todo.WebApi.Controllers;
using Xunit;

namespace todo.Tests.Controllers
{
    public class StepControllerTests
    {
        private readonly Mock<IStepService> _mockService;
        private readonly StepController _controller;

        public StepControllerTests()
        {
            _mockService = new Mock<IStepService>();
            _controller = new StepController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllStep_ReturnsOkResult_WithListOfSteps()
        {
            int itemId = 1;
            var steps = new List<StepResponse> { new StepResponse(), new StepResponse() };
            _mockService.Setup(s => s.GetAllStep(itemId)).ReturnsAsync(steps);
            
            var result = await _controller.GetAllStep(itemId);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(steps, okResult.Value);
        }

        [Fact]
        public async Task GetStep_ReturnsOkResult_WhenStepExists()
        {
            int itemId = 1;
            int stepId = 1;
            var step = new StepResponse { id = stepId };
            _mockService.Setup(s => s.GetStep(itemId, stepId)).ReturnsAsync(step);
            
            var result = await _controller.GetStep(itemId, stepId);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(step, okResult.Value);
        }

        [Fact]
        public async Task GetStep_ReturnsNotFound_WhenStepDoesNotExist()
        {
            int itemId = 1;
            int stepId = 999;
            _mockService.Setup(s => s.GetStep(itemId, stepId)).ReturnsAsync((StepResponse?)null);
            
            var result = await _controller.GetStep(itemId, stepId);
            
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteStep_ReturnsNoContent()
        {
            int itemId = 1;
            int stepId = 1;
            _mockService.Setup(s => s.DeleteStep(itemId, stepId)).Returns(Task.CompletedTask);
            
            var result = await _controller.DeleteStep(itemId, stepId);
            
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task CreateStep_ReturnsOkResult_WithCreatedStep()
        {
            int itemId = 1;
            var request = new StepRequest { title = "Test Step" };
            var response = new StepResponse { id = 1, Title = "Test Step" };
            _mockService.Setup(s => s.CreateStep(itemId, request)).ReturnsAsync(response);
            
            var result = await _controller.CreateStep(itemId, request);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(response, okResult.Value);
        }
    }
}
