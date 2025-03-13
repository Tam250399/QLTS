//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Domain.Api.BaoCao;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.BaoCaos;
using GS.Web.Factories.DanhMuc;
using GS.Web.Framework.Controllers;
using GS.Web.Framework.Mvc.Filters;
using GS.Web.Framework.Security;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.DanhMuc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GS.Web.Controllers
{
	[HttpsRequirement(SslRequirement.No)]
	public partial class BaoCaoController : BaseWorksController
	{
		#region Fields

		private readonly IHoatDongService _hoatdongService;
		private readonly INhanHienThiService _nhanHienThiService;
		private readonly IQuyenService _quyenService;
		private readonly IWorkContext _workContext;
		private readonly CauHinhChung _cauhinhChung;
		private readonly IBaoCaoModelFactory _itemModelFactory;
		private readonly IBaoCaoService _itemService;
		private readonly ICauHinhService _cauHinhService;
		private readonly IReportModelFactory _reportModelFactory;

		#endregion Fields

		#region Ctor

		public BaoCaoController(
			IHoatDongService hoatdongService,
			INhanHienThiService nhanHienThiService,
			IQuyenService quyenService,
			IWorkContext workContext,
			CauHinhChung cauhinhChung,
			IBaoCaoModelFactory itemModelFactory,
			IBaoCaoService itemService,
			ICauHinhService cauHinhService,
			IReportModelFactory reportModelFactory
			)
		{
			this._hoatdongService = hoatdongService;
			this._nhanHienThiService = nhanHienThiService;
			this._quyenService = quyenService;
			this._workContext = workContext;
			this._cauhinhChung = cauhinhChung;
			this._itemModelFactory = itemModelFactory;
			this._itemService = itemService;
			this._cauHinhService = cauHinhService;
			this._reportModelFactory = reportModelFactory;
		}

		#endregion Ctor

		#region Methods

		public virtual IActionResult List()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new BaoCaoSearchModel();
			var model = _itemModelFactory.PrepareBaoCaoSearchModel(searchmodel);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult List(BaoCaoSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedKendoGridJson();
			//prepare model
			var model = _itemModelFactory.PrepareBaoCaoListModel(searchModel);
			return Json(model);
		}

		public virtual IActionResult Create()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();
			//prepare model
			var model = _itemModelFactory.PrepareBaoCaoModel(new BaoCaoModel(), null);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		public virtual IActionResult Create(BaoCaoModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var item = model.ToEntity<BaoCao>();
				_itemService.InsertBaoCao(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", item.ToModel<BaoCaoModel>(), "BaoCao");
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}

			//prepare model
			model = _itemModelFactory.PrepareBaoCaoModel(model, null);
			return View(model);
		}

		public virtual IActionResult Edit(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();

			var item = _itemService.GetBaoCaoById(id);
			if (item == null)
				return RedirectToAction("List");
			//prepare model
			var model = _itemModelFactory.PrepareBaoCaoModel(null, item);
			return View(model);
		}

		[HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
		[FormValueRequired("save", "save-continue")]
		public virtual IActionResult Edit(BaoCaoModel model, bool continueEditing)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetBaoCaoById(model.ID);
			if (item == null)
				return RedirectToAction("List");
			if (ModelState.IsValid)
			{
				_itemModelFactory.PrepareBaoCao(model, item);
				_itemService.UpdateBaoCao(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", item.ToModel<BaoCaoModel>(), "BaoCao");
				SuccessNotification("Cập nhật dữ liệu thành công !");
				return continueEditing ? RedirectToAction("Edit", new { id = item.ID }) : RedirectToAction("List");
			}
			//prepare model
			model = _itemModelFactory.PrepareBaoCaoModel(model, item, true);
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult Delete(int id)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERQLHoSo))
				return AccessDeniedView();
			//try to get a store with the specified id
			var item = _itemService.GetBaoCaoById(id);
			if (item == null)
				return RedirectToAction("List");
			try
			{
				_itemService.DeleteBaoCao(item);
				_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa dữ liệu ", item.ToModel<BaoCaoModel>(), "BaoCao");
				//activity log
				SuccessNotification("Xoá dữ liệu thành công");
				return RedirectToAction("List");
			}
			catch (Exception exc)
			{
				ErrorNotification(exc);
				return RedirectToAction("Edit", new { id = item.ID });
			}
		}

		#endregion Methods

		#region CauHinhBaoCao

		public virtual IActionResult ListCauHinh()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new CauHinhBaoCaoSearchModel();
			return View(searchmodel);
		}

		[HttpPost]
		public virtual IActionResult ListCauHinh(CauHinhBaoCaoSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedKendoGridJson();
			var listItem = new List<CauHinhBaoCaoModel>();
			var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID).BaoCao;
			if (cauHinh != null)
			{
				listItem = cauHinh.toEntities<CauHinhBaoCaoModel>();
				if (searchModel.KeySearch != null && searchModel.KeySearch != String.Empty)
					listItem = cauHinh.toEntities<CauHinhBaoCaoModel>().FindAll(c => c.MaBaoCao.ToUpper().Contains(searchModel.KeySearch.ToUpper()) ||
						(!String.IsNullOrEmpty(c.TenBaoCao) && c.TenBaoCao.ToUpper().Contains(searchModel.KeySearch.ToUpper())));
				else listItem = cauHinh.toEntities<CauHinhBaoCaoModel>();
			}
			else
			{
				var donViCauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>();
				var listCauHinh = donViCauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>();
				foreach (var mabc in _cauHinhService.LoadCauHinh<DonViCauHinh>().BaoCao.toEntities<CauHinhBaoCaoModel>().Select(c => c.MaBaoCao).Distinct())
				{
					var ch = new CauHinhBaoCaoModel
					{
						MaBaoCao = mabc
					};
					listCauHinh.Add(ch);
				}
				donViCauHinh.BaoCao = listCauHinh.toStringJson();
				_cauHinhService.SaveCauHinh(donViCauHinh, DON_VI_ID: _workContext.CurrentDonVi.ID);
				listItem = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID).BaoCao.toEntities<CauHinhBaoCaoModel>();
			}
			var items = new PagedList<CauHinhBaoCaoModel>(listItem, searchModel.Page - 1, searchModel.PageSize);
			var model = new CauHinhBaoCaoListModel
			{
				//fill in model values from the entity
				Data = items,
				Total = items.TotalCount
			};
			return Json(model);
		}
		public virtual IActionResult EditCauHinhChung(string maBaoCao)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();
			var cauHinh = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
			var model = cauHinh.BaoCao.toEntities<CauHinhBaoCaoChungModel>().FirstOrDefault();
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult EditCauHinhChung(CauHinhBaoCaoChungModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();
			var donViCauHinh = _cauHinhService.LoadCauHinh<CauHinhBaoCaoChung>(DON_VI_ID: 0);
			var listCauHinh = donViCauHinh.BaoCao.toEntities<CauHinhBaoCaoChungModel>();
			listCauHinh.RemoveAt(listCauHinh.IndexOf(listCauHinh.FirstOrDefault()));
			listCauHinh.Add(model);
			donViCauHinh.BaoCao = listCauHinh.toStringJson();
			_cauHinhService.SaveCauHinh(donViCauHinh, DON_VI_ID: 0);
			_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", model, "CauHinhBaoCao");
			SuccessNotification("Cập nhật dữ liệu thành công !");
			return RedirectToAction("ListCauHinh");
		}
		public virtual IActionResult CreateCauHinh()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();
			//prepare model
			var model = new CauHinhBaoCaoModel();
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult CreateCauHinh(CauHinhBaoCaoModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();

			if (ModelState.IsValid)
			{
				var donViCauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
				var listCauHinh = donViCauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>();
				listCauHinh.Add(model);
				donViCauHinh.BaoCao = listCauHinh.toStringJson();
				_cauHinhService.SaveCauHinh(donViCauHinh, DON_VI_ID: _workContext.CurrentDonVi.ID);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", model, "CauHinhBaoCao");
				SuccessNotification("Tạo mới dữ liệu thành công !");
				return RedirectToAction("ListCauHinh");
			}

			//prepare model
			return View(model);
		}

		public virtual IActionResult EditCauHinh(string maBaoCao)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();
			var cauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
			var model = cauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>().Where(c => c.MaBaoCao == maBaoCao).FirstOrDefault();
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult EditCauHinh(CauHinhBaoCaoModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();
			var donViCauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
			var listCauHinh = donViCauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>();
			listCauHinh.RemoveAt(listCauHinh.IndexOf(listCauHinh.Where(c => c.MaBaoCao == model.MaBaoCao).FirstOrDefault()));
			listCauHinh.Add(model);
			donViCauHinh.BaoCao = listCauHinh.toStringJson();
			_cauHinhService.SaveCauHinh(donViCauHinh, DON_VI_ID: _workContext.CurrentDonVi.ID);
			_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", model, "CauHinhBaoCao");
			SuccessNotification("Cập nhật dữ liệu thành công !");
			return RedirectToAction("ListCauHinh");
		}

		[HttpPost]
		public virtual IActionResult DeleteCauHinh(string maBaoCao)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();
			var donViCauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
			var listCauHinh = donViCauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>();
			var model = listCauHinh.Where(c => c.MaBaoCao == maBaoCao).FirstOrDefault();
			listCauHinh.RemoveAt(listCauHinh.IndexOf(model));
			donViCauHinh.BaoCao = listCauHinh.toStringJson();
			_cauHinhService.SaveCauHinh(donViCauHinh, DON_VI_ID: _workContext.CurrentDonVi.ID);
			_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa thông tin ", model, "CauHinhBaoCao");
			SuccessNotification("Xoá dữ liệu thành công");
			return RedirectToAction("ListCauHinh");
		}

		public virtual IActionResult EditAllCauHinh()
		{
			return View(new CauHinhBaoCaoModel());
		}

		[HttpPost]
		public virtual IActionResult EditAllCauHinh(CauHinhBaoCaoModel model)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCao))
				return AccessDeniedView();
			var donViCauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>(DON_VI_ID: _workContext.CurrentDonVi.ID);
			var listCauHinh = donViCauHinh.BaoCao.toEntities<CauHinhBaoCaoModel>();
			if (listCauHinh != null && listCauHinh.Count > 0)
			{
				foreach (var item in listCauHinh)
				{
					item.MarginLeft = model.MarginLeft;

					item.MarginRight = model.MarginRight;

					item.MarginTop = model.MarginTop;

					item.MarginBottom = model.MarginBottom;

					item.ChucDanhNguoiLapBieu = model.ChucDanhNguoiLapBieu;

					item.ChucDanhKeToanTruong = model.ChucDanhKeToanTruong;

					item.ChucDanhThuTruongDonVi = model.ChucDanhThuTruongDonVi;

					item.TenNguoiLapBieu = model.TenNguoiLapBieu;

					item.TenKeToanTruong = model.TenKeToanTruong;

					item.TenThuTruongDonVi = model.TenThuTruongDonVi;

					item.DiaDanhBaoCao = model.DiaDanhBaoCao;

					item.DonViKhaiThac = model.DonViKhaiThac;
				}
				donViCauHinh.BaoCao = listCauHinh.toStringJson();
				_cauHinhService.SaveCauHinh(donViCauHinh, DON_VI_ID: _workContext.CurrentDonVi.ID);
				_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", model, "CauHinhBaoCao");
			}
			SuccessNotification("Cập nhật dữ liệu thành công !");
			return RedirectToAction("ListCauHinh");
		}

		#endregion CauHinhBaoCao
		#region CauHinhBaoCaoDongBo

		public virtual IActionResult ListCauHinhDB()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCaoDB))
				return AccessDeniedView();
			//prepare model
			var searchmodel = new CauHinhBaoCaoSearchModel();
			return View(searchmodel);
		}

		[HttpPost]
		public virtual IActionResult ListCauHinhDB(CauHinhBaoCaoSearchModel searchModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCaoDB))
				return AccessDeniedKendoGridJson();
			var listItem = new List<CauHinhBaoCaoDBModel>();
			var cauHinh = _cauHinhService.LoadCauHinh<CauHinhMapBC>().BaoCao;
			if (cauHinh != null)
			{
				listItem = cauHinh.toEntities<CauHinhBaoCaoDBModel>();
				if (searchModel.KeySearch != null && searchModel.KeySearch != String.Empty)
					listItem = cauHinh.toEntities<CauHinhBaoCaoDBModel>().FindAll(c => c.MaBaoCao.ToUpper().Contains(searchModel.KeySearch.ToUpper()) ||
						(!String.IsNullOrEmpty(c.TenBaoCao) && c.TenBaoCao.ToUpper().Contains(searchModel.KeySearch.ToUpper())));
				else listItem = cauHinh.toEntities<CauHinhBaoCaoDBModel>();
			}
			else
			{
				var donViCauHinh = _cauHinhService.LoadCauHinh<DonViCauHinh>();
				var listCauHinh = donViCauHinh.BaoCao.toEntities<CauHinhBaoCaoDBModel>();
				foreach (var mabc in _cauHinhService.LoadCauHinh<CauHinhMapBC>().BaoCao.toEntities<CauHinhBaoCaoDBModel>().Select(c => c.MaBaoCao).Distinct())
				{
					var ch = new CauHinhBaoCaoDBModel
					{
						MaBaoCao = mabc
					};
					listCauHinh.Add(ch);
				}
				donViCauHinh.BaoCao = listCauHinh.toStringJson();
				_cauHinhService.SaveCauHinh(donViCauHinh, DON_VI_ID: _workContext.CurrentDonVi.ID);
				listItem = _cauHinhService.LoadCauHinh<CauHinhMapBC>(DON_VI_ID: _workContext.CurrentDonVi.ID).BaoCao.toEntities<CauHinhBaoCaoDBModel>();
			}
			var items = new PagedList<CauHinhBaoCaoDBModel>(listItem, searchModel.Page - 1, searchModel.PageSize);
			var model = new CauHinhBaoCaoDBListModel
			{
				//fill in model values from the entity
				Data = items,
				Total = items.TotalCount
			};
			return Json(model);
		}
		public virtual IActionResult CreateCauHinhDB()
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCaoDB))
				return AccessDeniedView();
			//prepare model
			var model = new CauHinhBaoCaoDBModel();
			MapBaoCaoTaiSanDongBoSearch BaoCaoTaiSanDongBoSearch = new MapBaoCaoTaiSanDongBoSearch(); 
			var dictReportDatta = new Dictionary<string, string>();
			Dictionary<string, string> BaoCaoTaiSanDongBoSearchs = JsonConvert.DeserializeObject<Dictionary<string, string>>(BaoCaoTaiSanDongBoSearch.toStringJson());
			foreach (var key in BaoCaoTaiSanDongBoSearchs.Keys.ToList())
				BaoCaoTaiSanDongBoSearchs[key] = null;
			model.Lstbody = BaoCaoTaiSanDongBoSearchs;
			model.LstreportData = dictReportDatta;
			return View(model);
		}

		[HttpPost]
		public virtual IActionResult CreateCauHinhDB(CauHinhBaoCaoDBModel CHBCDBModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCaoDB))
				return AccessDeniedView();
			if (ModelState.IsValid) { 
				var CauHinhBaoCaoDB = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
				var listCauHinh = CauHinhBaoCaoDB.BaoCao.toEntities<CauHinhMapBaoCao>();
				Dictionary<string, string> dictreportData = new Dictionary<string, string>();
				Dictionary<string, string> dictbody = new Dictionary<string, string>();
				foreach (var body in CHBCDBModel.Lstbody)
				{
					if (!string.IsNullOrEmpty(body.Value))
					{
						dictbody.Add(body.Key, body.Value);
					}
				}
				CHBCDBModel.Body = dictbody;
				CauHinhMapBaoCao lsta = new CauHinhMapBaoCao();
				lsta.MaBaoCao = CHBCDBModel.MaBaoCao;
				lsta.TenBaoCao = CHBCDBModel.TenBaoCao;
				lsta.MaBaoCaoKho = CHBCDBModel.MaBaoCaoKho;
				lsta.Model = CHBCDBModel.Model;
				lsta.Body = CHBCDBModel.Body;
				lsta.reportData = dictreportData;
				lsta.ExcecuteStatment = CHBCDBModel.ExcecuteStatment;
				lsta.FullName_ReportClass = CHBCDBModel.FullName_ReportClass;
				listCauHinh.Add(lsta);
				CauHinhBaoCaoDB.BaoCao = listCauHinh.toStringJson();
				_cauHinhService.SaveCauHinh(CauHinhBaoCaoDB);
				_hoatdongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo mới thông tin ", CHBCDBModel, "CauHinhBaoCaoDongBo");
				SuccessNotification("Tạo mới dữ liệu thành công !");
				//return RedirectToAction("EditCauHinhDB", new { maBaoCao = CHBCDBModel.MaBaoCao });
				return JsonSuccessMessage("Cập nhật dữ liệu thành công !", CHBCDBModel);
			}
			//prepare model
			var list = ModelState.Values.Where(c => c.Errors.Count > 0).ToList();
			return JsonErrorMessage("Error", list);
		}
		public virtual IActionResult EditCauHinhDB(string maBaoCao)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCaoDB))
				return AccessDeniedView();
			var cauHinh = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
			var model = cauHinh.BaoCao.toEntities<CauHinhBaoCaoDBModel>().Where(c => c.MaBaoCao == maBaoCao).FirstOrDefault();
			MapBaoCaoTaiSanDongBoSearch BaoCaoTaiSanDongBoSearch = new MapBaoCaoTaiSanDongBoSearch();
			if (model.Body != null) {
				BaoCaoTaiSanDongBoSearch = JsonConvert.DeserializeObject<MapBaoCaoTaiSanDongBoSearch>(model.Body.toStringJson()??"");
			}
			var dictReportDatta = JsonConvert.DeserializeObject<Dictionary<string, string>>(model.reportData.toStringJson());
			string modelTypeName = $"{model.Model},GS.CORE";
			Type searchModelType = Type.GetType(modelTypeName);
			Dictionary<string,string> modelreportData = JsonConvert.DeserializeObject<Dictionary<string, string>>(Activator.CreateInstance(searchModelType).toStringJson());

			//var strreportData = model.reportData.ToString()??"";
			List<string> keys = new List<string>(modelreportData.Keys);
			foreach (var key in modelreportData.Keys.ToList())
				modelreportData[key] = null;
			if (dictReportDatta!= null) {
				foreach (string dictmodelreportData in keys) 
				{
					foreach(var a in dictReportDatta) 
					{  
						if(a.Key == dictmodelreportData)
						{
								modelreportData[dictmodelreportData] = a.Value;
						}
					}
				}
			}
			model.LstreportData = modelreportData;
			model.Lstbody = JsonConvert.DeserializeObject<Dictionary<string, string>>(BaoCaoTaiSanDongBoSearch.toStringJson());
			//model.LstreportData = JsonConvert.DeserializeObject<Dictionary<string, string>>(model.reportData.toStringJson());
			return View(model);
		}
		[HttpPost]
		public virtual IActionResult EditCauHinhDB(CauHinhBaoCaoDBModel CHBCDBModel)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCaoDB))
				return AccessDeniedView();
			//CHBCDBModel.Body = CHBCDBModel.Lstbody.toStringJson();
			Dictionary<string,string> dictbody = new Dictionary<string, string>();
			foreach( var body in CHBCDBModel.Lstbody)
			{
				if(!string.IsNullOrEmpty(body.Value))
				{
					dictbody.Add(body.Key,body.Value);
				}
			}
			CHBCDBModel.Body = dictbody;
			//CHBCDBModel.reportData = CHBCDBModel.LstreportData.toStringJson();
			Dictionary<string, string> dictreportData = new Dictionary<string, string>();
			foreach (var reportData in CHBCDBModel.LstreportData)
			{
				if (!string.IsNullOrEmpty(reportData.Value) && !reportData.Value.Equals("0") )
				{
					dictreportData.Add(reportData.Key, reportData.Value);
				}
			}
			CHBCDBModel.reportData = dictreportData;
			
			var CauHinhBaoCaoDB = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
			//var lsta = CHBCDBModel.ToEntity<CauHinhMapBaoCao>();
			CauHinhMapBaoCao lsta = new CauHinhMapBaoCao() ;
			lsta.MaBaoCao = CHBCDBModel.MaBaoCao;
			lsta.TenBaoCao = CHBCDBModel.TenBaoCao;
			lsta.MaBaoCaoKho = CHBCDBModel.MaBaoCaoKho;
			lsta.Model = CHBCDBModel.Model;
			lsta.Body = CHBCDBModel.Body;
			lsta.reportData = CHBCDBModel.reportData;
			lsta.ExcecuteStatment = CHBCDBModel.ExcecuteStatment;
			lsta.FullName_ReportClass = CHBCDBModel.FullName_ReportClass;
			lsta.FullName_SearchModelClass = CHBCDBModel.FullName_SearchModelClass;
			var listCauHinh = CauHinhBaoCaoDB.BaoCao.toEntities<CauHinhMapBaoCao>();
			listCauHinh.RemoveAt(listCauHinh.IndexOf(listCauHinh.Where(c => c.MaBaoCao == CHBCDBModel.MaBaoCao).FirstOrDefault()));
			listCauHinh.Add(lsta);
			CauHinhBaoCaoDB.BaoCao = listCauHinh.toStringJson();
			_cauHinhService.SaveCauHinh(CauHinhBaoCaoDB);
			_hoatdongService.InsertHoatDong(enumHoatDong.CapNhat, "Cập nhật thông tin ", CHBCDBModel, "CauHinhBaoCaoDongBo");
			SuccessNotification("Cập nhật dữ liệu thành công !");
			return JsonSuccessMessage("Cập nhật dữ liệu thành công !", CHBCDBModel);
		}


		[HttpPost]
		public virtual IActionResult DeleteCauHinhBaoCaoDB(string maBaoCao)
		{
			if (!_quyenService.Authorize(StandardPermissionProvider.USERCauHinhBaoCaoDB))
				return AccessDeniedView();
			var CauHinhBCDB = _cauHinhService.LoadCauHinh<CauHinhMapBC>(DON_VI_ID: _workContext.CurrentDonVi.ID);
			var listCauHinh = CauHinhBCDB.BaoCao.toEntities<CauHinhBaoCaoDBModel>();
			var model = listCauHinh.Where(c => c.MaBaoCao == maBaoCao).FirstOrDefault();
			listCauHinh.RemoveAt(listCauHinh.IndexOf(model));
			CauHinhBCDB.BaoCao = listCauHinh.toStringJson();
			_cauHinhService.SaveCauHinh(CauHinhBCDB);
			_hoatdongService.InsertHoatDong(enumHoatDong.Xoa, "Xóa thông tin ", model, "CauHinhBaoCaoDB");
			SuccessNotification("Xoá dữ liệu thành công");
			return RedirectToAction("ListCauHinhDB");
		}
		#endregion CauHinhBaoCaoDongBo

	}
}