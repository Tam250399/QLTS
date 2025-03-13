using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi
{
    public class DuLieuDongBoModel<T>where T:class
    {
        public DuLieuDongBoModel()
        {
            this.data = new List<T>();
        }
        public List<T>  data { get; set; }
    }
}
