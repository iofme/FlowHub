using API.Data;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ListRepository(AppDbContext context) : IListRepository
    {
        public async Task<List> AddCardByList(Card card, int id)
        {
            var listId = await context.List.Include(l => l.Cards).FirstOrDefaultAsync(l => l.Id == id);
            listId!.Cards.Add(card);
            await context.SaveChangesAsync();

            return listId;
        }

        public async Task<List> CreatedList(List list)
        {
            await context.List.AddAsync(list);
            await context.SaveChangesAsync();
            return list;
        }

        public async Task<List> DeletedList(List list)
        {
            context.List.Remove(list);
            await context.SaveChangesAsync();
            return list;
        }

        public async Task<IEnumerable<List>> GetListAsync()
        {
            return await context.List.Include(l => l.Cards).ToListAsync();
        }
    }
}