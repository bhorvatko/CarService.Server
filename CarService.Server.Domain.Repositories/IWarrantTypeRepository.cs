﻿using CarService.Server.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Server.Domain.Repositories
{
    public interface IWarrantTypeRepository : IRepository<WarrantType>
    {
        Task<WarrantType> GetWarratTypeWithSteps(int id);
    }
}
