using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using todo.BLL.Services.Interfaces;
using todo.Contracts.DTOs.StepDTOs;
using todo.DAL.Repositories.Interfaces;
using todo.Models.Models;

namespace todo.BLL.Services.Realizations
{
    public class StepService : IStepService

    {

        private readonly IStepRepository _repository;
        private readonly IMapper _mapper;

        public StepService(IStepRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<IEnumerable<StepResponse>> GetAllStep(int itemId)
        {
            var result = await _repository.GetAllSteps(itemId);
            return _mapper.Map<IEnumerable<StepResponse>>(result);


        }

        public async Task<StepResponse?> GetStep(int itemId, int id)
        {
            try
            {
                var step = await _repository.GetStepById(itemId, id);
                return _mapper.Map<StepResponse>(step);
            }
            catch (Exception ex)
            {
                throw new Exception("BLL error:" + ex.Message, ex);
            }
        }

        public async Task DeleteStep(int itemId, int id)
        {
            try
            {
                await _repository.DeleteStep(id);
            }
            catch (Exception ex)
            {
                throw new Exception("BLL error:" + ex.Message, ex);
            }
        }

        public async Task<StepResponse> CreateStep(int itemId, StepRequest request)
        {

            if (request.title.Length > 30)
            {
                throw new ArgumentException("The title must be no more than 30 characters long ");
            }
            try
            {
                var step = _mapper.Map<Step>(request);
                step.TodoItemId = itemId;
                var result = await _repository.AddStep(step);
                return _mapper.Map<StepResponse>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("BLL error: " + ex.Message, ex);
            }

        }
    }
}
