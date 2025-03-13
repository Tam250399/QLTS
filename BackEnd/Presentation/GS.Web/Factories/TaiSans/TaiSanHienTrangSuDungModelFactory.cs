using GS.Core;
using GS.Core.Caching;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.TaiSans;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.TaiSans
{
	public class TaiSanHienTrangSuDungModelFactory : ITaiSanHienTrangSuDungModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ITaiSanHienTrangSuDungService _itemService;
        private readonly IBienDongService _bienDongService;
        private readonly IBienDongChiTietService _bienDongChiTietService;
        #endregion

        #region Ctor

        public TaiSanHienTrangSuDungModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ITaiSanHienTrangSuDungService itemService,
			IBienDongService bienDongService,
			IBienDongChiTietService bienDongChiTietService

			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
            this._bienDongService = bienDongService;
            this._bienDongChiTietService = bienDongChiTietService;
        }
		#endregion

		#region TaiSanHienTrangSuDung
		public TaiSanHienTrangSuDungSearchModel PrepareTaiSanHienTrangSuDungSearchModel(TaiSanHienTrangSuDungSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			return searchModel;
		}

		public TaiSanHienTrangSuDungListModel PrepareTaiSanHienTrangSuDungListModel(TaiSanHienTrangSuDungSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchTaiSanHienTrangSuDungs(Keysearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new TaiSanHienTrangSuDungListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>c.ToModel<TaiSanHienTrangSuDungModel>()),
				Total = items.TotalCount
			};
			return model;
		}
		public TaiSanHienTrangSuDungModel PrepareTaiSanHienTrangSuDungModel(TaiSanHienTrangSuDungModel model, TaiSanHienTrangSuDung item, bool excludeProperties = false)
		{
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<TaiSanHienTrangSuDungModel>();
			}
			return model;
		}
		public void InsertHienTrangSuDungForBienDong(decimal bienDongId, decimal taiSanId, string jsonHienTrang)
		{
			var lstHT = jsonHienTrang.toEntity<HienTrangList>();
			if (lstHT!= null && lstHT.lstObjHienTrang!= null && lstHT.lstObjHienTrang.Count>0)
			{
				var TaiSanHienTrangSuDungs = lstHT.lstObjHienTrang.Select(p =>
			   {
				   var item = new TaiSanHienTrangSuDung
				   {
					   BIEN_DONG_ID = bienDongId,
					   TAI_SAN_ID = taiSanId,
					   GIA_TRI_CHECKBOX = p.GiaTriCheckbox,
					   GIA_TRI_NUMBER = p.GiaTriNumber,
					   GIA_TRI_TEXT = p.GiaTriText,
					   TEN_HIEN_TRANG = p.TenHienTrang,
					   KIEU_DU_LIEU_ID = p.KieuDuLieuId ?? 0,
					   NHOM_HIEN_TRANG_ID = p.NhomHienTrangId,
					   HIEN_TRANG_ID = p.HienTrangId??0,
					   ID = 0
				   };
				   return item;
			   }).ToList();
				_itemService.InsertTaiSanHienTrangSuDungs(TaiSanHienTrangSuDungs);
			}
		}
		/// <summary>
		/// Chỉ sử dụng khi cập nhật biến động bị sai hiện trạng
		/// </summary>
		/// <param name="bienDongId"></param>
		/// <param name="taiSanId"></param>
		/// <param name="jsonHienTrang"></param>
		public void UpdateTaSanHienTrangForBienDong(decimal bienDongId, decimal taiSanId, string jsonHienTrang)
		{
			var lstHTNew = jsonHienTrang.toEntity<HienTrangList>();
			var lstHTOld = _itemService.GetTaiSanHienTrangSuDungByBienDongId(bienDongId);
			if ( lstHTNew?.lstObjHienTrang?.Count() > 0 && lstHTOld?.Count() > 0)
			{
                foreach (var hienTrangOld in lstHTOld)
                {
                    if (hienTrangOld != null)
                    {
						var hienTrangNew = lstHTNew.lstObjHienTrang.Where(c => c.HienTrangId == hienTrangOld.HIEN_TRANG_ID).FirstOrDefault();
                        if (hienTrangNew != null)
						{
							    hienTrangOld.GIA_TRI_NUMBER = hienTrangNew.GiaTriNumber;					
							    hienTrangOld.GIA_TRI_CHECKBOX = hienTrangNew.GiaTriCheckbox;
							_itemService.UpdateTaiSanHienTrangSuDung(hienTrangOld);
						}
					}
                }
			}
		}
		/// <summary>
		/// Update lại hiện trạng json của bdct và không chạy tính khấu hao hao mòn
		/// Chỉ áp dụng khi cập nhật hiện trạng bị sai dữ liệu
		/// </summary>
		/// <param name="bienDongId"></param>
		/// <param name="jsonHienTrang"></param>
		public void UpdateHienTrangSuDungObjOfBienDongChiTiet(decimal bienDongId, string jsonHienTrang)
		{

			var bienDongChiTiet = _bienDongChiTietService.GetBienDongChiTietByBDId(bienDongId);
            if (bienDongChiTiet != null)
            {
				bienDongChiTiet.HTSD_JSON = jsonHienTrang;
				// update lại hiện trạng json của bdct nhưng ko chạy tính lại khấu hao hao mòn
				_bienDongChiTietService.UpdateHienTrangBienDongChiTiet(bienDongChiTiet);

			}
               			
		}
		#endregion
	}
	

}
