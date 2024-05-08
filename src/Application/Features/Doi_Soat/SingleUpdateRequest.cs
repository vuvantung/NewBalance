using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewBalance.Application.Features.Doi_Soat
{
    public class SingleUpdateRequest
    {
        public string TABLENAME {  get; set; }
        public string IDCOLUMNNAME { get; set;}
        public string IDCOLUMNVALUE { get; set;}
        public string CHANGECOLUMNNAME { get; set;}
        public string CHANGECOLUMNVALUE { get; set;}
    }
}
