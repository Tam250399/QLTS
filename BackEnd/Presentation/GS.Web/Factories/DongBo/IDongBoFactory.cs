using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DB;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.HeThong;
using GS.Web.Models.ImportTaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.DongBo
{
    public partial interface IDongBoFactory
    {
        MessageReturn DongBoTaiSan(String maDonVi = null, int? TakeNumber = 200);
        MessageReturn DBTaiSan(String maDonVi = null, int? TakeNumber = 200);
        MessageReturn DongBoBienDongDaTiepNhan(String DBId = null, int? TakeNumber = 200);
        MessageReturn DBBienDongDaTiepNhan(String DBId = null, int? TakeNumber = 200);
        void DongBoTaiSanByStore();
        MessageReturn GuiTaiSanSangKho();
        void DongBoDanhMuc<T>(List<T> model) where T : BaseGSModel;
        Task<ResponseApi> DongBoThuCongDanhMuc(List<NguoiDungModel> model);
        void XoaDanhMuc<TModel>(TModel model) where TModel : BaseGSEntityModel;
        Task<ResponseApi> DongBoTaiSanThuCong(List<decimal> ListTaiSanId, decimal LoaiBienDongId=0);
        void DongBoBienDong(PrameterDongBoTaiSanModel prameterDongBoTaiSanModel);
        void XoaBienDong(ParameterXoaBienDong parameterXoaBienDong);
        Task<MessageReturn> GetMaDongBo(DateTime? NgayDongBo=null, List<string> ListMaTaiSan= null);
        Task<ResponseApi> DongBoTaiSanTheoDonVi(decimal DonViId, decimal LoaiBienDongId);
        void DongBoTSTD<T>(List<T> model) where T : BaseGSModel;
        List<ImportExcelTaiSanDatNhaModel> ImportFileExcel(List<ImportExcelTaiSanDatNhaModel> DatNha=null);
        //List<ImportExcelTaiSanKhacModel> ImportFileExcel( List<ImportExcelTaiSanKhacModel> TaiSanKhac = null);
        MessageReturn PrepareReport(decimal? QueueProcessID);
        Task<ResponseApi> UpdateDanhMuc(string nameDanhMuc, bool isThemMoi = false);
        Task<ResponseApi> DongBoBienDongTuFile(PrameterDongBoTaiSanModel prameterDongBoTaiSanModel);
        Task<ResponseApi> DongBoHaoMonTuFile(PrameterDongBoTaiSanModel prameterDongBoTaiSanModel);
        Task<ResponseApi> DongBoQuyetDinhTichThuTSTD();
        Task<ResponseApi> DongBoTaiSanTSTD();
        Task<ResponseApi> DongBoPAXL();
        Task<ResponseApi> DongBoTSXL();
        Task<ResponseApi> DongBoKetQua();
        Task<ResponseApi> DongBoThuChi();

        #region Change password
         Task<ResponseApi> ChangePasswordByKhoCSDL(PasswordRequestModel prameter);
        Task<ResponseApi> ResetPasswordByKhoCSDL(PasswordRequestModel prameter);
        #endregion

        Task<ResponseApi> GetNguoiDungByKhoCSDL(string name);
        Task<ResponseApi> UpdateNguoiDungs(List<NguoiDungModel> model);
        void DongBoThuCongDanhMuc2<TModel>(List<TModel> model) where TModel : BaseGSModel;
    }
}
