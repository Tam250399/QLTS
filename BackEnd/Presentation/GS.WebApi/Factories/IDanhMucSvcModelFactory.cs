using System;
using System.Collections.Generic;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.WebApi.Models.DanhMuc;
using GS.WebApi.Models.DMDC;

namespace GS.WebApi.Factories
{
    /// <summary>
    /// Represents the interface of the country model factory
    /// </summary>
    public partial interface IDanhMucSvcModelFactory
    {
        /// <summary>
        /// Get states and provinces by country identifier
        /// </summary>
        /// <param name="countryId">Country identifier</param>
        /// <param name="addSelectStateItem">Whether to add "Select state" item to list of states</param>
        /// <returns>List of identifiers and names of states and provinces</returns>
        IList<NhanXeModel> GetAllNhanXe();

        string Ping(string s);
        //IList<NhanXeModel> GetNhanXe();
        #region kiên
        #region quốc gia

        IList<QuocGiaModel> GetAllQuocGias();

        MessageReturn UpdateQuocGia(QuocGiaModel model, NguoiDung currentUser);

        MessageReturn UpDateListQuocGia(List<QuocGiaModel> ListQuocGiaModel, NguoiDung currentUser);
        MessageReturn DeleteQuocGia(decimal ID = 0);
        #endregion
        #region Hiện trạng

        IList<HienTrangModel> GetAllHienTrangs();

        MessageReturn UpDateHienTrang(HienTrangModel model, NguoiDung currentUser);

        MessageReturn UpDateListHienTrang(List<HienTrangModel> ListHienTrangModel, NguoiDung currentUser);
        MessageReturn DeleteHienTrang(decimal ID = 0);
        #endregion
        #region LoaiDonVi

        IList<LoaiDonViModel> GetAllLoaiDonVis();

        MessageReturn UpDateLoaiDonVi(LoaiDonViModel model, NguoiDung currentUser);

        MessageReturn UpDateListLoaiDonVi(List<LoaiDonViModel> ListLoaiDonViModel, NguoiDung currentUser);
        MessageReturn DeleteLoaiDonVi(decimal ID = 0);
        #endregion
        #region NguonVon
        IList<NguonVonModel> GetAllNguonVons();
        MessageReturn UpDateNguonVon(NguonVonModel model, NguoiDung currentUser);
        MessageReturn UpdateListNguonVon(List<NguonVonModel> ListNguonVonModel, NguoiDung currentUser);
        #endregion
        #region NhanXe
        MessageReturn UpDateNhanXe(NhanXeModel model, NguoiDung currentUser);
        MessageReturn UpDateListNhanXe(List<NhanXeModel> ListNhanXeModel, NguoiDung currentUser);
        #endregion
        #region DonVi
        ResultDonVi GetAllDonVis(int pageSize = int.MaxValue, int pageIndex = 0);
        MessageReturn UpdateDonVi(DonViModel model, NguoiDung currentUser);
        MessageReturn UpdateListDonVi(List<DonViModel> ListDonViModel, NguoiDung currentUser);
        MessageReturn DeleteDonVi(decimal ID = 0);
        #endregion
        #region Hinh thức xử lý
        IList<PhuongAnXuLyModel> GetAllPhuongAnXuLys();
        MessageReturn UpdatePhuongAnXuLy(PhuongAnXuLyModel model, NguoiDung currentUser);
        MessageReturn UpDateListPhuongAnXuLy(List<PhuongAnXuLyModel> ListPhuongAnXuLyModel, NguoiDung currentUser);
        MessageReturn DeletePhuongAnXuLy(decimal ID = 0);
        #endregion
        #region Phương án xử lý
        IList<HinhThucXuLyModel> GetAllHinhThucXuLys();
        MessageReturn UpdateHinhThucXuLy(HinhThucXuLyModel model, NguoiDung currentUser);
        MessageReturn UpDateListHinhThucXuLy(List<HinhThucXuLyModel> ListHinhThucXuLyModel, NguoiDung currentUser);
        MessageReturn DeleteHinhThucXuLy(decimal ID = 0);

        #endregion
        #region Nguồn gốc tài sản
        IList<NguonGocTaiSanModel> GetAllNguonGocTaiSans();
        MessageReturn UpdateNguonGocTaiSan(NguonGocTaiSanModel model, NguoiDung currentUser);
        MessageReturn UpDateListNguonGocTaiSan(List<NguonGocTaiSanModel> ListNguonGocTaiSanModel, NguoiDung currentUser);
        MessageReturn DeleteNguonGocTaiSan(decimal ID = 0);
        #endregion
        #region Mục đích sử dụng
        IList<MucDichSuDungModel> GetAllMucDichSuDungs();
        MessageReturn UpdateMucDichSuDung(MucDichSuDungModel model, NguoiDung currentUser);
        MessageReturn UpDateListMucDichSuDung(List<MucDichSuDungModel> ListHinhThucXuLyModel, NguoiDung currentUser);
        MessageReturn DeleteMucDichSuDung(decimal ID=0);
        #endregion
        #region Người dùng
        ResultNguoiDung GetAllNguoiDungs(int pageSize = 1000, int pageIndex = 0);
        MessageReturn UpdateNguoiDung(NguoiDungModel model, NguoiDung currentUser);
        MessageReturn UpdateListNguoiDung(List<NguoiDungModel> ListNguoiDungModel, NguoiDung currentUser);
        #endregion
        #region Chế độ hao mòn

