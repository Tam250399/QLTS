//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Core.Domain.CCDC;
using GS.Web.Framework.Models;
using GS.Web.Validators.CCDC;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GS.Web.Models.CCDC
{
    [Validator(typeof(KiemKeHoiDongValidator))]
    public class KiemKeHoiDongModel : BaseGSEntityModel
    {
        public KiemKeHoiDongModel() {

        }
        public Decimal KIEM_KE_ID { get; set; }
        public String HO_TEN { get; set; }
        public String CHUC_VU { get; set; }
        public String DAI_DIEN { get; set; }
        public Decimal VI_TRI_ID { get; set; }

        // more
        public SelectList DDLViTriHoiDong { get; set; }
        public enumViTriKiemKeHoiDong viTriKiemKeHoiDong { get; set; }
        public bool isFirst { get; set; }
        public string TenViTri {get;set;}
    }
    public partial class KiemKeHoiDongSearchModel :BaseSearchModel {
        public KiemKeHoiDongSearchModel()
        {
        }
        public string KeySearch {get;set;}
        public int KiemKeId { get; set; }
    }
    public partial class KiemKeHoiDongListModel : BasePagedListModel<KiemKeHoiDongModel>
    {
       
    }
}

