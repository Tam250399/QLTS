using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi
{
    public class GetMaTaiSanKhoModel
    {
        public string syncDate { get; set; }
    }
    public class ParamDongBoModel
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


    public class DeleteDanhMucKho
    {
        public long id { get; set; }
    }

}
