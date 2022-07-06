using MicroserviceTemplate.Domain;

namespace MicroserviceTemplate.Application.Interfaces
{
    public interface IProfileService
    {
        public Task AddProfile(Profile profile);
        public Task<IEnumerable<Profile>> GetProfiles();
        public Task<Profile?> GetById(Guid id);
        public Task AddPermissionToProfile(Guid profileId, Guid permissionId);
        public Task DeleteAllProfilesAsync();
    }
}