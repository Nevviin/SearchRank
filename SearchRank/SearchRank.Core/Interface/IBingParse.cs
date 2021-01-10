using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRank.Core.Interface
{
   public  interface IBingParse
    {
        public string GetBingPageRank(string resultsHtml, string urlToSearch);
    }
}
