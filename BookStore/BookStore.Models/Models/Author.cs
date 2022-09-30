using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Models
{
    public record Author : Person
    {
        public string Nickname { get; init; }
    }
}
