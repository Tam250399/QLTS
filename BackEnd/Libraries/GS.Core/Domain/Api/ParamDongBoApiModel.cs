using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Core.Domain.Api
{
    public class GetMaTaiSanKhoApiModel
    {
        public string syncDate { get; set; }
    }
    public class ParamDongBoApiModel
    {
        public decimal? LoaiBienDongId { get; set; }
        public List<decimal> ListTaiSanId { get; set; }
        public Decimal? BienDongId { get; set; }
        public decimal DonViId { get; set; }
        public decimal? NguonTaiSanId { get; set; }
        public int TakeNumber { get; set; }
        public decimal? TaiSanId { get; set; }
        public string MaTaiSanDb { get; set; }
        public string MaDonVi { get; set; }
        public List<decimal> BienDongIds { get; set; }
        public bool IsThemMoi { get; set; }
    }


    public class DeleteDanhMucKhoApi
    {
        public long id { get; set; }
    }

}
