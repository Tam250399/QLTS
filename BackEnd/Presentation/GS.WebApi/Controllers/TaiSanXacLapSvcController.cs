using GS.Core;
using GS.Core.Configuration;
using GS.Core.Domain.Api;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Services.BienDongs;
using GS.Services.DanhMuc;
using GS.Services.DB;
using GS.Services.NghiepVu;
using GS.Services.SHTD;
using GS.Services.TaiSans;
using GS.Web.Framework.Kendoui;
using GS.WebApi.Factories;
using GS.WebApi.Factories.Common;
using GS.WebApi.Factories.Common.ConsumingApi;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models;
using GS.WebApi.Models.TaiSanXacLap;
using GS.WebApi.Models.TaiSan;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using GS.WebApi.Factories.ConsumingApi;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Common;
using GS.Services.HeThong;
using GS.Services.Common;

namespace GS.WebApi.Controllers
{
    public class TaiSanXacLapSvcController : BaseAdminController
    {
        #region Ctor
        private readonly ITaiSanSvcFactory _taiSanSvcFactory;
        private readonly GSConfig _gSConfig;
        private readonly IKho_TaiSanToanDanFactory  _kho_TaiSanToanDanFactory;
        private readonly IHoatDongService _hoatDongService;
        private readonly IGSAPIService _gSAPIService;
        private readonly ICommonFactory _commonFactory;
        public TaiSanXacLapSvcController(
            ITaiSanSvcFactory taiSanSvcFactory,
            GSConfig gSConfig,
			IKho_TaiSanToanDanFactory kho_TaiSanToanDanFactory,
			IHoatDongService hoatDongService,
			IGSAPIService gSAPIService,
			ICommonFactory commonFactory
			)
        {
            this._taiSanSvcFactory = taiSanSvcFactory;
            this._gSConfig = gSConfig;
			this._kho_TaiSanToanDanFactory = kho_TaiSanToanDanFactory;
			this._hoatDongService = hoatDongService;
			this._gSAPIService = gSAPIService;
			this._commonFactory = commonFactory;
        }
        #endregion

        #region Method
        [HttpPost]
        public virtual IActionResult UpdateListQuyetDinhTichThu([FromBody]List<TaiSanDBModel> model)
        {
            var result = new MessageReturn();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult UpdateListTaiSanToanDan([FromBody]List<QuyetDinhTichThuModel> model)
        {
            var result = new MessageReturn();
            return Ok(result);
        }
		[HttpPost]
		public async Task<IActionResult> UpdateQuyetDinhTichThu([FromBody]List<QuyetDinhTichThu> ListQuyetDinhTichThu)
		{
			if (ListQuyetDinhTichThu == null)
				return Ok(new MessageReturn(MessageReturn.ERROR_CODE, "Dữ liệu không đúng"));
			var Kho_QDTichThu =  _kho_TaiSanToanDanFactory.PrepareDuLieuDongBoQuyetDinhTichThu(ListQuyetDinhTichThu);
			ResponseApi responseApi = await PostDuLieu(CommonTSXacLap.QuyenDinh, Kho_QDTichThu, "QuyetDinhTichThu");
			//if (responseApi.Status)
			//{
			//	foreach (var item in ListQuyetDinhTichThu)
			//	{
			//		Kho_DonVi kho_DonVi = await GetIdDongBo<Kho_DonVi>(CommonDanhMuc.DonVi, item.ID.ToString());
			//		if (kho_DonVi != null)
			//		{
			//			DonVi donVi = _donViService.GetDonViById(item.ID);
			//			donVi.DB_ID = (int?)kho_DonVi.id;
			//			_donViService.UpdateDonVi(donVi);
			//			_hoatDongService.InsertHoatDong(enumHoatDong.DbThanhCong, "Đồng bộ thành công danh mục đơn vị");
			//		}
			//	}
			//}
			return Ok(responseApi);
		}
		#endregion
		async Task<ResponseApi> PostDuLieu(string action, Object obj, string TenDanhMuc)
		{
			string _action =  action + CommonAction.DongBo;
			_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DbCho, $"Gửi dữ liệu Đồng bộ Tài sản xác lập {TenDanhMuc}", 0, TenDanhMuc, obj);
			var response = await _gSAPIService.PostObjectGSApi<ResponseApi>(_action, obj, _commonFactory.GetTokenKhoCSDLQG());
			InsertLogModel model = new InsertLogModel()
			{
				ResponseApi = response,
				Data = obj,
				LoaiDuLieu = "Tài sản toàn dân"
			};
			if (response != null && response.Status)
			{
				_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.DaGuiDuLieu, "Đã Gửi dữ liệu đồng bộ tài sản toàn dân", 0, TenDanhMuc, model);
				return response;
			}
			else
			{
				_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.GuiDuLieuLoi, "Đã Gửi dữ liệu đồng bộ tài sản toàn dân bị lỗi", 0, TenDanhMuc, model);
				return response;
			}
		}
	}
}