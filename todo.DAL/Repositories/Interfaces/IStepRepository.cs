using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using todo.DAL.Models;

namespace todo.DAL.Repositories.Interfaces
{
    public interface IStepRepository
    {
        public Task<IEnumerable<Step>> GetAllSteps();

        public Task<Step> GetStepById(int id);

        public Task DeleteStep(int id);
    }
}
