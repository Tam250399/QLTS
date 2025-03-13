//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.DanhMuc;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
    public class DiaBanModelFactory : IDiaBanModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IDiaBanService _itemService;
        private readonly IQuocGiaService _quocGiaService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuocGiaModelFactory _quocGiaModelFactory;
        #endregion

        #region Ctor

        public DiaBanModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IDiaBanService itemService,
            INhanHienThiService nhanHienThiService,
            IQuocGiaService quocGiaService,
            IQuocGiaModelFactory quocGiaModelFactory
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._nhanHienThiService = nhanHienThiService;
            this._quocGiaService = quocGiaService;
            this._quocGiaModelFactory = quocGiaModelFactory;
        }

        #endregion

        #region DiaBan
        public DiaBanSearchModel PrepareDiaBanSearchModel(DiaBanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public DiaBanListModel PrepareDiaBanListModel(DiaBanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchDiaBans(Keysearch: searchModel.KeySearch, ParentId: searchModel.ParentId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new DiaBanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var m = c.ToModel<DiaBanModel>();
                    m.DiabanSubCount = _itemService.GetCountDiaBanSub(c.ID);
                    m.TrangThai = _nhanHienThiService.GetGiaTriEnum(c.TrangThai);
                    m.TenLoaiDiaBan = _nhanHienThiService.GetGiaTriEnum(c.LoaiDiaBan);
                    var quocGia = _quocGiaService.GetQuocGiaById(c.QUOC_GIA_ID ?? 0);
                    m.TenQuocGia = quocGia != null ? quocGia.TEN : "";
                    m.pageIndex = searchModel.Page;
                    return m;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }
        public DiaBanModel PrepareDiaBanModel(DiaBanModel model, DiaBan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<DiaBanModel>();
                if (item.PARENT_ID > 0)
                {
                    model.TenDiaBanCha = _itemService.GetDiaBanById((decimal)model.PARENT_ID).TEN.ToString();
                }
            }
            model.QuocGiaAvaliable = _quocGiaService.GetAllQuocGias().Select(c => new SelectListItem
            {
                Text = c.TEN,
                Value = c.ID.ToString(),
                Selected = c.ID == model.QUOC_GIA_ID
            }).ToList();
            //  model.QuocGiaAvaliable = _quocGiaModelFactory.PrepareSelectListQuocGias();
            model.LoaiDiaBanAvaliable = model.enumLoaiDiaBan.ToSelectList();
            model.TrangThaiAvaliable = model.enumTrangThai.ToSelectList();
            if (model.ID > 0)
            {
                model.LoaiDiaBanAvaliable = ((enumLOAI_DIABAN)model.LOAI_DIA_BAN_ID).ToSelectList();
                model.TrangThaiAvaliable = ((enumTRANG_THAI_DIABAN)model.TRANG_THAI_ID).ToSelectList();
            }
            //more

            return model;
        }
        public void PrepareDiaBan(DiaBanModel model, DiaBan item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.MO_TA = model.MO_TA;
            item.MA_CHA = model.MA_CHA;
            item.VUNG_KINH_TE_ID = model.VUNG_KINH_TE_ID;
            item.MA_PHAN_CAP = model.MA_PHAN_CAP;
            item.PARENT_ID = model.PARENT_ID;
            item.TREE_NODE = model.TREE_NODE;
            item.TREE_LEVEL = model.TREE_LEVEL;
            item.TRANG_THAI_ID = model.TRANG_THAI_ID;
            item.LOAI_DIA_BAN_ID = model.LOAI_DIA_BAN_ID;
            item.QUOC_GIA_ID = model.QUOC_GIA_ID;

        }
        public bool CheckMaDiaBan(decimal Id, string Ma)
        {
            var diaban = _itemService.GetDiaBanByMa(Ma);
            if (diaban == null)
                return true;
            if (Id > 0 && diaban.ID == Id)
                return true;
            return false;
        }
        public IList<SelectListItem> PrepareDiaBanAvailabele(decimal? ParentId = 0, decimal? Valselected = 0, decimal? CapDiaBan = (int)enumLOAI_DIABAN.ALL, decimal? QuocGiaId = 0, bool IsAddFirst = false, string strFirstRow = null, bool IsListChaCon = true)
        {
            var tree = "";
            var selectlist = _itemService.GetDiaBans(CapDiaban: CapDiaBan, ParentId: ParentId, QuocGiaId: QuocGiaId).OrderBy(c => c.TREE_NODE).Select(c => new SelectListItem
            {
                Text = IsListChaCon ? tree.PadLeft(Convert.ToInt32(c.TREE_LEVEL - 1) * 3, '-') + c.TEN : c.TEN,
                Value = c.ID.ToString(),
                Selected = Valselected == c.ID
            }).OrderBy(c=>c.Text).ToList();
            if (IsAddFirst)
            {
                selectlist.AddFirstRow(strFirstRow);
            }
            return selectlist;
        }
        public bool CheckDiaBanXaByHuyenId(decimal HuyenId)
        {
            var diaban = _itemService.GetDiaBans(ParentId: HuyenId);
            if (diaban == null||diaban.Count()==0)
                return true;
            return false;
        }
        #endregion
    }
}

