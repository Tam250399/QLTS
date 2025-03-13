//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation;
using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Web.Factories.HeThong;
using GS.Web.Factories.NghiepVu;
using GS.Web.Factories.TaiSans;
using GS.Web.Framework.Validators;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Linq;

namespace GS.Web.Validators.NghiepVu
{
    public partial class YeuCauValidator : BaseGSValidator<YeuCauModel>
    {
        private readonly IYeuCauModelFactory _yeuCauModelFactory;
        private readonly ITaiSanOtoModelFactory _taiSanOtoModelFactory;

        public YeuCauValidator(IYeuCauModelFactory yeuCauModelFactory,
            ITaiSanOtoModelFactory taiSanOtoModelFactory,
            ICauHinhModelFactory cauHinhModelFactory)
        {
            this._yeuCauModelFactory = yeuCauModelFactory;
            _taiSanOtoModelFactory = taiSanOtoModelFactory;
            RuleFor(x => x.NGAY_BIEN_DONG).NotNull().WithMessage("Ngày biến động không được để trống");
            RuleFor(x => x.NGAY_BIEN_DONG).Must((model, ngayBD) =>
            {
                DateTime date;
                return DateTime.TryParse(ngayBD.ToString(), out date);
            }).WithMessage("Sai định dạng ngày");
            RuleFor(x => x.CHUNG_TU_NGAY).Must((model, code) =>
            {
                if (code.HasValue)
                {
                    DateTime date;
                    return DateTime.TryParse(code.ToString(), out date);
                }
                return true;
            }).WithMessage("Sai định dạng ngày");
            //nếu đang có yêu cầu chưa duyệt
            RuleFor(x => x.NGAY_BIEN_DONG).Must((model, ngayBD) =>
            {
                if (ngayBD.HasValue && model.NGAY_SU_DUNG.HasValue && ngayBD < model.NGAY_SU_DUNG)
                    return false;
                return true;
            }).WithMessage("Ngày biến động không được nhỏ hơn ngày sử dụng");
            RuleFor(x => x.NGAY_BIEN_DONG).Must((model, NGAY_BIEN_DONG) =>
            {
                if (NGAY_BIEN_DONG.HasValue)
                    return !cauHinhModelFactory.CheckYearIsLockSoTaiSan(NGAY_BIEN_DONG.Value.Year);
                return true;
            }).WithMessage("Năm này đã bị khóa sổ");
            RuleFor(x => x.NGAY_BIEN_DONG).Must((model, NgayBienDong) =>
            {
                if (model.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
                {
                    if (string.IsNullOrEmpty(model.strTaiSanIds))
                        return _yeuCauModelFactory.IsNgayBienDongMoiNhat(NgayBienDong, model.TAI_SAN_ID, model.ID);
                    // sử dụng khi mà thêm mới biến động giảm nhiều tài sản
                    else
                        return _yeuCauModelFactory.IsNgayBienDongMoiNhat(NgayBienDong, model.strTaiSanIds);
                }
                return true;
            }).WithMessage("Ngày biến động phải lớn hơn ngày biến động trước đó");

            RuleFor(x => x.NGAY_BIEN_DONG).Must((model, ngayBD) =>
            {
                if (model.LOAI_BIEN_DONG_ID != (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO)
                    return _yeuCauModelFactory.IsTrungNgayBD(ngayBD, model.TAI_SAN_ID, model.ID);
                return true;
            }).WithMessage("Ngày biến động không được trùng");

            RuleFor(x => x.NGAY_BIEN_DONG).Must((model, ngayBD) =>
            {
                if (ngayBD.HasValue && ngayBD > DateTime.Now)
                    return false;
                return true;
            }).WithMessage("Ngày biến động không được lớn hơn ngày " + DateTime.Now.ToShortDateString());

            RuleFor(x => x.CHUNG_TU_NGAY).Must((model, ngayCT) =>
            {
                if (ngayCT.HasValue && ngayCT > DateTime.Now)
                    return false;
                return true;
            }).WithMessage("Ngày chứng từ không được lớn hơn ngày " + DateTime.Now.ToShortDateString());
           
            RuleFor(x => x.CHUNG_TU_NGAY).Must((model, ngayCT) =>
            {
                if (ngayCT.HasValue && ngayCT < model.NGAY_SU_DUNG)
                    return false;
                return true;
            }).WithMessage("Ngày chứng từ không được nhỏ hơn ngày sử dụng");
            // them validate 10/07/2021            
          
            RuleFor(x => x.QUYET_DINH_SO).Must((model, quyetdinhso) =>
            {
                if(model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                {
                    if (string.IsNullOrEmpty(quyetdinhso))
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Số quyết định không được để trống");
            RuleFor(x => x.QUYET_DINH_NGAY).Must((model, quyetdinhngay) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                {
                    if (quyetdinhngay == null)
                    {
                        return false;
                    }
                }
                return true;
            }).WithMessage("Ngày quyết định không được để trống");         
            RuleFor(x => x.CHUNG_TU_SO).Must((model, chungtuso) =>
            {
                if(model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO  )
                {
                    if (string.IsNullOrEmpty(chungtuso)
                    && (model.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.BAN_CHUYEN_NHUONG || model.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.THANH_LY)
                    && model.YCCTModel.IS_BAN_THANH_LY == true)
                    {
                        return false;
                    }
                    else if (string.IsNullOrEmpty(chungtuso) && (model.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.BAN_CHUYEN_NHUONG || model.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.THANH_LY)
                    && model.YCCTModel.IS_BAN_THANH_LY == false)
                    {
                        return true;
                    }
                    else if (string.IsNullOrEmpty(chungtuso))
                    {
                        return false;
                    }                     
                }
                return true;
            }).WithMessage("Số biên bản/ hợp đồng không được để trống");
            RuleFor(x => x.CHUNG_TU_NGAY).Must((model, chungtungay) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO)
                {
                    if (chungtungay == null
                    && (model.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.BAN_CHUYEN_NHUONG || model.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.THANH_LY)
                    && model.YCCTModel.IS_BAN_THANH_LY == true)
                    {
                        return false;
                    }
                    else if ((model.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.BAN_CHUYEN_NHUONG || model.LY_DO_BIEN_DONG_ID == (int)enumLY_DO_GIAM_TOAN_BO.THANH_LY)
                    && model.YCCTModel.IS_BAN_THANH_LY == false)
                    {
                        return true;
                    }
                    else if(chungtungay == null)
                        return false;
                }
                return true;
            }).WithMessage("Ngày biên bản/ hợp đồng không được để trống");

            RuleFor(x => x.NGUYEN_GIA).Must((model, NGUYEN_GIA_BD) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI ||
                    model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI ||
                    model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO ||
                    model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                {
                    if (NGUYEN_GIA_BD == default(decimal?) || NGUYEN_GIA_BD <= 0)
                        return false;
                }
                return true;
            }).WithMessage("Giá trị không được trống");

            //Thay đổi thông tin
            RuleFor(x => x.TAI_SAN_TEN).Must((model, TenTaiSan) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                {
                    if (string.IsNullOrEmpty(TenTaiSan))
                        return false;
                    return true;
                }
                return true;
            }).WithMessage("Tên tài sản không được trống");
            //RuleFor(x => x.YCCTModel.DIA_CHI).Must((model, DiaChi) =>
            //{
            //    if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
            //    {
            //        if (string.IsNullOrEmpty(DiaChi))
            //        {
            //            return false;
            //        }
            //        else

            //            return true;
            //    }
            //    return true;
            //}).WithMessage("Địa chỉ không được để trống");
            RuleFor(x => x.YCCTModel.DatTinhId).Must((model, TinhId) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                {
                    if (TinhId <= 0 )
                    {
                        return false;
                    }
                    else

                        return true;
                }
                return true;
            }).WithMessage("Chưa chọn Tỉnh/Thành phố.");
            RuleFor(x => x.YCCTModel.DatHuyenId).Must((model, HuyenId) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                {
                    if (HuyenId <= 0)
                    {
                        return false;
                    }
                    else

                        return true;
                }
                return true;
            }).WithMessage("Chưa chọn huyện");
            //RuleFor(x => x.YCCTModel.DatXaId).Must((model, XaId) =>
            //{
            //    if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
            //    {
            //        if (XaId <= 0)
            //        {
            //            return false;
            //        }
            //        else

