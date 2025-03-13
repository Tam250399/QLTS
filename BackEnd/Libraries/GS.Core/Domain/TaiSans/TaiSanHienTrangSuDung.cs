using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.TaiSans
{
    public class TaiSanHienTrangSuDung : BaseEntity
    {
        public decimal TAI_SAN_ID { get; set; }
        public decimal BIEN_DONG_ID { get; set; }
        public decimal? NHOM_HIEN_TRANG_ID { get; set; }
        public decimal KIEU_DU_LIEU_ID { get; set; }
        public string TEN_HIEN_TRANG { get; set; }
        public string GIA_TRI_TEXT { get; set; }
        public decimal? GIA_TRI_NUMBER { get; set; }
        public bool? GIA_TRI_CHECKBOX { get; set; }
        public decimal? HIEN_TRANG_ID { get; set; }
        public virtual TaiSan TaiSan { get; set; }
        public virtual BienDong BienDong { get; set; }
        public virtual HienTrang HienTrang { get; set; }
    }
    public enum enumHienTrangSuDungDat
    {
        DienTichLamViec = 72,
        KinhDoanh = 73,
        KhongKinhDoanh = 75,
        ChoThue = 78,
        LienDoanhLienKet = 79,
        TruSoLamViec = 204,
        DeO = 181,
        BoTrong = 182,
        BiLanChiem = 183,
        SuDungKhac = 205,
        SuDungHonHop = 81
    }
    public enum enumHienTrangSuDungNha
    {
        DienTichLamViec = 82,
        KinhDoanh = 84,
        KhongKinhDoanh = 83,
        ChoThue = 85,
        LienDoanhLienKet = 86,
        TruSoLamViec = 206,
        DeO = 178,
        BoTrong = 179,
        BiLanChiem = 180,
        SuDungKhac = 207,

        SuDungHonHop = 87
    }
    public enum enumHienTrangSuDungVkt
    {
        QuanLyNhaNuoc = 92,
        KinhDoanh = 94,
        KhongKinhDoanh = 93,
        ChoThue = 97,
        LienDoanhLienKet = 96,
        QuanLyDuAn = 186,
        SuDungKhac = 187
    }
    public enum enumHienTrangSuDungOto
    {
        QuanLyNhaNuoc = 99,
        KinhDoanh = 101,
        KhongKinhDoanh = 100,
        ChoThue = 104,
        LienDoanhLienKet = 103,
        QuanLyDuAn = 184,
        SuDungKhac = 102
    }
    public enum enumHienTrangSuDungPtk
    {
        QuanLyNhaNuoc = 109,
        KhongKinhDoanh = 110,
        KinhDoanh = 111,
        SuDungKhac = 112,
        ChoThue = 113,
        LienDoanhLienKet = 114,
        QuanLyDuAn = 184,
        
    }
}
