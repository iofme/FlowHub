using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository
{
    public class RegisterDTO
    {
        public required string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public required DateTime Aniversario { get; set; }
        public required string Cargo { get; set; }
    }
}