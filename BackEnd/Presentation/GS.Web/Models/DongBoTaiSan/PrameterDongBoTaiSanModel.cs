using GS.Core.Domain.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.DongBoTaiSan
{
    public class PrameterDongBoTaiSanModel
    {
        public PrameterDongBoTaiSanModel()
        {
            this.DdlDonViCha = new List<SelectListItem>();
            this.DdlDonViCon = new List<SelectListItem>();
            this.ListQuyetDinhTichThu = new List<QuyetDinhTichThu>();
            this.ListTaiSanToanDan = new List<TaiSanTd>();
        }
        public decimal? DonViId { get; set; }
        public decimal? LoaiBienDongId { get; set; }
        public decimal? TaiSanId { get; set; }
        public decimal? LoaiHinhTaiSanId { get; set; }
        public bool? IsGiamDieuChuyenToanBo { get; set; }
        public IList<decimal> ListTaiSanId { get; set; }
        public decimal BienDongId { get; set; }
        public decimal? DonViCha { get; set; }
        public string TenDonVi { get; set; }
        public decimal? DonViCon { get; set; }
        public List<SelectListItem> DdlDonViCha { get; set; }
        public List<SelectListItem> DdlDonViCon { get; set; }
        public List<SelectListItem> DdlLoaiBienDong { get; set; }
        public List<SelectListItem> DdlNguonTaiSan { get; set; }
        public decimal? NguonTaiSanId { get; set; }
        public string FileContent { get; set; }
        //TSTD
        public IList<QuyetDinhTichThu> ListQuyetDinhTichThu { get; set; }
        public IList<TaiSanTd> ListTaiSanToanDan { get; set; }


    }
    public class ParameterKhaiThacTaiSan
    {
        public decimal? KhaiThacId { get; set; }
        public int HinhThucKhaiThacId { get; set; }
        public decimal nguonId { get; set; }
    }
    public class ParameterXoaBienDong
    {
        public string BienDongGuid { get; set; }
        public int DonViId { get; set; }
        public string MaTaiSanDb { get; set; }
        public DateTime NgayBienDong { get; set; }
    }
}
