using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using todo.BLL.Services.Interfaces;
using todo.Contracts.DTOs.StepDTOs;
using todo.Contracts.DTOs.TodoItemDTOs;
using todo.DAL.Repositories.Interfaces;
using todo.Models.Models;

namespace todo.BLL.Services.Realizations
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;
        private IMapper _mapper;

        public TodoItemService(ITodoItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TodoItemResponse>> GetAll()
        {

            try
            {
                var items = await _repository.GetAllTodoItems();

                var result = _mapper.Map<IEnumerable<TodoItemResponse>>(items);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("BLL error:" + ex.Message, ex);
            }
        }

        public async Task<TodoItemResponse?> GetTodoItem(int id)
        {

            try
            {
                var item = _mapper.Map<TodoItemResponse>(await _repository.GetTodoItem(id));
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception("BLL error: " + ex.Message, ex);

            }
        }

        public async Task DeleteTodoItem(int id)
        {
           
            await _repository.DeleteById(id);
        }

        public async Task<TodoItemResponse> CreateTodoItem(TodoItemRequest item)
        {

            if (item.Title.Length > 50 || item.Title == null)
            {
                throw new ArgumentException("The title must be no more than 50 characters long");
            }
            if (item.Description.Length > 200)
            {
                throw new ArgumentException("The description must be no more than 200 characters long");
            }

            try
            {
                var newItem = _mapper.Map<TodoItem>(item);

                var result = await _repository.CreateTodoItem(newItem);

                return _mapper.Map<TodoItemResponse>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("BLL error: "  + ex.Message, ex);
            }
        }

        public async Task<TodoItemResponse> UpdateTodoItem(int id, TodoItemRequest item)
        {

            if (item.Title.Length > 50 || item.Title == null)
            {
                throw new ArgumentException("The title must be no more than 50 characters long and not null");
            }
            if (item.Description.Length > 200)
            {
                throw new ArgumentException("The description must be no more than 200 characters long");
            }

            try
            {
                var mappedModel = _mapper.Map<TodoItem>(item);
                mappedModel.Id = id;
                var result = await _repository.UpdateTodoItem(mappedModel);
                return _mapper.Map<TodoItemResponse>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("BLL error:" + ex.Message, ex);
            }
        }
    }
}