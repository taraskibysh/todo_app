using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.DAL.DbContexts;
using todo.Models.Models;
using todo.Models.Models;
using todo.DAL.Repositories.Interfaces;

namespace todo.DAL.Repositories.Realizations
{
    public class StepRepository : IStepRepository{

        private readonly TodoContext _dbContext;

        public StepRepository(TodoContext dbContext){

            _dbContext = dbContext;
        }
        public async Task DeleteStep( int id)
        {
            var item = await _dbContext.Steps.FirstOrDefaultAsync(i => i.Id == id );
            if (item != null)
            {
                _dbContext.Steps.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Step>> GetAllSteps(int itemId)
        {
            return await _dbContext.Steps.Where(i => i.TodoItem.Id == itemId).ToListAsync();
        }

        public async Task<Step?> GetStepById(int itemId, int id)
        {
            return await _dbContext.Steps.FirstOrDefaultAsync(s => s.Id == id && s.TodoItem.Id == itemId);
        }

        public async Task<Step> AddStep( Step step)
        {

            
            var todoItem = _dbContext.TodoItems
                .Include(t => t.Steps)
                .FirstOrDefault(t => t.Id == step.TodoItemId);

             var result = _dbContext.Steps.Add(step);

            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
    }
}