            //            return true;
            //    }
            //    return true;
            //}).WithMessage("Chưa chọn xã");
            //Điều chuyển 1 phần
            RuleFor(x => x.YCCTModel.DAT_TONG_DIEN_TICH).Must((model, dienTich) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    if (dienTich > 0)
                        return true;
                    else
                        return false;
                return true;
            }).WithMessage("Phải nhập diện tích điều chuyển");

            RuleFor(x => x.YCCTModel.NHA_TONG_DIEN_TICH_XD).Must((model, dienTich) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                    if (dienTich > 0)
                        return true;
                    else
                        return false;
                return true;
            }).WithMessage("Phải nhập diện tích điều chuyển");

            //RuleFor(x => x.YCCTModel.DAT_TONG_DIEN_TICH).Must((model, dienTich) =>
            //{
            //	if ((model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI) && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
            //		if (dienTich > 0)
            //			return true;
            //		else
            //			return false;
            //	return true;
            //}).WithMessage("Phải nhập diện tích biến động");
            //RuleFor(x => x.YCCTModel.NHA_TONG_DIEN_TICH_XD).Must((model, dienTich) =>
            //{
            //	if ((model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_TANG_GIA_TRI) && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
            //		if (dienTich > 0)
            //			return true;
            //		else
            //			return false;
            //	return true;
            //}).WithMessage("Phải nhập diện tích biến động");
            RuleFor(x => x.LOAI_TAI_SAN_ID).Must((model, loaiTaiSan) =>
            {
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN)
                {
                    if (!loaiTaiSan.HasValue && !model.LOAI_TAI_SAN_DON_VI_ID.HasValue)
                        return false;
                    else
                        return true;
                }
                return true;
            }).WithMessage("Loại tài sản không được trống");
            //RuleFor(x => x.YCCTModel.OTO_TAI_TRONG).Must((model, taiTrong) =>
            //{
            //    if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN && model.LOAI_HINH_TAI_SAN_ID== (int)enumLOAI_HINH_TAI_SAN.OTO)
            //    {
            //        if (taiTrong > 0 || model.SO_CHO_NGOI>0)
            //            return true;
            //        return false;
            //    }
            //    return true;
            //}).WithMessage("Phải nhập tải trọng hoặc số chỗ");
            RuleFor(x => x.LY_DO_BIEN_DONG_ID).NotEqual(0).WithMessage("Phải chọn lý do");
            RuleFor(x => x.YCCTModel.DAT_TONG_DIEN_TICH).Must((model, code) => _yeuCauModelFactory.CheckDienTichBienDong(model)).WithMessage("Diện tích phải lớn hơn 0");
            RuleFor(x => x.YCCTModel.NHA_TONG_DIEN_TICH_XD).Must((model, code) => _yeuCauModelFactory.CheckDienTichBienDong(model)).WithMessage("Diện tích phải lớn hơn 0");
            //RuleFor(x => x.YCCTModel.PHI_THU).Must((model, tienThanhLy) =>
            //{
            //    if (model.YCCTModel.IS_BAN_THANH_LY == true && (model.YCCTModel.PHI_THU == null || model.YCCTModel.PHI_THU == 0))
            //    {
            //        return false;
            //    }
            //    return true;
            //}).WithMessage("Tiền thanh lý phải có giá trị.");
            RuleFor(x => x.DonViNhanDieuChuyenId).Must((model, donViId) =>
            {
                return yeuCauModelFactory.CheckDonViDieuChuyen(model);
                //return true;
            }).WithMessage("Bạn chưa chọn đơn vị nhận điều chuyển.");
            RuleFor(x => x.DonViNhanDieuChuyenId).Must((model, donViId) =>
            {
                return yeuCauModelFactory.CheckTrungDonViDieuChuyen(model);
            }).WithMessage("Không thể điều chuyển đến đơn vị đang đứng.");
            RuleFor(x => x.YCCTModel.HINH_THUC_XU_LY_ID).Must((model, PhuongAnXuLyId) =>
            {
                var list_ly_do_giam_can_thu_tien = Enum.GetValues(typeof(enumLY_DO_GIAM_THU_TIEN)).Cast<enumLY_DO_GIAM_THU_TIEN>().Select(p => (decimal?)p);
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_TOAN_BO && list_ly_do_giam_can_thu_tien.Contains(model.LY_DO_BIEN_DONG_ID) && PhuongAnXuLyId <= 0)
                    return false;
                return true;
            }).WithMessage("Phải chọn hình thức xử lý.");
            RuleFor(x => x.SO_CHO_NGOI).Must((model, _choNgoi, context) =>
            {
                string messageReturn = string.Empty;
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO && model.YCCTModel.OTO_SO_CHO_NGOI > 0)
                {
                    var check = taiSanOtoModelFactory.checkSoChoNgoi(loaiTaiSan: model.LOAI_TAI_SAN_ID.GetValueOrDefault(), soChoNgoi: model.YCCTModel.OTO_SO_CHO_NGOI ?? 0, taiTrong: model.YCCTModel.OTO_TAI_TRONG ?? 0, messageReturn: ref messageReturn);

                    context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
                    return check;
                }
                context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
                return true;
            }).WithMessage("{messageReturn}");
            RuleFor(x => x.TAI_TRONG).Must((model, _taiTrong, context) =>
            {
                string messageReturn = string.Empty;
                if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.THAY_DOI_THONG_TIN && model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO && model.YCCTModel.OTO_TAI_TRONG > 0)
                {
                    var check = taiSanOtoModelFactory.checkSoChoNgoi(loaiTaiSan: model.LOAI_TAI_SAN_ID.GetValueOrDefault(), soChoNgoi: model.YCCTModel.OTO_SO_CHO_NGOI ?? 0, taiTrong: model.YCCTModel.OTO_TAI_TRONG ?? 0, messageReturn: ref messageReturn);

                    context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
                    return check;
                }
                context.MessageFormatter.AppendArgument("messageReturn", messageReturn);
                return true;
            }).WithMessage("{messageReturn}");
            //RuleFor(x => x.NGUYEN_GIA).Must((model, nguyenGia) =>
            //{
            //	if (model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI ||
            //		model.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.DIEU_CHUYEN_MOT_PHAN)
            //	{
            //		return _yeuCauModelFactory.CheckEachNguonVonMinusIsValid(model.TAI_SAN_ID, model.NGUON_VON_JSON);
            //	}
            //	return true;
            //}).WithMessage((model,message) =>
            //{
            //	return "Nguồn vốn giảm vượt quá nguồn vốn cho phép";
            //});
        }
    }
   
}