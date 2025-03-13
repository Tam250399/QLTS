using GS.Core.Domain.TaiSans;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;

namespace GS.Web.Extensions
{
    public static class MappingExtensions
    {
        public static ObjHienTrang ToObjHienTrang(this TaiSanHienTrangSuDung item)
        {
            var model = new ObjHienTrang() { };
            model.GiaTriCheckbox = item.GIA_TRI_CHECKBOX;
            model.GiaTriNumber = item.GIA_TRI_NUMBER;
            model.KieuDuLieuId = item.KIEU_DU_LIEU_ID;
            model.GiaTriText = item.GIA_TRI_TEXT;
            model.NhomHienTrangId = item.NHOM_HIEN_TRANG_ID;
            model.TenHienTrang = item.TEN_HIEN_TRANG;
            model.HienTrangId = item.HIEN_TRANG_ID;
            return model;
        }
    }
}