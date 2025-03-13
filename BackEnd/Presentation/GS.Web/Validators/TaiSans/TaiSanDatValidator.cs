//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;
using System;

namespace GS.Web.Validators.TaiSans
{
    public partial class TaiSanDatValidator : BaseGSValidator<TaiSanDatModel>
    {
        public TaiSanDatValidator(ITaiSanDatModelFactory taiSanDatModelFactory, IDiaBanModelFactory diaBanModelfactory, IWorkContext _workContext)
        {
            RuleFor(x => x.DIEN_TICH).NotEmpty().WithMessage("Diện tích khuôn viên không được để trống");
            RuleFor(x => x.DIA_CHI).NotEmpty().WithMessage("Địa chỉ không được để trống");
            RuleFor(x => x.DIA_CHI).Must((model, dc) =>
            {
                if (dc != null && dc.Trim().Length < 3)
                    return false;
                else
                    return true;
            }).WithMessage("Địa chỉ phải có tối thiểu 3 ký tự.");
            //RuleFor(x => x.DIA_CHI).Must((model, dc) =>
            //{
            //    return taiSanDatModelFactory.CheckDiaChiTaiSanDat(diaChi: dc, diaBanId: model.DIA_BAN_ID, donViId: _workContext.CurrentDonVi.ID, id: model.TAI_SAN_ID);

            //}).WithMessage("Địa chỉ này đã tồn tại.");

            RuleFor(x => x.HS_CNQSD_SO).Must((model, dc) =>
            {
                if (dc != null && dc.Trim().Length < 3)
                    return false;
                else
                    return true;
            }).WithMessage("Giấy CNQSD đất phải có tối thiểu 3 ký tự.");
            RuleFor(x => x.HS_QUYET_DINH_GIAO_SO).Must((model, dc) =>
            {
                if (dc != null && dc.Trim().Length < 3)
                    return false;
                else
                    return true;
            }).WithMessage("Quyết định giao đất phải có tối thiểu 3 ký tự.");
            RuleFor(x => x.HS_QUYET_DINH_CHO_THUE_SO).Must((model, dc) =>
            {
                if (dc != null && dc.Trim().Length < 3)
                    return false;
                else
                    return true;
            }).WithMessage("Quyết định cho thuê đất phải có tối thiểu 3 ký tự.");
            RuleFor(x => x.HS_HOP_DONG_CHO_THUE_SO).Must((model, dc) =>
            {
                if (dc != null && dc.Trim().Length < 3)
                    return false;
                else
                    return true;
            }).WithMessage("Hợp đồng cho thuê đất phải có tối thiểu 3 ký tự.");
            RuleFor(x => x.HS_PHAP_LY_KHAC).Must((model, dc) =>
            {
                if (dc != null && dc.Trim().Length < 3)
                    return false;
                else
                    return true;
            }).WithMessage("Giấy tờ pháp lý khác phải có tối thiểu 3 ký tự.");
            RuleFor(x => x.DIEN_TICH).Must((model, dienTichDat) =>
            {
                if (dienTichDat <= 0)
                {
                    return false;
                }
                return true;
            }).WithMessage("Diện tích khuôn viên phải > 0");
            //check hồ sơ giấy tờ vs ngày
            RuleFor(x => x.HS_CNQSD_SO).Must((model, CNQSD_SO) =>
            {
                if (String.IsNullOrEmpty(CNQSD_SO) && model.HS_CNQSD_NGAY != null)
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có hồ sơ chứng nhận sử dụng đất");
            RuleFor(x => x.HS_CNQSD_NGAY).Must((model, CNQSD_NGAY) =>
            {
                if (CNQSD_NGAY == null && !String.IsNullOrEmpty(model.HS_CNQSD_SO))
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có ngày chứng nhận sử dụng đất");
            RuleFor(x => x.HS_QUYET_DINH_GIAO_SO).Must((model, QUYET_DINH_GIAO_SO) =>
            {
                if (String.IsNullOrEmpty(QUYET_DINH_GIAO_SO) && model.HS_QUYET_DINH_GIAO_NGAY != null)
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có quyết định bàn giao số");
            RuleFor(x => x.HS_QUYET_DINH_GIAO_NGAY).Must((model, QUYET_DINH_GIAO_NGAY) =>
            {
                if (QUYET_DINH_GIAO_NGAY == null && !String.IsNullOrEmpty(model.HS_QUYET_DINH_GIAO_SO))
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có ngày quyết định bàn giao");
            RuleFor(x => x.HS_CHUYEN_NHUONG_SD_SO).Must((model, CHUYEN_NHUONG_SD_SO) =>
            {
                if (String.IsNullOrEmpty(CHUYEN_NHUONG_SD_SO) && model.HS_CHUYEN_NHUONG_SD_NGAY != null)
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có số chuyển nhượng SD");
            RuleFor(x => x.HS_CHUYEN_NHUONG_SD_NGAY).Must((model, CHUYEN_NHUONG_SD_NGAY) =>
            {
                if (CHUYEN_NHUONG_SD_NGAY == null && !String.IsNullOrEmpty(model.HS_CHUYEN_NHUONG_SD_SO))
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có ngày chuyển nhượng SD");
            RuleFor(x => x.HS_QUYET_DINH_CHO_THUE_SO).Must((model, QUYET_DINH_CHO_THUE_SO) =>
            {
                if (String.IsNullOrEmpty(QUYET_DINH_CHO_THUE_SO) && model.HS_QUYET_DINH_CHO_THUE_NGAY != null)
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có số quyết định cho thuê");
            RuleFor(x => x.HS_QUYET_DINH_CHO_THUE_NGAY).Must((model, QUYET_DINH_CHO_THUE_NGAY) =>
            {
                if (QUYET_DINH_CHO_THUE_NGAY == null && !String.IsNullOrEmpty(model.HS_QUYET_DINH_CHO_THUE_SO))
                {
                    return false;
                }
                return true;
            }).WithMessage("Chưa có ngày quyết định cho thuê");
            RuleFor(x => x.TinhId).Must((model, TinhId) =>
            {
                if (TinhId <= 0)
                {
                    return false;
                }
                else
                    return true;
            }).WithMessage("Chưa chọn Tỉnh/Thành phố.");
            RuleFor(x => x.HuyenId).Must((model, HuyenId) =>
            {
                if (HuyenId <= 0)
                {
                    return false;
                }
                else
                    return true;
            }).WithMessage("Chưa chọn Huyện");
            RuleFor(x => x.XaId).Must((model, XaId) =>
            {
                var lstDiaBanXa = diaBanModelfactory.CheckDiaBanXaByHuyenId(model.HuyenId);
                if (!lstDiaBanXa)
                {
                    if (XaId <= 0)
                    {
                        return false;
                    }
                    else
                        return true;
                }
                return true;
            }).WithMessage("Chưa chọn Xã");
            RuleFor(x => x.QuocGiaId).Must((model, QuocGiaId) =>
            {
                if (!(QuocGiaId > 0))
                {
                    return false;
                }
                else
                    return true;
            }).WithMessage("Chưa chọn Quốc gia.");

        }
    }
}

