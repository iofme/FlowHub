using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class GroupRepository(AppDbContext context) : IGroupRepository
    {
        public async Task<Group> AddUserByGroup(User user, Group group)
        {
            var groupSelect = await context.Groups.Include(g => g.User).FirstOrDefaultAsync(g => g.Id == group.Id);

            groupSelect.User.Add(user);

            await context.SaveChangesAsync();
            
            return groupSelect;
        }

        public async Task<Group> CreateGroup(Group group)
        {
            await context.Groups.AddAsync(group);
            await context.SaveChangesAsync();

            return group;
        }

        public async Task<Group> DeleteGroup(int id)
        {
            var groupDeleted = await context.Groups.FirstOrDefaultAsync(g => g.Id == id);
            context.Groups.Remove(groupDeleted!);
            await context.SaveChangesAsync();

            return groupDeleted!;
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await context.Groups.Include(g => g.User).Include(g => g.Listas).ToListAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await context.Groups.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Group>> GetGroupsByUser(User user)
        {
            return await context.Groups.Include(g => g.User.Contains(user)).ToListAsync();
        }

        
    }
}