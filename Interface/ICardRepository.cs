using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interface
{
    public interface ICardRepository
    {
        Task<Card> GetCardById(int id);
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<Card> CreateCardAsync(Card card);
        Task<Card> UpdateCardAsync(Card card);
        Task<Card> DeleteCardAsync(int id);
    }
}