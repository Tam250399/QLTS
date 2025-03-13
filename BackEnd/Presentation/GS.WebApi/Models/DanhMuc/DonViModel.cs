using GS.Web.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    public class DonViModel: BaseGSApiModel
    {
        public String MA { get; set; }
        public String TEN { get; set; }
        public String MA_DIA_BAN { get; set; }
        public String MA_DVQHNS { get; set; }
        public String DIA_CHI { get; set; }
        public String DIEN_THOAI { get; set; }
        public String FAX { get; set; }
        public String MA_CHA { get; set; }
        public decimal? PARENT_ID { get; set; }
        public decimal? DB_PARENT_ID { get; set; }
        public decimal? DB_LOAI_DON_VI_ID { get; set; }       
        public String MA_CAP_DON_VI { get; set; }
        public Decimal? CAP_DON_VI_ID { get; set; }
        public Boolean? TRANG_THAI_ID { get; set; }
        public Decimal? CHE_DO_HACH_TOAN_ID { get; set; }
        public Decimal? NGUOI_CAP_NHAT_ID { get; set; }
        public string TrangThaiDongBo { get; set; }
        public string Error { get; set; }
        public int? DB_ID { get; set; }
        public Decimal? LOAI_CAP_DON_VI_ID { get; set; }
        public Boolean? LA_DON_VI_NHAP_LIEU { get; set; }
    }
    public class DonViSearchModel
    {
        public DateTime? NGAY_CAP_NHAT_TU { get; set; }
        public DateTime? NGAY_CAP_NHAT_DEN { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    } 
    public  class ResultDonVi
    {
        public ResultDonVi()
        {
            this.ListDonVi = new List<DonViModel>();
        }
        public List<DonViModel> ListDonVi { get; set; }
        public int Total { get; set; }    
        public int TotalPage { get; set; }
    }
}
