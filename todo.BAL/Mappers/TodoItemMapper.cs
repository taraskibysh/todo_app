using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using todo.Models.Models;
using todo.Contracts.DTOs.TodoItemDTOs;

namespace todo.BLL.Mappers
{
    public class TodoItemMapper : Profile
    {

        public TodoItemMapper()
        {
            CreateMap<TodoItemRequest, TodoItem>();
            CreateMap<TodoItem, TodoItemResponse>();
        }
    }
}
