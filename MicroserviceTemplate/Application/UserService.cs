using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using MicroserviceTemplate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new NotFoundResult();
            }
            return user;
        }

        public async virtual Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async virtual Task<IActionResult> AddProfileToUser(Guid profileId, Guid userId)
        {
            // get permission and profile
            var user = await _context.Users.FindAsync(userId);
            var profile = await _context.Profiles.FindAsync(profileId);

            if(user == null || profile == null)
            {
                return new NotFoundResult();
            }
            
            var profiles = new List<Profile>();
            if (user.Profiles != null)
            {
                profiles.AddRange(user.Profiles);
            }
            profiles.Add(profile);
            user.Profiles = profiles;

            await _context.SaveChangesAsync();
            
            return new OkResult();           
        }

        public async virtual Task<ActionResult> DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            return new NotFoundResult();
        }

        public async Task<ActionResult> UpdateUser(Guid id, User user)
        {
            var oldUser = await _context.Users.FindAsync(id);


            if (oldUser != null)
            {
                oldUser.Name = user.Name;
                oldUser.Email = user.Email;
                oldUser.Profiles = user.Profiles;
                oldUser.LastUpdated = user.LastUpdated;

                await _context.SaveChangesAsync();

                return new OkResult();
            }

            return new NotFoundResult();
        }

    }
}
