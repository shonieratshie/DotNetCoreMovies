using Dapper.Application.Interfaces;
using Dapper.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Infrastructure.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IConfiguration configuration;
        public MovieRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Movie entity)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", entity.Name);
                parameters.Add("@CategoryID", entity.CategoryID);
                parameters.Add("@Rating", entity.Rating);
                parameters.Add("@DateCreated", DateTime.Now);
                parameters.Add("@Description", "");
                parameters.Add("@Deleted", false);
                parameters.Add("@Category", entity.Category);
                connection.Open();
                var result = await connection.ExecuteAsync("CreateMovie", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var command = new SqlCommand("DeleteMovie", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@MovieID", id);
                var result = await command.ExecuteNonQueryAsync();
                return result;
            }
        }

        public async Task<Movie> UpdateMovieAsync(Movie entity)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@MovieID", entity.MovieID);
            parameters.Add("@Name", entity.Name);
            parameters.Add("@Rating", entity.Rating);
            parameters.Add("@Category", entity.Category);

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Movie>("UpdateMovie", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        } 
       
        public async Task<IReadOnlyList<Movie>> GetAllAsync()
        {
            var movies = new List<Movie>();

            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            using (var command = new SqlCommand("GetAllNonDeletedMovies", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                await connection.OpenAsync();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var movie = new Movie
                        {
                            // map properties from reader to movie object
                            MovieID = (int)reader["MovieID"],
                            Name = (string)reader["Name"],
                            CategoryID = (int)reader["CategoryID"],
                            Rating = (int)reader["Rating"],
                            DateCreated = (DateTime)reader["DateCreated"],
                            Description = (string)reader["Description"],
                            Deleted = (bool)reader["Deleted"],
                            Category = (string)reader["Category"]
                        };

                        movies.Add(movie);
                    }
                }
            }

            return movies;
        }




        public async Task<Movie> GetByNameAsync(string name)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Name", name);
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Movie>("GetMoviesByName", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Movie> GetByIDAsync(int id)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MovieID", id);
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Movie>("GetMovieById", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<Movie> GetByRatingIDAsync(int id)
        {
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Rating", id);
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Movie>("GetMoviesByRating", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
