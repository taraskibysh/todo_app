using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.Contracts.DTOs.StepDTOs;

namespace todo.BLL.Services.Interfaces
{
    public interface IStepService
    {
        public Task<IEnumerable<StepResponse>> GetAllStep(int itemId);

        public Task<StepResponse?> GetStep(int itemId, int id);

        public Task DeleteStep(int itemId,int id);

        public Task<StepResponse> CreateStep(int itemId, StepRequest request);

    }
}
