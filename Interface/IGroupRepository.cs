using API.Models;

namespace API.Interface
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<IEnumerable<Group>> GetGroupsByUser(User user);
        Task<Group> DeleteGroup(int id);
        Task<Group> GetGroupById(int id);
        Task<Group> CreateGroup(Group group);
        Task<Group> AddUserByGroup(User user, Group group);
    }
}