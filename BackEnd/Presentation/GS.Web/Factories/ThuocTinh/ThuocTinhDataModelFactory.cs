//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
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
using GS.Core.Domain.ThuocTinhs;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.ThuocTinhs;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.ThuocTinh;
using GS.Services.TaiSans;
using GS.Services.DanhMuc;
using GS.Services.SHTD;

namespace GS.Web.Factories.ThuocTinhs
{
    public class ThuocTinhDataModelFactory:IThuocTinhDataModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IThuocTinhDataService _itemService;
            private readonly IThuocTinhTaiSanService _thuocTinhTaiSanService;
            private readonly ITaiSanService _taiSanService;
            private readonly IThuocTinhModelFactory _thuocTinhModelFactory;
            private readonly IThuocTinhService _thuocTinhService;
            private readonly IPhuongAnXuLyService _phuongAnXuLyService;
            private readonly IHinhThucXuLyService hinhThucXuLyService;
            private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        #endregion

        #region Ctor

        public ThuocTinhDataModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IThuocTinhDataService itemService,
            IThuocTinhTaiSanService thuocTinhTaiSanService,
            ITaiSanService taiSanService,
            IThuocTinhModelFactory thuocTinhModelFactory,
            IThuocTinhService thuocTinhService,
            IPhuongAnXuLyService phuongAnXuLyService,
            IHinhThucXuLyService hinhThucXuLyService,
            ITaiSanTdXuLyService taiSanTdXuLyService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._thuocTinhTaiSanService = thuocTinhTaiSanService;
            this._taiSanService = taiSanService;
            this._thuocTinhModelFactory = thuocTinhModelFactory;
            this._thuocTinhService = thuocTinhService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this.hinhThucXuLyService = hinhThucXuLyService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
        }
        #endregion
        
        #region ThuocTinhData
        public ThuocTinhDataSearchModel PrepareThuocTinhDataSearchModel(ThuocTinhDataSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public ThuocTinhDataListModel PrepareThuocTinhDataListModel(ThuocTinhDataSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchThuocTinhDatas(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new ThuocTinhDataListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<ThuocTinhDataModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public ThuocTinhDataModel PrepareThuocTinhDataModel(ThuocTinhDataModel model, ThuocTinhData item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ThuocTinhDataModel>();
            }
            //more
           
            return model;
        }
       public void PrepareThuocTinhData(ThuocTinhDataModel model, ThuocTinhData item)
        {
		item.THUOC_TINH_ID = model.THUOC_TINH_ID;
		item.DATA = model.DATA;
		item.DON_VI_ID = model.DON_VI_ID;
		item.DON_VI_BO_PHAN_ID = model.DON_VI_BO_PHAN_ID;
		item.NGAY_TAO = model.NGAY_TAO;
		item.NGAY_CAP_NHAT = model.NGAY_CAP_NHAT;
		item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            
        }
        public List<modelThuocTinh> PrepareListmodelThuocTinh(int LoaiHinhTaiSan = 0 , int LoaiTaiSan = 0,int TaiSanId =0)
        {
            var listmodel = new List<modelThuocTinh>();
            var ttTaiSan =_thuocTinhTaiSanService.GetThuocTinhTaiSan(LoaiHinhTaiSan, LoaiTaiSan);
            // lấy data thuộc tính theo tài sản
            var item = _itemService.GetThuocTinhDataByTaiSanId(TaiSanId: TaiSanId);
            if (item != null && item.Count() > 0)
            {
                //chuyển đổi từ dạng json về model
                listmodel = item.Select(c => PreparemodelThuocTinhByThuocTinhData(c, LoaiHinhTaiSan: LoaiHinhTaiSan, LoaiTaiSan: LoaiTaiSan)).ToList();
            }

            if (ttTaiSan.Count > 0)
            {
                // lấy hết cha
                var listTaiSanId = new List<decimal?>();
                foreach (var Chuoi in ttTaiSan.Where(c => c.TREE_NOTE != null).Select(c => c.TREE_NOTE).ToList())
                {
                    var list = Chuoi.Split('-').Select(c => (decimal?)(c.Trim().ToNumberInt32())).ToList();
                    listTaiSanId = listTaiSanId.Concat(list).Distinct().ToList();
                }
                //get all thuộc tính tài sản theo cha con
                var ttTaiSanAll = _thuocTinhTaiSanService.GetThuocTinhTaiSan(LoaiHinhTaiSan: LoaiHinhTaiSan, ListTaiSanId: listTaiSanId).ToList();
                if (item != null && item.Count() > 0)
                {
                    //remove những thuộc tính id đã có data 
                    foreach (var tt in item.Select(c => (decimal)c.THUOC_TINH_ID))
                    {
                        ttTaiSanAll.Remove(ttTaiSanAll.Where(c=>c.THUOC_TINH_ID ==tt).FirstOrDefault());
                    };
                }
                //lấy thuộc tính theo taisanall
                //var tts = _thuocTinhService.GetThuocTinhByIds(ttTaiSanAll.ToArray());
                listmodel = listmodel.Concat(ttTaiSanAll.Select(c => _thuocTinhModelFactory.PreparemodelThuocTinhByThuocTinh(ttData:c.ThuocTinh,Sapxep:(int?)c.SAP_XEP))).ToList();               
            }
            return listmodel;
        }
        public modelThuocTinh PreparemodelThuocTinhByThuocTinhData(ThuocTinhData ttData, int LoaiHinhTaiSan = 0, int LoaiTaiSan = 0,Guid GUID = new Guid())
        {
            var model = new modelThuocTinh();
            if (ttData != null)
            {
                var item = ttData.DATA.toEntity<ThuocTinhEntity>();
                if (item != null)
                {
                    model = _thuocTinhModelFactory.PreparemodelThuocTinhByThuocTinhEntity(model,item);
                }
                // gán thêm 
                if (ttData.THUOC_TINH_ID != null)
                {
                    model.ThuocTinhId = (int)ttData.THUOC_TINH_ID;
                    var ttTaiSan = _thuocTinhTaiSanService.GetThuocTinhTaiSanByLoaiTaiSanIdAndLoaiHinhTaiSanAndThuocTinhId(LoaiTaiSan: LoaiTaiSan, LoaiHinhTaiSan: LoaiHinhTaiSan, ThuocTinhId: (int)ttData.THUOC_TINH_ID);
                    if (ttTaiSan != null)
                    {
                        model.SapXep = (int)ttTaiSan.SAP_XEP;
                    }
                }
                if(!GUID.Equals(Guid.Empty))
                {
                    model.GuidView = GUID;
                    if (model.THUOC_TINH.Count() > 0)
                    {
                        foreach(var lst in model.THUOC_TINH)
                        {
                            lst.GuidView = GUID;
                        }
                    }
                }

            }
            return model;
        }
        public List<modelThuocTinh> PrepareListmodelThuocTinhForTaiSanTdXuLy(int PhuongAnXuLyId = 0, int TaiSanTdId = 0, Guid TaiSanXuLyGuid = new Guid())
        {
            var listmodel = new List<modelThuocTinh>();
            var ttPhuongAn = _phuongAnXuLyService.GetPhuongAnXuLyById(PhuongAnXuLyId);
            var tsxl = _taiSanTdXuLyService.GetTaiSanTdXuLyByGuId(TaiSanXuLyGuid);
            if (tsxl != null)
            {
                // lấy data thuộc tính theo tài sản xử lý
                var item = _itemService.GetThuocTinhDataByTaiSanId(TaiSanTdXuLyId: (int)tsxl.ID);
                if (item != null && item.Count() > 0 && tsxl.PHUONG_AN_XU_LY_ID == ttPhuongAn.ID)
                {
                    //chuyển đổi từ dạng json về model
                    listmodel = item.Select(c => PreparemodelThuocTinhByThuocTinhData(c)).ToList();
                }
                else if (ttPhuongAn != null)
                {
                    if (ttPhuongAn.CONFIG_CAU_HINH != null)
                    {
                        var tt = ttPhuongAn.CONFIG_CAU_HINH.toEntity<ThuocTinhEntity>();
                        if (tt != null)
                        {                          
                            var ttmodel = _thuocTinhModelFactory.PreparemodelThuocTinhByThuocTinhEntity(new modelThuocTinh(), tt);                          
                            listmodel.Add(ttmodel);
                        }
                    }
                }

            }
            else if (ttPhuongAn != null)
            {
                if (ttPhuongAn.CONFIG_CAU_HINH != null)
                {
                    var tt = ttPhuongAn.CONFIG_CAU_HINH.toEntity<ThuocTinhEntity>();
                    if (tt != null)
                    {
                        var ttmodel = _thuocTinhModelFactory.PreparemodelThuocTinhByThuocTinhEntity(new modelThuocTinh(), tt);
                        listmodel.Add(ttmodel);
                    }
                }
            }
            return listmodel;
        }
        #endregion
    }
}		

