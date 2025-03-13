using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaos
{
    public class CauHinhBaoCao : BaseEntity
    {
        public CauHinhBaoCao()
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
    public class CauHinhMapBaoCao : BaseEntity
    {
        public String TenBaoCao { get; set; }
        public String MaBaoCao { get; set; }
        public String MaBaoCaoKho { get; set; }
        public String Model { get; set; }
        public object Body { get; set; }
        public object reportData { get; set; }
        public string ExcecuteStatment { get; set; }
        public string FullName_ReportClass { get; set; }
        public string FullName_SearchModelClass { get; set; }
    }

}
