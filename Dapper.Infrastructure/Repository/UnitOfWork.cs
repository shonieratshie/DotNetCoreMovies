using Dapper.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IMovieRepository movieRepository)//), ICategoryRepository categoryRepository)
        {
            Movies = movieRepository;
            //Categories = categoryRepository;
        }
        public IMovieRepository Movies { get; }
       // public ICategoryRepository Categories { get; }
    }
}
