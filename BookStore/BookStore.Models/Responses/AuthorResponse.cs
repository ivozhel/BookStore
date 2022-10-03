using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class AuthorResponse : BaseResponse
    {
        public Author Author { get; set; }
    }
}
