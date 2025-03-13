using GS.Core;
using GS.Core.Domain.SHTD;
using GS.Services;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Services.SHTD;
using GS.WebApi.Models.ConsumingApi.TaiSanToanDan;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.WebApi.Factories.ConsumingApi
{
    public class Kho_TaiSanToanDanFactory : IKho_TaiSanToanDanFactory
    {
        private readonly IQuyetDinhTichThuService _quyetDinhTichThuService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IDonViService _donViService;
        private readonly INguonGocTaiSanService _nguonGocTaiSanService;
        private readonly INhanXeService _nhanXeService;
        private readonly IDongXeService _dongXeService;
        private readonly ITaiSanTdService _taiSanTdService;
        private readonly ITaiSanTdXuLyService _taiSanTdXuLyService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        private readonly IPhuongAnXuLyService _phuongAnXuLyService;
        private readonly IXuLyService _xuLyService;
        private readonly IHinhThucXuLyService _hinhThucXuLyService;

        public Kho_TaiSanToanDanFactory(IQuyetDinhTichThuService quyetDinhTichThuService,
            INguoiDungService nguoiDungService,
            IDonViService donViService,
            INguonGocTaiSanService nguonGocTaiSanService,
            INhanXeService nhanXeService,
            IDongXeService dongXeService,
            ITaiSanTdService taiSanTdService,
            ITaiSanTdXuLyService taiSanTdXuLyService,
            ILoaiTaiSanService loaiTaiSanService,
            IPhuongAnXuLyService phuongAnXuLyService,
            IXuLyService xuLyService,
            IHinhThucXuLyService hinhThucXuLyService)
        {
            this._quyetDinhTichThuService = quyetDinhTichThuService;
            this._nguoiDungService = nguoiDungService;
            this._donViService = donViService;
            this._nguonGocTaiSanService = nguonGocTaiSanService;
            this._nhanXeService = nhanXeService;
            this._dongXeService = dongXeService;
            this._taiSanTdService = taiSanTdService;
            this._taiSanTdXuLyService = taiSanTdXuLyService;
            this._loaiTaiSanService = loaiTaiSanService;
            this._phuongAnXuLyService = phuongAnXuLyService;
            this._xuLyService = xuLyService;
            this._hinhThucXuLyService = hinhThucXuLyService;
        }

        #region Dữ liệu đồng bộ quyết định tịch thu

        //public DuLieuDongBoQuyetDinhTichThu PrepareDuLieuDongBoQuyetDinhTichThu(List<QuyetDinhTichThu> ListQuyetDinhTichThu)
        //{
        //	//dữ liệu chuyển đi
        //	DuLieuDongBoQuyetDinhTichThu duLieu = new DuLieuDongBoQuyetDinhTichThu();
        //	//dữ liệu trong app
        //	foreach (var item in ListQuyetDinhTichThu)
        //	{
        //		//
        //		Kho_QuyetDinhTichThu quyetDinhTichThu = new Kho_QuyetDinhTichThu();
        //		// quyết định tịch thu
        //		confiscateDecision confiscateDecision = PrepareConfiscateDecisionByQuyetDinhTichThu(item);
        //		if (confiscateDecision == null)
        //			continue;
        //		quyetDinhTichThu.decision = confiscateDecision;
        //		var taiSanTDs = _taiSanTdService.GetTaiSanTdByQuyetDinhId(item.ID);
        //		if (taiSanTDs != null && taiSanTDs.Count > 0)
        //		{
        //			foreach (var tstd in taiSanTDs)
        //			{
        //				var nationalPublicAssets = PrepareNationalPublicAssetsByTaiSanTD(tstd);
        //				if (nationalPublicAssets == null)
        //					continue;
        //				quyetDinhTichThu.assets.Add(nationalPublicAssets);
        //			}
        //		}
        //		duLieu.data.Add(quyetDinhTichThu);
        //	}
        //	return duLieu;
        //}
        public List<confiscateDecision> PrepareDuLieuDongBoQuyetDinhTichThu(List<QuyetDinhTichThu> ListQuyetDinhTichThu)
        {
            var duLieu = new List<confiscateDecision>();
            if (ListQuyetDinhTichThu != null)
            {
                //dữ liệu trong app
                foreach (var item in ListQuyetDinhTichThu)
                {
                    if (item != null)
                    {
                        var cD = new confiscateDecision();
                        cD = PrepareConfiscateDecisionByQuyetDinhTichThu(item);
                        duLieu.Add(cD);
                    }

                }
            }

            return duLieu;
        }
        public confiscateDecision PrepareConfiscateDecisionByQuyetDinhTichThu(QuyetDinhTichThu quyetDinhTichThu)
        {
            if (quyetDinhTichThu == null)
                return null;
            confiscateDecision confiscateDecision = new confiscateDecision();
            confiscateDecision.syncId = quyetDinhTichThu.ID.ToString();
            confiscateDecision.assetSourceId = _nguonGocTaiSanService.GetNguonGocTaiSanById(quyetDinhTichThu.NGUON_GOC_ID ?? 0)?.DB_ID ?? 0;
            confiscateDecision.decisionNumber = quyetDinhTichThu.QUYET_DINH_SO;
            confiscateDecision.decisionDate = CommonHelper.toDateKhoString(quyetDinhTichThu.QUYET_DINH_NGAY);
            confiscateDecision.notes = quyetDinhTichThu.GHI_CHU;
            confiscateDecision.createdDate = CommonHelper.toDateKhoString(quyetDinhTichThu.NGAY_TAO ?? DateTime.Now);
            //tên người tạo
            //var nguoiDung = _nguoiDungService.GetNguoiDungById(quyetDinhTichThu.NGUOI_TAO_ID);
            // để tạm Admin do bị lệch ID người tạo
            confiscateDecision.creator = "Admin";
            //id đơn vị tạo
            confiscateDecision.unitId = quyetDinhTichThu.DonVi?.DB_ID;
            confiscateDecision.decisionType = GetDecisionType(quyetDinhTichThu.NGUON_GOC_ID);
            confiscateDecision.decider = quyetDinhTichThu.NGUOI_QUYET_DINH;
            confiscateDecision.decisionName = quyetDinhTichThu.TEN;
            //tên cơ quan ban hành
            var donVi = _donViService.GetDonViById(quyetDinhTichThu.CO_QUAN_BAN_HANH_ID.GetValueOrDefault());
            confiscateDecision.issuedUnitId = donVi?.DB_ID;
            return confiscateDecision;
        }
        #endregion Dữ liệu đồng bộ quyết định tịch thu

        #region Dữ liệu đồng bộ tài sản toàn dân
        public List<nationalPublicAssets> PrepareDuLieuDongBoTaiSanTd(List<TaiSanTd> ListTaiSanTd)
        {
            var duLieu = new List<nationalPublicAssets>();
            //dữ liệu trong app
            if (ListTaiSanTd != null)
            {
                foreach (var item in ListTaiSanTd)
                {
                    if (item != null)
                    {
                        var nP = new nationalPublicAssets();
                        nP = PrepareNationalPublicAssetsByTaiSanTD(item);
                        duLieu.Add(nP);
                    }

                }
            }

            return duLieu;
        }
        public nationalPublicAssets PrepareNationalPublicAssetsByTaiSanTD(TaiSanTd taiSanTd)
        {
            if (taiSanTd == null)
                return null;
            nationalPublicAssets nationalPublicAssets = new nationalPublicAssets();
            nationalPublicAssets.syncId = taiSanTd.ID.ToString();
            //quyết định đồng bộ
            var quyetDinh = _quyetDinhTichThuService.GetQuyetDinhTichThuById(taiSanTd.QUYET_DINH_TICH_THU_ID ?? 0);
            nationalPublicAssets.decisionId = Convert.ToInt32((quyetDinh?.DB_ID) ?? "0");

            nationalPublicAssets.assetName = taiSanTd.TEN;
            nationalPublicAssets.assetTypeId = (int?)taiSanTd.loaitaisan?.DB_ID;
            nationalPublicAssets.assetTypeGroupId = (int)((taiSanTd.LOAI_HINH_TAI_SAN_ID ?? 0).toLoaiHinhTaiSanTDKho());
            nationalPublicAssets.assetValue = (double?)(taiSanTd.GIA_TRI_XAC_LAP ?? 0);
            nationalPublicAssets.calculationUnitValue = (double?)taiSanTd.GIA_TRI_TINH;
            nationalPublicAssets.calculationUnitType = taiSanTd.DON_VI_TINH;
            nationalPublicAssets.houseLandId = (long?)(int.Parse(_taiSanTdService.GetTaiSanTdById(taiSanTd.TAI_SAN_DAT_ID ?? 0)?.DB_ID ?? "0"));
            if (nationalPublicAssets.houseLandId == 0)
            {
                nationalPublicAssets.houseLandId = null;
            }
            nationalPublicAssets.houseAddress = taiSanTd.DIA_CHI;
            if (taiSanTd.NGAY_SU_DUNG.HasValue)
                nationalPublicAssets.houseBuiltYear = taiSanTd.NGAY_SU_DUNG.Value.Year;
            if (taiSanTd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.DAT || taiSanTd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.NHA)
                nationalPublicAssets.houseAreaBuilding = (double?)taiSanTd.GIA_TRI_TINH;

            //nhãn xe
            var nhanXe = _nhanXeService.GetNhanXeById(taiSanTd.NHAN_XE_ID.GetValueOrDefault());
            nationalPublicAssets.brandId = (long?)nhanXe?.DB_ID;
            //dòng xe
            //var dongXe = _dongXeService.GetDongXeById(taiSanTd.);
            //nationalPublicAssets.productLineId
            nationalPublicAssets.vehicleRegistrationPlateNumber = taiSanTd.BIEN_KIEM_SOAT;
            nationalPublicAssets.vehicleSize = (int?)taiSanTd.SO_CHO_NGOI;
            nationalPublicAssets.vehicleCapacity = (double?)taiSanTd.TAI_TRONG;
            nationalPublicAssets.vehicleNumberOfWheelDrives = (int?)taiSanTd.SO_CAU_XE;
            nationalPublicAssets.notes = taiSanTd.GHI_CHU;
            if (taiSanTd.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN_TOAN_DAN.TAI_SAN_KHAC)
            {
                nationalPublicAssets.assetTypeName = taiSanTd.TEN_LOAI_TAI_SAN ?? taiSanTd.TEN;
            }
            else
            {
                nationalPublicAssets.assetTypeName = _loaiTaiSanService.GetLoaiTaiSanById(taiSanTd.LOAI_TAI_SAN_ID ?? 0)?.TEN;

            }
            nationalPublicAssets.assetTypeName = nationalPublicAssets.assetTypeName ?? "";
            return nationalPublicAssets;
        }
        #endregion Dữ liệu đồng bộ tài sản toàn dân

        public int GetDecisionType(decimal? nguonGocId)
        {
            var isTaiSanTichThu = _nguonGocTaiSanService.checkIsTaiSanTichThu(nguonGocId);
            if (isTaiSanTichThu)
                return 1;
            else
                return 2;
        }

        #region Dữ liệu đồng bộ phương án xử lý
        //public DuLieuDongBoPhuongAnXuLy PrepareDuLieuDongBoPhuongAnXuLy(List<XuLy> ListXuLy)
        //{
        //	DuLieuDongBoPhuongAnXuLy duLieu = new DuLieuDongBoPhuongAnXuLy();
        //	foreach (var xuLy in ListXuLy)
        //	{
        //		Kho_PhuongAnXuLy kho_PhuongAnXuLy = new Kho_PhuongAnXuLy();

        //		//nếu có kết quả thì sẽ k có phương án xử lý
        //		///
        //		///
        //		handlingDecisionPlan handlingDecisionPlan = PrepareHandlingDecisionPlanByXuLy(xuLy);
        //		if (handlingDecisionPlan== null)
        //			continue;
        //		kho_PhuongAnXuLy.handlingPlan = handlingDecisionPlan;

        //		//get tài sản xử lý
        //		var taiSanXuLys = _taiSanTdXuLyService.GetTaiSanTdsXuLyByXuLyId(xuLy.ID);
        //		if (taiSanXuLys!= null && taiSanXuLys.Count>0)
        //		{
        //			foreach (var tsxl in taiSanXuLys)
        //			{
        //				var handlingPlanNationalPublicAsset = PrepareHandlingPlanNationalPublicAssetFromTaiSanTdXuLy(tsxl);
        //				if (handlingPlanNationalPublicAsset == null)
        //					continue;
        //				kho_PhuongAnXuLy.assets.Add(handlingPlanNationalPublicAsset);
        //			}
        //		}

        //		duLieu.data.Add(kho_PhuongAnXuLy);
        //	}
        //	return duLieu;
        //}
        public List<handlingDecisionPlan> PrepareDuLieuDongBoPhuongAnXuLy(List<XuLy> ListXuLy)
        {
            var duLieu = new List<handlingDecisionPlan>();
            //dữ liệu trong app
            foreach (var item in ListXuLy)
            {
                var hD = new handlingDecisionPlan();
                hD = PrepareHandlingDecisionPlanByXuLy(item);
                duLieu.Add(hD);
            }
            return duLieu;
        }
        public handlingDecisionPlan PrepareHandlingDecisionPlanByXuLy(XuLy xuLy)
        {
            if (xuLy == null)
                return null;
            handlingDecisionPlan handlingDecisionPlan = new handlingDecisionPlan();
            handlingDecisionPlan.syncId = ((int)xuLy.ID).ToString();
            handlingDecisionPlan.decisionNumber = xuLy.QUYET_DINH_SO;
            handlingDecisionPlan.decisionDate = CommonHelper.toDateKhoString(xuLy.QUYET_DINH_NGAY);
            handlingDecisionPlan.decider = xuLy.NGUOI_QUYET_DINH;
            handlingDecisionPlan.createdDate = CommonHelper.toDateKhoString(xuLy.NGAY_TAO);
            var donViDeXuat = _donViService.GetDonViById(xuLy.DON_VI_ID.Value);
            handlingDecisionPlan.unitId = donViDeXuat.DB_ID.GetValueOrDefault();
            var donViCoQuan = _donViService.GetDonViById(xuLy.CO_QUAN_BAN_HANH_ID.GetValueOrDefault());
            if (donViCoQuan != null)
                handlingDecisionPlan.issuedUnitId = donViCoQuan.DB_ID;
            handlingDecisionPlan.notes = xuLy.GHI_CHU;

            return handlingDecisionPlan;
        }


        #endregion Dữ liệu đồng bộ phương án xử lý

        #region Dữ liệu đồng bộ tài sản theo phương án xử lý
        public List<handlingPlanNationalPublicAsset> PrepareDuLieuDongBoTaiSanPhuongAnXuLy(List<TaiSanTdXuLy> ListTaiSanTdXuLy)
        {
            var duLieu = new List<handlingPlanNationalPublicAsset>();
            //dữ liệu trong app
            foreach (var item in ListTaiSanTdXuLy)
            {
                var hP = new handlingPlanNationalPublicAsset();
                hP = PrepareHandlingPlanNationalPublicAssetFromTaiSanTdXuLy(item);
                duLieu.Add(hP);
            }
            return duLieu;
        }
        public handlingPlanNationalPublicAsset PrepareHandlingPlanNationalPublicAssetFromTaiSanTdXuLy(TaiSanTdXuLy taiSanTdXuLy)
        {
            if (taiSanTdXuLy == null)
                return null;
            handlingPlanNationalPublicAsset handlingPlanNationalPublicAsset = new handlingPlanNationalPublicAsset();
            handlingPlanNationalPublicAsset.syncId = taiSanTdXuLy.ID.ToString();
            var taiSanTD = _taiSanTdService.GetTaiSanTdById(taiSanTdXuLy.TAI_SAN_ID);
            var xuLy = _xuLyService.GetXuLyById(taiSanTdXuLy.XU_LY_ID);
            handlingPlanNationalPublicAsset.assetId = (taiSanTD?.DB_ID ?? "0").ToNumberInt32();
            handlingPlanNationalPublicAsset.planId = (xuLy?.DB_ID).ToNumberInt32();
            handlingPlanNationalPublicAsset.handlingFormId = (int?)taiSanTdXuLy.hinhthucxuly?.DB_ID;
            var phuongThucXuLy = _phuongAnXuLyService.GetPhuongAnXuLyById(taiSanTdXuLy.PHUONG_AN_XU_LY_ID.GetValueOrDefault());
            if (phuongThucXuLy != null)
                handlingPlanNationalPublicAsset.handlingMethodId = (int?)phuongThucXuLy.DB_ID;
            handlingPlanNationalPublicAsset.handlingCalculationUnitValue = (double?)taiSanTdXuLy.SO_LUONG;
            handlingPlanNationalPublicAsset.handlingCalculationUnitType = taiSanTD?.DON_VI_TINH;
            return handlingPlanNationalPublicAsset;
        }
        #endregion Dữ liệu đồng bộ tài sản theo phương án xử lý

        #region Dữ liệu đồng bộ kết quả xử lý
        //public DuLieuDongBoKetQuaXuLy PrepareDuLieuDongBoKetQuaXuLy(List<KetQua> ListKetQua)
        //{
        //	DuLieuDongBoKetQuaXuLy duLieu = new DuLieuDongBoKetQuaXuLy();
        //	foreach (var ketQua in ListKetQua)
        //	{
        //		Kho_KetQuaXuLy kho_KetQuaXuLy = new Kho_KetQuaXuLy();

        //		handlingDecisionResult handlingDecisionResult = PrepareHandlingDecisionResultFromKetQua(ketQua);
        //		if (handlingDecisionResult == null)
        //			continue;
        //		kho_KetQuaXuLy.handlingResult = handlingDecisionResult;
        //		handlingResultNationalPublicAsset handlingResultNationalPublicAsset = PrepareHandlingResultNationalPublicAssetFromKetQua(ketQua);
        //		if (handlingResultNationalPublicAsset!= null)
        //			kho_KetQuaXuLy.assets.Add(handlingResultNationalPublicAsset);


        //		duLieu.data.Add(kho_KetQuaXuLy);
        //	}
        //	return duLieu;
        //}
        public List<handlingResultNationalPublicAsset> PrepareDuLieuDongBoKetQuaXuLy(List<KetQua> ListKetQua)
        {
            var duLieu = new List<handlingResultNationalPublicAsset>();
            //dữ liệu trong app
            foreach (var item in ListKetQua)
            {
                var hD = new handlingResultNationalPublicAsset();
                hD = PrepareHandlingResultNationalPublicAssetFromKetQua(item);
                duLieu.Add(hD);
            }
            return duLieu;
        }
        public handlingDecisionResult PrepareHandlingDecisionResultFromKetQua(KetQua ketQua)
        {
            if (ketQua == null)
                return null;
            var handlingDecisionResult = new handlingDecisionResult();
            handlingDecisionResult.syncId = ketQua.ID.ToString();
            handlingDecisionResult.planId = (ketQua.taiSanTdXuLy.xuly.DB_ID.ToNumberInt32());
            handlingDecisionResult.decisionNumber = ketQua.taiSanTdXuLy.xuly.QUYET_DINH_SO;
            handlingDecisionResult.decisionDate = CommonHelper.toDateKhoString(ketQua.taiSanTdXuLy.xuly.QUYET_DINH_NGAY);
            handlingDecisionResult.createdDate = CommonHelper.toDateKhoString(ketQua.taiSanTdXuLy.xuly.NGAY_TAO);
            var donViDeXuat = _donViService.GetDonViById(ketQua.taiSanTdXuLy.xuly.DON_VI_ID.Value);
            handlingDecisionResult.unitId = donViDeXuat.DB_ID.GetValueOrDefault();
            var donViCoQuan = _donViService.GetDonViById(ketQua.taiSanTdXuLy.xuly.CO_QUAN_BAN_HANH_ID.GetValueOrDefault());
            if (donViCoQuan != null)
                handlingDecisionResult.issuedUnitName = donViCoQuan.TEN;
            handlingDecisionResult.notes = ketQua.taiSanTdXuLy.xuly.GHI_CHU;

            return handlingDecisionResult;
        }
        public handlingResultNationalPublicAsset PrepareHandlingResultNationalPublicAssetFromKetQua(KetQua ketQua)
        {
            if (ketQua == null)
                return null;
            handlingResultNationalPublicAsset handlingResultNationalPublicAsset = new handlingResultNationalPublicAsset();
            var taiSanTDXuLy = _taiSanTdXuLyService.GetTaiSanTdXuLyById(ketQua.TAI_SAN_TD_XU_LY_ID);
            var taiSanTD = _taiSanTdService.GetTaiSanTdById(taiSanTDXuLy?.TAI_SAN_ID ?? 0);
            var hinhThucXuLy = _hinhThucXuLyService.GetHinhThucXuLyById(taiSanTDXuLy?.HINH_THUC_XU_LY_ID ?? 0);
            var xuLy = _xuLyService.GetXuLyById(taiSanTDXuLy?.XU_LY_ID ?? 0);
            //handlingResultNationalPublicAsset.syncId = ((int)(taiSanTDXuLy?.ID ?? 0)).ToString();
            handlingResultNationalPublicAsset.syncId = ((int)ketQua.ID).ToString();
            handlingResultNationalPublicAsset.planId = int.Parse(xuLy.DB_ID ?? "0");
            handlingResultNationalPublicAsset.planAssetId = (taiSanTDXuLy?.DB_ID ?? "0").ToNumberInt32();
            handlingResultNationalPublicAsset.handlingFormId = (int)(hinhThucXuLy?.DB_ID ?? 0);
            var phuongThucXuLy = _phuongAnXuLyService.GetPhuongAnXuLyById(taiSanTDXuLy?.PHUONG_AN_XU_LY_ID ?? 0);
            //if (phuongThucXuLy != null)
            //	handlingResultNationalPublicAsset.handlingMethodId = (int)phuongThucXuLy.DB_ID;
            handlingResultNationalPublicAsset.handlingCalculationUnitValue = (double)(ketQua.SO_LUONG ?? 0);
            handlingResultNationalPublicAsset.handlingCalculationUnitType = taiSanTD?.DON_VI_TINH;
            //handlingResultNationalPublicAsset.handlingDate = CommonHelper.toDateKhoString(ketQua.NGAY_XU_LY);
            ////chỉ có ở giao/ chuyển giao CQCN/giao cho DM
            //if (true)
            //{
            //	var donViChuyen = _donViService.GetDonViById(ketQua.DON_VI_CHUYEN_ID.GetValueOrDefault());
            //	handlingResultNationalPublicAsset.transferUnitId = donViChuyen?.DB_ID;
            //	handlingResultNationalPublicAsset.transferDate = CommonHelper.toDateKhoString(ketQua.NGAY_XU_LY);
            //	handlingResultNationalPublicAsset.transferUnitName = donViChuyen.TEN;
            //}
            ////chỉ có ở bán
            //if (true)
            //{
            //	handlingResultNationalPublicAsset.contractNumber = ketQua.HOP_DONG_SO;
            //	handlingResultNationalPublicAsset.contractDate = CommonHelper.toDateKhoString(ketQua.HOP_DONG_NGAY);
            //	handlingResultNationalPublicAsset.documentNumber = null;
            //	handlingResultNationalPublicAsset.documentDate = null;
            //}
            //handlingResultNationalPublicAsset.contractPartner = null;
            //handlingResultNationalPublicAsset.assetValue = (long?)ketQua.GIA_TRI_BAN;
            ////chỉ có ở bán
            //handlingResultNationalPublicAsset.valueObtained = (long?)ketQua.GIA_TRI_BAN;
            ////chỉ có ở nộp nsnn
            //handlingResultNationalPublicAsset.valueToBudget = (long?)ketQua.GIA_TRI_NSNN;
            ////chỉ có ở bán
            //handlingResultNationalPublicAsset.valueInCustody = (long?)ketQua.GIA_TRI_TKTG;
            //handlingResultNationalPublicAsset.otherDocuments = ketQua.HO_SO_GIAY_TO_KHAC;
            //handlingResultNationalPublicAsset.notes = ketQua.taiSanTdXuLy.GHI_CHU;

            return handlingResultNationalPublicAsset;
        }
        #endregion

        #region Dữ liệu đồng bộ sổ sách thu chi
        public List<handlingDecisionAccounting> PrepareDuLieuDongBoSoSachThuChi(List<ThuChi> ListThuChi)
        {
            //DuLieuDongBoSoSachThuChi duLieu = new DuLieuDongBoSoSachThuChi();
            //foreach (var thuChi in ListThuChi)
            //{
            //	Kho_SoSachThuChi kho_SoSachThuChi = new Kho_SoSachThuChi();

            //	handlingDecisionAccounting handlingDecisionAccounting = PreparehandlingDecisionAccountingFromThuChi(thuChi);
            //	if (handlingDecisionAccounting == null)
            //		continue;
            //	kho_SoSachThuChi.handlingAccounting = handlingDecisionAccounting;

            //	duLieu.data.Add(kho_SoSachThuChi);
            //}
            var duLieu = new List<handlingDecisionAccounting>();
            //dữ liệu trong app
            foreach (var item in ListThuChi)
            {
                var hD = new handlingDecisionAccounting();
                hD = PreparehandlingDecisionAccountingFromThuChi(item);
                duLieu.Add(hD);
            }
            return duLieu;
        }
        public handlingDecisionAccounting PreparehandlingDecisionAccountingFromThuChi(ThuChi thuChi)
        {
            if (thuChi == null)
                return null;
            var handlingDecisionAccounting = new handlingDecisionAccounting();
            handlingDecisionAccounting.syncId = ((int)thuChi.ID).ToString();
            var xuLyIds = thuChi.LIST_XU_LY_ID.ToListInt().Select(c =>
            {
                decimal d = c;
                return d;
            }).ToList();
            var xuLys = _xuLyService.GetXuLyByIds(xuLyIds);
            var xuLyDBIds = new List<int>();
            if (xuLys != null && xuLys.Count() > 0)
            {
                xuLyDBIds = xuLys.Select(c => c.DB_ID.ToNumberInt32()).ToList();
            }
            //kết quả xử lý
            handlingDecisionAccounting.planId = xuLyDBIds;
            handlingDecisionAccounting.valueRemaining = (long?)(thuChi.SO_TIEN_PHAI_THU ?? 0 - thuChi.SO_TIEN_THU ?? 0);
            handlingDecisionAccounting.valueEstimated = (long)(thuChi.SO_TIEN_PHAI_THU ?? 0);
            handlingDecisionAccounting.valueObtained = (long)(thuChi.SO_TIEN_THU ?? 0);
            handlingDecisionAccounting.obtainedDate = CommonHelper.toDateKhoString(thuChi.NGAY_THU);
            handlingDecisionAccounting.valueInCustody = (long?)thuChi.TG_SO_TIEN;
            handlingDecisionAccounting.custodyDate = CommonHelper.toDateKhoString(thuChi.TG_NGAY_NOP);
            handlingDecisionAccounting.handlingCost = (long)(thuChi.CHI_PHI ?? 0);
            handlingDecisionAccounting.createdDate = CommonHelper.toDateKhoString(thuChi.TG_NGAY_NOP);
            // đơn vị 
            var donvi = _donViService.GetDonViById((decimal)thuChi.xuLy.DON_VI_ID);
            handlingDecisionAccounting.unitId = donvi.DB_ID.ToNumberInt32();
            return handlingDecisionAccounting;
        }
        #endregion
    }
}