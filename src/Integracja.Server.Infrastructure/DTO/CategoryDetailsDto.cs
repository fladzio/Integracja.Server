﻿using System.Collections.Generic;

namespace Integracja.Server.Infrastructure.DTO
{
    public class CategoryDetailsDto : CategoryDto
    {
        public IEnumerable<QuestionShortDto> Questions { get; set; }
    }
}
