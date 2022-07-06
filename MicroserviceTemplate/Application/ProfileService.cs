using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using MicroserviceTemplate.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceTemplate.Application
{
    public class ProfileService : IProfileService
    {
        private ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }      

        public async Task<IEnumerable<Profile>> GetProfiles()
        {
            var result = await _context.Profiles
                            .AsNoTracking()
                            .ToListAsync();

            return result;
        }

        public async virtual Task AddProfile(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
        }

        public async virtual Task AddPermissionToProfile(Guid profileId, Guid permissionId)
        {
            // get permission and profile
            var permission = await _context.Permissions.FindAsync(permissionId);
            var profile = await _context.Profiles.FindAsync(profileId);

            // add new permission to profile
            if (permission != null && profile != null) {
                var permissions = new List<Permission>();
                if (profile.Permissions != null)
                {
                    permissions.AddRange(profile.Permissions);
                }
                permissions.Add(permission);
                profile.Permissions = permissions;
            }

            // save changes
            await _context.SaveChangesAsync();
        }

        public async virtual Task DeleteAllProfilesAsync()
        {
            foreach (var profile in _context.Profiles)
            {
                _context.Profiles.Remove(profile);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Profile?> GetById(Guid id)
        {
            return await _context.Profiles.FindAsync(id);              
        }
    }
}
