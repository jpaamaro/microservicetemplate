using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using MicroserviceTemplate.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceTemplate.Application
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }       

        public async Task<IEnumerable<User>> GetUsers()
        {
            var result = await _context.Users
                            .AsNoTracking()
                            .ToListAsync();

            return result;
        }

        public async virtual Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async virtual Task AddProfileToUser(Guid profileId, Guid userId)
        {
            // get permission and profile
            var user = await _context.Users.FindAsync(userId);
            var profile = await _context.Profiles.FindAsync(profileId);

            // add new permission to profile
            if (user != null && profile != null)
            {
                var profiles = new List<Profile>();
                if (user.Profiles != null)
                {
                    profiles.AddRange(user.Profiles);
                }
                profiles.Add(profile);
                user.Profiles = profiles;
            }

            // save changes
            await _context.SaveChangesAsync();
        }

        public async virtual Task DeleteAllUsersAsync()
        {
            foreach (var user in _context.Users)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
        }

    }
}
