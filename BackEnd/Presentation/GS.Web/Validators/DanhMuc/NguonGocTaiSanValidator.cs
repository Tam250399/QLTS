//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;
using System.Linq;

namespace GS.Web.Validators.DanhMuc
{
    public partial class NguonGocTaiSanValidator : BaseGSValidator<NguonGocTaiSanModel>
    {
        public NguonGocTaiSanValidator(INguonGocTaiSanModelFactory _nguonGocTaiSanModelFactory)
        {
            RuleFor(model => model.MA).NotEmpty().WithMessage("Mã không được để trống");
            RuleFor(model => model.TEN).NotEmpty().WithMessage("Tên không được để trống");
            RuleFor(x => x.MA).Must((model, code) =>
            {
                return _nguonGocTaiSanModelFactory.CheckMaNguonGocTaiSan(model.ID, model.MA);
            }).WithMessage("Mã đã tồn tại");
        }
    }
}

