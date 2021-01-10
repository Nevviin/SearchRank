using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRank.Core.Interface
{
    public interface IGoogleParse
    {
        public string GetGooglePageRank(string resultsHtml, string urlToSearch, string keyWords);
    }
}
