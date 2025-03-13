//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;
using System;

namespace GS.Web.Validators.DanhMuc
{
    public partial class ChucDanhValidator : BaseGSValidator<ChucDanhModel>
    {
        public ChucDanhValidator(IChucDanhModelFactory chucDanhModelFactory)
        {
            RuleFor(x => x.MA_CHUC_DANH).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(x => x.MA_CHUC_DANH).MaximumLength(20).WithMessage("Mã chức danh không quá 20 ký tự");
            RuleFor(x => x.TEN_CHUC_DANH).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.TEN_CHUC_DANH).MaximumLength(250).WithMessage("Tên chức danh không quá 250 ký tự");
            RuleFor(x => x.KHOI_DON_VI_ID).NotEqual(0).WithMessage("Chọn khối đơn vị");
            


            RuleFor(x => x.MA_CHUC_DANH).Must((model, code) =>
            {
                return chucDanhModelFactory.CheckMaChucDanh(ma: model.MA_CHUC_DANH, id: model.ID);
            }).WithMessage("Mã đã tồn tại");
        }
    }
}

