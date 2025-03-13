using GS.Core.Domain.SHTD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.TaiSanApi
{
    /// <summary>
    /// các thông tin để lấy tài sản gửi sang kho
    /// </summary>
    public class SearchTaiSanModel
    {
        public decimal? DonViId { get; set; }
        public decimal? LoaiBienDongId { get; set; }
        public decimal? TaiSanId { get; set; }
        public decimal? LoaiHinhTaiSanId { get; set; }
        public decimal? nguonId { get; set; }
        public bool? IsGiamDieuChuyenToanBo { get; set; }
        public IList<decimal?> ListTaiSanId { get; set; }
        public decimal? BienDongId { get; set; }
        public string FileContent { get; set; }
        public IList<QuyetDinhTichThu> ListQuyetDinhTichThu { get; set; }
        public IList<TaiSanTd> ListTaiSanToanDan { get; set; }
    }
    public class ParameterXoaBienDong
    {
        public string BienDongGuid { get; set; }
        public int DonViId { get; set; }
        public decimal? nguonId { get; set; }
        public string MaTaiSanDb { get; set; }
        public DateTime NgayBienDong { get; set; }
    }
}
