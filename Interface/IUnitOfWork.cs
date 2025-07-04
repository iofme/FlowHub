using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interface
{
    public interface IUnitOfWork
    {
        ICardRepository CardRepository { get; }
        IListRepository ListRepository { get; }
        IUserRepository UserRepository { get; }
        IGroupRepository GroupRepository{ get; }
        void Commit();
    }
}