//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Models.TaiSanXacLap;

namespace GS.WebApi.Models.TaiSan
{
    public class TaiSanNhatKyModel : BaseGSApiModel
    {
        public TaiSanNhatKyModel()
        {

        }
        public Decimal? TAI_SAN_ID { get; set; }
        public Decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public DateTime? NGAY_DONG_BO { get; set; }
        public DateTime? NGAY_CAP_NHAP { get; set; }
    }
    public class NhatKyDongBo
    {
        public NhatKyDongBo()
        {
            this.ListMaTaiSan = new List<string>();
            this.ListQuyetDinhTichThu = new List<QuyetDinh>();
        }
        public IList<string> ListMaTaiSan { get; set; }
        public List<QuyetDinh> ListQuyetDinhTichThu { get; set; }
        public string MaDonVi { get; set; }
    }
}

