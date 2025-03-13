using GS.Core;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.TaiSans;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.DanhMuc;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.TaiSans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Factories.TaiSans
{
	public class CapNhatTaiSanModelFactory:ICapNhatTaiSanModelFactory
	{
        private readonly ITaiSanService _taiSanService;
        private readonly IWorkContext _workContext;
        private readonly ILoaiTaiSanDonViServices _loaiTaiSanDonViServices;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
        private readonly IDonViService _donviService;
		public CapNhatTaiSanModelFactory(ITaiSanService taiSanService,
            IWorkContext workContext,
            ILoaiTaiSanDonViServices loaiTaiSanDonViServices,
            ILoaiTaiSanService loaiTaiSanService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            IDonViService donviService)
		{
            this._taiSanService = taiSanService;
            this._workContext = workContext;
            this._loaiTaiSanDonViServices = loaiTaiSanDonViServices;
            this._loaiTaiSanService = loaiTaiSanService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._donviService = donviService;
        }
        

        public TaiSanSearchModel PrepareCapNhatTaiSanSearchModel(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.LoaiTaiSanAvailable = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(loaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.OTO, ParentLoaiTaiSanId: (int)enumLoaiTaiSanOto.CHUYEN_DUNG, isAddFirst: true);
            searchModel.SetGridPageSize();
            return searchModel;
        }
        //cap nhat danh muc dia ban
        public TaiSanSearchModel PrepareCapNhatTaiSanDatNhaSearchModel(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.LoaiTaiSanAvailable = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(loaiHinhTaiSanId: (int)enumLOAI_HINH_TAI_SAN.DAT, isAddFirst: true);
            searchModel.SetGridPageSize();
            return searchModel;
        }
        public TaiSanListModel PrepareCapNhatTaiSanListModel(TaiSanSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _taiSanService.GetTaiSanCanhBao(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, DonViId: _workContext.CurrentDonVi.ID, MA: searchModel.MA_CAU_HINH_CANH_BAO_TS, KeySearch:searchModel.KeySearch);
            //var items = _taiSanService.GetTaiSanOtoChuyenDung(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize,Keysearch:searchModel.KeySearch,donViId: _workContext.CurrentDonVi.ID);

            //prepare list model
            var model = new TaiSanListModel
            {
                //fill in model values from the entity
                //Data = items.Select(c => c.ToModel<CapNhatTaiSanModel>()),
                Data = items.Select(c => {
                    var m = c.ToModel<TaiSanModel>();
                    if (c.LOAI_TAI_SAN_ID > 0 || c.LOAI_TAI_SAN_DON_VI_ID > 0)
                    {
                        var loaitaisan = new LoaiTaiSanModel();
                        if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && m.LOAI_TAI_SAN_DON_VI_ID > 0)
                            loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(m.LOAI_TAI_SAN_DON_VI_ID ?? m.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                        else
                            loaitaisan = _loaiTaiSanService.GetLoaiTaiSanById(m.LOAI_TAI_SAN_ID ?? 0).ToModel<LoaiTaiSanModel>();
                        m.TenLoaiTaiSan = loaitaisan.TEN;
                    }
                    return m;
                }),
                Total = items.TotalCount

            };
            return model;
        }
        public List<TaiSanExportOto> PrepareExportTaiSanOto(TaiSanSearchModel searchModel)
        {
            List<TaiSanExportOto> rs = new List<TaiSanExportOto>();
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.MA_CAU_HINH_CANH_BAO_TS = "CAPNHAT_LOAITS";
            var items = _taiSanService.GetTaiSanCanhBao(pageIndex: searchModel.Page - 1, pageSize: 0, DonViId: _workContext.CurrentDonVi.ID, MA: searchModel.MA_CAU_HINH_CANH_BAO_TS, KeySearch: searchModel.KeySearch);

            //prepare list model
            rs = items.Select(c =>
            {
                var m = new TaiSanExportOto()
                {
                    TEN = c.TEN,
                    MA = c.MA
                };
                if (c.DON_VI_ID > 0)
                {
                    var donVi = _donviService.GetDonViById(c.DON_VI_ID ?? 0);
                    if (donVi != null)
                    {
                        m.DON_VI_SU_DUNG = donVi.TEN;
                    }
                }
                if (c.LOAI_TAI_SAN_ID > 0 || c.LOAI_TAI_SAN_DON_VI_ID > 0)
                {
                    var loaitaisan = new LoaiTaiSanModel();
                    if (c.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH && c.LOAI_TAI_SAN_DON_VI_ID > 0)
                        loaitaisan = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(c.LOAI_TAI_SAN_DON_VI_ID ?? c.LOAI_TAI_SAN_ID.Value).ToModel<LoaiTaiSanModel>();
                    else
                        loaitaisan = _loaiTaiSanService.GetLoaiTaiSanById(c.LOAI_TAI_SAN_ID ?? 0).ToModel<LoaiTaiSanModel>();
                    m.LOAI_TAI_SAN = loaitaisan.TEN;
                }
                return m;
            }).ToList();
            return rs;
        }
    }
}
