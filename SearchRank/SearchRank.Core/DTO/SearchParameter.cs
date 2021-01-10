using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRank.Core.DTO
{
   public class SearchParameter
    {
        public string SearchEngine { get; set; }
        public string UrlToSearch { get; set; }
        public string KeyWords { get; set; }
        public string NoOfResults { get; set; }
    }
}
