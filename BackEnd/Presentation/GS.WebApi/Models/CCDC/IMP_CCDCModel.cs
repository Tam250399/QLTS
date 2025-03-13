using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.CCDC
{
    public class IMP_CCDCModel
    {
        public IMP_CCDCModel()
        {
            //this.NHOM_CC = new IMP_NhomCCDC();
            //this.CONG_CU = new IMP_CongCu();
            //this.SUA_CHUA = new IMP_SuaChua();
            //this.CHO_THUE = new IMP_ChoThue();
            //this.LUAN_CHUYEN = new IMP_LuanChuyen();
            //this.GIAM_CC = new IMP_GiamCC();
            //this.PHAN_BO = new IMP_PhanBo();
        }
        public Decimal? LOAI_DU_LIEU { get; set; }//1: Nhập liệu - Phân bổ -- 2: Sửa chữa - bảo dưỡng -- 3: Cho thuê -- 4: Luân chuyển -- 5: Giảm CCDC
        public String DON_VI_MA { get; set; }
        public String TEN_LO_CC { get; set; }
        public String DON_VI_BO_PHAN_MA { get; set; }
        public String NGAY_XUAT_NHAP { get; set; }
        public Decimal? LY_DO_ID { get; set; }//MUA_MOI = 1 CHUYEN_NHUONG = 2 TIEP_NHAN = 3 NHAP_SO_DU_BAN_DAU = 4
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public String GHI_CHU { get; set; }
        public IMP_NhomCCDC NHOM_CC { get; set; }
        public IMP_CongCu CONG_CU { get; set; }
        public List<IMP_SuaChua> SUA_CHUA { get; set; }
        public List<IMP_ChoThue> CHO_THUE { get; set; }
        public List<IMP_LuanChuyen> LUAN_CHUYEN { get; set; }
        public List<IMP_GiamCC> GIAM_CC { get; set; }
        public List<IMP_PhanBo> PHAN_BO { get; set; }
    }
    public class IMP_NhomCCDC
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? THOI_HAN_PHAN_BO { get; set; }
        public String DON_VI_TINH { get; set; }
        public String MA_CHA { get; set; }
    }
    public class IMP_CongCu
    {
        public String TEN { get; set; }
        public String MA { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }//1: Đang sử dụng -- 2: Chưa sử dụng -- 3: Hỏng chờ xử lý
        public Decimal? SO_LUONG { get; set; }
        public Decimal? DON_GIA { get; set; }
        public Decimal? THANH_TIEN { get; set; }
    }
    public class IMP_SuaChua
    {
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public String CAP_QUYET_DINH { get; set; }
        public DateTime? NGAY_SUA_CHUA_TU { get; set; }
        public DateTime? NGAY_SUA_CHUA_DEN { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public String KHACH_HANG_MA { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public String GHI_CHU { get; set; }
    }
    public class IMP_ChoThue
    {
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public String CAP_QUYET_DINH { get; set; }
        public DateTime? NGAY_CHO_THUE_TU { get; set; }
        public DateTime? NGAY_CHO_THUE_DEN { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public String KHACH_HANG_MA { get; set; }
        public String HOP_DONG_SO { get; set; }
        public DateTime? HOP_DONG_NGAY { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public String GHI_CHU { get; set; }
    }
    public class IMP_PhanBo
    {
        public IMP_PhanBo()
        {
            //this.DON_VI_PHAN_BO = new IMP_DonVi();
        }
        public IMP_DonVi DON_VI_PHAN_BO { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public DateTime? NGAY_TANG { get; set; }
        public String TRANG_THAI { get; set; }//1: Đang sử dụng -- 2: Chưa sử dụng -- 3: Hỏng chờ xử lý
        public String GHI_CHU { get; set; }
    }
    public class IMP_LuanChuyen
    {
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public DateTime? NGAY_LUAN_CHUYEN { get; set; }
        public String DON_VI_NHAN_DIEU_CHUYEN_MA { get; set; }
        public DateTime? SO_LUONG { get; set; }
        public String GHI_CHU { get; set; }
    }
    public class IMP_GiamCC
    {
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? LY_DO_GIAM_ID { get; set; }//DIEU_CHUYEN = 4 DIEU_CHUYEN_NGOAI = 5 GIAM_HONG_MAT = 6 GIAM_BAN = 7 GIAM_TIEU_HUY = 8 GIAM_KHAC = 9
        public String GHI_CHU { get; set; }
    }
    public class IMP_DonVi
    {
        public String TEN { get; set; }
        public String MA { get; set; }
        public String DIA_CHI { get; set; }
        public String DIEN_THOAI { get; set; }
        public String FAX { get; set; }
        public String MA_CHA { get; set; }
    }
}
