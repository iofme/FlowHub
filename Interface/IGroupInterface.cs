using API.Models;

namespace API.Interface
{
    public interface IGroupInterface
    {
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<IEnumerable<Group>> GetGroupsByUser(User user);
        Task<Group> DeleteGroup();
        Task<Group> CreateGroup(Group group);
    }
}