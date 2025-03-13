//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/10/2021
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.BaoCao;
using GS.Web.Framework.Validators;
using GS.Web.Models.BaoCao;

namespace GS.Web.Validators.BaoCao
{
    public partial class BaoCaoDoiChieuValidator : BaseGSValidator<BaoCaoDoiChieuModel>
    {
        public BaoCaoDoiChieuValidator(IBaoCaoDoiChieuModelFactory BaoCaoDoiChieuModelFactory)
        {
        }
    }
}

