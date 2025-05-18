using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.DAL.Models;
using todo.DAL.Repositories.Interfaces;

namespace todo.DAL.Repositories.Realizations
{
    internal class StepRepository : IStepRepository{

        private readonly TodoContext _dbContext;

        public StepRepository(TodoContext dbContext){

            _dbContext = dbContext;
        }
        public async Task DeleteStep(int id)
        {
            var item = await _dbContext.Steps.FindAsync(id);
            if (item != null)
            {
                _dbContext.Steps.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Step>> GetAllSteps()
        {
            return await _dbContext.Steps.ToListAsync();
        }

        public async Task<Step> GetStepById(int id)
        {
            return await _dbContext.Steps.FindAsync(id);
        }
    }
}
