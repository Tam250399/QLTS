//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
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
using GS.Core.Domain.HeThong;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.HeThong;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.HeThong;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using GS.Services.DanhMuc;

namespace GS.Web.Factories.HeThong
{
    public class DinhMucModelFactory:IDinhMucModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IDinhMucService _itemService;
        private readonly IDbContext _dbContext;
        private readonly IDonViService _donViService;
        private readonly IDinhMucChiTietService _dinhMucChiTietService;
        private readonly IDinhMucChiTietModelFactory _dinhMucChiTietModelFactory;
        #endregion

        #region Ctor

        public DinhMucModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IDinhMucService itemService,
            IDbContext dbContext,
            IDonViService donViService, 
            IDinhMucChiTietService dinhMucChiTietService,
            IDinhMucChiTietModelFactory dinhMucChiTietModelFactory
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._dbContext = dbContext;
            this._donViService = donViService;
            this._dinhMucChiTietService = dinhMucChiTietService;
            this._dinhMucChiTietModelFactory = dinhMucChiTietModelFactory;
        }
        #endregion
        
        #region DinhMuc
        public DinhMucSearchModel PrepareDinhMucSearchModel(DinhMucSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public DinhMucListModel PrepareDinhMucListModel(DinhMucSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var donviid = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
            var donvicha = _donViService.GetDonViLonNhat(donviid.ID, donviid.TREE_NODE);
            //get items
            var items = _itemService.SearchDinhMucs(donViID:donvicha.ID,TuNgay:searchModel.TuNgay, DenNgay:searchModel.DenNgay,SoQuyetDinh:searchModel.SoQuyetDinh, NgayQuyetDinh:searchModel.NgayQuyetDinh, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            
            //prepare list model
            var model = new DinhMucListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => c.ToModel<DinhMucModel>()),
                Total = items.TotalCount
            };
            return model;
        }
        public DinhMucModel PrepareDinhMucModel(DinhMucModel model, DinhMuc item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DinhMucModel>();
                if (model.DEN_NGAY.HasValue)
                {
                    model.IS_HIEU_LUC = true;
                }
                var dinhmucchitiets = _dinhMucChiTietService.GetDinhMucChiTietByDinhMucId(item.ID);
                if (dinhmucchitiets.Count()>0) {
                    if (model.ChiTietDinhMuc.Count() > 0) {
                        foreach (var dinhmucchitietItem in dinhmucchitiets) {
                            foreach (var chitietdinhmucModel in model.ChiTietDinhMuc) {
                                if(dinhmucchitietItem.DINH_MUC_ID == chitietdinhmucModel.DINH_MUC_ID && dinhmucchitietItem.LOAI_TAI_SAN_ID == chitietdinhmucModel.LOAI_TAI_SAN_ID && dinhmucchitietItem.CHUC_DANH_ID == chitietdinhmucModel.CHUC_DANH_ID)
                                {
                                    _dinhMucChiTietModelFactory.PrepareDinhMucChiTietModel(chitietdinhmucModel,dinhmucchitietItem);
                                }

                            }
                        }
                    }
                    else
                    {
                        model.ChiTietDinhMuc = dinhmucchitiets.Select(c => c.ToModel<DinhMucChiTietModel>()).ToList();
                        if (model.ChiTietDinhMuc.Count() > 0)
                        {
                            foreach(var chitietdinhmuc in model.ChiTietDinhMuc)
                            {
                                _dinhMucChiTietModelFactory.PrepareDinhMucChiTietModel(chitietdinhmuc, null);
                            }
                        }
                    }
                }
            }
            //more
           
            return model;
        }
       public void PrepareDinhMuc(DinhMucModel model, DinhMuc item)
        {
		item.MA = model.MA;
		item.QUYET_DINH_SO = model.QUYET_DINH_SO;
		item.QUYET_DINH_NGAY = model.QUYET_DINH_NGAY?? DateTime.Now;
		item.TU_NGAY = model.TU_NGAY ?? DateTime.Now;
            item.DEN_NGAY = model.DEN_NGAY;
		item.THONG_TU = model.THONG_TU;
		item.DON_VI_ID = model.DON_VI_ID;
            
        }
        // Check định mức 
        public virtual KetQuaDinhMuc CheckDinhMucOto(decimal? ChucDanhId = null, decimal? LoaiTaiSanId = 0, decimal? DonViId =0 , DateTime? NgayGhiTang = null, decimal? NguyenGia = null)
        {
            OracleParameter p1 = new OracleParameter("P_CHUC_DANH_ID", OracleDbType.Int32, ChucDanhId, ParameterDirection.Input);
            OracleParameter p2 = new OracleParameter("P_LOAI_TAI_SAN_ID", OracleDbType.Int32, LoaiTaiSanId, ParameterDirection.Input);
            OracleParameter p3 = new OracleParameter("P_DON_VI_ID", OracleDbType.Int32, DonViId, ParameterDirection.Input);
            OracleParameter p4 = new OracleParameter("P_NGAY_GHI_TANG", OracleDbType.Date, NgayGhiTang, ParameterDirection.Input);
            OracleParameter p5 = new OracleParameter("p_NGUYEN_GIA", OracleDbType.Int32, NguyenGia, ParameterDirection.Input);
            OracleParameter p_out = new OracleParameter("TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);

            try
            {
                var result = _dbContext.EntityFromStore<KetQuaDinhMuc>("CHECK_DINH_MUC_OTO", p1, p2, p3, p4, p5, p_out);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckMaDinhMuc(decimal id, string Ma)
        {
            var diaBanTest = _itemService.CheckMaDinhMuc(Ma);
            if (diaBanTest == null)
            {
                return true;
            }
            if (id >= 0 && diaBanTest.ID == id)
            {
                return true;
            }
            return false;
        }
        public bool CheckSoQuyetDinhDinhMuc(decimal id, string Ma)
        {
            var diaBanTest = _itemService.CheckSoQuyetDinhDinhMuc(Ma);
            if (diaBanTest == null)
            {
                return true;
            }
            if (id >= 0 && diaBanTest.ID == id)
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}		

