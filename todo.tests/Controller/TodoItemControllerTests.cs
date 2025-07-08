using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using todo.BLL.Services.Interfaces;
using todo.Contracts.DTOs.TodoItemDTOs;
using todo.WebApi.Controllers;
using Xunit;

namespace todo.tests.Controller
{
    public class TodoItemControllerTests
    {
        private readonly Mock<ITodoItemService> _serviceMock;
        private readonly TodoItemController _controller;

        public TodoItemControllerTests()
        {
            _serviceMock = new Mock<ITodoItemService>();
            _controller = new TodoItemController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllItem_ReturnsOkResult_WithListOfItems()
        {
            var expectedItems = new List<TodoItemResponse>
            {
                new TodoItemResponse { Id = 1, Title = "Task 1" },
                new TodoItemResponse { Id = 2, Title = "Task 2" }
            };

            _serviceMock.Setup(s => s.GetAll()).ReturnsAsync(expectedItems);


            var result = await _controller.GetAllItem();
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<TodoItemResponse>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetItem_ExistingId_ReturnsOkResult_WithItem()
        {
            var item = new TodoItemResponse { Id = 1, Title = "Sample Task" };
            _serviceMock.Setup(s => s.GetTodoItem(1)).ReturnsAsync(item);
            
            var result = await _controller.GetItem(1);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TodoItemResponse>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task GetItem_NonExistingId_ReturnsNotFound()
        {
            _serviceMock.Setup(s => s.GetTodoItem(It.IsAny<int>())).ReturnsAsync((TodoItemResponse)null);

            var result = await _controller.GetItem(99);
            
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateItem_ValidRequest_ReturnsOkResult_WithCreatedItem()
        {
            
            var request = new TodoItemRequest { Title = "New Task" };
            var response = new TodoItemResponse { Id = 3, Title = "New Task" };

            _serviceMock.Setup(s => s.CreateTodoItem(request)).ReturnsAsync(response);
            
            var result = await _controller.CreateItem(request);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TodoItemResponse>(okResult.Value);
            Assert.Equal("New Task", returnValue.Title);
        }

        [Fact]
        public async Task UpdateItem_ValidRequest_ReturnsOkResult_WithUpdatedItem()
        {
            var request = new TodoItemRequest { Title = "Updated Task" };
            var response = new TodoItemResponse { Id = 1, Title = "Updated Task" };

            _serviceMock.Setup(s => s.UpdateTodoItem(1, request)).ReturnsAsync(response);
            
            var result = await _controller.UpdateItem(1, request);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TodoItemResponse>(okResult.Value);
            Assert.Equal("Updated Task", returnValue.Title);
        }

        [Fact]
        public async Task DeleteItem_ValidId_ReturnsNoContent()
        {
            _serviceMock.Setup(s => s.DeleteTodoItem(1)).Returns(Task.CompletedTask);
            
            var result = await _controller.DeleteItem(1);
            
            Assert.IsType<NoContentResult>(result);
        }
    }
}
