using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Group
    {
        public int Id { get; set; }
        public required string DisplayName { get; set; }
        public List<List>? Listas { get; set; }
        public List<User>? User { get; set; }
    }
}