using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using todo.Contracts.DTOs.StepDTOs;
using todo.Models.Models;

namespace todo.BLL.Mappers
{
    public class StepMapper : Profile
    {
        public StepMapper()
        {
            CreateMap<StepRequest, Step>();
            CreateMap<Step, StepResponse>();
        }

    }
}
