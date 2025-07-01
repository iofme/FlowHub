using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interface
{
    public interface IListRepository
    {
        Task<IEnumerable<List>> GetListAsync();
        Task<List> CreatedList(List list);
        Task<List> DeletedList(List list);
        Task<List> AddCardByList(Card card, int id);
    }
}