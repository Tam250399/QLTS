//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;

namespace GS.WebApi.Models.TaiSan
{
    public class DBTaiSanModel : BaseGSApiModel
    {
        public DBTaiSanModel()
        {

        }
        public Guid GUID { get; set; }
        public String QLDKTS_MA { get; set; }//Mã tài sản trong phần mềm QLDKTS
        public String DB_MA { get; set; }//Mã tài sản của đơn vị đồng bộ
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? LOAI_TAI_SAN_ID { get; set; }
        public String DON_VI_DONG_BO_ID { get; set; }
        public String DATA_JSON { get; set; }
        public DateTime NGAY_DONG_BO { get; set; }
        public string Error { get; set; }
    }
}

