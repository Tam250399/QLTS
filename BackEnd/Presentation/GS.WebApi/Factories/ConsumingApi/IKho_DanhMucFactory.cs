using GS.Core.Domain.Common;
using GS.WebApi.Models.ConsumingApi.DanhMucApi;
using GS.WebApi.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Factories.Common.ConsumingApi
{
    public interface IKho_DanhMucFactory
    {
        #region Quốc gia
        IList<QuocGiaModel> PrepareQuocGiaModel(List<Kho_QuocGia> kho_QuocGia);
        #endregion
        #region Lý do biến động
        IList<LyDoBienDongModel> PrepareLyDoBienDongModel(List<Kho_LyDoBienDong> kho_LyDoBienDong);
        #endregion
        #region Địa bàn
        IList<DiaBanModel> PrepareDiaBanModel(List<Kho_DiaBan> kho_DiaBan);
        #endregion
        #region Loai don vi
        IList<LoaiDonViModel> PrepareLoaiDonViModel(List<Kho_LoaiDonVi> kho_LoaiDonVi);
         #endregion
        #region  don vi
        IList<DonViModel> PrepareDonViModel(List<Kho_DonVi> kho_DonVi);
        #endregion
        #region  Loai tài sản
        IList<LoaiTaiSanModel> PrepareLoaiTaiSanModel(List<Kho_LoaiTaiSan> kho_LoaiTaiSan);
        int GetMapLoaiHinhTaiSanKho(int LoaiHinhTaiSan);
        #endregion
        #region  Mục đích sử dụng
        IList<MucDichSuDungModel> PrepareMucDichSuDungModel(List<Kho_MucDichSuDung> kho_MucDichSuDung);
        #endregion
        #region nguồn gốc tài sản
        IList<NguonGocTaiSanModel> PrepareNguonGocTaiSanModel(List<Kho_NguonGocTaiSan> kho_NguonGocTaiSan);
        #endregion
        #region Hiện trạng sử dụng
        IList<HienTrangModel> PrepareHienTrangModel(List<Kho_HienTrang> kho_HienTrang);
        #endregion
        #region Người dùng
        IList<NguoiDungModel> PrepareNguoiDungModel(List<Kho_NguoiDung> kho_NguoiDung);
        #endregion
        #region Nhãn hiệu

        #endregion
        #region Dòng sản phẩm

        #endregion
        MessageReturn PrepareMessageReturrn(string strReturnApi);
      int?  GetIdNhomTaiSanKhoByLoaiHinhTaiSan(decimal LoaiHinhTaiSanID);
    }
}
