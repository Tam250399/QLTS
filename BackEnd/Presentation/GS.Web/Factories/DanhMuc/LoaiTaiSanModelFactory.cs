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
	public class LoaiTaiSanModelFactory : ILoaiTaiSanModelFactory
	{
		#region Fields    		
		private readonly ICacheManager _cacheManager;
		private readonly IWorkContext _workContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly ILoaiTaiSanService _itemService;
		private readonly ICheDoHaoMonService _chedohaomonService;
		private readonly INhanHienThiService _nhanhienthiService;
		private readonly ILoaiTaiSanKhauHaoService _loaiTaiSanKhauHaoService;
		private readonly IDonViService _donViService;
		private readonly ILoaiDonViService _loaiDonViService;
		#endregion

		#region Ctor

		public LoaiTaiSanModelFactory(
			ICacheManager cacheManager,
			IWorkContext workContext,
			IStaticCacheManager staticCacheManager,
			ILoaiTaiSanService itemService,
			ICheDoHaoMonService chedohaomonService,
			INhanHienThiService nhanhienthiService,
			ILoaiTaiSanKhauHaoService loaiTaiSanKhauHaoService,
			IDonViService donViService,
			ILoaiDonViService loaiDonViService
			)
		{
			this._cacheManager = cacheManager;
			this._workContext = workContext;
			this._staticCacheManager = staticCacheManager;
			this._itemService = itemService;
			this._chedohaomonService = chedohaomonService;
			this._nhanhienthiService = nhanhienthiService;
			this._loaiTaiSanKhauHaoService = loaiTaiSanKhauHaoService;
			this._donViService = donViService;
			this._loaiDonViService = loaiDonViService;
		}
		#endregion

		#region LoaiTaiSan
		public LoaiTaiSanSearchModel PrepareLoaiTaiSanSearchModel(LoaiTaiSanSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			searchModel.SetGridPageSize();
			var lstCheDo = _chedohaomonService.GetAllCheDoHaoMons().OrderBy(c => c.ID);
			searchModel.CheDoAvaliable = lstCheDo.Select(c => new SelectListItem
			{
				Text = c.TEN_CHE_DO,
				Value = c.ID.ToString()
			}).ToList();
			return searchModel;
		}

		public LoaiTaiSanListModel PrepareLoaiTaiSanListModel(LoaiTaiSanSearchModel searchModel)
		{
			
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchLoaiTaiSans(Keysearch: searchModel.KeySearch, ParentId: searchModel.ParentId,
			   ChedoId: searchModel.CheDoId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new LoaiTaiSanListModel
			{
				
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					
					var m = c.ToModel<LoaiTaiSanModel>();
					m.CountSub = _itemService.GetCountSub(c.ID);
					if (c.LOAI_HINH_TAI_SAN_ID != null)
					{
						m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.LoaiHinhTaiSan);
					}
					return m;
				}).ToList(),
				Total = items.TotalCount
			};
			return model;
		}
		public LoaiTaiSanListModel PrepareLoaiTaiSanToanDanListModel(LoaiTaiSanSearchModel searchModel)
		{

			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchLoaiTaiSanToanDan(Keysearch: searchModel.KeySearch, ParentId: searchModel.ParentId,
			   ChedoId: searchModel.CheDoId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize);

			//prepare list model
			var model = new LoaiTaiSanListModel
			{

				//fill in model values from the entity
				Data = items.Select(c =>
				{

					var m = c.ToModel<LoaiTaiSanModel>();
					m.CountSub = _itemService.GetCountSub(c.ID);
					if (c.LOAI_HINH_TAI_SAN_ID != null)
					{
						m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.LoaiHinhTaiSan);
					}
					return m;
				}).ToList(),
				Total = items.TotalCount
			};
			return model;
		}
		public LoaiTaiSanListModel PrepareChonLoaiTaiSanListModel(LoaiTaiSanSearchModel searchModel)
		{
			if (searchModel == null)
				throw new ArgumentNullException(nameof(searchModel));

			//get items
			var items = _itemService.SearchAllLoaiTaiSan(Keysearch: searchModel.KeySearch,isHasNoChildrend:searchModel.IsHasNoChildren, ParentId: searchModel.ParentId,
			   ChedoId: searchModel.CheDoId, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, id: searchModel.Id);

			//prepare list model
			var model = new LoaiTaiSanListModel
			{
				//fill in model values from the entity
				Data = items.Select(c =>
				{
					var m = c.ToModel<LoaiTaiSanModel>();
					m.CountSub = _itemService.GetCountSub(c.ID);
					if (c.LOAI_HINH_TAI_SAN_ID != null)
					{
						m.TenLoaiHinhTaiSan = _nhanhienthiService.GetGiaTriEnum(c.LoaiHinhTaiSan);
					}
					if (m.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT || m.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
					{
						var countSub = _itemService.GetCountSub(c.ID);
						if (countSub > 0)
							m.isDisabled = true;
					}
					else if (!(c.HM_TY_LE > 0))
						m.isDisabled = true;
					return m;
				}).ToList(),
				Total = items.TotalCount
			};
			return model;
		}
		public LoaiTaiSanModel PrepareLoaiTaiSanModel(LoaiTaiSanModel model, LoaiTaiSan item, bool excludeProperties = false)
		{
						
			if (item != null)
			{
				//fill in model values from the entity
				model = item.ToModel<LoaiTaiSanModel>();
				if (item.PARENT_ID > 0)
				{
					model.TaiSanChaName = _itemService.GetLoaiTaiSanById((decimal)item.PARENT_ID).TEN;
				}
			}
			model.CheDoHaoMonAvaliable = _chedohaomonService.GetAllCheDoHaoMons().OrderBy(c => c.ID).Select(c =>
			new SelectListItem
			{
				Text = c.TEN_CHE_DO,
				Value = c.ID.ToString(),
				Selected = model.CHE_DO_HAO_MON_ID == c.ID
			}).ToList();
			model.LoaiHinhTaiSanAvaliable = model.enumLoaiHinhTaiSan.ToSelectList();
			if (model.ID > 0)
			{
				model.LoaiHinhTaiSanAvaliable = ((enumLOAI_HINH_TAI_SAN)model.LOAI_HINH_TAI_SAN_ID).ToSelectList();
			}
			model.DDLLoaiXe = (new enumLoaiXe()).ToSelectList().ToList();
			//more

			return model;
		}
		public void PrepareLoaiTaiSan(LoaiTaiSanModel model, LoaiTaiSan item)
		{
			
			item.MA = model.MA;
			item.TEN = model.TEN;
			item.LOAI_HINH_TAI_SAN_ID = model.LOAI_HINH_TAI_SAN_ID;
			item.HM_THOI_HAN_SU_DUNG = model.HM_THOI_HAN_SU_DUNG;
			item.HM_TY_LE = model.HM_TY_LE;

			item.MO_TA = model.MO_TA;
			item.CHE_DO_HAO_MON_ID = model.CHE_DO_HAO_MON_ID;
			item.PARENT_ID = model.PARENT_ID;
			item.DON_VI_TINH = model.DON_VI_TINH;
			if (item.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
			{
				item.OTO_LOAI_XE_ID = model.OTO_LOAI_XE_ID;
				item.OTO_CHO_NGOI_TU = model.OTO_CHO_NGOI_TU ?? 0;
				item.OTO_CHO_NGOI_DEN = model.OTO_CHO_NGOI_DEN;
			}
		}
		public bool CheckMaLoaiTaiSan(decimal Id, string Ma, decimal CheDoHaoMonId)
		{
			return !_itemService.CheckMaLoaiTaiSan(id: Id, ma: Ma, CheDoHaoMonId: CheDoHaoMonId);
		}
		/// <summary>
		/// Kiểm tra xem loai tai san co tai san con khong
		/// Neu co return true<br/>
		/// khong co return false<br/>
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool CheckLoaiTaiSanCha(decimal id)
		{
			var countSub = _itemService.GetCountSub(id);
			if (countSub > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		public IList<SelectListItem> PrepareSelectListLoaiTaiSan(decimal? valSelected = 0, 
			decimal? cheDoId = 0, bool isAddFirst = false, string strFirstRow = null,
			decimal? loaiHinhTaiSanId = 0, string valueFirstRow = "", bool isDisabled = true, 
			List<decimal> listLoaiHinhTaiSanId = null, decimal? ParentLoaiTaiSanId = null,
			bool? isCreateOrEditTaiSan = false, decimal? donViId = 0, bool isAll = false, List<decimal> listLoaiHinhTaiSanIgnore = null, bool? tsQLNTSCD = false)
		{
			if (string.IsNullOrEmpty(strFirstRow))
			{
				switch (loaiHinhTaiSanId)
				{
					case (int)enumLOAI_HINH_TAI_SAN.DAT:
						strFirstRow = "-- Chọn mục đích sử dụng --";
						break;
					case (int)enumLOAI_HINH_TAI_SAN.NHA:
						strFirstRow = "-- Chọn cấp nhà --";
						break;
					case (int)enumLOAI_HINH_TAI_SAN.OTO:
						strFirstRow = "-- Chọn loại xe --";
						break;
					default:
						strFirstRow = "-- Chọn loại tài sản --";
						break;
				}
			}
			string tree = "";
			var lstLoaiTaiSan = _itemService.GetListLoaiTaiSanTreeNodeByRoot(cheDoHaoMonId: cheDoId, LoaiHinhTaiSanId: loaiHinhTaiSanId, ListLoaiHinhTaiSanId: listLoaiHinhTaiSanId, ParentLoaiTaiSanId: ParentLoaiTaiSanId, ListIgnoreLTS: listLoaiHinhTaiSanIgnore);
			//Nếu là form tăng mới tài sản thì lọc loại tài sản đất theo loại hình đơn vị
			if (isCreateOrEditTaiSan.Value && loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAT)
			{
				LoaiDonVi ldv_CQNN = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_CO_QUAN_NHA_NUOC);
				LoaiDonVi ldv_ToChuc = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_TO_CHUC);
				LoaiDonVi ldv_SuNghiep = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_SU_NGHIEP);
				DonVi dvTaoTaiSan = new DonVi();
				if (donViId > 0)
					dvTaoTaiSan = _donViService.GetDonViById(donViId.Value);
				else
					dvTaoTaiSan = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
				if ( dvTaoTaiSan.LoaiDonVi.TREE_NODE.Contains(ldv_ToChuc.TREE_NODE))
				{
					LoaiTaiSan lts_datSuNghiep = _itemService.GetLoaiTaiSanByMa(enumLoaiTaiSan_Dat.LTS_DAT_SU_NGHIEP);
					lstLoaiTaiSan = lstLoaiTaiSan.Where(c => (c.TREE_NODE != null && !c.TREE_NODE.Contains(lts_datSuNghiep.TREE_NODE)) || c.TREE_NODE == null).
						Where(c => _itemService.GetCountSub(c.ID) ==0).ToList();
                }
                else if(dvTaoTaiSan.LoaiDonVi.TREE_NODE.Contains(ldv_CQNN.TREE_NODE)) // Trường hợp đơn vị cq thì cho thêm 3 loại ts
                {
					var listLoaiTaiSanCQNN = Enum.GetValues(typeof(enumLoaiTaiSan_DatCQNN)).Cast<enumLoaiTaiSan_DatCQNN>().Select(c => ((int)c).ToString());

					LoaiTaiSan lts_datSuNghiep = _itemService.GetLoaiTaiSanByMa(enumLoaiTaiSan_Dat.LTS_DAT_SU_NGHIEP);
					lstLoaiTaiSan = lstLoaiTaiSan.Where(c => (c.TREE_NODE != null && !c.TREE_NODE.Contains(lts_datSuNghiep.TREE_NODE)) || c.TREE_NODE == null|| listLoaiTaiSanCQNN.Contains(c.MA)).
						Where(c => _itemService.GetCountSub(c.ID) == 0).ToList();
				}
				else if (dvTaoTaiSan.LoaiDonVi.TREE_NODE.Contains(ldv_SuNghiep.TREE_NODE))
				{
					LoaiTaiSan lts_datTruSo = _itemService.GetLoaiTaiSanByMa(enumLoaiTaiSan_Dat.LTS_DAT_TRU_SO);
					lstLoaiTaiSan = lstLoaiTaiSan.Where(c => (c.TREE_NODE != null && !c.TREE_NODE.Contains(lts_datTruSo.TREE_NODE)) || c.TREE_NODE == null).ToList();
				}
				//var loaiDonViSuNghiep = _loaiDonViService.GetLoaiDonViByMa(enumLoaiTa)
			}
			//select mặc định của các loại hình tài sản nếu k có valSelected
			if (valSelected == 0 || valSelected == null)
				if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
					valSelected = (int)enumLoaiTaiSanDefault.TS_CO_DINH_DAC_THU;
				else if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)
					valSelected = (int)enumLoaiTaiSanDefault.TS_HUU_HINH_KHAC;
			var selectList = new List<SelectListItem>();
			//lấy hết những loại tài sản có thể được chọn
			if (isAll)
			{
				selectList = lstLoaiTaiSan.Where(c => (_itemService.GetCountSub(c.ID) == 0) || c.HM_TY_LE > 0).OrderBy(c => c.MA).Select(c => new SelectListItem
				{
					Text = c.MA + " - 123 " + c.TEN,
					Value = c.ID.ToString(),
					Selected = valSelected == c.ID,
				}).ToList();
			}
			else
			{
				if (isDisabled)
				{
					if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAT || loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
					{
						
						selectList = lstLoaiTaiSan.Select(c =>
						{
							var disabled = _itemService.GetCountSub(c.ID) > 0;
							return new SelectListItem
							{
								Text = c.MA + " - " + c.TEN,
								Value = c.ID.ToString(),
								Selected = valSelected == c.ID,
								Disabled =disabled

							};
						}).ToList();
					}
					else
					{
                        if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA || loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO)
                        {
							// loại hình tài sản toàn đân Ko check theo tỷ lệ hao mòn nữa nên lấy max tree lvl
							if (lstLoaiTaiSan != null && lstLoaiTaiSan.Count() > 0)
							{
								var maxLv = lstLoaiTaiSan.Select(c => c.TREE_LEVEL).Max();

								selectList = lstLoaiTaiSan.OrderBy(c => c.MA).Select(c => {
									var disabled = c.TREE_LEVEL != maxLv;
									return new SelectListItem
									{
										Text = c.MA + " - " + c.TEN,
										Value = c.ID.ToString(),
										Selected = valSelected == c.ID,
										Disabled = disabled
									};
								}).ToList();
							}
							
						}
                        else
                        {
							if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC && !tsQLNTSCD.GetValueOrDefault())
							{
								//selectList = lstLoaiTaiSan.Where(c => c.HM_TY_LE > 0).Where(c => c.MA != enumLoaiTaiSan_Khac.TS_DE_HONG_DE_VO).OrderBy(c => c.MA).Select(c => new SelectListItem
								//{
								//	Text = c.MA + " - " + c.TEN,
								//	Value = c.ID.ToString(),
								//	Selected = valSelected == c.ID,
								//}).ToList();

								selectList = lstLoaiTaiSan.OrderBy(c => c.MA)//.Where(c => c.MA != enumLoaiTaiSan_Khac.TS_DE_HONG_DE_VO)
									.Select(c => {
										var disabled = (_itemService.GetCountSub(c.ID) > 0) || !(c.HM_TY_LE > 0);
										return  new SelectListItem
										{
											Text = c.MA + " - " + c.TEN,
											Value = c.ID.ToString(),
											Selected = valSelected == c.ID,
											Disabled=disabled
										};
									}).ToList();
							}
							else if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC && tsQLNTSCD.GetValueOrDefault())
							{
								//selectList = lstLoaiTaiSan.Where(c => c.HM_TY_LE > 0).OrderBy(c => c.MA).Select(c => new SelectListItem
								//{
								//	Text = c.MA + " - " + (_nhanhienthiService.GetGiaTri(String.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)) != String.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).ToLower() ?
								//	_nhanhienthiService.GetGiaTri(string.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)) : c.TEN),
								//	Value = c.ID.ToString(),
								//	Selected = valSelected == c.ID,
								//}).ToList();
								selectList = lstLoaiTaiSan.OrderBy(c => c.MA).Select(c => {
									var disabled = (_itemService.GetCountSub(c.ID) > 0) || !(c.HM_TY_LE > 0);
									return new SelectListItem
									{
										Text = c.MA + " - " + (_nhanhienthiService.GetGiaTri(String.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)) != String.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).ToLower() ?
									 _nhanhienthiService.GetGiaTri(string.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)) : c.TEN),
										Value = c.ID.ToString(),
										Selected = valSelected == c.ID,
										Disabled = disabled
									};
								}).ToList();
							}
							else
							{
								//                        selectList = lstLoaiTaiSan.Where(c => c.HM_TY_LE > 0).OrderBy(c => c.MA).Select(c => new SelectListItem
								//                        {
								//                            Text = c.MA + " - " + c.TEN,
								//                            Value = c.ID.ToString(),
								//                            Selected = valSelected == c.ID,
								//	Disabled = true
								//}).ToList();
								selectList = lstLoaiTaiSan.Select(c =>
								{
									var disabled = (_itemService.GetCountSub(c.ID) > 0) || !(c.HM_TY_LE > 0);
									return new SelectListItem
									{
										Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.MA + " - " + c.TEN,
										Value = c.ID.ToString(),
										Selected = valSelected == c.ID,
										Disabled = disabled
									};
								}).ToList();

							}

						}
						
					}
				}
				else
				{
					selectList = lstLoaiTaiSan.Select(c => new SelectListItem
					{
						Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.MA + " - " + c.TEN,
						Value = c.ID.ToString(),
						Selected = valSelected == c.ID,
					}).ToList();
				}
			}

			if (isAddFirst)
				selectList.AddFirstRow(strFirstRow, valueFirstRow);

			return selectList;
		}
		public IList<SelectListItem> PrepareSelectListTaiSanDatNha(decimal? valSelected = 0,
			decimal? cheDoId = 0, bool isAddFirst = false, string strFirstRow = null,
			decimal? loaiHinhTaiSanId = 0, string valueFirstRow = "", bool isDisabled = true,
			int[] listLoaiHinhTaiSanId = null, decimal? ParentLoaiTaiSanId = null,
			bool? isCreateOrEditTaiSan = false, decimal? donViId = 0, bool isAll = false, List<decimal> listLoaiHinhTaiSanIgnore = null, bool? tsQLNTSCD = false)
		{
			if (string.IsNullOrEmpty(strFirstRow))
			{
				switch (loaiHinhTaiSanId)
				{
					case (int)enumLOAI_HINH_TAI_SAN.DAT:
						strFirstRow = "-- Chọn mục đích sử dụng --";
						break;
					case (int)enumLOAI_HINH_TAI_SAN.NHA:
						strFirstRow = "-- Chọn cấp nhà --";
						break;
					case (int)enumLOAI_HINH_TAI_SAN.OTO:
						strFirstRow = "-- Chọn loại xe --";
						break;
					default:
						strFirstRow = "-- Chọn loại tài sản --";
						break;
				}
			}
			string tree = "";
			var lstLoaiTaiSan = _itemService.GetListLoaiTaiSanDatNhaTreeNodeByRoot(cheDoHaoMonId: cheDoId, LoaiHinhTaiSanId: loaiHinhTaiSanId, ListLoaiHinhTaiSanId: listLoaiHinhTaiSanId, ParentLoaiTaiSanId: ParentLoaiTaiSanId, ListIgnoreLTS: listLoaiHinhTaiSanIgnore);
			//Nếu là form tăng mới tài sản thì lọc loại tài sản đất theo loại hình đơn vị
			if (isCreateOrEditTaiSan.Value && loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAT)
			{
				LoaiDonVi ldv_CQNN = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_CO_QUAN_NHA_NUOC);
				LoaiDonVi ldv_ToChuc = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_TO_CHUC);
				LoaiDonVi ldv_SuNghiep = _loaiDonViService.GetLoaiDonViByMa(enumLoaiDonVi.LDV_SU_NGHIEP);
				DonVi dvTaoTaiSan = new DonVi();
				if (donViId > 0)
					dvTaoTaiSan = _donViService.GetDonViById(donViId.Value);
				else
					dvTaoTaiSan = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
				if (dvTaoTaiSan.LoaiDonVi.TREE_NODE.Contains(ldv_ToChuc.TREE_NODE))
				{
					LoaiTaiSan lts_datSuNghiep = _itemService.GetLoaiTaiSanByMa(enumLoaiTaiSan_Dat.LTS_DAT_SU_NGHIEP);
					lstLoaiTaiSan = lstLoaiTaiSan.Where(c => (c.TREE_NODE != null && !c.TREE_NODE.Contains(lts_datSuNghiep.TREE_NODE)) || c.TREE_NODE == null).
						Where(c => _itemService.GetCountSub(c.ID) == 0).ToList();
				}
				else if (dvTaoTaiSan.LoaiDonVi.TREE_NODE.Contains(ldv_CQNN.TREE_NODE)) // Trường hợp đơn vị cq thì cho thêm 3 loại ts
				{
					var listLoaiTaiSanCQNN = Enum.GetValues(typeof(enumLoaiTaiSan_DatCQNN)).Cast<enumLoaiTaiSan_DatCQNN>().Select(c => ((int)c).ToString());

					LoaiTaiSan lts_datSuNghiep = _itemService.GetLoaiTaiSanByMa(enumLoaiTaiSan_Dat.LTS_DAT_SU_NGHIEP);
					lstLoaiTaiSan = lstLoaiTaiSan.Where(c => (c.TREE_NODE != null && !c.TREE_NODE.Contains(lts_datSuNghiep.TREE_NODE)) || c.TREE_NODE == null || listLoaiTaiSanCQNN.Contains(c.MA)).
						Where(c => _itemService.GetCountSub(c.ID) == 0).ToList();
				}
				else if (dvTaoTaiSan.LoaiDonVi.TREE_NODE.Contains(ldv_SuNghiep.TREE_NODE))
				{
					LoaiTaiSan lts_datTruSo = _itemService.GetLoaiTaiSanByMa(enumLoaiTaiSan_Dat.LTS_DAT_TRU_SO);
					lstLoaiTaiSan = lstLoaiTaiSan.Where(c => (c.TREE_NODE != null && !c.TREE_NODE.Contains(lts_datTruSo.TREE_NODE)) || c.TREE_NODE == null).ToList();
				}
				//var loaiDonViSuNghiep = _loaiDonViService.GetLoaiDonViByMa(enumLoaiTa)
			}
			//select mặc định của các loại hình tài sản nếu k có valSelected
			if (valSelected == 0 || valSelected == null)
				if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
					valSelected = (int)enumLoaiTaiSanDefault.TS_CO_DINH_DAC_THU;
				else if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)
					valSelected = (int)enumLoaiTaiSanDefault.TS_HUU_HINH_KHAC;
			var selectList = new List<SelectListItem>();
			//lấy hết những loại tài sản có thể được chọn
			if (isAll)
			{
				selectList = lstLoaiTaiSan.Where(c => (_itemService.GetCountSub(c.ID) == 0) || c.HM_TY_LE > 0).OrderBy(c => c.MA).Select(c => new SelectListItem
				{
					Text = c.MA + " - 123 " + c.TEN,
					Value = c.ID.ToString(),
					Selected = valSelected == c.ID,
				}).ToList();
			}
			else
			{
				if (isDisabled)
				{
					if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAT || loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
					{

						selectList = lstLoaiTaiSan.Select(c =>
						{
							var disabled = _itemService.GetCountSub(c.ID) > 0;
							return new SelectListItem
							{
								Text = c.MA + " - " + c.TEN,
								Value = c.ID.ToString(),
								Selected = valSelected == c.ID,
								Disabled = disabled

							};
						}).ToList();
					}
					else
					{
						if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA || loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO)
						{
							// loại hình tài sản toàn đân Ko check theo tỷ lệ hao mòn nữa nên lấy max tree lvl
							if (lstLoaiTaiSan != null && lstLoaiTaiSan.Count() > 0)
							{
								var maxLv = lstLoaiTaiSan.Select(c => c.TREE_LEVEL).Max();

								selectList = lstLoaiTaiSan.OrderBy(c => c.MA).Select(c => {
									var disabled = c.TREE_LEVEL != maxLv;
									return new SelectListItem
									{
										Text = c.MA + " - " + c.TEN,
										Value = c.ID.ToString(),
										Selected = valSelected == c.ID,
										Disabled = disabled
									};
								}).ToList();
							}

						}
						else
						{
							if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC && !tsQLNTSCD.GetValueOrDefault())
							{
								selectList = lstLoaiTaiSan.Where(c => c.MA != enumLoaiTaiSan_Khac.TS_DE_HONG_DE_VO).OrderBy(c => c.MA)
									.Select(c => {
										var disabled = (_itemService.GetCountSub(c.ID) > 0) || !(c.HM_TY_LE > 0);
										return new SelectListItem
										{
											Text = c.MA + " - " + c.TEN,
											Value = c.ID.ToString(),
											Selected = valSelected == c.ID,
											Disabled = disabled
										};
									}).ToList();
							}
							else if (loaiHinhTaiSanId == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC && tsQLNTSCD.GetValueOrDefault())
							{
								selectList = lstLoaiTaiSan.OrderBy(c => c.MA).Select(c => {
									var disabled = (_itemService.GetCountSub(c.ID) > 0) || !(c.HM_TY_LE > 0);
									return new SelectListItem
									{
										Text = c.MA + " - " + (_nhanhienthiService.GetGiaTri(String.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)) != String.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC).ToLower() ?
									 _nhanhienthiService.GetGiaTri(string.Format("TS-{0}-{1}", c.MA, enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)) : c.TEN),
										Value = c.ID.ToString(),
										Selected = valSelected == c.ID,
										Disabled = disabled
									};
								}).ToList();
							}
							else
							{
								//                        selectList = lstLoaiTaiSan.Where(c => c.HM_TY_LE > 0).OrderBy(c => c.MA).Select(c => new SelectListItem
								//                        {
								//                            Text = c.MA + " - " + c.TEN,
								//                            Value = c.ID.ToString(),
								//                            Selected = valSelected == c.ID,
								//	Disabled = true
								//}).ToList();
								selectList = lstLoaiTaiSan.Select(c =>
								{
									var disabled = (_itemService.GetCountSub(c.ID) > 0) || !(c.HM_TY_LE > 0);
									return new SelectListItem
									{
										Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.MA + " - " + c.TEN,
										Value = c.ID.ToString(),
										Selected = valSelected == c.ID,
										Disabled = disabled
									};
								}).ToList();

							}

						}

					}
				}
				else
				{
					selectList = lstLoaiTaiSan.Select(c => new SelectListItem
					{
						Text = tree.PadLeft((c.TREE_LEVEL.ToNumberInt32() - 1) * 5, '-') + c.MA + " - " + c.TEN,
						Value = c.ID.ToString(),
						Selected = valSelected == c.ID,
					}).ToList();
				}
			}

			if (isAddFirst)
				selectList.AddFirstRow(strFirstRow, valueFirstRow);

			return selectList;
		}
		public List<LoaiHinhTaiSanModel> PrepareListLoaiHinhTaiSanModel()
		{
			int[] valueExclude = new int[] {(int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, 
										   (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC,(int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD };
			var models = new List<LoaiHinhTaiSanModel>();
			foreach (enumLOAI_HINH_TAI_SAN _enum in (enumLOAI_HINH_TAI_SAN[])Enum.GetValues(typeof(enumLOAI_HINH_TAI_SAN)))
			{
				if ((int)_enum > 0 && !valueExclude.Contains((int)_enum))
				{
					var model = new LoaiHinhTaiSanModel();
					model.Id = (int)_enum;
					model.Name = _nhanhienthiService.GetGiaTriEnum(_enum);
					models.Add(model);
				}
			}
			return models;
		}
		public List<LoaiHinhTaiSanModel> PrepareListLoaiHinhTaiSanModelForSelect()
		{
			int[] valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC };
			var models = new List<LoaiHinhTaiSanModel>();
			foreach (enumLOAI_HINH_TAI_SAN _enum in (enumLOAI_HINH_TAI_SAN[])Enum.GetValues(typeof(enumLOAI_HINH_TAI_SAN)))
			{
				if ((int)_enum > 0 && !valueExclude.Contains((int)_enum))
				{
					var model = new LoaiHinhTaiSanModel();
					model.Id = (int)_enum;
					model.Name = _nhanhienthiService.GetGiaTriEnum(_enum);
					models.Add(model);
				}
			}
			return models;
		}
		public IList<SelectListItem> PrepareSelectlistListLoaiHinhTaiSan(string FirstRow = "-- Chọn loại tài sản --", bool isExcutedTSDT = false)
		{
			var ListLHTS = new List<SelectListItem>() { };
			int[] valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO,
											(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC, (int)enumLOAI_HINH_TAI_SAN.TSCD_KHAC,(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_QUAN_LY_NHU_TSCD };
            if (isExcutedTSDT)
            {
				valueExclude = valueExclude.Append((int)enumLOAI_HINH_TAI_SAN.DAC_THU).ToArray();

			}
			foreach (enumLOAI_HINH_TAI_SAN _enum in (enumLOAI_HINH_TAI_SAN[])Enum.GetValues(typeof(enumLOAI_HINH_TAI_SAN)))
			{
				if ((int)_enum > 0 && !valueExclude.Contains((int)_enum))
				{
					var lhts = new SelectListItem
					{
						Text = _nhanhienthiService.GetGiaTriEnum(_enum),
						Value = ((int)_enum).ToString(),
						Selected = false,
					};
					ListLHTS.Add(lhts);
				}
			}
			ListLHTS.AddFirstRow(TextDisplay: FirstRow);
			return ListLHTS;
		}
		public List<LoaiHinhTaiSanModel> PrepareListLoaiHinhTaiSanDinhMucModel()
		{
			int[] valueExclude = new int[] { (int)enumLOAI_HINH_TAI_SAN.OTO,(int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI};
			var models = new List<LoaiHinhTaiSanModel>();
			foreach (enumLOAI_HINH_TAI_SAN _enum in valueExclude)
			{
				if ((int)_enum > 0 )
				{
					var model = new LoaiHinhTaiSanModel();
					model.Id = (int)_enum;
					model.Name = _nhanhienthiService.GetGiaTriEnum(_enum);
					models.Add(model);
				}
			}
			return models;
		}
		public String PrepareTenTaiSanByListId(string stringID)
		{
			var tenListTS = "Tất cả";
			if (!string.IsNullOrWhiteSpace(stringID))
			{
				tenListTS = "";
				var listID = stringID.Split(',').Select(c => c.ToNumber()).ToArray();
				tenListTS = string.Join(',', _itemService.GetLoaiTaiSanByIds(listID).Select(c => c.TEN).ToList());
			}
			return tenListTS;
		}
		#endregion
	}
}

