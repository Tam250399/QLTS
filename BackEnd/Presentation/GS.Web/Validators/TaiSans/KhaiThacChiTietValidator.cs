using FluentValidation;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Validators.TaiSans
{
	public partial class KhaiThacChiTietValidator : BaseGSValidator<KhaiThacChiTietModel>
	{
		public KhaiThacChiTietValidator(IKhaiThacChiTietModelFactory khaiThacChiTietModelFactory)
		{
			//RuleFor(p => p.SO_TIEN_THU_DUOC).NotEmpty().WithMessage("Số tiền thu được không được trống");
			//RuleFor(p => p.NGAY_KHAI_THAC).NotEmpty().WithMessage("Phải nhập ngày khai thác");
			//RuleFor(p => p.NGAY_KHAI_THAC).Must((model, ngayKhaiThac) =>
			//{
			//	return khaiThacChiTietModelFactory.ValidateNgayKhaiThac(model);
			//}).WithMessage("Ngày khai thác phải lớn hơn ngày quyết định");
			RuleFor(c => c.SO_TIEN_THU_DUOC).Must((model, sotienthuduoc) => {
				if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK)
				{
					if (sotienthuduoc == 0)
					{
						return false;
					}
				}
				return true;
			}).WithMessage("Số tiền thu được không được trống");
			RuleFor(p => p.NGAY_KHAI_THAC).Must((model, ngayKhaiThac) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS)
				{
					if (ngayKhaiThac == null)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Ngày bắt đầu cho thuê không được để trống");

			RuleFor(p => p.NGAY_KHAI_THAC).Must((model, ngayKhaiThac) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
				{
					if (ngayKhaiThac == null)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Ngày bắt đầu LDLK không được để trống");

			RuleFor(p => p.NGAY_KHAI_THAC).Must((model, ngayKhaiThac) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.CHO_THUE_TS && model.LOAI_HINH_KHAI_THAC_ID != (int)enumHinhThucKhaiThac.LDLK)
				{
					return khaiThacChiTietModelFactory.ValidateNgayKhaiThac(model);
				}
				return true;
			}).WithMessage("Ngày khai thác phải lớn hơn ngày quyết định");


			RuleFor(p => p.NGAY_KHAI_THAC).Must((model, ngayKhaiThac) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS)
				{
					if(model.NGAY_KHAI_THAC < model.NGAY_SU_DUNG)
                    {
						return false;
                    }
					return true;
				}
				return true;
			}).WithMessage("Ngày cho thuê không được nhỏ hơn ngày đưa vào sử dụng");

			RuleFor(p => p.NGAY_KHAI_THAC).Must((model, ngayKhaiThac) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
				{
					if (model.NGAY_KHAI_THAC < model.NGAY_SU_DUNG)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Ngày LDLK không được nhỏ hơn ngày đưa vào sử dụng");

			//more
			//RuleFor(p => p.HOP_DONG_SO).NotEmpty().WithMessage("Số hợp đồng không được trống");
			RuleFor(p => p.HOP_DONG_SO).Must((model, hopdongso) => {
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS || model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
				{
					if (string.IsNullOrEmpty(hopdongso))
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Số hợp đồng không được trống");
			//RuleFor(p => p.HOP_DONG_NGAY).NotEmpty().WithMessage("Ngày hợp đồng không được trống");
			RuleFor(p => p.HOP_DONG_NGAY).Must((model, hopdongngay) => {
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS || model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
				{
					if (hopdongngay == null)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Ngày hợp đồng không được trống");
			//RuleFor(x => x.HOP_DONG_NGAY).LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày hợp đồng không được lớn hơn ngày hiện tại");
			RuleFor(x => x.HOP_DONG_NGAY).LessThanOrEqualTo(DateTime.Now).WithMessage((model, str) =>
			{
				string messageReturn = string.Empty;
				switch (model.LOAI_HINH_KHAI_THAC_ID)
				{
					case (int)enumHinhThucKhaiThac.CHO_THUE_TS:
						messageReturn = "Ngày hợp đồng không được lớn hơn ngày hiện tại";
						break;
					default:
						messageReturn = "";
						break;
				}
				return messageReturn;
			});
			RuleFor(p => p.DOI_TAC_ID).Must((model, doitacid) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS)
				{
					if (doitacid == null || doitacid == 0)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Chưa chọn đơn vị thuê");
			RuleFor(p => p.DOI_TAC_ID).Must((model, doitacid) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
				{
					if (doitacid == null || doitacid == 0)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Chưa chọn đối tác LDLK");
			RuleFor(p => p.NGAY_KHAI_THAC_DEN).Must((model, ngaykhaithacden) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS)
				{
					if (ngaykhaithacden < model.NGAY_KHAI_THAC)
					{ 
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Ngày cho thuê đến phải lớn hơn ngày bắt đầu cho thuê");
			RuleFor(p => p.NGAY_KHAI_THAC_DEN).Must((model, ngaykhaithacden) =>
			{
				if ( model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
				{
					if (ngaykhaithacden < model.NGAY_KHAI_THAC)
					{ 
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Ngày LDLK đến phải lớn hơn ngày bắt đầu LDLK");
			//RuleFor(p => p.CHO_THUE_PHUONG_THUC_ID).Equal(0).WithMessage("Chọn phương thức cho thuê");
			//RuleFor(p => p.CHO_THUE_PHUONG_THUC_ID).NotEqual(0).WithMessage((model, str) =>
			RuleFor(p => p.CHO_THUE_PHUONG_THUC_ID).Must((model, phuongthucchothue) => {
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS)
				{
					if (phuongthucchothue == 0)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Chọn phương thức cho thuê");
			RuleFor(p => p.LDLK_HINH_THUC_ID).Must((model, phuongThucLDLK) => {
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
				{
					if (phuongThucLDLK == 0)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Chọn hình thức LDLK");
			RuleFor(p => p.DIEN_TICH_KHAI_THAC).Must((model, dienTichKhaiThac) =>
			{
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
				{
					var DienTichKhaiThac = dienTichKhaiThac ?? 0;

					if (DienTichKhaiThac == 0)
					{
						return false;
					}
				}
				return true;
			}).WithMessage("Diện tích khai thác phải lớn hơn 0");

			

			RuleFor(p => p.DIEN_TICH_KHAI_THAC).Must((model, dienTichKhaiThac) =>
			{
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                {
					var dienTich = model.DIEN_TICH ?? 0;
					var DienTichKhaiThac = dienTichKhaiThac ?? 0;

					if (DienTichKhaiThac > dienTich )
					{
						return false;
					}
				}
				
				return true;
			}).WithMessage("Diện tích khai thác không được lớn hơn diện tích sử dụng");

			RuleFor(p => p.CHO_THUE_GIA).Must((model, CHO_THUE_GIA) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS )	
				{
					if (CHO_THUE_GIA == null)
					{
						return false;
					}
					return true;
				}
				return true;
			}).WithMessage("Giá cho thuê không được để trống");
			RuleFor(p => p.NGAY_KHAI_THAC_DEN).Must((model, ngaykhaithacden) =>
			{
				if (model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.CHO_THUE_TS || model.LOAI_HINH_KHAI_THAC_ID == (int)enumHinhThucKhaiThac.LDLK)
				{
					if (!ngaykhaithacden.HasValue)
					{ return false; }
					return true;
				}
				return true;
			}).WithMessage("Ngày khai thác đến không được để trống");

		}
	}
}
