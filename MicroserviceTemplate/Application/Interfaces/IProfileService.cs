using MicroserviceTemplate.Domain;

namespace MicroserviceTemplate.Application.Interfaces
{
    public interface IProfileService
    {
        public Task AddProfile(Profile profile);
        public Task<IEnumerable<Profile>> GetProfiles();
    }
}