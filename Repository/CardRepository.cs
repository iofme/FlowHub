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
    public class CardRepository(AppDbContext context) : ICardRepository
    {
        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await context.Card.ToListAsync();
        }

        public async Task<Card> GetCardById(int id)
        {
            return await context.Card.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}