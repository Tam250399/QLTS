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
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Framework.Extensions;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.DanhMuc
{
    public class LoaiDonViModelFactory : ILoaiDonViModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly ILoaiDonViService _itemService;
        #endregion

        #region Ctor

        public LoaiDonViModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            ILoaiDonViService itemService
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
        }
        #endregion

        #region LoaiDonVi
        public LoaiDonViSearchModel PrepareLoaiDonViSearchModel(LoaiDonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public LoaiDonViListModel PrepareLoaiDonViListModel(LoaiDonViSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchLoaiDonVis(Keysearch: searchModel.KeySearch, ParentID: searchModel.ParentID, TreeLevel: searchModel.TreeLevel, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

            //prepare list model
            var model = new LoaiDonViListModel
            {
                //fill in model values from the entity
                Data = items.Where(c => c.ID != 14).Select(c =>
               {
                   var m = c.ToModel<LoaiDonViModel>();
                   m.LOAI_DON_VI_SUB_COUNT = _itemService.GetCountLoaiDonViSub(KeySearch: searchModel.KeySearch, ParentID: m.ID);
                   m.MA = m.MA ?? String.Empty;
                   return m;
               }),
                Total = items.TotalCount
            };
            return model;
        }

        public LoaiDonViModel PrepareLoaiDonViModel(LoaiDonViModel model, LoaiDonVi item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<LoaiDonViModel>();
            }
            //more
            model.dllTreeLaiHinhDonVi = PrepareSelectListLoaiDonVi(ValSelected: model.PARENT_ID, isAddFirst: true);
            model.dllCheDoHachToan = model.CheDoHoachToanEnum.ToSelectList();

            return model;
        }

        public void PrepareLoaiDonVi(LoaiDonViModel model, LoaiDonVi item)
        {
            item.MA = model.MA;
            item.TEN = model.TEN;
            item.PARENT_ID = model.PARENT_ID;
            item.SAP_XEP = model.SAP_XEP;
            item.MA_PHAN_CAP = model.MA_PHAN_CAP;
            item.TREE_LEVEL = model.TREE_LEVEL;
            item.TREE_NODE = model.TREE_NODE;
            item.CHE_DO_HOACH_TOAN_ID = model.CHE_DO_HOACH_TOAN_ID;

        }
        public bool CheckMaLoaiDonVi(decimal id = 0, string ma = null)
        {
            return !_itemService.CheckMaLoaiDonVi(id: id, ma: ma);
        }

        public IList<SelectListItem> PrepareSelectListLoaiDonVi(decimal? ValSelected = 0, bool isAddFirst = false, string valueFirst = "")
        {
            string tree = "";
            var selectList = _itemService.GetAllLoaiDonVis().OrderBy(c => c.TREE_NODE).Select(c => new SelectListItem
            {
                Text = tree.PadLeft(Convert.ToInt32(c.TREE_LEVEL - 1) * 3, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = ValSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow("----------Chọn loại đơn vị----------", valueFirst);

            return selectList;
        }
        public IList<SelectListItem> PrepareSelectListLoaiDoiTac(decimal? ValSelected = 0, bool isAddFirst = false, string valueFirst = "")
        {
            string tree = "";
            var IdLoaiToChuc = _itemService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_TO_CHUC)?.ID;
            var IdToChucDoanhNghiep = _itemService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_DOANH_NGHIEP)?.ID;
            // Lựa chọn "Loại hình Đơn Vị"  (riêng "Tổ chức" chọn đến bậc 2; các loại hình khác chọn bậc 1)
            var selectList = _itemService.GetAllLoaiDonVis().OrderBy(c => c.TREE_NODE)
                .Where(c => c.PARENT_ID == null || c.PARENT_ID == IdLoaiToChuc)
                .Where(c => c.ID != IdToChucDoanhNghiep) // bỏ tổ chức doanh nghiệp
                .Select(c => new SelectListItem
            {
                Text = tree.PadLeft(Convert.ToInt32(c.TREE_LEVEL - 1) * 3, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = ValSelected == c.ID
            }).ToList();
            if (isAddFirst)
                selectList.AddFirstRow("----------Chọn loại đơn vị----------", valueFirst);

            return selectList;
        }

        public IList<SelectListItem> PrepareMultiSelectLoaiDonVi(IList<int> valSelecteds = null, bool addItemAll = true)
        {
            var lstLoaiDonVi = _itemService.GetAllLoaiDonVis().OrderBy(c => c.TREE_NODE);
            string tree = "";
            var selectList = lstLoaiDonVi.Select(c => new SelectListItem
            {
                Text = tree.PadLeft(Convert.ToInt32(c.TREE_LEVEL - 1) * 3, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelecteds == null ? false : valSelecteds.Contains(c.ID.ToNumberInt32())
            }).ToList();
            if (addItemAll)
                selectList.AddFirstRow("Tất cả đơn vị", "0");
            selectList[0].Selected = true;
            return selectList;
        }

        public IList<SelectListItem> PrepareMultiSelectLoaiDonViForBaoCao(IList<int> valSelecteds = null)
        {
            var lstLoaiDonVi = _itemService.GetLoaiDonViForBaoCao();
            string tree = "";
            var selectList = lstLoaiDonVi.Select(c => new SelectListItem
            {
                Text = tree.PadLeft(Convert.ToInt32(c.TREE_LEVEL - 1) * 3, '-') + c.TEN,
                Value = c.ID.ToString(),
                Selected = valSelecteds == null ? false : valSelecteds.Contains(c.ID.ToNumberInt32())
            }).ToList();
            return selectList;
        }
        #endregion
    }
}

