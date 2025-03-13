//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
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
using GS.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GS.Web.Factories.SHTD
{
    public class ThuChiModelFactory:IThuChiModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IThuChiService _itemService;
            private readonly IXuLyModelFactory _xuLyModelFactory;
            private readonly IXuLyService _xuLyService;
        #endregion

        #region Ctor

        public ThuChiModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IThuChiService itemService,
            IXuLyModelFactory xuLyModelFactory,
            IXuLyService xuLyService
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._xuLyModelFactory = xuLyModelFactory;
            this._xuLyService = xuLyService;
        }
        #endregion
        
        #region ThuChi
        public ThuChiSearchModel PrepareThuChiSearchModel(ThuChiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }

        public ThuChiListModel PrepareThuChiListModel(ThuChiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchThuChis(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize,KetQuaId:searchModel.KetQuaId, ListXuLyIdString: searchModel.ListXuLyIdString);
           
            if (!string.IsNullOrEmpty(searchModel.ListXuLyIdString))
            {
                var MinId = items.Min(c => c.ID); // min id là id của lần nhập đầu tiên
                var listModel = items.Select(c => {
                    var m = c.ToModel<ThuChiModel>();
                    if (c.ID == MinId)
                    {
                        m.Is_First = true;
                        m.Is_Disable = false;
                    }

                    return m;
                }).OrderBy(c => c.NGAY_THU).ThenBy(c => c.ID).ToList();
                return  new ThuChiListModel
                {
                    //fill in model values from the entity
                    Data = listModel,
                    Total = items.TotalCount
                };
            }
                     
            //prepare list model
            var model = new ThuChiListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareThuChiModelForList(new ThuChiModel(),c)),
                Total = items.TotalCount
            };
            
            return model;
        }
        public ThuChiListModel PrepareThuChiKetQuaListModel(ThuChiSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchThuChiKetQuas(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KetQuaId: searchModel.KetQuaId, ListXuLyIdString: searchModel.ListXuLyIdString);

            //prepare list model
            var model = new ThuChiListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareThuChiModelForList(new ThuChiModel(), c)),
                Total = items.TotalCount
            };
            if (!string.IsNullOrEmpty(searchModel.KeySearch))
            {
                model.Data = model.Data?.Where(c => c.TenKetQua.ToLower().Contains(searchModel.KeySearch.ToLower()));
            }
            return model;
        }
        public ThuChiModel PrepareThuChiModelForList(ThuChiModel model, ThuChi item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ThuChiModel>();
                //if (item.XU_LY_ID != null)
                //{
                //    var xuly = _xuLyService.GetXuLyById((decimal)item.XU_LY_ID);
                //    if(xuly!=null)
                //    model.TenKetQua = xuly.QUYET_DINH_SO + " - " + xuly.QUYET_DINH_NGAY.toDateVNString();
                //}
                if (item.LIST_XU_LY_ID != null)
                {
                    var ListXuLy = item.LIST_XU_LY_ID.ToListInt().Select(c => _xuLyService.GetXuLyById(c));
                    if (ListXuLy != null)
                    {
                        string TenKetQua = "";
                        foreach (var xuly in ListXuLy)
                        {
                            if (xuly != null)
                            {
                                TenKetQua = TenKetQua + xuly.QUYET_DINH_SO + " - " + xuly.QUYET_DINH_NGAY.toDateVNString() + ", ";
                            }
                        }
                        model.TenKetQua = TenKetQua.Substring(0,TenKetQua.Length -2);
                    }
                        
                }

            }
            return model;
        }
        /// <summary>
        /// Truyền vào List xử lý string return ra Tên quyết định xử lý theo format
        /// </summary>
        /// <param name="ListXuLyID"></param>
        /// <returns></returns>
        public string PrepareDanhSachQuyetDinhXuLy(string ListXuLyID)
        {
            var result = "";
            if (!string.IsNullOrEmpty(ListXuLyID))
            {
               
                    var ListXuLy = ListXuLyID.ToListInt().Select(c => _xuLyService.GetXuLyById(c));
                    if (ListXuLy != null)
                    {
                       
                        foreach (var xuly in ListXuLy)
                        {
                            if (xuly != null)
                            {

                            result = result + $"Số {xuly.QUYET_DINH_SO} ngày {xuly.QUYET_DINH_NGAY.toDateVNString()} của {xuly.NGUOI_QUYET_DINH}, ";
                            }
                        }
                        result = result.Substring(0, result.Length - 2);
                    }

            }
            return result;
        }
        public ThuChiModel PrepareThuChiModel(ThuChiModel model, ThuChi item, bool excludeProperties = false)
        {
                model.SelectedXuLyIds = new List<int>();
            // nếu có list xl thì là edit 
                if (!string.IsNullOrEmpty(model.LIST_XU_LY_ID))
                {
                    model.SelectedXuLyIds = model.LIST_XU_LY_ID.ToListInt();
                }

            //more

            //model.DDLKetQuaXuLy = _xuLyModelFactory.PrepareDDLKetQuaXuLyTaiSan(isAddFirst: true,valSelected:(int)(model.XU_LY_ID ?? 0));
            
           
            model.DDLKetQuaXuLy = _xuLyModelFactory.PrepareMultiDDLKetQuaXuLyTaiSan(isAddFirst: true, valSelected: model.SelectedXuLyIds);
            return model;
        }
        /// <summary>
        /// tính số tiền thu chi tiếp theo cho model
        /// </summary>
        /// <param name="ListThuChiId"></param>
        /// <returns></returns>
        public virtual void TinhSoTienThuChiTiepTheo(ThuChiModel model )
        {
            var ListThuChi = _itemService.GetThuChis(ListThuChiId: model.LIST_XU_LY_ID);

            if (ListThuChi != null && ListThuChi.Count() > 0) // tức là đã có thu chi đầu tiền rồi
            {
                var ThuChiCuNhat = ListThuChi.OrderByDescending(c => c.NGAY_THU).ThenByDescending(c => c.ID).FirstOrDefault();
                model.SO_TIEN_PHAI_THU = ThuChiCuNhat.SO_TIEN_CON_PHAI_THU;
                model.SO_TIEN_CON_PHAI_THU = model.SO_TIEN_PHAI_THU;
                model.SO_TIEN_THU = 0;
                model.Is_First = false;
            }
            else
            {
                model.Is_First = true;
                model.SO_TIEN_PHAI_THU = 0;
                model.SO_TIEN_CON_PHAI_THU = 0;
                model.SO_TIEN_THU = 0;

            }
        }
        /// <summary>
        /// Check Trường hợp thay đổi ngày thu của thu chi đầu tiên. Nếu đã tồn tại thu chi có ngày thu < ngày thu sửa thì Add error vào model state
        /// </summary>
        /// <param name="ModelState"></param>
        /// <param name="ListXuLyId"></param>
        /// <returns></returns>
        public void CheckValidNgayThuDauTien(ModelStateDictionary ModelState, ThuChiModel model)
        {
            var ThuChiDauTien = _itemService.GetThuChiDauTien(model.LIST_XU_LY_ID);
            if (ThuChiDauTien != null && model.ID == ThuChiDauTien.ID)
            {
                // Check tồn tại
                var IsInvalid= _itemService.GetThuChis(ListThuChiId: model.LIST_XU_LY_ID).Any(c => c.ID != ThuChiDauTien.ID && model.NGAY_THU > c.NGAY_THU);
                // nếu tồn tại thì add error
                if (IsInvalid)
                {
                    ModelState.AddModelError(nameof(model.NGAY_THU), "Đã tồn tại ngày thu chi nhỏ hơn ngày thu chi đầu tiên");
                }
            }
        }
        public bool CheckValidChiPhiDauTien( ThuChiModel model)
        {
            var ThuChiDauTien = _itemService.GetThuChiDauTien(model.LIST_XU_LY_ID);
            if (ThuChiDauTien == null || model.ID == ThuChiDauTien?.ID)
            {
                return model.CHI_PHI > 0;
            }
            return true;
        }
        public ThuChiModel PrepareThuChiModelForCreateOrUpdate(string ListXuLyIdString, decimal Id)
        {
            var Item = _itemService.GetThuChiById(Id);
            var ListItem = _itemService.GetThuChis(ListThuChiId: ListXuLyIdString);
            var model = new ThuChiModel { LIST_XU_LY_ID = ListXuLyIdString, DON_VI_ID = _workContext.CurrentDonVi.ID };
            var isFirst = false;
            // xác định Ngày thu đầu tiên. Nếu là IsFirst thì cho ngày thu thoải mái
            if (ListItem == null || ListItem.Count() ==0)
            {
                isFirst = true;
            }
            else
            {
                var itemFirst = ListItem.OrderBy(c => c.ID).FirstOrDefault();
                if (itemFirst.ID == Id)
                {
                    isFirst = true;
                }
                model.NGAY_THU_FIRST = itemFirst.NGAY_THU;
            }
           
            
            if (Item != null) // trường hợp update
            {
                model = Item.ToModel<ThuChiModel>();
                model.Is_First = isFirst;
            }
            else // trường hợp thêm mới
            {
                TinhSoTienThuChiTiepTheo(model);
            }
            return model;
        }
        public void UpdateListXuLyIdWhenCreate(string ListThuChiFirst, string ListThuChi)
        {
            var listThuChi = _itemService.GetThuChis(ListThuChiId: ListThuChiFirst);
            if (listThuChi != null && ListThuChiFirst.Count() > 0 && !string.IsNullOrEmpty(ListThuChi))
            {
                foreach (var thuChi in listThuChi)
                {
                    thuChi.LIST_XU_LY_ID = ListThuChi;
                    _itemService.UpdateThuChi(thuChi);
                }
            }
        }
        public void PrepareThuChi(ThuChiModel model, ThuChi item)
        {
            item.KET_QUA_ID = model.KET_QUA_ID;
            item.SO_TIEN_PHAI_THU = model.SO_TIEN_PHAI_THU;
            item.SO_TIEN_CON_PHAI_THU = model.SO_TIEN_CON_PHAI_THU;
            item.SO_TIEN_THU = model.SO_TIEN_THU;
            item.NGAY_THU = model.NGAY_THU;
            item.TG_SO_TIEN = model.TG_SO_TIEN;
            item.TG_NGAY_NOP = model.TG_NGAY_NOP;
            item.CHI_PHI = model.CHI_PHI;
            item.XU_LY_ID = model.XU_LY_ID;
            item.LIST_XU_LY_ID = model.LIST_XU_LY_ID;
            item.DON_VI_ID = model.DON_VI_ID;
            item.SO_TIEN_NOP_NSNN = model.SO_TIEN_NOP_NSNN;
           
        }
        #endregion	
     }
}		

