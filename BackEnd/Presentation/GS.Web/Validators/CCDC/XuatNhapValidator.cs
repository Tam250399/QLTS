//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Framework.Validators;
using GS.Web.Models.CCDC;
using System.Linq;

namespace GS.Web.Validators.CCDC
{
    public partial class XuatNhapValidator : BaseGSValidator<XuatNhapModel>
    {
        public XuatNhapValidator()
        {
			//RuleFor(m => m.NGAY_XUAT_NHAP).NotEmpty().WithMessage("Ngày tăng không được để trống");

			//bool checkPhanBo = true;
			//RuleFor(m => m.ListMap).Must((model, code) =>
			//{
			//    foreach (var m in model.ListMap)
			//    {
			//        if (m.SO_LUONG > m.SoLuongCoThePhanBo || m.SO_LUONG == 0)
			//        {
			//            return false;
			//        }
			//    }
			//    return checkPhanBo;
			//}).WithMessage("Số lượng phân bổ không hợp lệ");
			RuleFor(m => m.TRANG_THAI_ID).Must((model, trangThaiId) =>
			{
				if (trangThaiId <= 0)
				{
					return false;
				}
				return true;
			}).WithMessage("Bạn chưa chọn trạng thái");
			RuleFor(m => m.QUYET_DINH_SO).Must((model, quyetdinhso) =>
			{
				if (quyetdinhso != null && quyetdinhso.Length > 255)
				{
					return false;
				}				
				return true;
			}).WithMessage("Số quyết định vượt quá 255 kí tự");
			RuleFor(m => m.QUYET_DINH_SO).Must((model, quyetdinhso) =>
			{
				if (quyetdinhso != null && quyetdinhso.Length < 3)
				{
					return false;
				}
				return true;
			}).WithMessage("Số quyết định phải lớn hơn 3 kí tự");
			RuleFor(m => m.GHI_CHU).Must((model, ghichu) =>
			{
				if (ghichu != null && ghichu.Length < 3)
				{
					return false;
				}
				return true;
			}).WithMessage("Diễn giải phải lớn hơn 3 kí tự");
			RuleFor(m => m.GHI_CHU).Must((model, ghichu) =>
			{
				if (ghichu != null && ghichu.Length > 255)
				{
					return false;
				}
				return true;
			}).WithMessage("Diễn giải phải lớn hơn 255 kí tự");
		}
    }
}

