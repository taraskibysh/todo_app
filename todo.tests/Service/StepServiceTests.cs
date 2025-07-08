using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Moq;
using todo.BLL.Services.Realizations;
using todo.Contracts.DTOs.StepDTOs;
using todo.DAL.Repositories.Interfaces;
using todo.Models.Models;

namespace todo.tests.Service
{
    public class StepServiceTests
    {

        private readonly Mock<IStepRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly StepService _service;

        public StepServiceTests()
        {
            _mockRepo = new Mock<IStepRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new StepService(_mockRepo.Object, _mockMapper.Object);
        }


        [Fact]
        public async Task GetAllStep_Valid_ReturnMappedList()
        {
            int id = 1;
            var item = new List<Step> { new Step() { Id = 1, Title = "step task", IsDone = false } };
            var response = new List<StepResponse> { new StepResponse() {id = 1, Title = "step task", IsDone = false } };

            _mockRepo.Setup(r => r.GetAllSteps(id)).ReturnsAsync(item);
            _mockMapper.Setup(m => m.Map<IEnumerable<StepResponse>>(item)).Returns(response);

            var result = await _service.GetAllStep(id);


            Assert.Equal(response, result);
        }

        [Fact]
        public async Task GetStep_Valid_ReturnStepResponce()
        {

            int id = 1;
            int itemId = 1;
            var item = new Step { Id = 1, IsDone = false, Title = "title", TodoItemId = itemId };
            var responce = new StepResponse { id = 1, IsDone = false, Title = "title" };

            _mockRepo.Setup(r => r.GetStepById(itemId, id)).ReturnsAsync(item);
            _mockMapper.Setup(m => m.Map<StepResponse>(item)).Returns(responce);

            var result = await _service.GetStep(itemId, id);

            Assert.NotNull(result);
            Assert.Equal(result, responce);

        }

        [Fact]
        public async Task GetStep_Valid_ReturnNothing()
        {

            int id = 1;
            int itemId = 1;

            _mockRepo.Setup(r => r.GetStepById(itemId, id)).ReturnsAsync((Step)null);

            var result = await _service.GetStep(itemId, id);

            Assert.Null(result);

        }

        [Fact]
        public async Task DeleteStep_Valid_CallsRepository()
        {
            await _service.DeleteStep(1, 1);

            _mockRepo.Verify(r => r.DeleteStep(1), Times.Once );


        }


        public async Task CreateStep_Valid_ReturnStepResponce()
        {
            int itemId = 1;
            var request = new StepRequest { IsDone = false, title = "title" };
            var item = new Step { Id = 1, IsDone = false, Title = "title", TodoItemId = 1 };
            var responce = new StepResponse { id = 1, IsDone = false, Title = "title", TodoItemId = 1 };

            _mockRepo.Setup(r => r.AddStep(item)).ReturnsAsync(item);
            _mockMapper.Setup(m => m.Map<Step>(request)).Returns(item);
            _mockMapper.Setup(m => m.Map<StepResponse>(item)).Returns(responce);

            var result = await _service.CreateStep(itemId, request);


            Assert.Equal(result, responce);
            ;
        }

        [Fact]
        public async Task CreateStep_WithInvalidTitle_ThrowArgumentException()
        {
            int itemId = 1;
            var request = new StepRequest { IsDone = false, title = new string('a', 31) };
            var item = new Step { Id = 1, IsDone = false, Title = new string('a', 31), TodoItemId = 1 };
            var responce = new StepResponse { id = 1, IsDone = false, Title = new string('a', 31), TodoItemId = 1 };

            _mockRepo.Setup(r => r.AddStep(item)).ReturnsAsync(item);
            _mockMapper.Setup(m => m.Map<Step>(request)).Returns(item);
            _mockMapper.Setup(m => m.Map<StepResponse>(item)).Returns(responce);

            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateStep(itemId, request));
        }
    }
}
