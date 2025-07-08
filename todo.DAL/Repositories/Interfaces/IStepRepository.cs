using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using todo.Models.Models;

namespace todo.DAL.Repositories.Interfaces
{
    public interface IStepRepository
    {
        public Task<IEnumerable<Step>> GetAllSteps(int itemId);

        public Task<Step> GetStepById(int itemId, int id);

        public Task DeleteStep(int id);

        public Task<Step> AddStep(Step step);
    }
}
