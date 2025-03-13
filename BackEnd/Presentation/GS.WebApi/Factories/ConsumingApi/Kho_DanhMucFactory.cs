using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Services.DanhMuc;
using GS.WebApi.Models.ConsumingApi;
using GS.WebApi.Models.ConsumingApi.DanhMucApi;
using GS.WebApi.Models.DanhMuc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GS.WebApi.Factories.Common.ConsumingApi
{
    public class Kho_DanhMucFactory : IKho_DanhMucFactory
    {
        private readonly IDiaBanService _diaBanService;
        private readonly ILoaiTaiSanService _loaiTaiSanService;
        public Kho_DanhMucFactory(
            IDiaBanService diaBanService,
            ILoaiTaiSanService loaiTaiSanService
            )
        {
            this._diaBanService = diaBanService;
            this._loaiTaiSanService = loaiTaiSanService;
        }
        public IList<QuocGiaModel> PrepareQuocGiaModel(List<Kho_QuocGia> kho_QuocGia)
        {
            List<QuocGiaModel> ListQuocGiaModel = new List<QuocGiaModel>();
            foreach (var item in kho_QuocGia)
            {
                QuocGiaModel model = new QuocGiaModel();
                model.MA = item.code;
                model.TEN = item.name;
                model.MO_TA = item.description;
                ListQuocGiaModel.Add(model);
            }
            return ListQuocGiaModel;
        }
        public IList<LyDoBienDongModel> PrepareLyDoBienDongModel(List<Kho_LyDoBienDong> kho_LyDoBienDong)
        {
            List<LyDoBienDongModel> ListLyDoBienDongModel = new List<LyDoBienDongModel>();
            foreach (var item in kho_LyDoBienDong)
            {
                LyDoBienDongModel model = new LyDoBienDongModel();
                model.MA = item.code;
                model.TEN = item.name;
                model.ID = Convert.ToInt64(item.syncId);
                //model.NHOM_TAI_SAN_MA = item.assetTypeCode;
                ListLyDoBienDongModel.Add(model);
            }
            return ListLyDoBienDongModel;
        }
        public IList<DiaBanModel> PrepareDiaBanModel(List<Kho_DiaBan> kho_DiaBan)
        {
            List<DiaBanModel> ListDiaBanModel = new List<DiaBanModel>();
            foreach (var item in kho_DiaBan)
            {
                DiaBanModel model = new DiaBanModel();

                model.TEN = item.name;
                //model.TRANG_THAI_ID = item.isActive;
                ListDiaBanModel.Add(model);
            }
            return ListDiaBanModel;
        }
        public IList<LoaiDonViModel> PrepareLoaiDonViModel(List<Kho_LoaiDonVi> kho_LoaiDonVi)
        {
            List<LoaiDonViModel> ListLoaiDonViModel = new List<LoaiDonViModel>();
            //foreach (var item in kho_LoaiDonVi)
            //{
            //    LoaiDonViModel model = new LoaiDonViModel();
            //    model.MA = item.code;
            //    model.TEN = item.name;
            //    model.MA_CHA = item.parentId;
            //    model.CHE_DO_HOACH_TOAN_ID = item.accountingStandard;
            //    model.ID =long.Parse(item.syncId);
            //    ListLoaiDonViModel.Add(model);
            //}
            return ListLoaiDonViModel;
        }
        public IList<DonViModel> PrepareDonViModel(List<Kho_DonVi> kho_DonVi)
        {
            List<DonViModel> ListDonViModel = new List<DonViModel>();
            foreach (var item in kho_DonVi)
            {
                DonViModel model = new DonViModel();
                // model.ID = item.syncId;
                model.MA = item.code;
                model.TEN = item.name;
                // model.MA_CHA = item.parentCode;
                model.CHE_DO_HACH_TOAN_ID = item.accountingStandard;
                model.DIA_CHI = item.address;
                model.DIEN_THOAI = item.phoneNumber;
                model.FAX = item.fax;
                model.MA_DVQHNS = item.nationalBudgetCode;
                model.TRANG_THAI_ID = item.isActive;
                // model.MA_CAP_DON_VI = item.unitLevelCode;
                // model.MA_DIA_BAN = item.regionCode;
                //  model.MA_LOAI_DON_VI = item.unitTypeCode;
                ListDonViModel.Add(model);
            }
            return ListDonViModel;
        }
        public IList<LoaiTaiSanModel> PrepareLoaiTaiSanModel(List<Kho_LoaiTaiSan> kho_LoaiTaiSan)
        {
            List<LoaiTaiSanModel> ListLoaiTaiSanModel = new List<LoaiTaiSanModel>();
            foreach (var item in kho_LoaiTaiSan)
            {
                //LoaiTaiSanModel model = new LoaiTaiSanModel();
                //model.ID = item.syncId;
                //model.MA = item.code;
                //model.TEN = item.name;
                ////model.MA_CHA = item.parentCode;
                //model.MO_TA = item.description;
                //model.MA_NHOM_TAI_SAN = item.assetType;
                //ListLoaiTaiSanModel.Add(model);
            }
            return ListLoaiTaiSanModel;
        }
        public IList<MucDichSuDungModel> PrepareMucDichSuDungModel(List<Kho_MucDichSuDung> kho_MucDichSuDung)
        {
            List<MucDichSuDungModel> ListMucDichSuDungModel = new List<MucDichSuDungModel>();
            foreach (var item in kho_MucDichSuDung)
            {
                //MucDichSuDungModel model = new MucDichSuDungModel();
                //model.MA = item.code;
                //model.TEN = item.name;
                //model.LOAI_HINH_TAI_SAN_ID=0;
                //model.ID = item.syncId;
                //ListMucDichSuDungModel.Add(model);
            }
            return ListMucDichSuDungModel;
        }
        public IList<NguonGocTaiSanModel> PrepareNguonGocTaiSanModel(List<Kho_NguonGocTaiSan> kho_NguonGocTaiSan)
        {
            List<NguonGocTaiSanModel> ListNguonGocTaiSanModel = new List<NguonGocTaiSanModel>();
            foreach (var item in kho_NguonGocTaiSan)
            {
                NguonGocTaiSanModel model = new NguonGocTaiSanModel();
                model.MA = item.code;
                model.TEN = item.name;
                //model.MO_TA = item.description;
                ListNguonGocTaiSanModel.Add(model);
            }
            return ListNguonGocTaiSanModel;
        }
        public IList<HienTrangModel> PrepareHienTrangModel(List<Kho_HienTrang> kho_HienTrang)
        {
            List<HienTrangModel> ListHienTrangModel = new List<HienTrangModel>();
            foreach (var item in kho_HienTrang)
            {
                //HienTrangModel model = new HienTrangModel();
                //model.ID = item.syncId;
                //model.TEN_HIEN_TRANG = item.name;
                //model.LOAI_HINH_TAI_SAN_ID = 0;
                ////model.MO_TA = item.description;
                //ListHienTrangModel.Add(model);
            }
            return ListHienTrangModel;
        }
        public IList<NguoiDungModel> PrepareNguoiDungModel(List<Kho_NguoiDung> kho_NguoiDung)
        {
            List<NguoiDungModel> ListNguoiDungModel = new List<NguoiDungModel>();
            foreach (var item in kho_NguoiDung)
            {
                NguoiDungModel model = new NguoiDungModel();
                model.TEN_DANG_NHAP = item.username;
                model.TEN_DAY_DU = item.fullName;
                model.MAT_KHAU_KEY = item.passwordHash;
                model.MOBILE = item.phoneNumber;
                model.IS_QUAN_TRI = item.isAdministrator;
                model.TRANG_THAI_ID = item.lockoutEnabled == true ? 2 : 1;
                // model.LIST_DON_VI = item.listUnitCode;
                ListNguoiDungModel.Add(model);
            }
            return ListNguoiDungModel;
        }
        public MessageReturn PrepareMessageReturrn(string strReturnApi)
        {
            if (string.IsNullOrEmpty(strReturnApi))
                return null;
            else
            {
                ResponseApi result = new ResponseApi();
                MessageReturn messageReturn = new MessageReturn();
                result = JsonConvert.DeserializeObject<ResponseApi>(strReturnApi);
                if (result.Status)
                {
                    messageReturn.Code = MessageReturn.SUCCESS_CODE;
                }
                else
                {
                    messageReturn.Code = MessageReturn.ERROR_CODE;
                }
                messageReturn.Message = result.Message;
                messageReturn.ObjectInfo = result;
                return messageReturn;
            }
        }
        public int? GetIdNhomTaiSanKhoByLoaiHinhTaiSan(decimal LoaiHinhTaiSanID)
        {
            int? assetTypeId = null;
            if (LoaiHinhTaiSanID > 0)
            {
                var loaiTaiSan = _loaiTaiSanService.GetLoaiTaiSanByTreeLevel(1).Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSanID && c.CHE_DO_HAO_MON_ID == 23).FirstOrDefault();
                assetTypeId = (int?)loaiTaiSan.DB_ID;
            }
            else
            {
                assetTypeId = (int)enumIdNhomTaiSanKho.CoQuanToChuc;
            }
            return assetTypeId;
        }
        public int GetMapLoaiHinhTaiSanKho(int LoaiHinhTaiSan)
        {
            int value = 0;
            switch (LoaiHinhTaiSan)
            {
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC:
                    {
                        value = 20;
                        break;
                    }
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT:
                    {
                        value = 17;
                        break;
                    }
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA:
                    {
                        value = 18;
                        break;
                    }
                case (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO:
                    {
                        value = 19;
                        break;
                    }
                case (int)enumLOAI_HINH_TAI_SAN.OTO:
                    {
                        value = 19;
                        break;
                    }
                default:
                    break;
            }
            return value;
        }      
    }
}
