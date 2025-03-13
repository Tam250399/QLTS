using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.BaoCaoSvc
{
    public enum enumDonViTinhGiaTri
    {
        Dong = 1,
        NghinDong = 2,
        TrieuDong = 3,
        TyDong = 4
    }
    public enum enumDonViDienTichRequest
    {
        MetVuong = 1,
        NghinMetVuong = 2,
        MuoiNghinMetVuong = 3
    }
    public class ReportBaseSearchModel : BaseSearchModel
    {
        public string MaBaoCao { get; set; }
        public int FileType { get; set; }
        public String StringDonVi { get; set; }
        public Decimal DonVi { get; set; }
        public int DonViTien { get; set; }
        public int DonViDienTich { get; set; }
        public int DonViDienTichDat { get; set; }
        public int DonViDienTichNha { get; set; }
        public DateTime? NgayBaoCao { get; set; }
        public DateTime? NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public decimal NamBaoCao { get; set; }
        public int MauSo { get; set; }
        public int BacDonVi { get; set; }
        public int BacTaiSan { get; set; }
        public String StringLoaiTaiSan { get; set; }
        public String StringLoaiDonVi { get; set; }
    }
}
