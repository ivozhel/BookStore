using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Requests
{
    public class LoginRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
