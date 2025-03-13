//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core.Domain.TaiSans;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;
using System;

namespace GS.Web.Validators.TaiSans
{
    public partial class KhaiThacValidator : BaseGSValidator<KhaiThacModel>
    {
        public KhaiThacValidator(IKhaiThacModelFactory khaiThacModelFactory)
        {

            //RuleFor(x => x.NGAY_KHAI_THAC).NotEmpty().WithMessage("Ngày khai thác không được để trống");
            RuleFor(x => x.QUYET_DINH_SO).Must((model, QUYET_DINH_SO) =>
            {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.KHAI_THAC_KHAC)
                {
                    if (model.QUYET_DINH_SO == null)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Số quyết định không được để trống");
            //RuleFor(x => x.QUYET_DINH_SO).NotEmpty().WithMessage("Số quyết định không được để trống");
            RuleFor(x => x.QUYET_DINH_NGAY).Must((model, QUYET_DINH_NGAY) =>
            {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.KHAI_THAC_KHAC)
                {
                    if (model.QUYET_DINH_NGAY == null)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Ngày quyết định không được để trống");
            //RuleFor(x => x.QUYET_DINH_NGAY).NotEmpty().WithMessage("Ngày quyết định không được để trống");
            //RuleFor(x => x.NGUOI_DUYET).NotEmpty().WithMessage("Người quyết định không được để trống");
            RuleFor(x => x.KHAI_THAC_NGAY_TU).Must((model, ngaykhaithac) => {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK)
                {
                    if (!ngaykhaithac.HasValue)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage((model, str) =>
            {
                string messageReturn = string.Empty;
                switch (model.LOAI_HINH_KHAI_THAC_ID)
                {
                    case (int)enumHinhThucKhaiThac.SXKD:
                        messageReturn = "Ngày SXKD không được để trống";
                        break;
                    case (int)enumHinhThucKhaiThac.CHO_THUE_TS:
                        messageReturn = "";
                        //messageReturn = "Ngày cho thuê không được để trống";
                        break;
                    case (int)enumHinhThucKhaiThac.LDLK:
                        messageReturn = "Ngày LDLK không được để trống";
                        break;
                    default:
                        messageReturn = "Ngày khai thác không được để trống";
                        break;
                }
                return messageReturn;
            });
            //RuleFor(x => x.CHO_THUE_PHUONG_THUC_ID).NotEqual(0).WithMessage("Phương thức cho thuê không được để trống");
            //RuleFor(x => x.CHO_THUE_PHUONG_THUC_ID).Must((model, phuongthucchothue) => {
            //    if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS)
            //    {
            //        if (!phuongthucchothue.HasValue || phuongthucchothue.Equals(0))
            //        {
            //            return false;
            //        }
            //        return true;
            //    }
            //    return true;
            //}).WithMessage("Phương thức cho thuê không được để trống");
            //RuleFor(x => x.KHAI_THAC_NGAY_DEN).NotEmpty().WithMessage((model, str) =>
            RuleFor(x => x.KHAI_THAC_NGAY_DEN).Must((model, ngaykhaithac_den) => {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK)
                {
                    if (ngaykhaithac_den == null)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage((model, str) =>
            {
                string messageReturn = string.Empty;
                switch (model.LOAI_HINH_KHAI_THAC_ID)
                {
                    case (int)enumHinhThucKhaiThac.SXKD:
                        messageReturn = "Ngày kết thúc SXKD không được để trống";
                        break;
                    //case (int)enumHinhThucKhaiThac.CHO_THUE_TS:
                    //    messageReturn = "";
                    //    break;
                    case (int)enumHinhThucKhaiThac.LDLK:
                        messageReturn = "Ngày kết thúc LDLK không được để trống";
                        break;
                    default:
                        messageReturn = "Ngày kết thúc khai thác không được để trống";
                        break;
                }
                return messageReturn;
            });
            //RuleFor(x => x.HOP_DONG_SO).NotEmpty().WithMessage("Số hợp đồng không được để trống");
            //RuleFor(x => x.HOP_DONG_SO).Must((model, HOP_DONG_SO) =>
            //{
            //    if ((model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK /*|| model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS*/) && String.IsNullOrEmpty(HOP_DONG_SO) && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.SXKD)
            //    {
            //        return false;
            //    }
            //    return true;
            //}).WithMessage("Chưa có số hợp đồng");
            //RuleFor(x => x.DOI_TAC_ID).Must((model, doiTac) =>
            //{
            //    if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS && doiTac <= 0)
            //    {
            //        return false;
            //    }
            //    return true;
            //}).WithMessage("Đối tác không được để trống.");
            RuleFor(x => x.KHAI_THAC_NGAY_DEN).Must((model, ngayDen) =>
            {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS)
                {
                    if (ngayDen.HasValue && model.KHAI_THAC_NGAY_TU.HasValue && ngayDen < model.KHAI_THAC_NGAY_TU)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Ngày kết thúc khai thác phải lớn hơn ngày bắt đầu");
            //RuleFor(x => x.HOP_DONG_NGAY).NotEmpty().WithMessage("Ngày hợp đồng không được để trống");
            //RuleFor(x => x.HOP_DONG_NGAY).Must((model, HOP_DONG_NGAY) =>
            //{
            //    if ((model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK /*|| model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS*/) && HOP_DONG_NGAY == null && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.SXKD)
            //    {
            //        return false;
            //    }
            //    return true;
            //}).WithMessage("Chưa có ngày hợp đồng ");
            RuleFor(x => x.HOP_DONG_NGAY).Must((model, HOP_DONG_NGAY) =>
            {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.SXKD && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK && HOP_DONG_NGAY.HasValue && model.QUYET_DINH_NGAY.HasValue)
                {
                    if (model.QUYET_DINH_NGAY < HOP_DONG_NGAY)
                    {
                        return false;

                    }
                }
                return true;
            }).WithMessage("Ngày hợp đồng không được nhỏ hơn ngày quyết định");
            RuleFor(x => x.HOP_DONG_NGAY).Must((model, HOP_DONG_NGAY) =>
            {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.SXKD && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && HOP_DONG_NGAY.HasValue && model.NGAY_KHAI_THAC.HasValue && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK)
                {
                    if (model.NGAY_KHAI_THAC > HOP_DONG_NGAY)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Ngày hợp đồng không được nhỏ hơn ngày khai thác");

            RuleFor(x => x.HOP_DONG_NGAY).Must((model, HOP_DONG_NGAY, context) =>
            {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.SXKD && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK && HOP_DONG_NGAY.HasValue && model.KHAI_THAC_NGAY_TU.HasValue)
                {
                    if (model.KHAI_THAC_NGAY_TU > HOP_DONG_NGAY)
                    {
                        string messageReturn = string.Empty;
                        switch (model.LOAI_HINH_KHAI_THAC_ID)
                        {
                            case (int)enumHinhThucKhaiThac.SXKD:
                                messageReturn = "SXKD";
                                break;
                            case (int)enumHinhThucKhaiThac.CHO_THUE_TS:
                                messageReturn = "";
                                break;
                            case (int)enumHinhThucKhaiThac.LDLK:
                                messageReturn = "LDLK";
                                break;
                            default:
                                messageReturn = "khai thác";
                                break;
                        }
                        context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
                        return false;
                    }
                }
                return true;
            }).WithMessage("Ngày hợp đồng không được nhỏ hơn ngày bắt đầu {messageReturn}");
            RuleFor(x => x.QUYET_DINH_NGAY).Must((model, QUYET_DINH_NGAY) =>
            {
                if (QUYET_DINH_NGAY.HasValue && model.NGAY_KHAI_THAC.HasValue && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK)
                {
                    if (model.NGAY_KHAI_THAC < QUYET_DINH_NGAY)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Ngày hợp đồng không được nhỏ hơn ngày khai thác");
            RuleFor(x => x.SO_TIEN_TAM_TINH).Must((model, SO_TIEN_TAM_TINH) =>
            {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.KHAI_THAC_KHAC)
                {
                    if (model.SO_TIEN_TAM_TINH == 0 || model.SO_TIEN_TAM_TINH == null)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Số tiền thu được phải có giá trị > 0.");
            //RuleFor(x => x.SO_TIEN_TAM_TINH).Must((model, SO_TIEN_TAM_TINH) =>
            //{
            //    if ( (SO_TIEN_TAM_TINH == 0)&& model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.KHAI_THAC_KHAC)
            //        return false;
            //    return true;
            //}).WithMessage("Số tiền thu được phải có giá trị > 0.");
            //RuleFor(x => x.QUYET_DINH_NGAY).LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày quyết định không được lớn hơn ngày hiện tại");
            RuleFor(x => x.QUYET_DINH_NGAY).LessThanOrEqualTo(DateTime.Now).WithMessage((model, str) =>
            {
                string messageReturn = string.Empty;
                switch (model.LOAI_HINH_KHAI_THAC_ID)
                {
                    case (int)enumHinhThucKhaiThac.CHO_THUE_TS:
                        messageReturn = "Ngày quyết định không được lớn hơn ngày hiện tại";
                        break;
                    default:
                        messageReturn = "Ngày quyết định không được lớn hơn ngày hiện tại";
                        break;
                }
                return messageReturn;
            });
            RuleFor(x => x.HOP_DONG_NGAY).Must((model, hopdongngay) => {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK)
                {
                    if (hopdongngay > DateTime.Now)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage("Ngày hợp đồng không được lớn hơn ngày hiện tại");
            //RuleFor(x => x.KHAI_THAC_NGAY_TU).LessThanOrEqualTo(DateTime.Now).WithMessage((model, str) =>
            RuleFor(x => x.KHAI_THAC_NGAY_TU).Must((model, ngaykhaithac_tu) => {
                if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK)
                {
                    if (ngaykhaithac_tu > DateTime.Now)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage((model, str) =>
            {
                string messageReturn = string.Empty;
                switch (model.LOAI_HINH_KHAI_THAC_ID)
                {
                    case (int)enumHinhThucKhaiThac.SXKD:
                        messageReturn = "Ngày SXKD không được lớn hơn ngày hiện tại";
                        break;

                    case (int)enumHinhThucKhaiThac.LDLK:
                        messageReturn = "Ngày LDLK không được lớn hơn ngày hiện tại";
                        break;
                    case (int)enumHinhThucKhaiThac.KHAI_THAC_KHAC:
                        messageReturn = "Ngày khai thác không được lớn hơn ngày hiện tại";
                        break;
                }
                return messageReturn;
            });
            //RuleFor(x => x.KHAI_THAC_NGAY_DEN).LessThanOrEqualTo(DateTime.Now).WithMessage((model, str) =>
            //  {
            //      string messageReturn = string.Empty;
            //      switch (model.LOAI_HINH_KHAI_THAC_ID)
            //      {
            //          case (int)enumHinhThucKhaiThac.SXKD:
            //              messageReturn = "Ngày SXKD không được lớn hơn ngày hiện tại";
            //              break;
            //          case (int)enumHinhThucKhaiThac.CHO_THUE_TS:
            //              messageReturn = "";
            //              break;
            //          case (int)enumHinhThucKhaiThac.LDLK:
            //              messageReturn = "Ngày LDLK không được lớn hơn ngày hiện tại";
            //              break;
            //          default:
            //              messageReturn = "Ngày khai thác không được lớn hơn ngày hiện tại";
            //              break;
            //      }
            //      return messageReturn;
            //  });
        }
    }
}

