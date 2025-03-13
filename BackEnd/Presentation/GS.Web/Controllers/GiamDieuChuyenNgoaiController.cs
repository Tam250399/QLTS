using GS.Core;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;
using GS.Services.CCDC;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.Security;
using GS.Web.Factories.CCDC;
using GS.Web.Factories.DanhMuc;
using GS.Web.Models.CCDC;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Controllers
{
    public class GiamDieuChuyenNgoaiController : BaseWorksController
    {
        #region Fields
        private readonly IHoatDongService _hoatdongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IQuyenService _quyenService;
        private readonly IWorkContext _workContext;
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICongCuService _congCuService;
        private readonly IXuatNhapService _xuatNhapService;
        private readonly INhapXuatCongCuService _nhapXuatCongCuService;
        private readonly IXuatNhapModelFactory _xuatNhapModelFactory;
        private readonly IKiemKeCongCuModelFactory _kiemKeCongCuModelFactory;
        private readonly IGiamDieuChuyenModelFactory _itemModelFactory;
        private readonly IDonViBoPhanModelFactory _donViBoPhanModelFactory;
        private readonly IDonViService _donViService;
        private readonly IDonViModelFactory _donViModelFactory;
        #endregion

        #region Ctor
        public GiamDieuChuyenNgoaiController(
            IHoatDongService hoatdongService,
            INhanHienThiService nhanHienThiService,
            IQuyenService quyenService,
            IWorkContext workContext,
            CauHinhChung cauhinhChung,
            ICongCuService congCuService,
            IXuatNhapService xuatNhapService,
            INhapXuatCongCuService nhapXuatCongCuService,
            IXuatNhapModelFactory xuatNhapModelFactory,
            IKiemKeCongCuModelFactory kiemKeCongCuModelFactory,
            IGiamDieuChuyenModelFactory itemModelFactory,
            IDonViBoPhanModelFactory donViBoPhanModelFactory,
            IDonViService donViService,
            IDonViModelFactory donViModelFactory
            )
        {
            this._hoatdongService = hoatdongService;
            this._nhanHienThiService = nhanHienThiService;
            this._quyenService = quyenService;
            this._workContext = workContext;
            this._cauhinhChung = cauhinhChung;
            this._congCuService = congCuService;
            this._xuatNhapService = xuatNhapService;
            this._nhapXuatCongCuService = nhapXuatCongCuService;
            this._xuatNhapModelFactory = xuatNhapModelFactory;
            this._kiemKeCongCuModelFactory = kiemKeCongCuModelFactory;
            this._itemModelFactory = itemModelFactory;
            this._donViBoPhanModelFactory = donViBoPhanModelFactory;
            this._donViService = donViService;
            this._donViModelFactory = donViModelFactory;
        }
        #endregion

        #region Methods
        public virtual IActionResult List()
        {
            if (!_quyenService.Authorize(StandardPermissionProvider.QLGiamCongCuDungCu))
                return AccessDeniedView();
            //prepare model
            var searchmodel = new GiamDieuChuyenSearchModel();
            searchmodel.IsDieuChuyenNgoai = true;

            return View(searchmodel);
        }

        [HttpPost]
        public virtual IActionResult List(GiamDieuChuyenSearchModel searchModel)
        {
            searchModel.LoaiDieuChuyen = (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN_NGOAI;
            var model = _itemModelFactory.PrepareGiamDieuChuyenListModel(searchModel);

            return Json(model);
        }
        #endregion
    }
}
