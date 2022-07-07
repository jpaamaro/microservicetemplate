using MicroserviceTemplate.Application.Interfaces;
using MicroserviceTemplate.Domain;
using MicroserviceTemplate.Infrastructure;
using Microsoft.AspNetCore.Mvc;
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

        /*
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
        */

        public async virtual Task<ActionResult> DeleteProfileAsync(Guid id)
        {
            var profile = await _context.Profiles.FindAsync(id);

            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
                return new OkResult();
            }
            return new NotFoundResult();
        }

        public async Task<ActionResult<Profile>> GetById(Guid id)
        {
            var profile = await _context.Profiles.FindAsync(id);          
            if(profile == null)
            {
                return new NotFoundResult();
            }
            return profile;
        }

        public async Task<ActionResult> UpdateProfile(Guid id, Profile profile)
        {
            var oldProfile = await _context.Profiles.FindAsync(id);


            if(oldProfile != null)
            {
                oldProfile.Name = profile.Name;
                oldProfile.Permissions = profile.Permissions;
                oldProfile.LastUpdated = profile.LastUpdated;

                await _context.SaveChangesAsync();

                return new OkResult();
            }

            return new NotFoundResult();
        }
    }
}
