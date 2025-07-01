using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ListRepository(AppDbContext context) : IListRepository
    {
        public async Task<IEnumerable<List>> GetListAsync()
        {
            return await context.List.ToListAsync();
        }
    }
}