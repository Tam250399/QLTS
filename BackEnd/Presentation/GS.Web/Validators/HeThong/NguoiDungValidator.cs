//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core;
using GS.Services.HeThong;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Validators;
using GS.Web.Models.HeThong;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace GS.Web.Validators.HeThong
{
    public partial class NguoiDungValidator : BaseGSValidator<NguoiDungModel>
    {
        public NguoiDungValidator(INhanHienThiService _nhanHienThiService, 
            INguoiDungModelFactory nguoiDungModelFactory, 
            ICauHinhService _cauHinhService,
            IVaiTroNguoiDungService _vaiTroNguoiDungService
            )
        {
            RuleFor(x => x.TEN_DAY_DU).NotEmpty().WithMessage("Tên đầy đủ không được để trống");
            RuleFor(x => x.TEN_DANG_NHAP).NotEmpty().WithMessage("Tên đăng nhập không được để trống");
            RuleFor(x => x.MAT_KHAU).Must((model, matkhau) => {
                if (!model.IsEdit)
                {
                    if (!string.IsNullOrEmpty(matkhau))
                        return true;
                    else
                        return false;
                }    
                return true;
            }).WithMessage("Mật khẩu không được để trống");
            //RuleFor(x => x.MAT_KHAU).NotEmpty().WithMessage("Mật khẩu không được để trống");
            RuleFor(x => x.MAT_KHAU).Must((model, matkhau)=> {
                //8 ký tự, 1 chữ hoa, một chữ thường, một chữ số
                if (!model.IsEdit)
                {
                    var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d].{8,}$");
                    return regex.IsMatch(matkhau ?? "");
                }
                return true;
            }).WithMessage("Mật khẩu phải có ít nhất 8 ký tự, một chữ hoa, một chữ thường, một chữ số");
            RuleFor(x => x.TEN_DANG_NHAP).Must((model, code) => nguoiDungModelFactory.CheckTenDangNhap(model.TEN_DANG_NHAP, model.ID)).WithMessage("Tên nhập vào đã trùng");
            //RuleFor(x => x.MAT_KHAU).Must((model, matkhau) => !matkhau.Contains(model.TEN_DANG_NHAP)).WithMessage("Mật khẩu không được bao gồm tên đăng nhập");
            RuleFor(x => x.EMAIL).EmailAddress().WithMessage("Chưa đúng định dạng email");

            RuleFor(x => x.SelectedVaiTro).Must((model, selectedVaiTro) =>
            {
                if(model.IsCapDonVi)
                {
                    var vaitros = _vaiTroNguoiDungService.GetMapByNguoiDungId(model.ID);
                    var VTcauhinh = _cauHinhService.GetCauHinh("cauhinh.danhsachvaitro").GIA_TRI.toEntities<int>();
                    var check = true;
                    foreach (var tmp in vaitros)
                    {
                        if (!VTcauhinh.Contains(Convert.ToInt32(tmp.VAI_TRO_ID)))
                        {
                            VTcauhinh.Add(Convert.ToInt32(tmp.VAI_TRO_ID));
                        }
                    }
                    foreach (var vt in selectedVaiTro)
                    {
                        bool containsItem = VTcauhinh.Any(c => c == vt);
                        if (!containsItem)
                        {
                            check = false;
                            break;
                        }
                    }
                    return check;
                }    
                return true;
            }).WithMessage("Nhóm người dùng nằm ngoài quyền hạn");
        }
    }
    
}

