//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.HeThong;
using GS.Web.Framework.Models;
using GS.Web.Validators.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.HeThong
{
    [Validator(typeof(NhatKyValidator))]
    public class NhatKyModel : BaseGSEntityModel
    {
        public NhatKyModel()
        {
            this.GUID = Guid.NewGuid();
        }
        public String TEN_DANG_NHAP { get; set; }
        public String MO_TA { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public String TEN_DAY_DU { get; set; }
        [UIHint("JsonView")]
        public String DU_LIEU { get; set; }
        public String PHAN_LOAI { get; set; }
        public String IP_ADDRESS { get; set; }
        public Guid GUID { get; set; }
        public String UNG_DUNG { get; set; }
        public String PAGE_URL { get; set; }
        public int CAP_DO { get; set; }
        public String NOI_DUNG { get; set; }

        public SelectList CAPDOlist { get; set; }
        public LogLevel capdoLevel { get; set; }
        public LogLevel tenCapDo
        {
            get => (LogLevel)CAP_DO;
            set => CAP_DO = (int)value;
        }
    }
    public partial class NhatKySearchModel : BaseSearchModel
    {
        public NhatKySearchModel()
        {

        }
        public string KeySearch { get; set; }
        public string Username { get; set; }
        [UIHint("DateNullable")]
        public DateTime? Fromdate { get; set; }
        [UIHint("DateNullable")]
        public DateTime? Todate { get; set; }
        public int CAP_DO { get; set; }
        public SelectList CAPDOlist { get; set; }
        public LogLevel capdoLevel { get; set; }



    }
    public partial class NhatKyListModel : BasePagedListModel<NhatKyModel>
    {

    }
}

