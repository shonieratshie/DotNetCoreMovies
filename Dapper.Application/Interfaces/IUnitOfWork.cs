﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
       // ICategoryRepository Categories { get; }
    }
}
