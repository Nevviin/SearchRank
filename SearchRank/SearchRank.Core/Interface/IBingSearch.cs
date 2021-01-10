using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRank.Core.Interface
{
    public interface IBingSearch
    {
     public string GetBingResult(string urlToSearch, string searchKeyWords, string noOfResults);
       
    }
}
