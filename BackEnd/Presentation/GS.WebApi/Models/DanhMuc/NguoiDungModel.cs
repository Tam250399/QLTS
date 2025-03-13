using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.WebApi.Validator.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.DanhMuc
{
    [Validator(typeof(NguoiDungValidator))]
    public class NguoiDungModel : BaseGSApiModel
    {       
        public NguoiDungModel()
        {
            LIST_DON_VI = new List<NguoiDungDonViModel>();
        }
        public String GUID { get; set; }
        public String TEN_DANG_NHAP { get; set; }
        public String MAT_KHAU { get; set; }
        public String PASSWORD_HASH { get; set; }
        public String TEN_DAY_DU { get; set; }
        public String EMAIL { get; set; }
        public int TRANG_THAI_ID { get; set; }
        public DateTime? NGAY_TRUY_CAP { get; set; }
        public bool IS_QUAN_TRI { get; set; }   
        public String MOBILE { get; set; }
        public String MAT_KHAU_KEY { get; set; }
        public String TMP_MAT_KHAU { get; set; }
        /// <summary>
        /// danh sách các đơn vị mà người dùng có quyền quản lý tài sản
        /// định dạng string Json
        /// 
        /// </summary>
        public List<NguoiDungDonViModel> LIST_DON_VI { get; set; }
        public string DB_ID { get; set; }
        public string Error { get; set; }
        public string GHI_CHU { get; set; }
    }
    public class NguoiDungDonViModel
    {
        public string MA_DON_VI { get; set; }
        public string MA_DVQHNS { get; set; }
    }
    public class SearchNguoiDung
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
    }
    public class ResultNguoiDung
    {
        public ResultNguoiDung()
        {
            this.ListNguoiDung = new List<NguoiDungModel>();
        }
        public List<NguoiDungModel> ListNguoiDung { get; set; }
        public int Total { get; set; }
        public int TotalPage { get; set; }
    }
}
