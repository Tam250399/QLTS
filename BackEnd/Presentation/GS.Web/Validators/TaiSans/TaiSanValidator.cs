//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;
using System;
using System.Linq;

namespace GS.Web.Validators.TaiSans
{
	public partial class TaiSanValidator : BaseGSValidator<TaiSanModel>
	{
		public TaiSanValidator(ITaiSanModelFactory taisanModelFactory
			, ILoaiTaiSanModelFactory loaiTaiSanModelFactory
			, IYeuCauModelFactory yeuCauModelFactory
			, IDuAnModelFactory duAnModelFactory
			, ICauHinhModelFactory cauHinhModelFactory
			, ILyDoBienDongService lyDoBienDongService
			, ILyDoBienDongModelFactory lyDoBienDongModelFactory
		
			)
		{
			//RuleFor(x => x.MA).NotEmpty().WithMessage("Mã không được để trống");
			RuleFor(x => x.TEN).Must((model, Ten) =>
			{
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
					return true;
				else if (!string.IsNullOrEmpty(Ten))
					return true;
				return false;
			}).WithMessage("Tên không được để trống");

			RuleFor(x => x.PHUONG_THUC_MUA_SAM_ID).Must((model, PhuongThucMuaSam) =>
			{

				if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT
					&& model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA
					&& model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC
					&& (lyDoBienDongModelFactory.CheckLyDoTheoEnum(model.LY_DO_BIEN_DONG_ID.Value, enum_LY_DO_BIEN_DONG.MUA_SAM)))
				{
                    if (PhuongThucMuaSam == 0 || PhuongThucMuaSam == null)
                    {
						return false;
                    }
					return true;
				}
				return true;

			}).WithMessage("Phương thức mua sắm không được bỏ trống");

			RuleFor(x => x.DON_VI_MUA_SAM_TAP_TRUNG_ID).Must((model, donVi) =>
			{
				if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT
					&& model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA
					&& model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC
					&& (lyDoBienDongModelFactory.CheckLyDoTheoEnum(model.LY_DO_BIEN_DONG_ID.Value, enum_LY_DO_BIEN_DONG.MUA_SAM)))
				{
					if (model.PHUONG_THUC_MUA_SAM_ID == (int)enumPHUONG_THUC_MUA_SAM.TapTrung && (model.DON_VI_MUA_SAM_TAP_TRUNG_ID == 0 || model.DON_VI_MUA_SAM_TAP_TRUNG_ID == null))
					{
						return false;
					}
					return true;
				}
				return true;

			}).WithMessage("Đơn vị mua sắm tập trung không được bỏ trống");

