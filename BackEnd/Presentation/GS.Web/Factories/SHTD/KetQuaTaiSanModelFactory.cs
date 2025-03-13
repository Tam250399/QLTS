using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.SHTD;
using GS.Services.DanhMuc;
using GS.Services.SHTD;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.SHTD
{
    public class KetQuaTaiSanModelFactory:IKetQuaTaiSanModelFactory
    {
        #region Fields    		
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IKetQuaTaiSanServices _itemService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly IXuLyService _xuLyService;
        private readonly IXuLyKetQuaServices _xuLyKetQuaServices;
        private readonly IPhuongAnXuLyService _phuongAnXuLyServices;
        #endregion

        #region Ctor

        public KetQuaTaiSanModelFactory(
            ICacheManager cacheManager,
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,
            IKetQuaTaiSanServices itemService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            IXuLyService xuLyService,
            IXuLyKetQuaServices xuLyKetQuaServices,
            IPhuongAnXuLyService phuongAnXuLyServices
            )
        {
            this._cacheManager = cacheManager;
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager;
            this._itemService = itemService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._xuLyService = xuLyService;
            this._xuLyKetQuaServices = xuLyKetQuaServices;
            this._phuongAnXuLyServices = phuongAnXuLyServices;
        }
        #endregion

        #region KetQuaTaiSan
        public KetQuaTaiSanSearchModel PrepareKetQuaTaiSanSearchModel(KetQuaTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }

        public KetQuaTaiSanListModel PrepareKetQuaTaiSanListModel(KetQuaTaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchKetQuaTaiSans(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, XuLyKetQuaId:(decimal)searchModel.XuLyKetQuaId,Is_Create:searchModel.Is_Create);

            //prepare list model
            var model = new KetQuaTaiSanListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareKetQuaTaiSanModel(new KetQuaTaiSanModel(), c)),
                Total = items.TotalCount
            };
            return model;
        }
        public KetQuaTaiSanModel PrepareKetQuaTaiSanModel(KetQuaTaiSanModel model, KetQuaTaiSan item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<KetQuaTaiSanModel>();
                model.TaiSanTen = item.taisantdxuly.taisantd.TEN;
                model.LoaiTaiSanTen = item.taisantdxuly.taisantd.TEN_LOAI_TAI_SAN;
                if (item.taisantdxuly.hinhthucxuly !=null)
                {
                    model.PhuongAnXuLyTen = item.taisantdxuly.hinhthucxuly.TEN;
                }
            }
            if (excludeProperties)
            {
                model.DDLTaiSanTD = PrePareDDLTaiSanTD((decimal)model.XU_LY_KET_QUA_ID,isAddFirst:true,valSelected:model.TAI_SAN_TD_XU_LY_ID!=null?(decimal)model.TAI_SAN_TD_XU_LY_ID:0);
            }
            return model;
        }
        public void PrepareKetQuaTaiSan(KetQuaTaiSanModel model, KetQuaTaiSan item)
        {
            item.SO_TIEN = model.SO_TIEN;
            item.SO_LUONG = model.SO_LUONG;
            item.TAI_SAN_TD_ID = model.TAI_SAN_TD_ID;
            item.XU_LY_KET_QUA_ID = model.XU_LY_KET_QUA_ID;
            item.TAI_SAN_TD_XU_LY_ID = model.TAI_SAN_TD_XU_LY_ID;
        }
        public List<SelectListItem> PrePareDDLTaiSanTD(decimal XuLyKetQuaId,bool isAddFirst,string strAddFirst ="Chọn tài sản xử lý",decimal valSelected = 0)
        {
            var DDLTaiSanTD = new List<SelectListItem>();
            var xulyketqua = _xuLyKetQuaServices.GetXuLyKetQuaById(XuLyKetQuaId);
            if (xulyketqua != null)
            {
                var taisanxuly = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(xulyketqua.xulyTD.ID);
                if (taisanxuly!= null && taisanxuly.Count() > 0)
                {
                    DDLTaiSanTD = taisanxuly.Select(c => new SelectListItem()
                    {
                        Value = c.ID.ToString(),
                        Text = c.taisantd.TEN + "(" + c.taisantd.TEN_LOAI_TAI_SAN + ")-"+c.hinhthucxuly.TEN,
                        Selected = valSelected==c.ID
                    }).ToList();
                }
                if (isAddFirst)
                {
                    DDLTaiSanTD.Insert(0, new SelectListItem() { Value = "0", Text = strAddFirst });
                }
            }           
            return DDLTaiSanTD;
        }
        public decimal PrePareSoLuongConLai(decimal TaiSanTDXuLyId, decimal XuLyKetQuaId,decimal KetQuaTaiSanId)
        {
            var soluongcon = 0;
            var soluongdaxuly = _itemService.GetKetQuaTaiSans(TaiSanTDXuLyId: TaiSanTDXuLyId, XuLyKetQuaId: XuLyKetQuaId).Select(c => c.SO_LUONG).Sum();
            var soluongbandau = _taiSanTdXuLyService.GetTaiSanTdXuLyById(TaiSanTDXuLyId);
            var item = _itemService.GetKetQuaTaiSanById(KetQuaTaiSanId);
            soluongcon = (soluongbandau != null ? (int)soluongbandau.SO_LUONG : 0) - (soluongdaxuly != null ? (int)soluongdaxuly : 0)+(item!=null? (int)item.SO_LUONG:0);
            return soluongcon;
        }
        #endregion
    }
}
