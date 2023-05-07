using Dapper.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<int> DeleteAsync(int id);
        Task<T> GetByNameAsync(string name);
        Task<T> GetByIDAsync(int name);
        Task<T> GetByRatingIDAsync(int id);

        Task<T> UpdateMovieAsync(Movie movie);

    }
}
