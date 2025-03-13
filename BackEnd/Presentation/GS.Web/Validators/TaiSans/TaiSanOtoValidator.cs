//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core.Domain.DanhMuc;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.TaiSans;

namespace GS.Web.Validators.TaiSans
{
	public partial class TaiSanOtoValidator : BaseGSValidator<TaiSanOtoModel>
	{
		public TaiSanOtoValidator(ITaiSanOtoModelFactory taiSanOtoModelFactory)
		{
			RuleFor(x => x.BIEN_KIEM_SOAT).Must((model, _bks) =>
			{
				if (!model.IsPhuongTienVanTai)
				{
					if (string.IsNullOrEmpty(_bks))
						return false;
					_bks = _bks.Replace('_', ' ').Replace('-', ' ').Trim();
					if (string.IsNullOrEmpty(_bks))
						return false;
				}
				return true;
			}).WithMessage("Bạn chưa nhập biển kiểm soát");
			RuleFor(x => x.BIEN_KIEM_SOAT).Must((model, _bks) =>
			{
				var check = taiSanOtoModelFactory.checkBienKiemSoat(_bks == null ? _bks : _bks.Replace('_', ' '), model.TAI_SAN_ID, model.IsPhuongTienVanTai);
				return check;
			}).WithMessage("Biển kiểm soát không được trùng.");
			RuleFor(x => x.SO_CHO_NGOI).Must((model, _choNgoi) =>
			{
				if (model.IsPhuongTienVanTai)
					return true;
				if ((model.SO_CHO_NGOI == null || model.SO_CHO_NGOI == 0) && (model.TAI_TRONG == 0 || model.TAI_TRONG == null))
					return false;
				return true;
			}).WithMessage("Mời nhập số chỗ ngồi hoặc tải trọng");
			RuleFor(p => p.SO_CAU_XE).Must((model, So_cau_xe) =>
			{
				if (!model.IsPhuongTienVanTai && model.SO_CHO_NGOI > 0 && model.SO_CHO_NGOI < 9 && !(model.SO_CAU_XE > 0))
				{
					return false;
				}
				return true;
			}).WithMessage("Phải nhập cầu xe");
			RuleFor(p => p.NHAN_XE_ID).Must((model, NHAN_XE_ID) =>
			{
				if (!model.IsPhuongTienVanTai &&(model.NHAN_XE_ID == 0 || model.NHAN_XE_ID == null ))
				{
					return false;
				}
				return true;
			}).WithMessage("Nhãn xe không được bỏ trống");
			RuleFor(x => x.SO_CHO_NGOI).Must((model, _choNgoi,context) =>
			{
				string messageReturn = string.Empty;
				if (model.SO_CHO_NGOI > 0 )
				{
					var check = taiSanOtoModelFactory.checkSoChoNgoi(loaiTaiSan: model.LOAI_TAI_SAN_ID.GetValueOrDefault(), soChoNgoi: model.SO_CHO_NGOI ?? 0, taiTrong: model.TAI_TRONG??0, messageReturn: ref messageReturn);

					context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
					return check;
				}
				context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
				return true;
			}).WithMessage("{messageReturn}");
			RuleFor(x => x.TAI_TRONG).Must((model, _taiTrong, context) =>
			{
				string messageReturn = string.Empty;
				if (model.TAI_TRONG > 0)
				{
					var check = taiSanOtoModelFactory.checkSoChoNgoi(loaiTaiSan: model.LOAI_TAI_SAN_ID.GetValueOrDefault(), soChoNgoi: model.SO_CHO_NGOI ?? 0, taiTrong: model.TAI_TRONG ?? 0, messageReturn: ref messageReturn);

					context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
					return check;
				}
				context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
				return true;
			}).WithMessage("{messageReturn}");
			RuleFor(x => x.TAI_TRONG).Must((model, _taiTrong) =>
			{
				if (_taiTrong < 0)
					return false;
				return true;
			}).WithMessage("Tải trọng không được là số âm");
			RuleFor(x => x.SO_CHO_NGOI).Must((model, _choNgoi) =>
			{
				if (_choNgoi < 0)
					return false;
				return true;
			}).WithMessage("Số chỗ ngồi không được là số âm");
			RuleFor(x => x.CONG_XUAT).Must((model, _congSuat) =>
			{
				if (_congSuat < 0)
					return false;
				return true;
			}).WithMessage("Công suất ngồi không được là số âm");
			RuleFor(x => x.DUNG_TICH).Must((model, _dungTich) =>
			{
				if (_dungTich < 0)
					return false;
				return true;
			}).WithMessage("Công suất ngồi không được là số âm");
			RuleFor(x => x.SO_KHUNG).Must((model, sk) =>
			{
				if (sk != null && sk.Trim().Length<3)
					return false;
				return true;
			}).WithMessage("Số khung phải có tối thiểu 3 ký tự.");
			RuleFor(x => x.SO_MAY).Must((model, sm) =>
			{
				if (sm != null && sm.Trim().Length < 3)
					return false;
				return true;
			}).WithMessage("Số máy phải có tối thiểu 3 ký tự.");
		}
	}
}