			//RuleFor(x => x.TEN).NotEmpty().WithMessage("Tên không được để trống");
			RuleFor(x => x.TEN).Must((model, tenTS) =>
			{
				if (model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
					return true;
				if (tenTS == null)
					return false;
				if (tenTS.Trim().Length < 3)
					return false;
				else
					return true;
			}).WithMessage("Tên tài sản phải có tối thiểu 3 ký tự.");
			RuleFor(x => x.NGAY_NHAP).NotEmpty().WithMessage("Ngày kê khai không được để trống");
			RuleFor(x => x.NGAY_NHAP).Must((model, ngaynhap) =>
			{
				if (ngaynhap.HasValue && model.YeuCauId > 0)
				{
					var check = yeuCauModelFactory.IsLonHonNgayBD(ngaynhap.Value, model.ID, model.YeuCauId ?? 0);
					if (check)
						return false;
					else
						return true;
				}
				return true;
			}).WithMessage("Ngày không được lớn hơn ngày kê khai của biến động");
			RuleFor(x => x.NGAY_NHAP).Must((model, ngaynhap) =>
			{
				if (model.isTangMoi == true && ngaynhap.HasValue)
				{
					DateTime compareDate = new DateTime(2018, 01, 01);
					if (ngaynhap < compareDate)
						return false;
					else
						return true;
				}
				return true;
			}).WithMessage("Ngày tăng tài sản phải lớn hơn hoặc bằng ngày 01/01/2018");
			RuleFor(x => x.NGAY_NHAP).Must((model, ngaynhap) =>
			{
				if (ngaynhap.HasValue)
					return !cauHinhModelFactory.CheckYearIsLockSoTaiSan(ngaynhap.Value.Year);
				return true;
			}).WithMessage("Năm này đã bị khóa sổ");
			//RuleFor(x => x.NGAY_SU_DUNG).NotEmpty().WithMessage("Ngày sử dụng không được để trống");
			RuleFor(x => x.NGAY_SU_DUNG).Must((model, ngaysd) =>
			{
				if (!(lyDoBienDongModelFactory.CheckLyDoTheoEnum(model.LY_DO_BIEN_DONG_ID.Value, enum_LY_DO_BIEN_DONG.KIEM_KE_PHAT_HIEN_THUA)) && !(ngaysd.HasValue))
					return false;
				return true;
			}).WithMessage("Ngày sử dụng không được để trống");
			RuleFor(x => x.LOAI_TAI_SAN_ID).Must((model, loaiTSId, context) =>
			{
				string message = string.Empty;
				switch (model.LOAI_HINH_TAI_SAN_ID)
				{
					case (int)enumLOAI_HINH_TAI_SAN.DAT:
						message = "Bạn chưa chọn mục đích sử dụng";
						break;
					case (int)enumLOAI_HINH_TAI_SAN.NHA:
						message = "Bạn chưa chọn cấp nhà";
						break;
					case (int)enumLOAI_HINH_TAI_SAN.OTO:
						message = "Bạn chưa chọn loại xe";
						break;
					default:
						message = "Bạn chưa chọn loại tài sản";
						break;
				}
				context.MessageFormatter
					.AppendArgument("Message", message);

				if (!(loaiTSId > 0) && !(model.LOAI_TAI_SAN_DON_VI_ID > 0))
				{
					return false;
				}
				else
					return true;
			}).WithMessage("{Message}");

			RuleFor(x => x.LOAI_TAI_SAN_ID).Must((model, loaiTSId) =>
			{
				if (loaiTSId.HasValue)
				{
					var check = loaiTaiSanModelFactory.CheckLoaiTaiSanCha(loaiTSId.Value);
					if (check)
						return false;
					else
						return true;
				}
				else
					return true;
			}).WithMessage("Không được chọn loại tài sản cha.");
			RuleFor(x => x.LY_DO_BIEN_DONG_ID).NotEqual(0, null).WithMessage("Chưa chọn lý do tăng");
			RuleFor(x => x.MA).Must((model, code) =>
			{
				return taisanModelFactory.CheckMaTaiSan(model.MA, model.ID);
			}).WithMessage("Mã tài sản đã tồn tại");
			RuleFor(x => x.TEN).Must((model, ten) =>
			{
				if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.OTO && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC)
					return taisanModelFactory.CheckTenTaiSan(ten: ten, id: model.ID, donViId: model.DON_VI_ID);
				return true;
			}).WithMessage("Tên tài sản đã tồn tại");
			RuleFor(x => x.NAM_SAN_XUAT).Must((model, namSX) =>
			{
				if (namSX.HasValue && model.NGAY_SU_DUNG.HasValue && namSX > model.NGAY_SU_DUNG.Value.Year)
				{
					return false;
				}
				return true;
			}).WithMessage("Năm sản xuất phải nhỏ hơn hoặc bằng năm sử dụng");
			RuleFor(x => x.QUYET_DINH_SO).Must((model, CNQSD_SO) =>
			{
				if (String.IsNullOrEmpty(CNQSD_SO) && model.QUYET_DINH_NGAY != null)
				{
					return false;
				}
				return true;
			}).WithMessage("Chưa có số quyết định");
			RuleFor(x => x.QUYET_DINH_SO).Must((model, CNQSD_SO) =>
			{
				if (String.IsNullOrEmpty(CNQSD_SO) && model.QUYET_DINH_NGAY != null)
				{
					if (CNQSD_SO.Trim().Length < 3 && model.QUYET_DINH_NGAY != null)
						return true;
					return false;
				}
				return true;
			}).WithMessage("Số quyết định phải có tối thiểu 3 ký tự.");
			RuleFor(x => x.QUYET_DINH_NGAY).Must((model, CNQSD_NGAY) =>
			{
				if (CNQSD_NGAY == null && !String.IsNullOrEmpty(model.QUYET_DINH_SO))
				{
					return false;
				}

				return true;
			}).WithMessage("Chưa có ngày quyết định");
			RuleFor(x => x.CHUNG_TU_SO).Must((model, chungtuso) =>
			{
				if (String.IsNullOrEmpty(chungtuso) && model.CHUNG_TU_NGAY != null)
				{
					return false;
				}

				return true;
			}).WithMessage("Chưa có số chứng từ");
			RuleFor(x => x.CHUNG_TU_SO).Must((model, chungtuso) =>
			{
				if (String.IsNullOrEmpty(chungtuso) && model.CHUNG_TU_NGAY != null)
				{
					if (chungtuso.Trim().Length < 3 && model.CHUNG_TU_NGAY != null)
						return true;
					return false;
				}
				return true;
			}).WithMessage("Số chứng từ phải có tối thiểu 3 ký tự.");
			RuleFor(x => x.CHUNG_TU_NGAY).Must((model, CNQSD_NGAY) =>
			{
				if (CNQSD_NGAY == null && !String.IsNullOrEmpty(model.CHUNG_TU_SO))
				{
					return false;
				}

				return true;
			}).WithMessage("Chưa có ngày chứng từ");
			RuleFor(x => x.nvYeuCauChiTietModel.NGUYEN_GIA).Must((model, NGUYEN_GIA) =>
			{
				if (NGUYEN_GIA == default(decimal) || NGUYEN_GIA == null)
					return false;
				return true;
			}).WithMessage("Tổng nguyên giá phải có giá trị > 0.");
            RuleFor(x => x.nvYeuCauChiTietModel.DAT_GIA_TRI_QUYEN_SD_DAT).Must((model, Gia_QSD) =>
            {
                if(model.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    if (Gia_QSD == null)
                        return false;
                    return true;
                } return true;
            }).WithMessage("Giá trị quyền sử dụng đất không được để trống");
            RuleFor(x => x.nvYeuCauChiTietModel.DAT_GIA_TRI_QUYEN_SD_DAT).Must((model, Gia_QSD) =>
            {
                if (model.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.DAT)
                {
                    if (Gia_QSD > model.nvYeuCauChiTietModel.NGUYEN_GIA)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }).WithMessage("Giá trị quyền sử dụng đất không được lớn hơn nguyên giá");
            RuleFor(x => x.nvYeuCauChiTietModel.HM_GIA_TRI_CON_LAI).Must((model, GIA_TRI_CON_LAI) =>
			{
				if (GIA_TRI_CON_LAI > model.nvYeuCauChiTietModel.NGUYEN_GIA)
				{
					return false;
				}
				return true;
			}).WithMessage("Giá trị còn lại không được lớn hơn tổng nguyên giá.");
			RuleFor(x => x.NGAY_NHAP).Must((model, NGAY_NHAP) =>
			{
				var toDay = DateTime.Now;
				if (NGAY_NHAP > toDay)
					return false;
				return true;
			}).WithMessage("Ngày nhập không được lớn hơn ngày hiện tại.");
			//RuleFor(x => x.nvYeuCauChiTietModel.MUC_DICH_SU_DUNG_ID).Must((model, mucDichSD) =>
			//{
			//	if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && (mucDichSD == null || mucDichSD <= 0))
			//		return false;
			//	return true;
			//}).WithMessage("Chưa chọn mục đích được giao.");
			RuleFor(x => x.GIA_MUA_TIEP_NHAN).Must((model, GiaMuaTiepNhan) =>
			{
				// giá hóa đơn bắt cho oto và phải nhập
				if (model.LY_DO_BIEN_DONG_ID == lyDoBienDongService.GetIdLyDoBienDongByMa(enum_LY_DO_BIEN_DONG.MUA_SAM) && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO && !(GiaMuaTiepNhan > 0))
				{
					return false;
				}
				return true;
			}).WithMessage("Phải nhập giá mua trên hóa đơn.");
			RuleFor(x => x.DU_AN_ID).Must((model, duAn) =>
			{
				if (model.isCreateTSDA != null && model.isCreateTSDA.Value && (model.DU_AN_ID == null || model.DU_AN_ID <= 0))
				{
					return false;
				}
				return true;
			}).WithMessage("Dự án không được để trống.");


			//RuleFor(x => x.QUYET_DINH_NGAY).Must((model, CNQSD_NGAY) =>
			//{
			//	if (CNQSD_NGAY != null && model.NGAY_SU_DUNG.HasValue)
			//	{
			//		if (model.NGAY_SU_DUNG < CNQSD_NGAY)
			//		{
			//			return true;
			//		}
			//	}
			//	return false;
			//}).WithMessage("Ngày quyết định phải nhỏ hơn hoặc bằng ngày sử dụng");
			//RuleFor(x => x.QUYET_DINH_NGAY).Must((model, CNQSD_NGAY) =>
			//{
			//	if (CNQSD_NGAY != null && model.NGAY_NHAP.HasValue)
			//	{
			//		if (model.NGAY_NHAP < CNQSD_NGAY)
			//		{
			//			return true;
			//		}
			//	}
			//	return false;
			//}).WithMessage("Ngày quyết định phải nhỏ hơn hoặc bằng ngày nhập");


			#region Tai san du an
			// check ngay nhập với ngày bắt đầu dự án khi tao tài sản 
			RuleFor(x => x.NGAY_NHAP).Must((model, ngaynhap) =>
			{
				if (model.IsBanQuanLyDuAn == true && model.DU_AN_ID.HasValue && ngaynhap.HasValue)
				{
					DateTime? StartDateduan = duAnModelFactory.CheckNgayDuAn(model.DU_AN_ID ?? 0);
					if (StartDateduan.HasValue)
					{
						var _tungay = StartDateduan.Value.Date;
						if (ngaynhap < _tungay)
						{
							return false;
						};
					}
				}
				return true;
			}).WithMessage("Ngày nhập phải lớn hơn hoặc bằng ngày bắt đầu của dự án");

			RuleFor(x => x.HIEN_TRANG_SU_DUNG_ALL).Must((model, htsd) =>
			{
				if (model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.DAT && model.LOAI_HINH_TAI_SAN_ID != (int)enumLOAI_HINH_TAI_SAN.NHA)
				{
					var listHienTrang = model.nvYeuCauChiTietModel.lstHienTrang;
					if (listHienTrang != null && listHienTrang.Count() > 0)
					{
						var count = listHienTrang.Where(c => c.GiaTriCheckbox == true || c.GiaTriNumber > 0).Count();
						if (count == 0)
						{
							return false;
						}
					}
				}
				return true;
			}).WithMessage("Hiện trạng sử dụng không được để trống");

			//RuleFor(x => x.LOAI_TAI_SAN_ID).Must((model, loaiTS) =>
			//{
			//	return taisanModelFactory.CheckDonViSuNghiepKhongNhapXeChucDanh(model);
				
			//}).WithMessage("Đơn vị sự nghiệp không được chọn nhóm xe chức danh");


			#endregion
		}
	}
}

