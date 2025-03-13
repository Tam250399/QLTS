//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Web.Framework.Models;
using GS.Web.Validators.BienDongs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.BienDongs
{
    [Validator(typeof(BienDongValidator))]
    public class BienDongModel : BaseGSEntityModel
    {
        public BienDongModel()
        {

        }
        public Decimal TAI_SAN_ID { get; set; }
        public String TAI_SAN_MA { get; set; }
        public String TAI_SAN_TEN { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? NGUYEN_GIA { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        public String CHUNG_TU_SO { get; set; }
        public DateTime? CHUNG_TU_NGAY { get; set; }
        public DateTime NGAY_BIEN_DONG { get; set; }
        public DateTime? NGAY_DUYET { get; set; }
        public Decimal? NGUOI_DUYET_ID { get; set; }
        public DateTime? NGAY_SU_DUNG { get; set; }
        public Decimal? LOAI_BIEN_DONG_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal TRANG_THAI_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public Guid GUID { get; set; }
        public String GHI_CHU { get; set; }
        public DateTime? QUYET_DINH_NGAY { get; set; }
        public String QUYET_DINH_SO { get; set; }
		public bool? IS_BIENDONG_CUOI { get; set; }
        public Decimal? LOAI_TAI_SAN_DON_VI_ID { get; set; }
        public Decimal? TONG_HOA_HONG_CHIET_KHAU { get; set; }
        public Decimal? HOA_HONG_NOP_NSNN { get; set; }
        public Decimal? HOA_HONG_DE_LAI_DON_VI { get; set; }
        public Decimal? DAT_TONG_DIEN_TICH { get; set; }
        public Decimal? NHA_TONG_DIEN_TICH_XD { get; set; }
        public Decimal? VKT_DIEN_TICH { get; set; }

        public String TenLyDoBienDong { get; set; }
        public String TenBoPhanSuDung { get; set; }
        public String TenLoaiTaiSan { get; set; }
    }
    public partial class BienDongSearchModel : BaseSearchModel
    {
        public BienDongSearchModel()
        {
            BoPhanSuDungAvailable = new List<SelectListItem>();
            LoaiBienDongAvailable = new List<SelectListItem>();
            TrangThaiDongBoAvailabe = new List<SelectListItem>();

            LstLoaiHinhTaiSan = new List<int>();
            LstLoaiBienDong = new List<int>();
            LstTrangThaiDongBo = new List<int>();
        }
        public string KeySearch { get; set; }
        public Guid TaiSanGuid { get; set; }
        public string MaDonVi { get; set; }
        public Decimal? LOAI_LY_DO_BD_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public Decimal? LY_DO_BIEN_DONG_ID { get; set; }
        public string CHUNG_TU_SO { get; set; }
        public string QUYET_DINH_SO { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal NGUOI_TAO_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? DON_VI_BO_PHAN_ID { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_BIEN_DONG { get; set; }
        [UIHint("DateNullable")]
        public DateTime? FromDate { get; set; }
        [UIHint("DateNullable")]
        public DateTime? ToDate { get; set; }
        public SelectList Trangthailist { get; set; }
        public IList<SelectListItem> LoaiBienDongAvailable { get; set; }
        public enumTRANG_THAI_YEU_CAU enumtrangthaiyeucau { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }

        public SelectList LoaiHinhTaiSanAvailable { get; set; }
        public IList<SelectListItem> BoPhanSuDungAvailable { get; set; }
        public SelectList NguonTaiSanAvailable { get; set; }
        public decimal? NguonTaiSanId { get; set; }
        public IList<SelectListItem> TrangThaiDongBoAvailabe { get; set; }

        public IList<int> LstLoaiHinhTaiSan { get; set; }
        public IList<int> LstLoaiBienDong { get; set; }
        public IList<int> LstTrangThaiDongBo { get; set; }

        public decimal? taisanId { get; set; }
        public int? pageIndex { get; set; }
    }
    public partial class BienDongListModel : BasePagedListModel<BienDongModel>
    {

    }
}

