//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.NghiepVu;
using GS.Web.Validators.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.TaiSans
{
    [Validator(typeof(TaiSanOtoValidator))]
    public class TaiSanOtoModel : BaseGSEntityModel
    {
        public TaiSanOtoModel()
        {
            NVYeuCauChiTietModel = new YeuCauChiTietModel();
            lstHienTrang = new List<ObjHienTrang>();
            ddlDongXe = new List<SelectListItem>();
            dllChucDanh = new List<SelectListItem>();
            SelectedChucDanhIds = new List<int>();
        }

        public Decimal TAI_SAN_ID { get; set; }
        [UIHint("InputBKS")]
        public String BIEN_KIEM_SOAT { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_CHO_NGOI { get; set; }
        [UIHint("InputAddon")]
        public Decimal? DUNG_TICH { get; set; }
        public Decimal? CHUC_DANH_ID { get; set; }

        #region Thêm vào để xử lý yêu cầu chọn nhiều chức danh 1 lúc -- hungnt
        public string LIST_CHUC_DANH_ID {get; set; }
        public IList<int> SelectedChucDanhIds { get; set; }
        public IList<SelectListItem> dllChucDanh { get; set; }
        #endregion

        [UIHint("InputTaiTrong")]
        public Decimal? TAI_TRONG { get; set; }
        public String SO_KHUNG { get; set; }
        public Decimal? NHAN_XE_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? CONG_XUAT { get; set; }
        public String SO_MAY { get; set; }
		[UIHint("InputAddon")]
		public decimal? SO_CAU_XE { get; set; }
		public string GCN_DANG_KY { get; set; }
		public string CO_QUAN_CAP_DANG_KY { get; set; }
        [UIHint("DateNullable")]
        public DateTime? NGAY_DANG_KY { get; set; }
		//add more
		public IList<SelectListItem> dllNhanXe { get; set; }
        public IList<SelectListItem> ddlDongXe { get; set; }
        public YeuCauChiTietModel NVYeuCauChiTietModel { get; set; }
        public LoaiTaiSanModel LoaiTaiSanModel { get; set; }
        public TaiSanModel TaiSanModel { get; set; }
        public List<HienTrangModel> ListHienTrangModel { get; set; }
        public decimal? LOAI_TAI_SAN_ID { get; set; }
        public IList<ObjHienTrang> lstHienTrang { get; set; }
        public bool IsPhuongTienVanTai { get; set; }
        public string TenChucDanh { get; set; }
        public string TenDongXe { get; set; }
        public decimal? DONG_XE_ID { get; set; }
        public String Pre_BIEN_KIEM_SOAT { get; set; }
        public String Suff_BIEN_KIEM_SOAT { get; set; }

        public string GetChucDanhsBySelectedList()
        {
            string ListChucDanh = "";
            if (this.SelectedChucDanhIds.Count > 0)
            {
                if (!this.SelectedChucDanhIds.Contains(0))
                {
                    foreach (var lcd in this.SelectedChucDanhIds)
                    {
                        ListChucDanh += lcd + ",";
                    }
                    ListChucDanh = ListChucDanh.Remove(ListChucDanh.Length - 1);
                }
                else // nếu có 0 thì chỉ lưu 0 (tất cả)
                {
                    ListChucDanh = "0";
                }

            }
            else
            {
                ListChucDanh = null;

            }
            return ListChucDanh;
        }
    }
    public partial class TaiSanOtoSearchModel : BaseSearchModel
    {
        public TaiSanOtoSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class TaiSanOtoListModel : BasePagedListModel<TaiSanOtoModel>
    {

    }
}

