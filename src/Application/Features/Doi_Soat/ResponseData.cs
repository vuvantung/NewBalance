using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Application.Features.Doi_Soat
{
    public class ResponseData<T>
    {
        public string code { get; set; }
        public string message { get; set; }
        public int total { get; set; }
        public List<T> data { get; set; }
    }
}
