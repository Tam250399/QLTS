using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.BaoCaos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.BaoCaos
{
    [Validator(typeof(CauHinhBaoCaoValidator))]
    public class CauHinhBaoCaoModel : BaseGSEntityModel
    {
        public CauHinhBaoCaoModel()
        {

        }
        public String TenBaoCao { get; set; }
        public String MaBaoCao { get; set; }
        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }
        public String ChucDanhNguoiLapBieu { get; set; }
        public String ChucDanhKeToanTruong { get; set; }
        public String ChucDanhThuTruongDonVi { get; set; }
        public String TenNguoiLapBieu { get; set; }
        public String TenKeToanTruong { get; set; }
        public String TenThuTruongDonVi { get; set; }
        public String DiaDanhBaoCao { get; set; }
        public String DonViKhaiThac { get; set; }
        /// <summary>
        /// Câu lệnh chạy store procedure
        /// </summary>
        public string ExcecuteStatment { get; set; }
        /// <summary>
        /// Full name của class searchModel báo cáo
        /// </summary>
        public string FullName_SearchModelClass { get; set; }
        /// <summary>
        /// Full name của class Report báo cáo
        /// </summary>
        public string FullName_ReportClass { get; set; }
        /// <summary>
        /// Full name của class dữ liệu báo cáo trả ra
        /// </summary>
        public string FullName_ModelReturnClass { get; set; }
    }

    public class CauHinhBaoCaoSearchModel : BaseSearchModel
    {
        public string KeySearch { get; set; }
    }

    public class CauHinhBaoCaoListModel : BasePagedListModel<CauHinhBaoCaoModel>
    {

    }

    [Validator(typeof(CauHinhBaoCaoDBValidator))]
    public class CauHinhBaoCaoDBModel : BaseGSEntityModel
    {
        //    public CauHinhBaoCaoDBModel()
        //    {
        //        Body = new Dictionary<string, string>();
        //        reportData = new Dictionary<string, string>();
        //    }
        public string TenBaoCao { get; set; }
        public string MaBaoCao { get; set; }
        public string MaBaoCaoKho { get; set; }
        public string Model { get; set; }
        public string FullName_ReportClass { get; set; }
        public string FullName_SearchModelClass { get; set; }
        public Dictionary<string, string> Body { get; set; }
        public Dictionary<string, string> reportData { get; set; }
        public Dictionary<string, string> Lstbody { get; set; }
        public Dictionary<string, string> LstreportData { get; set; }
        //more
        public string ExcecuteStatment { get; set; }
    }
    public class CauHinhBaoCaoDBListModel : BasePagedListModel<CauHinhBaoCaoDBModel>
    {

    }

    [Validator(typeof(CauHinhBaoCaoDBValidator))]
    public class CauHinhMapBaoCaoDongBoModel : BaseGSEntityModel
    {
        public CauHinhMapBaoCaoDongBoModel()
        {
            Body = new List<InfoPropertyReport>();
            reportData = new List<InfoPropertyReport>();
        }
        public string TenBaoCao { get; set; }
        public string MaBaoCao { get; set; }
        public string MaBaoCaoKho { get; set; }
        public string Model { get; set; }
        public string FullName_ReportClass { get; set; }
        public string FullName_SearchModelClass { get; set; }
        public List<InfoPropertyReport> Body { get; set; }
        public List<InfoPropertyReport> reportData { get; set; }
        public List<InfoPropertyReport>  Lstbody { get; set; }
        public List<InfoPropertyReport> LstreportData { get; set; }
        //more
        public string ExcecuteStatment { get; set; }
    }
    public class CauHinhMapBaoCaoDongBoListModel : BasePagedListModel<CauHinhMapBaoCaoDongBoModel>
    {

    }
    public class InfoPropertyReport
    {
        public string name { get; set; }
        public string reName { get; set; }
        public string type { get; set; }
        public string reType { get; set; }
        public string defaultValue { get; set; }
    }
}
