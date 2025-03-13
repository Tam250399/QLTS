//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DMDC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial interface IDonViService
    {
        #region DonVi

        List<string> GetListGoiYMaDonViCon(decimal ParentId);
        IList<DonVi> GetDonViMuaSamForInputSearch(string tenDonVi = null, decimal CurrentDV_ID = 0);
        IList<DonVi> GetAllDonVis(decimal TreeLevel = 0);
        IList<DonVi> GetAllDonVisChuaDb();
        IPagedList<DonVi> SearchAllDonViXacNhanDuLieu(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, DateTime? NGAY_XAC_NHAN = null);
        IPagedList<DonVi> SearchAllDonViChuaNhapTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, string MaBo = null, decimal? ParentID = 0, decimal? CapDonViSearch = 0, IList<int> listCapDonVis = null, decimal? LoaiTaiSanId = 0, decimal? donViId = 0);
        IPagedList<DonVi> SearchDonViDieuChuyens(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,
            decimal? LoaiDonViSearch = 0, decimal? CapDonViSearch = 0, decimal? NguoiDungID = 0, bool IsQuanTri = false,
            decimal? ParentID = 0, decimal? TreeLevel = 1, bool ischondonvi = false, bool isSelectList = false, 
            DateTime? NGAY_CAP_NHAT_TU = null, DateTime? NGAY_CAP_NHAT_DEN = null, bool isNhapLieuOnly = false, decimal? tinhId = 0, bool isTinh = false
            , decimal? boNganhId = 0);

        IPagedList<DonVi> SearchDonVis(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,
            decimal? LoaiDonViSearch = 0, decimal? CapDonViSearch = 0, decimal? NguoiDungID = 0, bool IsQuanTri = false,
            decimal? ParentID = 0, decimal? TreeLevel = 1, bool ischondonvi = false, bool isSelectList = false, DateTime? NGAY_CAP_NHAT_TU = null, DateTime? NGAY_CAP_NHAT_DEN = null, bool isNhapLieuOnly = false, decimal? donViId = null, IList<int> listCapDonVis = null, bool xuatExcel = false);
        DonVi GetDonViById(decimal Id);
        IList<DonVi> GetDonViByIds(decimal[] newsIds);
        void InsertDonVi(DonVi entity);
        void UpdateDonVi(DonVi entity);
        void DeleteDonVi(DonVi entity);
        InfoCacheDonvi GetProfileUser(Guid guidNguoiDung, decimal? donviId = 0);
        DonVi GetDonViLonNhat(decimal DonViId, string TREE_NODE = null);
        DonVi GetDonViLonNhatByMa(string DonViMa, string TREE_NODE = null);
        IList<DonVi> GetAllDonViByNguoiDungId(decimal nguoiDungId);
        int GetSoDonViConByID(decimal? id = 0);
        IList<DonVi> GetDonViForInputSearch(string tenDonVi = null, decimal? NguoiDungID = 0, decimal CurrentDV_ID = 0);
        IList<DonVi> GetDonViCap1ForInputSearch(string tenDonVi = null, decimal? donViId = 0, decimal? NguoiDungID = 0);
        IList<DonVi> GetDonViCapDuoiTrucThuocForInputSearch(string tenDonVi = null, decimal? NguoiDungID = 0, decimal CurrentDV_ID = 0);

        bool CheckMaDonVi(decimal id = 0, string ma = null);
        void GenTreeNode(DonVi entity);
        IList<decimal> GetListIdDonViChild(decimal DonViId, bool isIncludeItself = true);
        IQueryable<decimal> GetIQueryableIdDonViChild(decimal DonViId, bool isIncludeItself = true);
        IList<decimal> GetListIdDonViChild(List<decimal> idDonVis, bool isIncludeItself = true);
        IList<DonVi> GetListDonViChild(decimal parentId, bool isIncludeItself = true);
        IList<DonVi> GetDonViConForInputSearch(string tenDonVi = null, decimal? donViId = 0, decimal? donViSelected = 0);
        IList<DonVi> GetAllDonViChildUsingProc(decimal ParentId);
        IQueryable<DonVi> GetIQueryableAllDonViChildUsingProc(decimal ParentId);
        IQueryable<DonVi> GetDonViForMultilSelectInput(decimal IdDonVi = 0, string ListSelectedId = null);
        IList<DonVi> GetAllDonViUsingProc();

        IQueryable<DonVi> GetIQueryableAllDonViBoNganhTinh();
        

        DonVi GetDonViByMa(string Ma = null);
        void InsertListDonVi(List<DonVi> entities);
        void UpdateListDonVi(List<DonVi> entities);
        DonVi GetDonViByMaDVQHNS(string MaDVQHNS = null);
        IList<DonVi> GetAllDonViBoNganhTinh();
        /// <summary>
        /// Lấy các đơn vị con quản lý tài sản công hoặc tài sản dự án của Đơn vị ban quản lý dự án
        /// <br></br>
        /// "Văn phòng quản lý" tài sản công hoặc "đơn vị dự án" quản lý tài sản dự án
        /// </summary>
        /// <param name="donViId">đơn vị cha ban quản lý dự án</param>
        /// <param name="isDonViBanQuanLyDuAn">false: Văn phòng quản lý tsc<br></br> true: đơn vị dự án quản lý tài sản dự án</param>
        /// <returns></returns>
        DonVi GetDonViBanQuanLyDuAn(decimal donViId, bool? isDonViBanQuanLyDuAn = false);
        #endregion
        #region Cache
        IList<DonVi> GetCacheDonVis();
        DonVi GetCacheDonViByMa(string Ma = null);
        IList<DonVi> GetDonVisForCBB(string Ten = null, decimal ID = 0);
        /// <summary>
        /// Đon vị có phải là ban quản lý dự án hay không
        /// </summary>
        /// <param name="donViId">Đon vị id</param>
        /// <returns>đơn vị là ban quản lý dự án: true - false</returns>
        bool isDonViBanQuanLyDuAn(decimal donViId);
        /// <summary>
        /// Kiểm tra xem có đơn vị con dưới 1 cấp có ban quản lý dự án không
        /// <br></br>
        /// Nếu không là ban ql dự án thì trả về đơn vị hiện tại
        /// </summary>
        /// <param name="donViId"></param>
        /// <returns></returns>
        bool isHasChildDonViBanQLDA(decimal donViId);
        /// <summary>
        /// Check đơn vị chỉ quản lý tài sản dự án, k có tài sản công
        /// </summary>
        /// <param name="donViId"></param>
        /// <returns></returns>
        bool IsOnlyTaiSanDuAn(decimal donViId);
        bool isDonViSuNghiep(decimal donViId);
        IPagedList<DonVi> SearchListKiemTraLoaiHinhDonVi(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null,
            decimal? LoaiDonViSearch = 0, decimal? CapDonViSearch = 0, decimal? donViId = null, bool IsQuanTri = false, decimal? NguoiDungID = null);
        #endregion
        #region read only
        DonVi GetReadOnlyDonViByMa(string Ma = null);
        List<decimal> GetAllDonViIds(bool db_id_null = false);
        #endregion

        #region  Search đơn vị chưa cập nhật mã T
        IQueryable<DMDC_DonViNganSach> GetListDonViChuaCapNhatMaT(decimal donViId);
        IList<DonVi> GetDonViTongHopForInputSearch(string tenDonVi = null);
        IPagedList<DMDC_DonViNganSach> SearchListDonViChuaCapNhatMaT(int pageIndex = 0, int pageSize = 10, string Keysearch = null, decimal donViId = 0);
        bool CheckTaiKhoanDuocCapNhatMaT(decimal donViId, decimal nguoiDungID, bool isQuanTri=false);
        Boolean CheckDonVi(decimal DonViId, decimal NguoiDungId);
        DonVi GetDonViTrucThuoc(decimal DonViId, string TREE_NODE = null);
        #endregion

        #region Đơn vị được phép nhập tài sản đơn vị
        IList<DonVi> GetDonViDuocPhepNhapTSDVForInputSearch(string tenDonVi = null, decimal? donViId = 0, decimal? NguoiDungID = 0);
        IQueryable<DonVi> GetIQueryableAllDonViDuocNhapLoaiTaiSanDonVi();
        List<DonVi> GetAllDonVis2(bool db_id_null = false,decimal TakeNumber = 100);
        bool CheckQuyen(decimal currentDonViId, decimal currentNguoiDungId, decimal? donViChaID = null, decimal? donViID = null);
        #endregion
    }
}