        IList<CheDoHaoMonModel> GetAllCheDoHaoMons();

        MessageReturn UpdateCheDoHaoMon(CheDoHaoMonModel model, NguoiDung currentUser);

        MessageReturn UpDateListCheDoHaoMon(List<CheDoHaoMonModel> ListCheDoHaoMonModel, NguoiDung currentUser);
        MessageReturn DeleteCheDoHaoMon(decimal ID = 0, NguoiDung currentUser = null);
        #endregion
        #endregion
        #region DongXe

        IList<DongXeModel> GetAllDongXes();
        MessageReturn UpdateDongXe(DongXeModel model, NguoiDung currentUser);
        MessageReturn UpdateDongXes(List<DongXeModel> ListModel, NguoiDung currentUser);
        #endregion
        #region LyDoBienDong
        IList<LyDoBienDongModel> GetAllLyDoBienDongs();
        MessageReturn UpdateLyDoBienDong(LyDoBienDongModel model, NguoiDung currentUser);
        MessageReturn UpdateListLyDoBienDong(List<LyDoBienDongModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteLyDoBienDong(decimal ID = 0);
        #endregion
        #region LoaiLyDoBienDong
       
        MessageReturn UpdateLoaiLyDoBienDong(List<LoaiLyDoBienDongModel> model, NguoiDung currentUser);
      
        MessageReturn DeleteLoaiLyDoBienDong(decimal ID = 0);
        #endregion
        #region DiaBan
        IList<DiaBanModel> GetAllDiaBans();
        MessageReturn UpdateDiaBan(DiaBanModel model, NguoiDung currentUser);
        MessageReturn UpdateDiaBans(List<DiaBanModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteDiaBan(decimal ID = 0, NguoiDung currentUser = null);
        #endregion
        #region LoaiTaiSan
        IList<LoaiTaiSanModel> GetAllLoaiTaiSanNhaNuocs();
        MessageReturn UpdateLoaiTaiSan(LoaiTaiSanModel model, NguoiDung currentUser);
        MessageReturn UpdateListLoaiTaiSan(List<LoaiTaiSanModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteLoaiTaiSan(decimal ID);
        #endregion
        #region ChucDanh
        IList<ChucDanhModel> GetAllChucDanhs();
        MessageReturn UpdateChucDanh(ChucDanhModel model, NguoiDung currentUser);
        MessageReturn UpdateChucDanhs(List<ChucDanhModel> ListModel, NguoiDung currentUser);
        #endregion
        #region Đơn vị bộ phận
        IList<DonViBoPhanModel> GetAllDonViBoPhan();     
        MessageReturn UpdateListDonViBoPhan(List<DonViBoPhanModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteDonViBoPhan(decimal ID = 0);
        #endregion
        #region DuAn
        IList<DuAnModel> GetAllDuAns();
        MessageReturn UpdateDuAn(DuAnModel mode, NguoiDung currentUserl);
        MessageReturn UpdateDuAns(List<DuAnModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteDuAn(decimal ID = 0, NguoiDung currentUser = null);
        #endregion
        #region Loại tài sản đơn vị
        MessageReturn UpdateListLoaiTaiSanDonVi(List<LoaiTaiSanDonViModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteLoaiTaiSanDonVi(decimal ID = 0);
        #endregion
        #region Danh mục dùng chung
        MessageReturn UpdateListDMDC_DiaBan(List<DMDC_DiaBanModel> ListModel, NguoiDung currentUser);
        MessageReturn UpdateListDMDC_DonViDuAn(List<DMDC_DonViDuAnModel> ListModel, NguoiDung currentUser);
        MessageReturn UpdateListDMDC_DonViNganSach(List<DMDC_DonViNganSachModel> ListModel, NguoiDung currentUser);
        MessageReturn UpdateListDMDC_DuAn(List<DMDC_DuAnModel> ListModel, NguoiDung currentUser);
        MessageReturn UpdateListDMDC_QuocGia(List<DMDC_QuocGiaModel> ListModel, NguoiDung currentUser);
        MessageReturn UpdateListDMDC_ToChucNganSach(List<DMDC_ToChucNganSachModel> ListModel, NguoiDung currentUser);
        #endregion
        #region Dòng xe
        MessageReturn UpdateDongXe(List<DongXeModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteDongXe(decimal ID = 0);
        #endregion
        #region Nhãn xe
        MessageReturn UpdateNhanXe(List<NhanXeModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteNhanXe(decimal ID = 0);
        #endregion
        #region Hình thức mua sắm
        MessageReturn UpdateHinhThucMuaSam(List<HinhThucMuaSamModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteHinhThucMuaSam(decimal ID = 0);
        #endregion
    }
}
