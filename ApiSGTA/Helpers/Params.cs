using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSGTA.Helpers
{
    public class Params
    {
        private int _pageSize = 5;
        private const int MaxPageSize = 20;
        private int _pageIndex = 1;

        private string _search;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = (value <= 0) ? 1 : value;
        }

        public string Search
        {
            get => _search;
            set => _search = (!String.IsNullOrEmpty(value)) ? value.ToLower() : "";
        }
    }
}