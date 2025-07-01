using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interface;

namespace API.Repository
{
    public class UnitOfWork(AppDbContext context, IUserRepository userRepository, IListRepository listRepository, ICardRepository cardRepository) : IUnitOfWork
    {
        public ICardRepository CardRepository => cardRepository;

        public IListRepository ListRepository => listRepository;

        public IUserRepository UserRepository => userRepository;

        public void Commit()
        {
            context.SaveChangesAsync();
        }

        public void Dispose()
        { 
            context.Dispose();
        }
    }
}