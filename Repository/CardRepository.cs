using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interface;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class CardRepository(AppDbContext context) : ICardRepository
    {
        public async Task<Card> CreateCardAsync(Card card)
        {
            await context.Card.AddAsync(card);
            await context.SaveChangesAsync();

            return card;
        }

        public async Task<Card> DeleteCardAsync(int id)
        {
            var cardDelete = await context.Card.FirstOrDefaultAsync(c => c.Id == id);
            context.Card.Remove(cardDelete!);
            await context.SaveChangesAsync();

            return cardDelete!;
        }

        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await context.Card.AsNoTracking().ToListAsync();
        }

        public async Task<Card> GetCardById(int id)
        {
            return await context.Card.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Card> UpdateCardAsync(Card card)
        {
            context.Card.Update(card);
            await context.SaveChangesAsync();

            return card;
        }
    }
}