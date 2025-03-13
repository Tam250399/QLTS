using DevExpress.CodeParser;
using GS.Core.Domain.CCDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.CCDC
{
    public class ImportCCDCModel
    {
        //from file
        /// <summary>
        /// column 1
        /// </summary>
        public string LoaiCongCu { get; set; }
        /// <summary>
        /// column 2
        /// </summary>
        public string MaCCDC { get; set; }
        /// <summary>
        /// column 3
        /// </summary>
        public string TenCCDC { get; set; }
        /// <summary>
        /// column 4
        /// </summary>
        public string BoPhanSuDung { get; set; }
        /// <summary>
        /// column 5
        /// </summary>
        public DateTime? NgayTang { get; set; }
        /// <summary>
        /// column 6
        /// </summary>
        public int? SoLuong { get; set; }
        /// <summary>
        /// column 7
        /// </summary>
        public string DonViTinh { get; set; }
        /// <summary>
        /// column 8
        /// </summary>
        public decimal? DonGiaMua { get; set; }
        /// <summary>
        /// column 9
        /// </summary>
        public decimal? ThanhTien { get; set; }
        /// <summary>
        /// column 10
        /// </summary>
        public decimal? MaNguonGoc { get; set; }
        /// <summary>
        /// column 11
        /// </summary>
        public decimal? MaTinhTrang { get; set; }
        /// <summary>
        /// column 12
        /// </summary>
        public string SoChungTu { get; set; }
        /// <summary>
        /// column 13
        /// </summary>
        public DateTime? NgayChungTu { get; set; }
        /// <summary>
        /// column 14
        /// </summary>
        public string NhaCungCap { get; set; }
        /// <summary>
        /// column 15
        /// </summary>
        public String NamTheoDoi { get; set; }
        /// <summary>
        /// column 16
        /// </summary>
        public decimal? KyPhanBo { get; set; }
        /// <summary>
        /// column 17
        /// </summary>
        public decimal? ThoiGianPhanBo { get; set; }
        /// <summary>
        /// column 18
        /// </summary>
        public decimal? TyLePhanBo { get; set; }
        /// <summary>
        /// column 19
        /// </summary>
        public decimal? ThoiGianPhanBoConLai { get; set; }
        /// <summary>
        /// column 20
        /// </summary>
        public decimal? GiaTriPhanBoMotKy { get; set; }
        /// <summary>
        /// column 21
        /// </summary>
        public decimal? GiaTriDaPhanBo { get; set; }
        /// <summary>
        /// column 22
        /// </summary>
        public decimal? GiaTriPhanBoConLai { get; set; }

    }

    #region Import CCDC
    public class IMP_CCDCModel
    {
        public IMP_CCDCModel()
        {
            //this.NHOM_CC = new IMP_NhomCCDC();
            //this.CONG_CU = new IMP_CongCu();
            //this.SUA_CHUA = new IMP_SuaChua();
            //this.CHO_THUE = new IMP_ChoThue();
            //this.LUAN_CHUYEN = new IMP_LuanChuyen();
            //this.GIAM_CCDC = new IMP_GiamCC();
            //this.PHAN_BO = new IMP_PhanBo();
        }

        public IMP_CongCu CONG_CU { get; set; }
        public List<IMP_LuanChuyen> LUAN_CHUYEN { get; set; }
        public List<IMP_GiamCC> GIAM_CCDC { get; set; }
        public List<IMP_PhanBo> PHAN_BO { get; set; }
        public IMP_THONGTINKHAC THONG_TIN_KHAC { get; set; }
        public string MESSAGE { get; set; }
    }
    public class IMP_THONGTINKHAC
    {
        public List<IMP_SuaChua> SUA_CHUA { get; set; }
        public List<IMP_ChoThue> CHO_THUE { get; set; }
    }
    public class IMP_NhomCCDC
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? THOI_HAN_PHAN_BO { get; set; }
        public String DON_VI_TINH { get; set; }
        public String TEN_CHA { get; set; }
    }
    public class IMP_CongCu
    {
        public Decimal ID { get; set; }
        public String TEN { get; set; }
        public String MA { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }//1: Đang sử dụng -- 2: Chưa sử dụng -- 3: Hỏng chờ xử lý
        public Decimal? SO_LUONG { get; set; }
        public Decimal? DON_GIA { get; set; }
        public Decimal? THANH_TIEN { get; set; }
        public String DON_VI_MA { get; set; }
        public String DON_VI_BO_PHAN_MA { get; set; }
        public DateTime? NGAY_XUAT_NHAP { get; set; }
        public Decimal? LY_DO_ID { get; set; }//MUA_MOI = 1 CHUYEN_NHUONG = 2 TIEP_NHAN = 3 NHAP_SO_DU_BAN_DAU = 4
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public String GHI_CHU { get; set; }
        public String NHOM_CCDC_MA { get; set; }
        public IMP_NhomCCDC NHOM_CCDC { get; set; }
    }
    public class IMP_SuaChua
    {
        public String MA_PHAN_BO { get; set; }
        public String DON_VI_SU_DUNG_MA  { get; set; }
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public String CAP_QUYET_DINH { get; set; }
        public DateTime? NGAY_SUA_CHUA_TU { get; set; }
        public DateTime? NGAY_SUA_CHUA_DEN { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public String MA_DOI_TAC { get; set; }
        public String TEN_DOI_TAC { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public String GHI_CHU { get; set; }
    }
    public class IMP_ChoThue
    {
        public String MA_PHAN_BO { get; set; }
        public String DON_VI_SU_DUNG_MA  { get; set; }
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public String CAP_QUYET_DINH { get; set; }
        public DateTime? NGAY_CHO_THUE_TU { get; set; }
        public DateTime? NGAY_CHO_THUE_DEN { get; set; }
        public Decimal? GIA_TRI { get; set; }
        public String MA_DOI_TAC { get; set; }
        public String TEN_DOI_TAC { get; set; }
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
            this.nhapXuatMap = new NhapXuatCongCu();
        }
        public Decimal? ID { get; set; }
        public NhapXuatCongCu nhapXuatMap { get; set; }
        public String MA_PHAN_BO { get; set; }
        public String DON_VI_PHAN_BO_MA { get; set; }
        public IMP_DonVi DON_VI_PHAN_BO { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public DateTime? NGAY_TANG { get; set; }
        public Decimal? TRANG_THAI { get; set; }//1: Đang sử dụng -- 2: Chưa sử dụng -- 3: Hỏng chờ xử lý
        public String GHI_CHU { get; set; }
    }
    public class IMP_LuanChuyen
    {
        public String MA_PHAN_BO { get; set; }
        public String DON_VI_SU_DUNG_MA  { get; set; }
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public DateTime? NGAY_LUAN_CHUYEN { get; set; }
        public String DON_VI_NHAN_DIEU_CHUYEN_MA { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public String GHI_CHU { get; set; }
    }
    public class IMP_GiamCC
    {
        public String MA_PHAN_BO { get; set; }
        public String DON_VI_SU_DUNG_MA  { get; set; }
        public String SO_QUYET_DINH { get; set; }
        public DateTime? NGAY_QUYET_DINH { get; set; }
        public DateTime? NGAY_DIEU_CHUYEN { get; set; }
        public String DON_VI_TIEP_NHAN { get; set; }
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
    #endregion
    #region Kiểm kê CCDC
    public class IMP_KEMKE_CCDC
    {
        public decimal ID { get; set; }
        public String DON_VI_MA { get; set; }
        public DateTime? NGAY_KIEM_KE { get; set; }
        public String PHONG_BAN_MA { get; set; }
        public String NGUOI_LAP_BIEU { get; set; }
        public String DAI_DIEN_BO_PHAN { get; set; }
        public String KE_TOAN { get; set; }
        public String TRUONG_BAN { get; set; }
        public List<IMP_THANH_VIEN_HD> THANH_VIEN_HOI_DONG { get; set; }
        public List<String> ARR_MA_CCDC { get; set; }
        public List<IMP_CCDCKIEM_KE> CCDC_KIEMKE { get; set; }
        public List<IMP_CCDC_THUA> CCDC_THUA { get; set; }
        public String MESSAGE { get; set; }
        public DateTime? NGAY_KIEM_KE1 { get; set; }
        public String DON_VI_SU_DUNG { get; set; }
        public String BO_PHAN_KIEM_KE { get; set; }
    }
    public class IMP_THANH_VIEN_HD
    {
        public String HO_TEN { get; set; }
        public String CHUC_VU { get; set; }
        public String DAI_DIEN { get; set; }
        public Decimal? VI_TRI { get; set; }
    }
    public class IMP_CCDCKIEM_KE
    {
        public decimal ID { get; set; }
        public String MA { get; set; }

        public decimal? SO_LUONG { get; set; }
        public decimal? SO_LUONG_SO_SACH { get; set; }
        public decimal DON_GIA { get; set; }
        public String DON_VI_SU_DUNG_MA  { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }

        public String GHI_CHU { get; set; }
    }
    public class IMP_CCDC_THUA
    {
        public String DON_VI_SU_DUNG_MA  { get; set; }
        public String MA { get; set; }
        public String TEN { get; set; }
        public String NHOM_CCDC_MA { get; set; }
        public Decimal? DON_GIA { get; set; }
        public Decimal? SO_LUONG { get; set; }
        public Decimal? TRANG_THAI { get; set; }
        public String GHI_CHU { get; set; }
        public decimal? NHOM_CCDC_ID { get; set; }
    }
    #endregion
}
