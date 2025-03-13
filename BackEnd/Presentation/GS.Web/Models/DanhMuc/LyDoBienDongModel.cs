//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.DanhMuc;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(LyDoBienDongValidator))]
    public class LyDoBienDongModel : BaseGSEntityModel
    {
        public LyDoBienDongModel()
        {

            enumListLoaiHinhTaiSan = new List<enumLOAI_HINH_TAI_SAN>();
        }
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_LY_DO_ID { get; set; }
        public Decimal? LOAI_LY_DO_BIEN_DONG_ID { get; set; }
        public Decimal? TRUONG_SAP_XEP { get; set; }
        public string LOAI_DON_VI { get; set; }
        //add more
        public string TenLoaiHinhTaiSan { get; set; }
        public string TenLyDoBienDong { get; set; }
        public string SelectedLoaiDonVi { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public enumLOAI_LY_DO_TANG_GIAM enumLyDoTangGiam { get; set; }
        public SelectList LoaiHinhTaiSanAvaliable { get; set; }
        public IList<SelectListItem> LoaiHinhDonViAvailable { get; set; }
        public SelectList LyDoTangGiamSanAvaliable { get; set; }
        public IList<int> SelectedLoaiDonViIds { get; set; }
        public string DB_ID_JSON { get; set; }
        public int? DB_ID { get; set; }
        public string LOAI_HINH_TAI_SAN_AP_DUNG_ID { get; set; }
        public IList<int> SelectedLoaiHinhTSIds { get; set; }
        public List<enumLOAI_HINH_TAI_SAN> enumListLoaiHinhTaiSan
        {
            get
            {
                if (LOAI_HINH_TAI_SAN_AP_DUNG_ID != null&& LOAI_HINH_TAI_SAN_AP_DUNG_ID!="")
                {
                    var strloaihinhtaisan = LOAI_HINH_TAI_SAN_AP_DUNG_ID.TrimStart('[').TrimEnd(']');
                    //var loaihinhtaisan = strloaihinhtaisan.Split('-').Select(c => (enumLOAI_HINH_TAI_SAN)(Convert.ToInt32(c))).ToList();
                    var loaihinhtaisan = strloaihinhtaisan.Split(',').Select(c => (enumLOAI_HINH_TAI_SAN)(Convert.ToInt32(c))).ToList();
                    return loaihinhtaisan;

                }
                else
                {
                    return new List<enumLOAI_HINH_TAI_SAN> { enumLOAI_HINH_TAI_SAN.ALL };
                }
            }
            set
            {
                LOAI_HINH_TAI_SAN_AP_DUNG_ID = string.Join(',', value.Select(c => (int)c).ToList());
            }
        }
    }
    public partial class LyDoBienDongSearchModel : BaseSearchModel
    {
        public LyDoBienDongSearchModel()
        {
        }
        public string KeySearch { get; set; }
        public int LOAI_HINH_TAI_SAN_ID { get; set; }
        public int LOAI_LY_DO_ID { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public enumLOAI_LY_DO_TANG_GIAM enumLoaiLyDoTangGiam { get; set; }
        public SelectList LoaiHinhTaiSanAvaliable { get; set; }
        public SelectList LoaiLyDoTangGiamAvaliable { get; set; }
        public bool loaihinhtaisancheck { get; set; }
        public string strLoaiHinhTSIds { get; set; }

    }
    public partial class LyDoBienDongListModel : BasePagedListModel<LyDoBienDongModel>
    {

    }
}

