using GS.Core.Configuration;

namespace GS.Core.Domain.CauHinh
{

    /// <summary>
    /// Cac tham so cau hinh chung cua he thong
    /// </summary>
    public class CauHinhChung : ICauHinh
    {
        /// <summary>
        /// Danh sách IP có thể sử dụng hệ thống (cách nhau bằng dấu ;)	
        /// </summary>
        public string IPTruyCapHeThong { get; set; }
        /// <summary>
        /// Danh sách IP có thể sử dụng phân hệ Quản trị (cách nhau bằng dấu ; )
        /// </summary>
        public string IPTruyCapQuanTri { get; set; }
        /// <summary>
        /// Địa chỉ máy chủ SMTP
        /// </summary>
        public string SMTPServerIP { get; set; }
        /// <summary>
        /// Địa chỉ cổng SMTP
        /// </summary>
        public int SMTPPort { get; set; }
        public bool LDAPEnable { get; set; }
        public string LDAPServerIP { get; set; }
        public int LDAPPort { get; set; }
        public string LDAPAdminUser { get; set; }
        public string LDAPAdminPassword { get; set; }
        public bool Log404Errors { get; set; }
        public decimal ValueDKTSKhac { get; set; }
        public int NhacNgayKetThucDuAn { get; set; }
        
    }
    public class XacThucChungThuSo : ICauHinh
    {
        public bool XacThucChuKySo { get; set; }
        public bool XacThucThaoTacNghiepVu { get; set; }
        public bool CDHTKhauHaoNhapDuDauKy { get; set; }
        public int DinhKyDoiMatKhau { get; set; }
        public bool DonViKhongCanXacThuc { get; set; }
    }
    public class KetXuatDuLieu : ICauHinh
    {
        // Thiet lap lich ket xuat du lieu
        public string LichKetXuat { get; set; }
    }
    
}
