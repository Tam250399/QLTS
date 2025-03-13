//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.TaiSans;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.TaiSans
{
    public class TaiSanNguonVonModelFactory : ITaiSanNguonVonModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ITaiSanNguonVonService _itemService;
        private readonly IBienDongService _bienDongService;
        #endregion

        #region Ctor

        public TaiSanNguonVonModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ITaiSanNguonVonService itemService,
            IBienDongService bienDongService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._bienDongService = bienDongService;
        }
        #endregion

        #region TaiSanNguonVon
        public TaiSanNguonVonSearchModel PrepareTaiSanNguonVonSearchModel(TaiSanNguonVonSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public TaiSanNguonVonListModel PrepareTaiSanNguonVonListModel(TaiSanNguonVonSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchTaiSanNguonVons(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new TaiSanNguonVonListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<TaiSanNguonVonModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public TaiSanNguonVonModel PrepareTaiSanNguonVonModel(TaiSanNguonVonModel model, TaiSanNguonVon item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<TaiSanNguonVonModel>();
            }
            //more

            return model;
        }
        public void PrepareTaiSanNguonVon(TaiSanNguonVonModel model, TaiSanNguonVon item)
        {
            item.TAI_SAN_ID = model.TAI_SAN_ID;
            item.NGUON_VON_ID = model.NGUON_VON_ID;
            item.GIA_TRI = model.GIA_TRI;

        }
        public virtual void InsertTaiSanNguonVonFromYeuCau(YeuCau yeuCau, BienDong bienDong)
        {
            var lstNguonVon = yeuCau.NGUON_VON_JSON.toEntities<NguonVonModel>();
            if (lstNguonVon != null && lstNguonVon.Count > 0)
            {
                List<TaiSanNguonVon> lst = new List<TaiSanNguonVon>();
                foreach (var nv in lstNguonVon)
                {
                    var tsnv = new TaiSanNguonVon();
                    tsnv.TAI_SAN_ID = yeuCau.TAI_SAN_ID;
                    tsnv.NGUON_VON_ID = nv.ID;
                    if (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
						if (nv.GiaTri > 0)
							tsnv.GIA_TRI = (-1 * nv.GiaTri) ?? 0;
						else
							tsnv.GIA_TRI = nv.GiaTri ?? 0;
					else
                        tsnv.GIA_TRI = nv.GiaTri ?? 0;
                    tsnv.BIEN_DONG_ID = bienDong.ID;
                    lst.Add(tsnv);
                    _itemService.InsertTaiSanNguonVon(tsnv);
                }
                //_itemService.InsertTaiSanNguonVons(lst);
            }
        }
        public virtual string GetNguonVonJsonFromListNguonVon(decimal idBienDong)
        {
            var listNguonVon =_itemService.GetTaiSanNguonVonByBienDongId(idBienDong);
            var bienDong = _bienDongService.GetBienDongById(idBienDong);
            if (listNguonVon!= null && listNguonVon.Count>0)
            {
                List<NguonVonModel> lstNguonVon = new List<NguonVonModel>();
                foreach (var tsNguonVon in listNguonVon)
                {
                    if (bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.BIEN_DONG_GIAM_GIA_TRI || 
                        bienDong.LOAI_BIEN_DONG_ID == (int)enumLOAI_LY_DO_TANG_GIAM.GIAM_DIEU_CHUYEN_MOT_PHAN)
                    {
                        NguonVonModel item = new NguonVonModel
                        {
                            GiaTri = -1 * tsNguonVon.GIA_TRI,
                            ID = tsNguonVon.NGUON_VON_ID,
                            TEN = tsNguonVon.nguonvon.TEN
                        };
                        lstNguonVon.Add(item);
                    }
                    else
                    {
                        NguonVonModel item = new NguonVonModel
                        {
                            GiaTri = tsNguonVon.GIA_TRI,
                            ID = tsNguonVon.NGUON_VON_ID,
                            TEN = tsNguonVon.nguonvon.TEN
                        };
                        lstNguonVon.Add(item);
                    }
                    
                }
                return lstNguonVon.toStringJson();
            }
            return "";
        }
        #endregion
    }
}

