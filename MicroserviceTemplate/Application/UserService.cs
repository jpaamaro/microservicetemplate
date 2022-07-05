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

        /*
        const string RAIN = "Rain";
        
       
        
        public async Task<IEnumerable<Incident>> GetRainyIncidents()
        {
            return await _context.Incidents
                            .Where(x => x.Summary == RAIN)
                            .OrderBy(message => message.Summary)
                            .AsNoTracking()
                            .ToListAsync();
        }

        public Incident GetById(Guid id)
        {
            return _context.Incidents
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public async Task SaveNew(Incident item)
        {
            _context.Incidents.Add(item);
            await _context.SaveChangesAsync();
        }
        */

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
        /*
        public async virtual Task DeleteAllMessagesAsync()
        {
            foreach (var message in _context.Incidents)
            {
                _context.Incidents.Remove(message);
            }

            await _context.SaveChangesAsync();
        }

        public async virtual Task DeleteMessageAsync(int id)
        {
            var message = await _context.Incidents.FindAsync(id);

            if (message != null)
            {
                _context.Incidents.Remove(message);
                await _context.SaveChangesAsync();
            }
        }
        */
    }
}
