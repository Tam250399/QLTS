using DevExpress.Xpo.Helpers;
using GS.Core.Domain.Api;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Web.Framework.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.DongBoTaiSan
{
    public class DongBoTaiSanModel
    {
        public DongBoTaiSanModel()
        {
            this.ddlLoaiTaiSan = new List<SelectListItem>();
            this.ddlLoaiBienDong = new List<SelectListItem>();
        }
        [GSResourceDisplayName("File tài sản")]
        [UIHint("WorkFile")]
        public string FileTaiSan { get; set; }
        public IFormFile FormFile { get; set; }
        public List<SelectListItem> ddlLoaiTaiSan { get; set; }
        public List<SelectListItem> ddlLoaiBienDong { get; set; }
        public int LoaiTaiSan { get; set; }
        public decimal LoaiBienDongId { get; set; }
        public string MA_DON_VI { get; set; }
        public decimal NGUON_TAI_SAN_ID { get; set; }
    }
    public class DongBoTaiSanSearchModel
    {
        public DongBoTaiSanSearchModel()
        {
            this.ddlDonVi = new List<SelectListItem>();
            this.LoaiHinhTaiSanSelected = new List<int>();
        }
        public decimal? donViId { get; set; }
        public SelectList LoaiHinhTaiSanAvailable { get; set; }
        public List<int> LoaiHinhTaiSanSelected { get; set; }
        public string strLoaiHinhTSIds { get; set; }
        [UIHint("DateNullable")]
        public DateTime? fromDate { get; set; }
        [UIHint("DateNullable")]
        public DateTime? toDate { get; set; }
        public List<SelectListItem> ddlDonVi { get; set; }
    }
    public class ResultDongBoModel
    {
        public ResultDongBoModel()
        {
            this.TaiSanDongBoApi = new TaiSanDongBoApi();
            this.QuyetDinhTichThuApi = new QuyetDinhTichThuApi();
        }
        public HaoMonDBModel HaoMonDB { get; set; }
        public KhauHaoDBModel KhauHaoDB { get; set; }
        public TaiSanDongBoApi TaiSanDongBoApi { get; set; }
        public QuyetDinhTichThuApi QuyetDinhTichThuApi { get; set; }
        public string Message { get; set; }

    }
    public class Authen
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
