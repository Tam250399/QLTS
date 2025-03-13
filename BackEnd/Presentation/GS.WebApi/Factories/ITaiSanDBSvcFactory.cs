using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.WebApi.Models.TaiSanXacLap;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Factories
{
    public interface ITaiSanDBSvcFactory
    {
        #region TaiSan
        ResultTS GetAllTaiSans(int LoaiHinhTaiSanId = 0, int pageSize = int.MaxValue, int pageIndex = 0, int DonViId = 0);
        MessageReturn UpdateTaiSan(TSDBModel model, NguoiDung currentUser);
        MessageReturn UpdateListTaiSan(List<TSDBModel> ListModel, NguoiDung currentUser, decimal nguontaisan);
        MessageReturn DeleteTaiSan(string MaTaiSan);
        MessageReturn UpdateBienDong(List<BDDBModel> ListBienDong, NguoiDung currentUser, decimal nguontaisan);
        MessageReturn DeleteBienDong(string MaTaiSan);
        List<string> GetAllMaTaiSan(int LoaiHinhTaiSanId = 0);
        #endregion
        #region TaiSanNhatKy
        MessageReturn UpdateTaiSanDaDongBo(List<string> ListMaTaiSan, List<QuyetDinh> ListQuyetDinhTichThu, string MaDonVi);
        #endregion
        #region TaiSanToanDan

        List<QuyetDinhTichThuModel> GetQuyetDinhTichThuModels();
        MessageReturn UpdateQuyetDinhTichThuModel(List<QuyetDinhTichThuModel> ListModel);
        MessageReturn UpdateTaiSanToanDanModel(List<TaiSanToanDanModel> ListModel);
        MessageReturn UpdatePhuongAnXuLyModel(List<XuLyModel> ListModel);
        MessageReturn UpdateTaiSanToanDanXuLyModel(List<TSToanDanXuLyModel> ListModel);
        MessageReturn UpdateKetQuaXuLyModel(List<KetQuaXuLyTaiSanModel> ListModel);
        MessageReturn UpdateThuChiModel(List<ThuChiModel> ListModel);
        //MessageReturn UpdateTaiSanToanDan(QuyetDinhTichThuModel model);
        //MessageReturn UpdateListQuyetDinhTichThu(List<QuyetDinhTichThuModel> ListModel);
        //MessageReturn UpdateListPhuongAnXuLy(List<QuyetDinhTichThuModel> ListModel);
        //MessageReturn UpdateListKetQuaXuLy(List<QuyetDinhTichThuModel> ListModel);
        //MessageReturn UpdateListQuanLyThuChi(List<QuyetDinhTichThuModel> ListModel);
        //MessageReturn UpdateTaiSanDaDongBo(List<string> ListMaTaiSan, List<QuyetDinh> ListQuyetDinhTichThu, string MaDonVi);
        #endregion
        #region KhaiThacTaiSan
        MessageReturn UpdateKhaiThacTaiSan(DBKhaiThacTaiSanModel model);
        #endregion
        #region Khấu hao, hao mòn tài sản
        MessageReturn UpdateListHaoMonTaiSan(List<HaoMonDBModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteHaoMonTaiSan(List<decimal> ListId);
        MessageReturn UpdateListKhauHaoTaiSan(List<KhauHaoDBModel> ListModel, NguoiDung currentUser);
        MessageReturn DeleteKhauHaoTaiSan(List<decimal> ListId);
        #endregion
        #region Validate
        bool CheckTonTaiDonVi(decimal DonViId);
        #endregion
        MessageReturn UpdateLogsTaiSanDongBo(LogsDongBoCsdlqgModel model);
    }
}
