//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using GS.Core.Infrastructure;

namespace GS.Core.Domain.DanhMuc
{
    public partial class HienTrang : BaseEntity
    {
        public String TEN_HIEN_TRANG { get; set; }
        public Decimal LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? KIEU_DU_LIEU_ID { get; set; }
        public enumLOAI_HINH_TAI_SAN LoaiHinhTS
        {
            get => (enumLOAI_HINH_TAI_SAN)LOAI_HINH_TAI_SAN_ID;
            set => LOAI_HINH_TAI_SAN_ID = (int)value;
        }
        public enumKIEU_DU_LIEU KieuDuLieu
        {
            get => (enumKIEU_DU_LIEU)KIEU_DU_LIEU_ID;
            set => KIEU_DU_LIEU_ID = (int)value;
        }
        public decimal? NHOM_HIEN_TRANG_ID { get; set; }
        public decimal? DB_ID { get; set; }
        public Decimal? SAP_XEP { get; set; }
        public Decimal? HIEN_THI_ID { get; set; }
        public String LOAI_DON_VI_AP_DUNG { get; set; }
    }

    public enum enumKIEU_DU_LIEU
    {
        TextBox,
        Numberic,
        CheckBox,
        RadioButton
    }
    public enum enumNHOM_HIEN_TRANG
    {
        ALL = 0,
        KINH_DOANH_KHONG_KINH_DOANH = 1,
        BAN_QL_DU_AN = 2
    }

    public enum enumHienTrang_HIEN_THI_ID
    {
        HIEN_THI = 1,
        KHONG_HIEN_THI = 0
    }

    /// Phải fix cứng để xử lý yêu cầu 
    /// Nếu loại hình đơn vị là “Đơn vị sự nghiệp” thì không cho nhập/tích chọn vào hiện trạng Trụ sở làm việc/Quản lý nhà nước (để mờ đi);
    ///Nếu loại hình đơn vị là “Cơ quan nhà nước”, “Tổ chức” thì không cho nhập/tích chọn vào các hiện trạng HĐSN(để mờ đi)
    public static class NHOM_HIEN_TRANG_ID
    {
        public static decimal[] TruSoLamViec = new decimal[] { 72, 82, 206, 204 };
        public static decimal[] QuanLyNhaNuoc = new decimal[] { 172, 92, 99, 152, 158, 164 };
        public static decimal[] HoatDongSuNghiep = new decimal[] { 73, 75, 173, 174, 78, 79, 83, 84, 85, 86, 176, 177, 93, 94, 96, 97, 100, 101, 103, 104, 110, 111, 113, 114, 116, 117, 119, 120, 153, 154, 156, 157, 159, 160, 162, 163, 165, 166, 168, 169 };
        public const string MaTruSoLamViec = "trụ sở làm việc";
        public const string MaQuanLyNhaNuoc = "quản lý nhà nước";
        public const string MaHoatDongSuNghiep = "hđsn";
    }
}



