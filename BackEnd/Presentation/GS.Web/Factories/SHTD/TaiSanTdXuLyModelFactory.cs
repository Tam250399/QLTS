//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.SHTD;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.SHTD;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.SHTD;
using GS.Services.DanhMuc;
using GS.Web.Factories.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Services.ThuocTinhs;

namespace GS.Web.Factories.SHTD
{
    public class TaiSanTdXuLyModelFactory : ITaiSanTdXuLyModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ITaiSanTdXuLyService _itemService;
        private readonly IHinhThucXuLyService hinhThucXuLyService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly IHinhThucXuLyModelFactory _hinhThucXuLyModelFactory;
        private readonly IXuLyService _xuLyService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly IThuocTinhDataService _thuocTinhDataService;
        private readonly IPhuongAnXuLyModelFactory _phuongAnXuLyModelFactory;
        private readonly IKetQuaService _ketQuaService;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public TaiSanTdXuLyModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanTdXuLyService itemService,
            IHinhThucXuLyService hinhThucXuLyService,
            IPhuongAnXuLyService phuongAnXuLyService,
            ITaiSanTdService taiSanTdService,
            IHinhThucXuLyModelFactory hinhThucXuLyModelFactory,
            IXuLyService xuLyService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            IThuocTinhDataService thuocTinhDataService,
            IPhuongAnXuLyModelFactory phuongAnXuLyModelFactory,
            IKetQuaService ketQuaService,
            IDonViService donViService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this.hinhThucXuLyService = hinhThucXuLyService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._taiSanTdService = taiSanTdService;
            this._hinhThucXuLyModelFactory = hinhThucXuLyModelFactory;
            this._xuLyService = xuLyService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._thuocTinhDataService = thuocTinhDataService;
            this._phuongAnXuLyModelFactory = phuongAnXuLyModelFactory;
            this._ketQuaService = ketQuaService;
            _donViService = donViService;
        }
        #endregion

        #region TaiSanTdXuLy
        public TaiSanTdXuLySearchModel PrepareTaiSanTdXuLySearchModel(TaiSanTdXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public TaiSanTdXuLyListModel PrepareTaiSanTdXuLyListModel(TaiSanTdXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var xuly = (searchModel.XuLyId >0 ? (_xuLyService.GetXuLyById(searchModel.XuLyId)):(_xuLyService.GetXuLyByGuid(searchModel.XuLyGuid)));
            var is_null = false; 
            if (xuly != null)
            {
                searchModel.XuLyId = (int)xuly.ID;
            }
            else
            {
                is_null = true;
            }
            //get items
            var items = _itemService.SearchTaiSanTdXuLys(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,XuLyId:searchModel.XuLyId, is_null:is_null, DonViId:_workContext.CurrentDonVi.ID,isKetQua:searchModel.isKetQua);

            //prepare list model
            var model = new TaiSanTdXuLyListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareTaiSanTdXuLyModel(null,c,false)),
                Total = items.TotalCount,
                
            };
            return model;
        }
        public TaiSanTdXuLyListModel PrepareTaiSanTdXuLyListModelForPhuongAn(TaiSanTdXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var xuly = _xuLyService.GetXuLyByGuid(searchModel.XuLyGuid);
            var is_null = false;
            if (xuly != null)
            {
                searchModel.XuLyId = (int)xuly.ID;
                searchModel.TrangThaiXuLy = (int)xuly.TRANG_THAI_ID;
            }
            else
            {
                is_null = true;
            }
            //get items
            var items = _itemService.SearchTaiSanTdXuLysForPhuongAn(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, XuLyId: searchModel.XuLyId, is_null: is_null,TrangThai:searchModel.TrangThaiXuLy, DonViId: _workContext.CurrentDonVi.ID);
            
            //prepare list model
            string PAXLJson = _phuongAnXuLyModelFactory.DDLPhuongAn().toStringJson();
            var model = new TaiSanTdXuLyListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareTaiSanTdXuLyModel(null, c, false, PAXLJson)),
                Total = items.TotalCount
            };
            return model;
        }

        //Hungnt -Add 
        public TaiSanTdXuLyListModel PrepareTaiSanTdXuLyListModelByListQuyetDinh(TaiSanTdXuLySearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            var xuly = _xuLyService.GetXuLyByGuid(searchModel.XuLyGuid);
            var is_null = false;
            if (xuly != null)
            {
                searchModel.XuLyId = (int)xuly.ID;
                searchModel.TrangThaiXuLy = (int)xuly.TRANG_THAI_ID;
            }
            else
            {
                is_null = true;
            }
            //get items
            var items = _itemService.SearchTaiSanTdXuLysForPhuongAn(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, XuLyId: searchModel.XuLyId, is_null: is_null, TrangThai: searchModel.TrangThaiXuLy, DonViId: _workContext.CurrentDonVi.ID);
            
            //prepare list model
            string PAXLJson = _phuongAnXuLyModelFactory.DDLPhuongAn().toStringJson();
            var model = new TaiSanTdXuLyListModel
            {
                //fill in model values from the entity

                Data = items.Where(c => searchModel.ListQuyetDinh.Contains(c.taisantd.QUYET_DINH_TICH_THU_ID)) // ch
                            .Select(c => PrepareTaiSanTdXuLyModel(null, c, false, PAXLJson))
                            .Where(c => c.PHUONG_AN_XU_LY_ID == null), // chỉ append tstdxl không có phương án xử lý - tránh bị lên trùng phương án xử lý đã lưu khi Edit
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanTdXuLyModel PrepareTaiSanTdXuLyModelForKetQua(TaiSanTdXuLyModel model, TaiSanTdXuLy item, bool excludeProperties = false)
        {
            if (item != null)
            {
                model = item.ToModel<TaiSanTdXuLyModel>();
                var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)model.ID).FirstOrDefault();
                if (thuoctinh != null)
                {
                    model.ThuocTinh = thuoctinh.DATA;
                }
                var ht = hinhThucXuLyService.GetHinhThucXuLyById(model.HINH_THUC_XU_LY_ID != null ? (int)model.HINH_THUC_XU_LY_ID : 0);
                if (ht != null)
                {
                    model.TenHinhThuc = ht.TEN;
                }
                var pa = _phuongAnXuLyService.GetPhuongAnXuLyById(model.PHUONG_AN_XU_LY_ID != null ? (int)model.PHUONG_AN_XU_LY_ID : 0);
                if (pa != null)
                {
                    model.TenPhuongAn = pa.TEN;
                    model.MaPhuongAnXuLy = pa.MA;
                }
                model.TenTaiSan = item.taisantd.TEN;
                model.TenLoaiTaiSan = item.taisantd.TEN_LOAI_TAI_SAN;
                model.TenNguonGoc = item.taisantd.quyetdinh.NguonGocTaiSan.TEN;
                var donViDieuChuyenID = _ketQuaService.GetKetQuaByTSPAXLID(item.ID)?.DON_VI_CHUYEN_ID;
                var donViDieuChuyen = _donViService.GetDonViById(donViDieuChuyenID ?? 0);
                model.DON_VI_DIEU_CHUYEN_ID = donViDieuChuyenID;
                model.DonViNhanDieuChuyenTen = donViDieuChuyen?.TEN;
            }
            //model.DDLPhuongAnXuLy = _phuongAnXuLyModelFactory.DDLPhuongAn();
            model.DDLHinhThucXuLy = _hinhThucXuLyModelFactory.DDLPhuongAnByHinhThuc(model.PHUONG_AN_XU_LY_ID != null ? (int)model.PHUONG_AN_XU_LY_ID : 0);
            return model;
        }
        public TaiSanTdXuLyModel PrepareTaiSanTdXuLyModel(TaiSanTdXuLyModel model, TaiSanTdXuLy item, bool excludeProperties = false,string PAXLJson = null, string HTXLJson = null)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanTdXuLyModel>();
                //model.ChiPhiXuLy = model.CHI_PHI_XU_LY.ToVNStringNumber();
                //model.NguyenGia = model.NGUYEN_GIA.ToVNStringNumber();
                //model.GiaTri= model.GIA_TRI.ToVNStringNumber();
                //model.GiaTriGT= model.GIA_TRI_GHI_TANG.ToVNStringNumber();
                //model.GiaTriNSNN = model.GIA_TRI_NSNN.ToVNStringNumber();
                //model.GiaTriTKTG = model.GIA_TRI_TKTG.ToVNStringNumber();
                //var thuoctinh = _thuocTinhDataService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)model.ID).FirstOrDefault();
                //if (thuoctinh != null)
                //{
                //    model.ThuocTinh = thuoctinh.DATA;
                //}
                model.JsonDDLPhuongAnXuLy = PAXLJson;
                model.JsonDDLHinhThucXuLy = _hinhThucXuLyModelFactory.DDLPhuongAnByHinhThuc(model.PHUONG_AN_XU_LY_ID!=null?(int)model.PHUONG_AN_XU_LY_ID:0).toStringJson();
                model.SoLuongCon = (decimal)_taiSanTdService.GetSoLuongConByTaiSanId(Id:(decimal)model.TAI_SAN_ID,LoaiXuLy:(int)enumLoaiXuLy.PhuongAn, xulyid: item.XU_LY_ID) + (decimal)model.SO_LUONG;             
                if (item.taisantd== null)
                {
                    item.taisantd = _taiSanTdService.GetTaiSanTdById(item.TAI_SAN_ID);
                }
                model.SLCoTheXuLy = (decimal)_taiSanTdService.GetSoLuongConByTaiSanId(Id: (decimal)model.TAI_SAN_ID, LoaiXuLy: (int)enumLoaiXuLy.PhuongAn)
                                    + (decimal)_itemService.GetTaiSanTdXuLyByTaiSanIdAndXuLyId(TaiSanId: (decimal)model.TAI_SAN_ID,XuLyId: item.XU_LY_ID)
                                    .Select(c=>c.SO_LUONG).Sum();                
                model.SoLuongBanDau = (decimal)item.taisantd.GIA_TRI_TINH;
                model.SoLuongBanDauText = ((decimal)item.taisantd.GIA_TRI_TINH).ToVNStringNumber(true);
                model.SoLuongText = ((decimal)item.SO_LUONG).ToVNStringNumber(true);
                //số lượng đã xử lý 
                model.isKetQua = _ketQuaService.GetKetQuaBys(TaiSanTDXuLy: item.ID).Count()>0?true:false;
                model.TenTaiSan = item.taisantd.TEN;
                model.DonViTinh = item.taisantd.DON_VI_TINH != null ? item.taisantd.DON_VI_TINH : "";
                model.SoQuyetDinh = item.taisantd.quyetdinh.QUYET_DINH_SO;
                
            }
            //more
            //model.ChiPhiCu = model.CHI_PHI_XU_LY??0;
            var ht = hinhThucXuLyService.GetHinhThucXuLyById(model.HINH_THUC_XU_LY_ID != null ? (int)model.HINH_THUC_XU_LY_ID : 0);
            if (ht != null)
            {
                model.TenHinhThuc = ht.TEN;
            }
            var pa = _phuongAnXuLyService.GetPhuongAnXuLyById(model.PHUONG_AN_XU_LY_ID != null ? (int)model.PHUONG_AN_XU_LY_ID : 0);
            if (pa != null)
            {
                 model.TenPhuongAn = pa.TEN;                                
            }          
            if (excludeProperties)
            {

                //model.DDLTaiSanTD = DDLTaiSan((int)model.TAI_SAN_ID,listSL:model.ListSL.ToList(),listVuViec:model.listVuViec,LoaiXuLy:model.LoaiXuLy,isAddFirst:true);
                //model.NgayTichThu = _quyetDinhTichThuService.GetQuyetDinhTichThuByIds(model.listVuViec.Select(c => (decimal)c).ToArray()).Select(c => c.QUYET_DINH_NGAY).Max();
                //model.DDLPhuongAnXuLy = _phuongAnXuLyService.GetAllPhuongAnXuLys().Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                //{
                //    Value = c.ID.ToString(),
                //    Text = c.TEN
                //}).ToList();
                //model.DDLPhuongAnXuLy.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "0", Text = "Chọn hình thức" });
                //if (model.HINH_THUC_XU_LY_ID == null)
                //{
                //    model.HINH_THUC_XU_LY_ID = 0;
                //}
                //if (model.PHUONG_AN_XU_LY_ID == null)
                //{
                //    model.PHUONG_AN_XU_LY_ID = 0;
                //}
            }
            return model;
        }
        public void PrepareTaiSanTdXuLy(TaiSanTdXuLyModel model, TaiSanTdXuLy item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.XU_LY_ID = model.XU_LY_ID;
            item.SO_LUONG = model.SO_LUONG;
            //item.DIEN_TICH = model.DIEN_TICH;
            //item.NGUYEN_GIA = model.NGUYEN_GIA;
            //item.GIA_TRI = model.GIA_TRI;
            //item.GIA_TRI_GHI_TANG = model.GIA_TRI_GHI_TANG;
            //item.GIA_TRI_NSNN = model.GIA_TRI_NSNN;
            //item.GIA_TRI_TKTG = model.GIA_TRI_TKTG;
            item.PHUONG_AN_XU_LY_ID = model.PHUONG_AN_XU_LY_ID;
            item.HINH_THUC_XU_LY_ID = model.HINH_THUC_XU_LY_ID;
            //item.CHI_PHI_XU_LY = model.CHI_PHI_XU_LY;
            //item.HOP_DONG_SO = model.HOP_DONG_SO;
            //item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
            item.GHI_CHU = model.GHI_CHU;
            item.GUID = model.GUID;
            //item.DON_VI_CHUYEN_ID = model.DON_VI_CHUYEN_ID;
            //item.NGAY_XU_LY = model.NGAY_XU_LY;
            //item.SO_TIEN_THU = model.SO_TIEN_THU;
            //item.KHOI_LUONG = model.KHOI_LUONG;
            //item.HO_SO_GIAY_TO_KHAC = model.HO_SO_GIAY_TO_KHAC;
            //item.TEN_DON_VI_NHAN_DIEU_CHUYEN = model.TEN_DON_VI_NHAN_DIEU_CHUYEN;
            //item.GIA_TRI_TAI_SAN_XU_LY = model.GIA_TRI_TAI_SAN_XU_LY;

        }
        public void PrepareTaiSanTdXuLyForUpdate(TaiSanTdXuLyModel model, TaiSanTdXuLy item)
        {
            item.SO_LUONG = model.SO_LUONG;
            //item.GIA_TRI_GHI_TANG = model.GIA_TRI_GHI_TANG;
            //item.GIA_TRI_NSNN = model.GIA_TRI_NSNN;
            //item.GIA_TRI_TKTG = model.GIA_TRI_TKTG;
            item.PHUONG_AN_XU_LY_ID = model.PHUONG_AN_XU_LY_ID;
            item.HINH_THUC_XU_LY_ID = model.HINH_THUC_XU_LY_ID;
            //item.CHI_PHI_XU_LY = model.CHI_PHI_XU_LY;
            //item.HOP_DONG_SO = model.HOP_DONG_SO;
            //item.HOP_DONG_NGAY = model.HOP_DONG_NGAY;
            item.GHI_CHU = model.GHI_CHU;
            //item.DON_VI_CHUYEN_ID = model.DON_VI_CHUYEN_ID;
            //item.SO_TIEN_THU = model.SO_TIEN_THU;
            //item.HO_SO_GIAY_TO_KHAC = model.HO_SO_GIAY_TO_KHAC;
            //item.NGAY_XU_LY = model.NGAY_XU_LY;
            //item.TEN_DON_VI_NHAN_DIEU_CHUYEN = model.TEN_DON_VI_NHAN_DIEU_CHUYEN;
            //item.GIA_TRI_TAI_SAN_XU_LY = model.GIA_TRI_TAI_SAN_XU_LY;

    }
        public List<SelectListItem> DDLTaiSan(int TaiSanId=0,List<ListSoLuong> listSL = null, List<int> listVuViec = null,int LoaiXuLy =0,bool isAddFirst = true)
        {
            var ddl = new List<SelectListItem>();
            var listTaiSanTd = new List<int>();
            if (listVuViec != null)
            {
                //lấy tài sản theo vụ việc
                listTaiSanTd = _taiSanTdService.GetTaiSanTdByListQuyetDinhId(listVuViec).Select(c=>(int)c.ID).ToList();
            }
            //gộp những tài sản giống nhau
            listSL = (from SL in listSL
                      group SL by SL.TAI_SAN_ID into g
                      select new ListSoLuong
                      {
                          TAI_SAN_ID = g.Key,
                          SO_LUONG = g.Select(c => c.SO_LUONG).Sum()
                      }).ToList();
            if (LoaiXuLy == (int)enumLoaiXuLy.KetQua)
            {
                ddl = _taiSanTdService.GetTaiSansChuaFullSoLuongForKetQua(listSL: listSL, listTaiSanTd: listTaiSanTd).Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.TEN + " (" + c.quyetdinh.QUYET_DINH_SO + " - " + c.quyetdinh.QUYET_DINH_NGAY.toDateVNString()+")" }).ToList();
            }
            else if (LoaiXuLy == (int)enumLoaiXuLy.DeXuat)
            { 
                ddl = _taiSanTdService.GetTaiSansChuaFullSoLuongForDeXuat(listSL: listSL, listTaiSanTd: listTaiSanTd).Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.TEN + " (" + c.quyetdinh.QUYET_DINH_SO + " - " + c.quyetdinh.QUYET_DINH_NGAY.toDateVNString()+")" }).ToList(); 
            }
               
            if (TaiSanId > 0)
            {
                if (ddl.Where(c => c.Value.Contains(TaiSanId.ToString())).Count() == 0)
                {
                    var ts = _taiSanTdService.GetTaiSanTdById(TaiSanId);
                    if (ts != null)
                    {
                        ddl.Insert(0, new SelectListItem { Value = ts.ID.ToString(), Text = ts.TEN+ " (" + ts.quyetdinh.QUYET_DINH_SO + " - " + ts.quyetdinh.QUYET_DINH_NGAY.toDateVNString() + ")" });
                    }

                }
            }
            if(isAddFirst)
            ddl.Insert(0, new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = "0", Text = "Chọn tài sản toàn dân" });
            return ddl;
        }
        #endregion
    }
}

