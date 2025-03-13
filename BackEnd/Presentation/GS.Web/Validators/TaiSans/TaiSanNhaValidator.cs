//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;
using System;

namespace GS.Web.Validators.TaiSans
{
    public partial class TaiSanNhaValidator : BaseGSValidator<TaiSanNhaModel>
    {
        public TaiSanNhaValidator(ITaiSanNhaModelFactory taiSanNhaModelFactory, IDiaBanModelFactory diaBanModelFactory)
        {
            RuleFor(x => x.NHA_DIA_CHI).NotEmpty().WithMessage((model,DiaChi)=> {
				if (model.isQuanLyDat)
				{
					return "Phải chọn khuôn viên đất";
				}
				else
				{
					return "Địa chỉ không được để trống";
				}
				
			});
            RuleFor(x => x.NHA_DIA_CHI).Must((model, dc) =>
            {
                if (model.isQuanLyDat == false && dc != null && dc.Trim().Length < 3)
                    return false;
                else
                    return true;
            }).WithMessage("Địa chỉ phải có tối thiểu 3 ký tự.");
            RuleFor(x => x.HS_QUYET_DINH_BAN_GIAO).Must((model, Quyetdinhbangiao) =>
            {
                if (String.IsNullOrEmpty(Quyetdinhbangiao) && model.HS_QUYET_DINH_BAN_GIAO_NGAY != null)
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có quyết định bàn giao");
            RuleFor(x => x.HS_QUYET_DINH_BAN_GIAO_NGAY).Must((model, Quyetdinhbangiaongay) =>
            {
                if (Quyetdinhbangiaongay == null && !String.IsNullOrEmpty(model.HS_QUYET_DINH_BAN_GIAO))
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có ngày quyết định bàn giao");
            RuleFor(x => x.HS_BIEN_BAN_NGHIEM_THU).Must((model, bienbannghiemthu) =>
            {
                if (String.IsNullOrEmpty(bienbannghiemthu) && model.HS_BIEN_BAN_NGHIEM_THU_NGAY != null)
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có biên bản nghiệm thu");
            RuleFor(x => x.HS_BIEN_BAN_NGHIEM_THU_NGAY).Must((model, bienbannghiemthungay) =>
            {
                if (bienbannghiemthungay == null && !String.IsNullOrEmpty(model.HS_BIEN_BAN_NGHIEM_THU))
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có ngày biên bản nghiệm thu");
            RuleFor(x => x.HS_PHAP_LY_KHAC).Must((model, hosokhac) =>
            {
                if (String.IsNullOrEmpty(hosokhac) && model.HS_PHAP_LY_KHAC_NGAY != null)
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có hồ sơ pháp lý khác");
            RuleFor(x => x.HS_PHAP_LY_KHAC_NGAY).Must((model, hosokhacngay) =>
            {
                if (hosokhacngay == null && !String.IsNullOrEmpty(model.HS_PHAP_LY_KHAC))
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có ngày hồ sơ pháp lý khác");
            RuleFor(x => x.NHA_SO_TANG).Must((model, nhaSoTang) =>
            {
                if (!nhaSoTang.HasValue || nhaSoTang <= 0)
                {
                    return false;
                }
                return true;
            }).WithMessage("Số tầng nhà phải có giá trị.");

            RuleFor(x => x.NAM_XAY_DUNG).Must((model, namXayDung) =>
            {
                if (!namXayDung.HasValue || namXayDung <= 0)
                {
                    return false;
                }
                return true;
            }).WithMessage("Năm xây dựng phải có giá trị.");
            RuleFor(x => x.DIEN_TICH_SAN_XAY_DUNG).Must((model, dienTichSanXD) =>
            {
                if (dienTichSanXD == null || dienTichSanXD <= 0)
                {
                    return false;
                }
                return true;
            }).WithMessage("Diện tích sàn sử dụng phải có giá trị.");

            RuleFor(x => x.NHA_TinhId).Must((model, TinhId) =>
            {
               
                if ((TinhId ?? 0) <= 0 && (model.TAI_SAN_DAT_ID ??0) <=0 )
                {
                    return false;
                }
                else
                    return true;
            }).WithMessage("Chưa chọn Tỉnh/Thành phố.");
            RuleFor(x => x.NHA_HuyenId).Must((model, HuyenId) =>
            {
                if ((HuyenId ?? 0) <= 0 && (model.TAI_SAN_DAT_ID ?? 0) <= 0)
                {
                    return false;
                }
                else
                    return true;
            }).WithMessage("Chưa chọn Huyện");
            RuleFor(x => x.NHA_XaId).Must((model, XaId) =>
            {
                var lstDiaBanXa = diaBanModelFactory.CheckDiaBanXaByHuyenId(model.NHA_HuyenId??0);
                if ((model.TAI_SAN_DAT_ID ?? 0) <= 0)
                {
                    if (!lstDiaBanXa)
                    {
                        if ((XaId ?? 0) <= 0 )
                        {
                            return false;
                        }
                        else
                            return true;
                    }
                    return true;
                }
               
                return true;
            }).WithMessage("Chưa chọn Xã");
        }
    }
}

