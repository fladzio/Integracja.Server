﻿using AutoMapper;
using Integracja.Server.Infrastructure.DTO;
using Integracja.Server.Web.Models.Shared.Question;

namespace Integracja.Server.Web.Mappers
{
    public static class AutoMapperWebConfig
    {
        public static IMapper Initialize()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AnswerModel, AnswerDto>();
                cfg.CreateMap<QuestionModel, QuestionAdd>();
            })
            .CreateMapper();
        }
    }
}
