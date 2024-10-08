﻿using ProductTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTracker.Application.Interfaces
{
    public  interface IOrganizationRepository :  IRepository<Organization>
    {
        Task<string> GetDataBase(string alias);
        Task<IReadOnlyList<Organization>> GetOrganizationByUserId( string userId);
    }
}
