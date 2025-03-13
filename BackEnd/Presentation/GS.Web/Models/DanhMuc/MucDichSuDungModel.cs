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

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(MucDichSuDungValidator))]
    public class MucDichSuDungModel : BaseGSEntityModel
    {
        public MucDichSuDungModel()
        {

        }
        public String MA { get; set; }
        public String TEN { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public enumLOAI_HINH_TAI_SAN enumLoaiHinhTaiSan { get; set; }
        public SelectList LoaiHinhTaiSanAvaliable { get; set; }
        public string TenLoaiHinhTaiSan { get; set; }
        public string DB_ID_JSON { get; set; }
        public int? DB_ID { get; set; }
    }
    public partial class MucDichSuDungSearchModel : BaseSearchModel
    {
        public MucDichSuDungSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class MucDichSuDungListModel : BasePagedListModel<MucDichSuDungModel>
    {

    }
}

