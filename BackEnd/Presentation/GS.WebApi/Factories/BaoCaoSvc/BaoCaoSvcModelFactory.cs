using GS.Core;
using GS.Core.Domain.Api.BaoCao;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.WebApi.Models.BaoCaoSvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GS.WebApi.Factories.BaoCaoSvc
{
    public class BaoCaoSvcModelFactory : IBaoCaoSvcModelFactory
    {
        private readonly IFileCongViecService _fileCongViecService;
        private readonly ICauHinhService _cauHinhService;
        private readonly IDonViService _donViService;

        private string FormatParamName = "={0}=";
        private string FormatParamDateTime = "TO_DATE('{0}','DD/MM/YYYY')";
        private string FormatParamString = "'{0}'";
        public BaoCaoSvcModelFactory(IFileCongViecService fileCongViecService,
            ICauHinhService cauHinhService,
            IDonViService donViService)
        {
            this._fileCongViecService = fileCongViecService;
            this._cauHinhService = cauHinhService;
            this._donViService = donViService;
        }
        public ReportBaseSearchModel PrepareReportBaseSearchModel(ThamSoNhanBaoCaoSvcSearchModel thamSoNhanBaoCao)
        {
            ReportBaseSearchModel model = new ReportBaseSearchModel();
            model.MaBaoCao = thamSoNhanBaoCao.MA_BAO_CAO;
            model.FileType = thamSoNhanBaoCao.LOAI_DATA_TRA_VE;
            model.StringDonVi = string.Join(",", thamSoNhanBaoCao.LIST_DON_VI_ID);
            model.DonVi = thamSoNhanBaoCao.LIST_DON_VI_ID.FirstOrDefault();
            switch ((enumDonViTinhGiaTri)thamSoNhanBaoCao.DON_VI_TINH_GIA_TRI)
            {
                case enumDonViTinhGiaTri.Dong:
                    model.DonViTien = (int)enumDonViTien.Dong;
                    break;
                case enumDonViTinhGiaTri.NghinDong:
                    model.DonViTien = (int)enumDonViTien.NghinDong;
                    break;
                case enumDonViTinhGiaTri.TrieuDong:
                    model.DonViTien = (int)enumDonViTien.TrieuDong;
                    break;
                case enumDonViTinhGiaTri.TyDong:
                    model.DonViTien = (int)enumDonViTien.TyDong;
                    break;
                default:
                    break;
            }
            switch ((enumDonViDienTichRequest)thamSoNhanBaoCao.DON_VI_TINH_GIA_TRI)
            {
                case enumDonViDienTichRequest.MetVuong:
                    model.DonViDienTich = (int)enumDonViDienTich.MetVuong;
                    model.DonViDienTichDat = (int)enumDonViDienTich.MetVuong;
                    model.DonViDienTichNha = (int)enumDonViDienTich.MetVuong;
                    break;
                case enumDonViDienTichRequest.NghinMetVuong:
                    model.DonViDienTich = (int)enumDonViDienTich.NghinMetVuong;
                    model.DonViDienTichDat = (int)enumDonViDienTich.NghinMetVuong;
                    model.DonViDienTichNha = (int)enumDonViDienTich.NghinMetVuong;
                    break;
                case enumDonViDienTichRequest.MuoiNghinMetVuong:
                    model.DonViDienTich = (int)enumDonViDienTich.MuoiNghinMetVuong;
                    model.DonViDienTichDat = (int)enumDonViDienTich.MuoiNghinMetVuong;
                    model.DonViDienTichNha = (int)enumDonViDienTich.MuoiNghinMetVuong;
                    break;
                default:
                    break;
            }
            model.NgayBaoCao = thamSoNhanBaoCao.NGAY_BAO_CAO;
            model.NgayBatDau = thamSoNhanBaoCao.TU_NGAY;
            model.NgayKetThuc = thamSoNhanBaoCao.DEN_NGAY;
            model.NamBaoCao = thamSoNhanBaoCao.NAM.GetValueOrDefault();
            if (!(model.NamBaoCao > 0) && model.NgayBaoCao != null)
            {
                model.NamBaoCao = model.NgayBaoCao.Value.Year;
            }
            model.MauSo = 1;
            model.BacDonVi = thamSoNhanBaoCao.BAC_DON_VI.GetValueOrDefault();
            model.BacTaiSan = thamSoNhanBaoCao.BAC_TAI_SAN.GetValueOrDefault();
            if (thamSoNhanBaoCao.LIST_LOAI_HINH_TAI_SAN_ID != null)
            {
                model.StringLoaiTaiSan = string.Join(",", thamSoNhanBaoCao.LIST_LOAI_HINH_TAI_SAN_ID);

            }
            if (thamSoNhanBaoCao.LIST_LOAI_DON_VI_ID != null)
            {
                model.StringLoaiDonVi = string.Join(",", thamSoNhanBaoCao.LIST_LOAI_DON_VI_ID);
            }




            return model;
        }
        public BaoCaoTaiSanDongBoSearch PrepareReportSearchModel(ThamSoNhanBaoCaoSvcSearchModel thamSoNhanBaoCao)
        {
            BaoCaoTaiSanDongBoSearch model = new BaoCaoTaiSanDongBoSearch();
            model.NHOM_TAI_SAN_LON = thamSoNhanBaoCao.NHOM_TAI_SAN_LON;
            model.MaBaoCao = thamSoNhanBaoCao.MA_BAO_CAO;
            model.StringDonVi = string.Join(",", thamSoNhanBaoCao.LIST_DON_VI_ID);
            model.DonVi = thamSoNhanBaoCao.LIST_DON_VI_ID.FirstOrDefault();
            DonVi donVi = _donViService.GetDonViById(model.DonVi);
            if (donVi != null)
            {
                model.TEN_DON_VI = donVi.TEN;
                model.MA_DON_VI = donVi.MA;
                model.MA_QUAN_HE_NGAN_SACH = donVi.MA_DVQHNS;
                model.TEN_DON_VI_CHA = donVi.DonViCha != null ? donVi.DonViCha.TEN : "";
                model.TenLoaiHinhDonVi = donVi.LoaiDonVi != null ? donVi.LoaiDonVi.TEN : "";
                if (donVi.MA == donVi.MA_BO)
                    model.TEN_BO_NGANH = donVi.TEN;
                else
                {
                    DonVi donViBo = _donViService.GetDonViByMa(donVi.MA_BO);
                    if (donViBo != null)
                        model.TEN_BO_NGANH = donViBo.TEN;
                }
            }
            model.FileType = thamSoNhanBaoCao.LOAI_DATA_TRA_VE;
            switch ((enumDonViTinhGiaTri)thamSoNhanBaoCao.DON_VI_TINH_GIA_TRI)
            {
                case enumDonViTinhGiaTri.Dong:
                    model.DonViTien = (int)enumDonViTien.Dong;
                    break;
                case enumDonViTinhGiaTri.NghinDong:
                    model.DonViTien = (int)enumDonViTien.NghinDong;
                    break;
                case enumDonViTinhGiaTri.TrieuDong:
                    model.DonViTien = (int)enumDonViTien.TrieuDong;
                    break;
                case enumDonViTinhGiaTri.TyDong:
                    model.DonViTien = (int)enumDonViTien.TyDong;
                    break;
                default:
                    break;
            }
            switch ((enumDonViDienTichRequest)thamSoNhanBaoCao.DON_VI_TINH_GIA_TRI)
            {
                case enumDonViDienTichRequest.MetVuong:
                    model.DonViDienTich = (int)enumDonViDienTich.MetVuong;
                    model.DonViDienTichDat = (int)enumDonViDienTich.MetVuong;
                    model.DonViDienTichNha = (int)enumDonViDienTich.MetVuong;
                    break;
                case enumDonViDienTichRequest.NghinMetVuong:
                    model.DonViDienTich = (int)enumDonViDienTich.NghinMetVuong;
                    model.DonViDienTichDat = (int)enumDonViDienTich.NghinMetVuong;
                    model.DonViDienTichNha = (int)enumDonViDienTich.NghinMetVuong;
                    break;
                case enumDonViDienTichRequest.MuoiNghinMetVuong:
                    model.DonViDienTich = (int)enumDonViDienTich.MuoiNghinMetVuong;
                    model.DonViDienTichDat = (int)enumDonViDienTich.MuoiNghinMetVuong;
                    model.DonViDienTichNha = (int)enumDonViDienTich.MuoiNghinMetVuong;
                    break;
                default:
                    break;
            }
            model.NgayBaoCao = thamSoNhanBaoCao.NGAY_BAO_CAO;
            model.NgayBatDau = thamSoNhanBaoCao.TU_NGAY;
            model.NgayKetThuc = thamSoNhanBaoCao.DEN_NGAY ?? thamSoNhanBaoCao.NGAY_BAO_CAO; 
            model.NamBaoCao = thamSoNhanBaoCao.NAM.GetValueOrDefault();
            if (!(model.NamBaoCao > 0) && model.NgayBaoCao != null)
            {
                model.NamBaoCao = model.NgayBaoCao.Value.Year;
            }
            model.BacDonVi = thamSoNhanBaoCao.BAC_DON_VI.GetValueOrDefault();
            model.BacTaiSan = thamSoNhanBaoCao.BAC_TAI_SAN.GetValueOrDefault();
            model.LoaiHinhTaiSan = thamSoNhanBaoCao.LIST_LOAI_HINH_TAI_SAN_ID?.FirstOrDefault();
            if (thamSoNhanBaoCao.LIST_LOAI_HINH_TAI_SAN_ID != null)
            {
                model.StringLoaiTaiSan = string.Join(",", thamSoNhanBaoCao.LIST_LOAI_HINH_TAI_SAN_ID);

            }
            if (thamSoNhanBaoCao.LIST_LOAI_DON_VI_ID != null)
            {
                model.StringLoaiDonVi = string.Join(",", thamSoNhanBaoCao.LIST_LOAI_DON_VI_ID);
            }
            return model;
        }
        public ReturnBaoCaoSvc PrepareReturnBaoCaoSvcFromQueueProcess(QueueProcess queue)
        {
            ReturnBaoCaoSvc returnBaoCaoSvc = new ReturnBaoCaoSvc
            {
                LOAI_DATA_TRA_VE_ID = queue.FILE_TYPE.GetValueOrDefault(),
                MA_BAO_CAO = queue.MA_BAO_CAO
            };
            //Kiểu dữ liệu khi chọn trả về là json
            if (returnBaoCaoSvc.LOAI_DATA_TRA_VE_ID == (int)enumEXPORT_FILE_TYPE.JSON)
                returnBaoCaoSvc.JsonValue = queue.DATA_JSON;
            //Kiểu dữ liệu khi chọn trả về là các loại file
            else
            {
                var file = _fileCongViecService.GetFileByGuid(queue.GUID_FILE);
                if (file == null)
                    return null;
                returnBaoCaoSvc.LOAI_FILE = file.LOAI_FILE;
                returnBaoCaoSvc.DUOI_FILE = file.DUOI_FILE;
                returnBaoCaoSvc.BinaryValue = file.NOI_DUNG_FILE;
            }
            return returnBaoCaoSvc;
        }

        public ReturnBaoCaoSvc PrepareReturnBaoCaoSvcFromQueueProcessAndQlCauHinh(QueueProcess queue)
        {
            ReturnBaoCaoSvc returnBaoCaoSvc = new ReturnBaoCaoSvc
            {
                LOAI_DATA_TRA_VE_ID = queue.FILE_TYPE.GetValueOrDefault(),
                MA_BAO_CAO = queue.MA_BAO_CAO
            };
            //Kiểu dữ liệu khi chọn trả về là json
            if (returnBaoCaoSvc.LOAI_DATA_TRA_VE_ID == (int)enumEXPORT_FILE_TYPE.JSON)
            {
                var cauhinhmapbaocao = _cauHinhService.LoadCauHinh<CauHinhMapBC>();
                if (cauhinhmapbaocao.BaoCao != null)
                {
                    var mapbaocao = cauhinhmapbaocao.BaoCao.toEntities<CauHinhMapBaoCao>().Where(p => p.MaBaoCao == queue.MA_BAO_CAO).FirstOrDefault();
                    if (mapbaocao != null && !string.IsNullOrEmpty(mapbaocao.reportData.toStringJson()))
                    {
                        var cauHinhMapBaoCao = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(mapbaocao.reportData.toStringJson());
                        List<Dictionary<string, string>> d = new List<Dictionary<string, string>>();
                        Dictionary<string, string> a = new Dictionary<string, string>();
                        var b = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(queue.DATA_JSON);
                        foreach (var b1 in b)
                        {
                            foreach (var b2 in b1)
                            {
                                foreach (var cauHinhMapBaoCaos in cauHinhMapBaoCao)
                                {
                                    if (cauHinhMapBaoCaos.Key.Equals(b2.Key))
                                    {
                                        //d.Add(cauHinhMapBaoCaos.Value, b2.Value);
                                        a.Add(cauHinhMapBaoCaos.Value, b2.Value);
                                    }
                                }

                            }
                            d.Add(a);
                            a = new Dictionary<string, string>();
                        }
                        returnBaoCaoSvc.JsonValue = d.toStringJson();
                    }
                }
            }
            //Kiểu dữ liệu khi chọn trả về là các loại file
            else
            {
                var file = _fileCongViecService.GetFileByGuid(queue.GUID_FILE);
                if (file == null)
                    return null;
                returnBaoCaoSvc.LOAI_FILE = file.LOAI_FILE;
                returnBaoCaoSvc.DUOI_FILE = file.DUOI_FILE;
                returnBaoCaoSvc.BinaryValue = file.NOI_DUNG_FILE;
            }
            return returnBaoCaoSvc;
        }

        public string PrepareStatementQueueProcess(string baseStatement, ThamSoNhanBaoCaoSvcSearchModel thamSoNhanBaoCao)
        {
            foreach (var property in typeof(ThamSoNhanBaoCaoSvcSearchModel).GetProperties())
            {
                if (property.GetValue(thamSoNhanBaoCao) == null)
                {
                    var param = string.Format(FormatParamName, property.Name);
                    baseStatement = baseStatement.Replace(param, "NULL");
                    continue;
                }

                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    var param = string.Format(FormatParamName, property.Name);
                    var value = string.Format(FormatParamDateTime, (property.GetValue(thamSoNhanBaoCao) as DateTime?).toDateVNString());
                    baseStatement = baseStatement.Replace(param, value);
                }
                else if (property.GetValue(thamSoNhanBaoCao) is IList)
                {
                    if (property.Name == "LIST_DON_VI_ID")
                    {
                        var paramDonVi = string.Format(FormatParamName, "DON_VI_ID");
                        baseStatement = baseStatement.Replace(paramDonVi, (property.GetValue(thamSoNhanBaoCao) as IList<decimal>).FirstOrDefault().ToString().Replace(',', '.'));
                    }
                    var param = string.Format(FormatParamName, property.Name);
                    var value = string.Format(FormatParamString, string.Join(",", (property.GetValue(thamSoNhanBaoCao) as IList<decimal>).Select(p => p.ToString().Replace(',', '.'))));
                    baseStatement = baseStatement.Replace(param, value);
                }
                else if (property.PropertyType == typeof(string))
                {
                    var param = string.Format(FormatParamName, property.Name);
                    var value = string.Format(FormatParamString, (property.GetValue(thamSoNhanBaoCao) as string));
                    baseStatement = baseStatement.Replace(param, value);
                }
                else
                {
                    var param = string.Format(FormatParamName, property.Name);
                    baseStatement = baseStatement.Replace(param, string.Join(",", property.GetValue(thamSoNhanBaoCao).ToString().Replace(',', '.')));
                }
            }
            return baseStatement;
        }
    }
}
