using MicroserviceTemplate.Domain;

namespace MicroserviceTemplate.Application.Interfaces
{
    public interface IUserService
    {
        public Task AddUser(User user);
        public Task<IEnumerable<User>> GetUsers();
    }
}