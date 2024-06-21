using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Application.Features.Doi_Soat
{
    public class ResponseSingle<T>
    {
        public string code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
