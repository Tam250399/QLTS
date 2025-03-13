using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.WebApi.Models;
using GS.WebApi.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace GS.WebApi.SoapServices
{
    [ServiceContract]
    public interface IDanhMucSvc
    {
        [OperationContract]
        string Ping(string s);       
        #region kiên
        #region quốc gia
        [OperationContract]
        IList<QuocGiaModel> GetAllQuocGias();
        [OperationContract]
        MessageReturn UpdateQuocGia(QuocGiaModel model,NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpDateListQuocGia(List<QuocGiaModel> ListQuocGiaModel, NguoiDung CurrentUser);
        #endregion
        #region Hiện trạng
        [OperationContract]
        IList<HienTrangModel> GetAllHienTrangs();
        [OperationContract]
        MessageReturn UpDateHienTrang(HienTrangModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpDateListHienTrang(List<HienTrangModel> ListHienTrangModel, NguoiDung CurrentUser);
        #endregion
        #region LoaiDonVi
        [OperationContract]
        IList<LoaiDonViModel> GetAllLoaiDonVis();
        [OperationContract]
        MessageReturn UpDateLoaiDonVi(LoaiDonViModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpDateListLoaiDonVi(List<LoaiDonViModel> ListLoaiDonViModel, NguoiDung CurrentUser);
        #endregion
        #region NguonVon
        [OperationContract]
        IList<NguonVonModel> GetAllNguonVons();
        [OperationContract]
        MessageReturn UpDateNguonVon(NguonVonModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpdateListNguonVon(List<NguonVonModel> ListNguonVonModel, NguoiDung CurrentUser);
        #endregion
        #region NhanXe
        [OperationContract]
        IList<NhanXeModel> GetAllNhanXe();
        [OperationContract]
        MessageReturn UpDateNhanXe(NhanXeModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpDateListNhanXe(List<NhanXeModel> ListNhanXeModel, NguoiDung CurrentUser);
        #endregion
        #region DonVi
        [OperationContract]
        IList<DonViModel> GetAllDonVis();
        [OperationContract]
        MessageReturn UpdateDonVi(DonViModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpdateListDonVi(List<DonViModel> ListDonViModel, NguoiDung CurrentUser);
        #region Hinh thức xử lý
        [OperationContract]
        IList<PhuongAnXuLyModel> GetAllPhuongAnXuLys();
        [OperationContract]
        MessageReturn UpdatePhuongAnXuLy(PhuongAnXuLyModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpDateListPhuongAnXuLy(List<PhuongAnXuLyModel> ListPhuongAnXuLyModel, NguoiDung CurrentUser);
        #endregion
        #region Nguồn gốc tài sản
        [OperationContract]
        IList<QuocGiaModel> GetAllNgupnGocTaiSans();
        [OperationContract]
        MessageReturn UpdateNguonGocTaiSan(NguonGocTaiSanModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpDateListNguonGocTaiSan(List<NguonGocTaiSanModel> ListNguonGocTaiSanModel, NguoiDung CurrentUser);
        #endregion
        #endregion
        #endregion
        #region tuấn anh
        #region DongXe
        [OperationContract]
        IList<DongXeModel> GetAllDongXes();
        [OperationContract]
        MessageReturn UpdateDongXe(DongXeModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpdateDongXes(List<DongXeModel> ListModel, NguoiDung CurrentUser);
        #endregion
        #region LyDoBienDong
        [OperationContract]
        IList<LyDoBienDongModel> GetAllLyDoBienDongs();
        [OperationContract]
        MessageReturn UpdateLyDoBienDong(LyDoBienDongModel model, NguoiDung CurrentUser);
        //[OperationContract]
        //MessageReturn UpdateLyDoBienDongs(List<LyDoBienDongModel> ListModel, NguoiDung CurrentUser);
        #endregion
        #region DiaBan
        [OperationContract]
        IList<DiaBanModel> GetAllDiaBans();
        [OperationContract]
        MessageReturn UpdateDiaBan(DiaBanModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpdateDiaBans(List<DiaBanModel> ListModel, NguoiDung CurrentUser);
        #endregion
        #region LoaiTaiSan
        [OperationContract]
        IList<LoaiTaiSanModel> GetAllLoaiTaiSanNhaNuocs();
        [OperationContract]
        MessageReturn UpdateLoaiTaiSan(LoaiTaiSanModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpdateLoaiTaiSans(List<LoaiTaiSanModel> ListModel, NguoiDung CurrentUser);
        #endregion
        #region ChucDanh
        [OperationContract]
        IList<ChucDanhModel> GetAllChucDanhs();
        [OperationContract]
        MessageReturn UpdateChucDanh(ChucDanhModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpdateChucDanhs(List<ChucDanhModel> ListModel, NguoiDung CurrentUser);
        #endregion
        #region DuAn
        [OperationContract]
        IList<DuAnModel> GetAllDuAns();
        [OperationContract]
        MessageReturn UpdateDuAn(DuAnModel model, NguoiDung CurrentUser);
        [OperationContract]
        MessageReturn UpdateDuAns(List<DuAnModel> ListModel, NguoiDung CurrentUser);
        #endregion
        #endregion
    }
}
