﻿using UniversityApi.Models;

namespace UniversityApi.Service
{
    public interface IUniversityService
    {
        ExampleModel Example();
        IEnumerable<UniversityVM> GetList();
    }
}
