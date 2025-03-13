using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.TaiSanApi
{
    public class Kho_XoaBienDong
    {
        public Kho_XoaBienDong()
        {
            this.data = new List<Data_XoaBienDong>();
        }
        public List<Data_XoaBienDong> data { get; set; }
    }
    public class Data_XoaBienDong
    {
        public string syncSourceId { get; set; }
        public int unitId { get; set; }
        public string assetCode { get; set; }
        public string mutationDate { get; set; }
    }
}
