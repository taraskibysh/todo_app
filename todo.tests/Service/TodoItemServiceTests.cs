using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using todo.BLL.Services.Realizations;
using todo.Contracts.DTOs.TodoItemDTOs;
using todo.DAL.Repositories.Interfaces;
using todo.Models.Models;
using Xunit;

namespace todo.tests.Services
{
    public class TodoItemServiceTests
    {
        private readonly Mock<ITodoItemRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TodoItemService _service;

        public TodoItemServiceTests()
        {
            _mockRepo = new Mock<ITodoItemRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new TodoItemService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsMappedList()
        {

            var items = new List<TodoItem> { new TodoItem { Id = 1, Title = "Test", Description = "Desc" } };
            var expected = new List<TodoItemResponse> { new TodoItemResponse { Id = 1, Title = "Test", Description = "Desc" } };

            _mockRepo.Setup(r => r.GetAllTodoItems()).ReturnsAsync(items);
            _mockMapper.Setup(m => m.Map<IEnumerable<TodoItemResponse>>(items)).Returns(expected);

            var result = await _service.GetAll();


            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetTodoItem_ReturnsMappedItem()
        {

            var item = new TodoItem { Id = 1, Title = "Test", Description = "Desc" };
            var response = new TodoItemResponse { Id = 1, Title = "Test", Description = "Desc" };

            _mockRepo.Setup(r => r.GetTodoItem(1)).ReturnsAsync(item);
            _mockMapper.Setup(m => m.Map<TodoItemResponse>(item)).Returns(response);


            var result = await _service.GetTodoItem(1);

            Assert.NotNull(result);
            Assert.Equal(response, result);
        }

        [Fact]
        public async Task DeleteTodoItem_CallsRepository()
        {
            await _service.DeleteTodoItem(1);

            _mockRepo.Verify(r => r.DeleteById(1), Times.Once);
        }

        [Fact]
        public async Task CreateTodoItem_WithInvalidTitle_Throws()
        {
            var item = new TodoItemRequest { Title = new string('a', 51), Description = new string('a', 100) };

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateTodoItem(item));
        }

        [Fact]
        public async Task CreateTodoItem_WithInvalidDescription_Throws()
        {
            var item = new TodoItemRequest { Title = "Title", Description = new string('a', 201) };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateTodoItem(item));
        }

        [Fact]
        public async Task CreateTodoItem_Valid_ReturnsResponse()
        {
            var request = new TodoItemRequest { Title = "Valid", Description = new string('a', 100) };
            var mappedModel = new TodoItem { Title = "Valid", Description = new string('a', 100) };
            var createdModel = new TodoItem { Id = 1, Title = "Valid", Description = new string('a', 100) };
            var response = new TodoItemResponse { Id = 1, Title = "Valid", Description = new string('a', 100) };

            _mockMapper.Setup(m => m.Map<TodoItem>(request)).Returns(mappedModel);
            _mockRepo.Setup(r => r.CreateTodoItem(mappedModel)).ReturnsAsync(createdModel);
            _mockMapper.Setup(m => m.Map<TodoItemResponse>(createdModel)).Returns(response);

            var result = await _service.CreateTodoItem(request);

            Assert.Equal(response, result);
        }

        [Fact]
        public async Task UpdateTodoItem_WithInvalidTitle_Throws()
        {
            var item = new TodoItemRequest { Title = new string('a', 51), Description = new string('a', 100) };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateTodoItem(1, item));
        }

        [Fact]
        public async Task UpdateTodoItem_Valid_ReturnsMappedResponse()
        {
            var request = new TodoItemRequest { Title = "Valid", Description = new string('a', 100) };
            var mapped = new TodoItem { Id = 1, Title = "Valid", Description = new string('a', 100) };
            var updated = new TodoItem { Id = 1, Title = "Valid", Description = new string('a', 100) };
            var response = new TodoItemResponse { Id = 1, Title = "Valid", Description = new string('a', 100) };

            _mockMapper.Setup(m => m.Map<TodoItem>(request)).Returns(mapped);
            _mockRepo.Setup(r => r.UpdateTodoItem(mapped)).ReturnsAsync(updated);
            _mockMapper.Setup(m => m.Map<TodoItemResponse>(updated)).Returns(response);

            var result = await _service.UpdateTodoItem(1, request);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
    }
}
