//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.SHTD;
using GS.Services.DanhMuc;
using GS.Services.SHTD;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Models.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using static GS.Core.Domain.SHTD.QuyetDinhTichThu;

namespace GS.Web.Factories.SHTD
{
    public class TaiSanTdModelFactory : ITaiSanTdModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ITaiSanTdService _itemService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly INguonGocTaiSanModelFactory _nguonGocTaiSanModelFactory;
        #endregion

        #region Ctor

        public TaiSanTdModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanTdService itemService,
            ILoaiTaiSanService loaiTaiSanService,
            INguonGocTaiSanService nguonGocTaiSanService,
            IQuyetDinhTichThuService quyetDinhTichThuService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            INguonGocTaiSanModelFactory nguonGocTaiSanModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._nguonGocTaiSanModelFactory = nguonGocTaiSanModelFactory;
        }
        #endregion

        #region TaiSanTd
        public TaiSanTdSearchModel PrepareTaiSanTdSearchModel(TaiSanTdSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true, isDisabled: false).ToList();
            searchModel.DDLNguonGocTaiSan = _nguonGocTaiSanModelFactory.PrepareSelectListNguonGocTaiSan();
            searchModel.SetGridPageSize();
            return searchModel;
        }

        public TaiSanTdListModel PrepareTaiSanTdListModel(TaiSanTdSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchTaiSanTds(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, QuyetDinhId: searchModel.QuyetDinhId, LoaiTaiSan: searchModel.LoaiTaiSanId, NguonGocTaiSan: searchModel.NguonGocTaiSan, TenTaiSan: searchModel.TenTaiSan, NgayQuyetDinhTu: searchModel.NgayQuyetDinhTu, NgayQuyetDinhDen: searchModel.NgayQuyetDinhDen, SoQuyetDinh: searchModel.SoQuyetDinh, TaiSanDatId: searchModel.TaiSanDatId, TrangThaiID: searchModel.TrangThaiId,ListNhaNhapId:searchModel.ListNhaNhapId.ToList());

            //prepare list model
            var model = new TaiSanTdListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareTaiSanTdModelForList(null, c)),
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanTdModel PrepareTaiSanTdModelForList(TaiSanTdModel model, TaiSanTd item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanTdModel>();
                //model.NguyenGia = model.NGUYEN_GIA.ToVNStringNumber();
                model.GiaTri = model.GIA_TRI_XAC_LAP.ToVNStringNumber();
                if (model.LOAI_TAI_SAN_ID != null)
                {
                    var loai = _loaiTaiSanService.GetLoaiTaiSanById((int)model.LOAI_TAI_SAN_ID);
                    if (loai != null)
                    {
                        model.TenLoaiTaiSan = loai.TEN;
                        var loaihinhtaisan = _loaiTaiSanService.GetLoaiTaiSanById((int)model.LOAI_TAI_SAN_ID);
                        if (loaihinhtaisan != null)
                        {
                            var loaiTsCha = loaihinhtaisan.TREE_NODE.Split('-');
                            if (loaiTsCha.Count() > 1)
                            {
                                model.TenLoaiHinhTaiSan = _loaiTaiSanService.GetLoaiTaiSanById(loaiTsCha[1].ToNumberInt32()).TEN;
                            }
                        }
                        model.TenLoaiHinhTaiSan = model.TenLoaiHinhTaiSan + " - " + loai.TEN;

                    }
                    var loaiTS = _loaiTaiSanService.GetLoaiTaiSanById((decimal)item.LOAI_TAI_SAN_ID);
                    model.TEN_LOAI_TAI_SAN = loaiTS != null ? loaiTS.TEN : "";
                }
                else
                {
                    model.TEN_LOAI_TAI_SAN = model.TEN_LOAI_TAI_SAN != null ? model.TEN_LOAI_TAI_SAN : "";
                }                             
                if (model.NGUON_GOC_TAI_SAN_ID != null)
                {
                    var nguongoc = _nguonGocTaiSanService.GetNguonGocTaiSanById((int)model.NGUON_GOC_TAI_SAN_ID);
                    if (nguongoc != null)
                    {
                        model.TenNguonGoc = nguongoc.TEN;
                    }
                }
                //if(_taiSanTdXuLyService.GetTaiSanTdXuLysByTaiSanId(item.ID).Where(c=>c.xuly.LOAI_XU_LY_ID == (int)enumLoaiXuLy.KetQua).Count() > 0)
                //{
                //    model.is_delete = false;
                //}
                if(item.quyetdinh.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.CHO_DUYET || item.quyetdinh.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.TU_CHOI || item.quyetdinh.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.NHAP)
                {
                    model.is_delete = true;
                }
                else
                {
                    model.is_delete = false;
                }
            }
            //more
            return model;
        }
        public TaiSanTdModel PrepareTaiSanTdModel(TaiSanTdModel model, TaiSanTd item, bool excludeProperties = false,bool is_soluongcon = false,int SLDaChon = 0,Guid TSXLGuid = new Guid(),int LoaiXuLy = 0, bool IS_DDL = true,int SoLuongTrenFrom = 0)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanTdModel>();
                if(item.quyetdinh != null)
                {
                    if (item.quyetdinh.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET)
                    {
                        model.is_detail = true;
                    }
                }
                else
                {
                    var quyetdinh = _quyetDinhTichThuService.GetQuyetDinhTichThuById((decimal)item.QUYET_DINH_TICH_THU_ID);
                    if (quyetdinh.TRANG_THAI_ID == (int)enumTRANGTHAI_QUYETDINH_TSTD.DUYET)
                    {
                        model.is_detail = true;
                    }
                }
                
                if (model.NGUON_GOC_TAI_SAN_ID != null)
                {
                    var nguongoc = _nguonGocTaiSanService.GetNguonGocTaiSanById((int)model.NGUON_GOC_TAI_SAN_ID);
                    if (nguongoc != null)
                    {
                        model.TenNguonGoc = nguongoc.TEN;
                    }
                }
                //số lượng còn
                if (is_soluongcon)
                {
                    
                    var soluongcon = _itemService.GetSoLuongConByTaiSanId(model.ID, SoLuong: SLDaChon,LoaiXuLy:LoaiXuLy);
                    if (soluongcon != null)
                    {
                        model.SoLuongCon = (int)soluongcon>=0?(int)soluongcon:0;
                    }
                    //nếu là sửa thì số lượng còn sẽ cộng thêm sỗ lượng đã chọn
                    if (SoLuongTrenFrom > 0)
                    {
                        model.SoLuongCon += SoLuongTrenFrom;
                    }
                    else {
                        var tsxl = _taiSanTdXuLyService.GetTaiSanTdXuLyByGuId(TSXLGuid);
                        if (tsxl != null)
                        {
                            model.SoLuongCon += tsxl.SO_LUONG;
                        }
                    }
                }
            }
            //more
            if (excludeProperties)
            {
                //if (IS_DDL)
                //{
                //    model.DDLLoaiTaiSan = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(cheDoId: 23, isAddFirst: true,isDisabled:false).ToList();
                //}
                //model.DDLNguonGocTaiSan = _nguonGocTaiSanModelFactory.PrepareSelectListNguonGocTaiSan(selected:model.NGUON_GOC_TAI_SAN_ID!=null?(decimal)model.NGUON_GOC_TAI_SAN_ID:0,isAddFirst:true,isDisable:true);
            }
            return model;
        }
        public void PrepareTaiSanTd(TaiSanTdModel model, TaiSanTd item)
        {
            
            item.GUID = model.GUID;
            item.QUYET_DINH_TICH_THU_ID = model.QUYET_DINH_TICH_THU_ID;
            item.TEN = model.TEN;
            //item.NGUON_GOC_TAI_SAN_ID = model.NGUON_GOC_TAI_SAN_ID;
            item.LOAI_TAI_SAN_ID = model.LOAI_TAI_SAN_ID;
            //item.NGUYEN_GIA = model.NGUYEN_GIA;
            item.SO_LUONG = model.SO_LUONG;
            //item.KHOI_LUONG = model.KHOI_LUONG;
            item.GIA_TRI_XAC_LAP = model.GIA_TRI_XAC_LAP;
            item.GHI_CHU = model.GHI_CHU;
            //item.DIEN_TICH = model.DIEN_TICH;
            item.GIA_TRI_TINH = model.GIA_TRI_TINH;
            item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
            item.TEN_LOAI_TAI_SAN = model.TEN_LOAI_TAI_SAN;
            item.BIEN_KIEM_SOAT = model.BIEN_KIEM_SOAT;
            item.NHAN_XE_ID = model.NHAN_XE_ID;
            item.SO_CHO_NGOI = model.SO_CHO_NGOI;
            item.TAI_TRONG = model.TAI_TRONG;
            item.TAI_SAN_DAT_ID = model.TAI_SAN_DAT_ID;
            item.DIA_CHI= model.DIA_CHI;
            item.DON_VI_TINH = model.DON_VI_TINH;
            item.SO_CAU_XE = model.SO_CAU_XE;          
            if(model.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
            {
                item.NGAY_SU_DUNG = model.NGAY_SU_DUNG ?? new DateTime((int)(model.NamSuDung ?? DateTime.Now.Year), 01, 01);
            }
            //item.TRANG_THAI_ID = model.TRANG_THAI_ID;

    }
        public List<SelectListItem> GetDDLTaiSan()
        {
            var ddl = new List<SelectListItem>();
            ddl = _itemService.GetAllTaiSanTds().Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.TEN+ " - " + c.SO_LUONG }).ToList();
            ddl.Insert(0, new SelectListItem { Value = "0", Text = "Chọn tài sản" });
            return ddl;
        }
        public List<SelectListItem> PrepareSelectListTSTD(decimal selected = 0, bool isAddFirst = true, decimal loaiHinhTaiSan = 0,string strAddFirst = "-- Chọn đất --")
        {            
            var  ddl = _itemService.GetTaiSanTds(LoaiHinhTaiSan: loaiHinhTaiSan).OrderByDescending(c => c.quyetdinh.QUYET_DINH_NGAY).Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.TEN,
                    Selected = c.ID == selected ? true : false
                }).ToList();
            if(isAddFirst)
            ddl.Insert(0, new SelectListItem { Value = "0", Text = strAddFirst });
            return ddl;
        }
        #endregion
    }
}		

