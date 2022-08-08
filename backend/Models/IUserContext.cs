using Microsoft.AspNetCore.Mvc;
namespace backend.Models
{
    public interface IUserContext
    {
        public Task<IEnumerable<User>> GetUsers();

        public Task<User> AddUser(UserDto user);
    }    
}