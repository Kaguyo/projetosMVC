using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using firstProject.Models;


namespace firstProject.Data
{
    public class UserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public  UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = "SELECT * FROM users"; // Certifique-se de que o nome da tabela está correto.
            var users = await connection.QueryAsync<User>(query);
            List<UserDTO> listaUserDto = new();
            
            foreach(var user in users)
            {
                UserDTO userDto = new();
                userDto.Id = user.Id;
                userDto.Nome = user.Nome;
                userDto.Email = user.Email;

                listaUserDto.Add(userDto);
            }

            return listaUserDto;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = "SELECT * FROM users WHERE id = @Id";
            var user = await connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
            UserDTO userDto = new();
            if (user != null)
            {
                userDto.Id = user.Id;
                userDto.Nome = user.Nome;
                userDto.Email = user.Email;
            }

            return userDto;
        }

        public async Task<int> AddAsync(User user)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = "INSERT INTO users (nome, email, hashSenha) VALUES (@Nome, @Email, @HashSenha)";
            return await connection.ExecuteAsync(query, user);
        }

        public async Task<int> UpdateAsync(UserDTO user)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = "UPDATE users SET nome = @Nome, email = @Email, hashSenha = @HashSenha WHERE id = @Id";
            return await connection.ExecuteAsync(query, user);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = "DELETE FROM users WHERE id = @Id";
            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}