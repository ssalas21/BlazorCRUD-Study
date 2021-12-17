using BlazorCRUD.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCRUD.Data.Dapper.Repositories {
    public class FilmRepository : IFilmRepository {

        private string ConnectionString;

        public FilmRepository(string connectionString) {
            ConnectionString = connectionString;
        }

        protected SqlConnection dbConnection() {
            return new SqlConnection(ConnectionString);
        }
        
        public Task<bool> DeleteFilm(int id) {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Film>> GetAllFilms() {
            var db = dbConnection();
            var sql = @"SELECT * FROM Films";
            return await db.QueryAsync<Film>(sql.ToString(), new { });
        }

        public async Task<Film> GetFilmDetails(int id) {
            var db = dbConnection();
            var sql = @"SELECT * FROM Films WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Film>(sql.ToString(), new { id });
        }

        public async Task<bool> InsertFilm(Film film) {
            var db = dbConnection();
            var sql = @"INSERT INTO Films (Title, Director, ReleaseDate) VALUES (@Title, @Director, @ReleaseDate)";
            var result = await db.ExecuteAsync(sql.ToString(), new { film.Title, film.Director, film.ReleaseDate });
            return result > 0;
        }

        public async Task<bool> UpdateFilm(Film film) {
            var db = dbConnection();
            var sql = @"UPDATE Films SET Title = @Title, Director = @Director, ReleaseDate = @ReleaseDate WHERE Id = @Id";
            var result = await db.ExecuteAsync(sql.ToString(), new { film.Title, film.Director, film.ReleaseDate, film.Id });
            return result > 0;
        }
    }
}
