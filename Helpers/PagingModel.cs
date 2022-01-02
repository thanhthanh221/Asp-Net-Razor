using System;

namespace Razor.Helper{
    public class PagingModel{
        public int currentPage{set;get;}
        public int countPages{set;get;}
        public Func<int?, string> GetFunc{set;get;}
        
    }
}