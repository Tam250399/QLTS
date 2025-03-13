//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using FluentValidation.Attributes;
using GS.Core.Domain.TaiSans;
using GS.Web.Framework.Models;
using GS.Web.Validators.DanhMuc;
using System;
using System.ComponentModel.DataAnnotations;

namespace GS.Web.Models.DanhMuc
{
    [Validator(typeof(NguonVonValidator))]
    public class NguonVonModel : BaseGSEntityModel
    {
        public NguonVonModel()
        {

        }
        public String TEN { get; set; }
        public int? AP_DUNG_ID { get; set; }
        public String GHI_CHU { get; set; }
        public decimal? TRANG_THAI_ID { get; set; }
        public decimal? THU_TU { get; set; }
        [UIHint("InputAddon")]
        public decimal? GiaTri { get; set; }
        public decimal? LoaiHinhTaiSanId { get; set; }
    }
    public class NguonVonBDModel
	{
		public NguonVonBDModel(TaiSanNguonVon taiSanNguonVon, NguonVonModel nguonVonModel = null, string Ten= null)
		{
            this.NGUON_VON_ID = taiSanNguonVon.NGUON_VON_ID;
            this.GIA_TRI_CU = taiSanNguonVon.GIA_TRI;
            this.GIA_TRI_BD = nguonVonModel != null ? nguonVonModel.GiaTri : 0;
            this.TEN = Ten;

        }
        public String TEN { get; set; }
		public decimal NGUON_VON_ID { get; set; }
        [UIHint("InputAddon")]
        public decimal? GIA_TRI_CU { get; set; }
        [UIHint("InputAddon")]
        public decimal? GIA_TRI_BD { get; set; }
    }
    public class NguonVonModelTestList
    {
        public NguonVonModelTest[] items { get; set; }
    }
    public class NguonVonModelTest
    {
        public decimal ID { get; set; }
        public String TEN { get; set; }
        public int? AP_DUNG_ID { get; set; }
        public String GHI_CHU { get; set; }
        public decimal? GiaTri { get; set; }
    }
    public partial class NguonVonSearchModel : BaseSearchModel
    {
        public NguonVonSearchModel()
        {
        }
        public string KeySearch { get; set; }
    }
    public partial class NguonVonListModel : BasePagedListModel<NguonVonModel>
    {

    }
}

