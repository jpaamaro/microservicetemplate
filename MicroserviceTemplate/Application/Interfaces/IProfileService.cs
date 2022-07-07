using MicroserviceTemplate.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTemplate.Application.Interfaces
{
    public interface IProfileService
    {
        public Task AddProfile(Profile profile);
        public Task<IEnumerable<Profile>> GetProfiles();
        public Task<ActionResult<Profile>> GetById(Guid id);
        //public Task AddPermissionToProfile(Guid profileId, Guid permissionId);
        public Task<ActionResult> DeleteProfileAsync(Guid id);
        public Task<ActionResult> UpdateProfile(Guid id, Profile profile);
    }
}