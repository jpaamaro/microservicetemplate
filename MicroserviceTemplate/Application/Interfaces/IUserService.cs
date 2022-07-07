using MicroserviceTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.Application.Interfaces
{
    public interface IUserService
    {
        public Task AddUser(User user);
        public Task<IEnumerable<User>> GetUsers();
        public Task<IActionResult> AddProfileToUser(Guid profileId, Guid userId);
        public Task<ActionResult> DeleteUserAsync(Guid id);
        public Task<ActionResult> UpdateUser(Guid id, User user);
        public Task<ActionResult<User>> GetById(Guid id);
    }
}