using System;
using System.Collections.Generic;
using GS.Core.Domain.TaiSans;
using System.Linq;
using System.Threading.Tasks;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.TaiSans;
using GS.Web.Models.BienDongs;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.NghiepVu;
using GS.Web.Models.KeToan;

namespace GS.Web.Factories.TaiSans
{
    public partial interface ITaiSanImportModelFactory
    {
        TaiSanModel InsertToTaiSan(ImportExcelTaiSan item, TaiSanModel model);
        BienDongModel InsertToBienDong(TaiSanModel item, BienDongModel model);
        BienDongChiTietModel InsertToBienDongChiTiet(ImportExcelTaiSanModel item, BienDongChiTietModel model, BienDongModel bd);
        void InsertTaiSanNguonVonFromBienDong(ImportExcelTaiSanModel item, TaiSanModel model, BienDongModel bd);
        void InsertHaoMonFromTsImport(BienDong bienDong, BienDongChiTiet bienDongChiTiet, HaoMonTaiSanModel haoMonTaiSanModel);
        void InsertKhauHaoFromTsImport(ImportExcelTaiSanModel item, BienDongModel bienDong, BienDongChiTietModel bienDongChiTiet, TaiSanKhauHaoModel taiSanKhauHaoModel);
    }
}
