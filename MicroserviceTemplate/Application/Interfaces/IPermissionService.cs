using MicroserviceTemplate.Domain;

namespace MicroserviceTemplate.Application.Interfaces
{
    public interface IPermissionService
    {
        public Task AddPermission(Permission permission);
        public Task<IEnumerable<Permission>> GetPermissions();
        public Task DeleteAllPermissionsAsync();
    }
}