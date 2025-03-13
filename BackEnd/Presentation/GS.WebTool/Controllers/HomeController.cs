using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GS.WebTool.Models;
using GS.WebTool.Infrastructure;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using GS.Core.Domain.DanhMuc;
using GS.Core;
using GS.Core.Infrastructure;
using Newtonsoft.Json;
using GS.Core.Data;
using GS.Core.Domain.TaiSans;
using System.Text;
using StackExchange.Profiling.Internal;
using GS.Services.DanhMuc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using DevExpress.XtraRichEdit.Fields;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace GS.WebTool.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGSFileProvider _fileProvider;
        private readonly IDonViService _donViService;
        public HomeController(IGSFileProvider gSFileProvider,
            IDonViService donViService)
        {
            this._fileProvider = gSFileProvider;
            this._donViService = donViService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExportJsonForDev()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public List<TaiSanDongBoModel> prepareTaiSanDongBo(decimal pLOAI_HINH_TAI_SAN = 0, int pNAM = 0, string pNHOM_DON_VI = "", int is32 = 0, string pMA_DON_VI = "")
        {
            List<string> parameter = new List<string>();
            if (pLOAI_HINH_TAI_SAN > 0)
                parameter.Add(string.Format("LOAI_HINH_TAI_SAN_ID = {0}", pLOAI_HINH_TAI_SAN));
            if (pNAM > 0)
                parameter.Add(string.Format("NAM = {0}", pNAM));
            //if (!string.IsNullOrEmpty(pNHOM_DON_VI))
            //    parameter.Add(string.Format("MA_NHOM_DON_VI = {0}", pNHOM_DON_VI));
            if (!string.IsNullOrEmpty(pMA_DON_VI))
            {
                List<string> lsMaDonVi = pMA_DON_VI.Split(',').ToList();
                List<string> str = new List<string>();
                foreach (var mdv in lsMaDonVi)
                {
                    //var donVi = _donViService.GetDonViByMa(mdv.Trim());
                    //if (donVi != null)
                    //{
                    //    var donVis = _donViService.GetListDonViChild(donVi.ID);
                    //    if (donVis != null && donVis.Count > 0)
                    //        str = str.Concat(donVis.Select(c => string.Format("(1,'{0}')", c.MA))).ToList();
                    //}
                    str.Add($"MA_TAI_SAN LIKE '{mdv}%'");
                }
                //parameter.Add(string.Format("(1,MA_DON_VI) IN ({0})", string.Join(",", str))); 
                parameter.Add(string.Format("({0})", string.Join(" OR ", str)));
            }
            else
            {
                parameter.Add("IS_LOI = 1");
            }
            //if (is32 >= 0)
            //    parameter.Add(string.Format("IS_32 = {0}", is32));
            string sql = "SELECT * FROM GS_TEMP_DONG_BO_DU_LIEU WHERE TRANG_THAI IS NOT NULL AND TAI_SAN_JSON IS NOT NULL" + (parameter.Count > 0 ? " AND " + string.Join(" AND ", parameter) : "");
            var lst = dbContextDKTS.getTable<TaiSanDongBoModel>(sql);
            DB_BienDongModel bdf = new DB_BienDongModel();
            DB_BienDongModel bdc = new DB_BienDongModel();

            foreach (var itm in lst)
            {
                lst = lst.OrderBy(c => c.LOAI_HINH_TAI_SAN_ID).ToList();
                if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT)
                    itm.TS_DAT = itm.TAI_SAN_JSON.toEntity<WT_TSDatDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.NHA)
                    itm.TS_NHA = itm.TAI_SAN_JSON.toEntity<WT_TSNhaDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.OTO)
                    itm.TS_OTO = itm.TAI_SAN_JSON.toEntity<WT_TSOtoDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC)
                    itm.TS_PTK = itm.TAI_SAN_JSON.toEntity<WT_TSOtoDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV)
                    itm.TS_CLN = itm.TAI_SAN_JSON.toEntity<WT_TSClnDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI)
                    itm.TS_MAY_MOC = itm.TAI_SAN_JSON.toEntity<WT_TSMayMocDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC)
                    itm.TS_VKT = itm.TAI_SAN_JSON.toEntity<WT_TSVktDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.VO_HINH)
                    itm.TS_VO_HINH = itm.TAI_SAN_JSON.toEntity<WT_TSVoHinhDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC)
                    itm.TS_HUU_HINH_KHAC = itm.TAI_SAN_JSON.toEntity<WT_TSMayMocDBModel>();
                else if (itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAC_THU)
                    itm.TS_DAC_THU = itm.TAI_SAN_JSON.toEntity<WT_TSMayMocDBModel>();
                itm.LST_BIEN_DONG = itm.LST_BIEN_DONG_JSON.toEntities<DB_BienDongModel>();
                itm.LST_HIEN_TRANG = itm.LST_HIEN_TRANG_JSON.toEntities<DB_TaiSanHienTrangSuDungModel>();
                itm.LST_NGUON_VON = itm.LST_NGUON_VON_JSON.toEntities<DB_TaiSanNguonVonModel>();
                itm.LST_HAO_MON = itm.KT_HAO_MON_JSON.toEntities<DB_KT_HaoMonModel>();

                if (itm.LST_BIEN_DONG != null && itm.LST_BIEN_DONG.Count > 0)
                {
                    itm.LST_BIEN_DONG = itm.LST_BIEN_DONG.Select(c =>
                    {
                        c.DATA_JSON = new
                        {
                            HS_CNQSD_SO = c.HS_CNQSD_SO,
                            HS_CNQSD_NGAY = String.IsNullOrEmpty(c.HS_CNQSD_SO) ? null : c.HS_CNQSD_NGAY,
                            HS_QUYET_DINH_GIAO_SO = c.HS_QUYET_DINH_GIAO_SO,
                            HS_QUYET_DINH_GIAO_NGAY = String.IsNullOrEmpty(c.HS_QUYET_DINH_GIAO_SO) ? null : c.HS_QUYET_DINH_GIAO_NGAY,
                            HS_CHUYEN_NHUONG_SD_SO = c.HS_CHUYEN_NHUONG_SD_SO,
                            HS_CHUYEN_NHUONG_SD_NGAY = String.IsNullOrEmpty(c.HS_CHUYEN_NHUONG_SD_SO) ? null : c.HS_CHUYEN_NHUONG_SD_NGAY,
                            HS_QUYET_DINH_CHO_THUE_SO = c.HS_QUYET_DINH_CHO_THUE_SO,
                            HS_QUYET_DINH_CHO_THUE_NGAY = String.IsNullOrEmpty(c.HS_QUYET_DINH_CHO_THUE_SO) ? null : c.HS_QUYET_DINH_CHO_THUE_NGAY,
                            HS_KHAC = c.HS_KHAC,
                            HS_QUYET_DINH_BAN_GIAO = c.HS_QUYET_DINH_BAN_GIAO,
                            HS_QUYET_DINH_BAN_GIAO_NGAY = String.IsNullOrEmpty(c.HS_QUYET_DINH_BAN_GIAO) ? null : c.HS_QUYET_DINH_BAN_GIAO_NGAY,
                            HS_BIEN_BAN_NGHIEM_THU = c.HS_BIEN_BAN_NGHIEM_THU,
                            HS_BIEN_BAN_NGHIEM_THU_NGAY = String.IsNullOrEmpty(c.HS_BIEN_BAN_NGHIEM_THU) ? null : c.HS_BIEN_BAN_NGHIEM_THU_NGAY,
                            HS_PHAP_LY_KHAC = c.HS_PHAP_LY_KHAC,
                            HS_PHAP_LY_KHAC_NGAY = String.IsNullOrEmpty(c.HS_PHAP_LY_KHAC) ? null : c.HS_PHAP_LY_KHAC_NGAY,
                            DAT_GIA_TRI_QUYEN_SD_DAT = itm.LOAI_HINH_TAI_SAN_ID == (int)enumLOAI_HINH_TAI_SAN.DAT ? itm.NGUYEN_GIA_BAN_DAU : null
                        }.toStringJson();
                        c.NGUON_VON_BD = itm.LST_NGUON_VON.Where(x => x.BIEN_DONG_GUID == c.GUID).ToList();
                        c.HIEN_TRANG_BD = itm.LST_HIEN_TRANG.Where(x => x.BIEN_DONG_GUID == c.GUID).ToList();
                        return c;
                    }).OrderBy(c => c.GUID).ToList();
                    //itm.LST_BIEN_DONG = itm.LST_BIEN_DONG.Select(c => { c.LY_DO_BIEN_DONG_MA = c.LY_DO_BIEN_DONG_MA.PadLeft(3, '0'); return c; }).OrderBy(c => c.NGAY_BIEN_DONG).ToList();
                    bdf = itm.LST_BIEN_DONG.Where(c => c.LOAI_BIEN_DONG_ID == 1).FirstOrDefault();
                    if (bdf == null)
                        bdf = itm.LST_BIEN_DONG.FirstOrDefault();
                    //bdl = itm.LST_BIEN_DONG.LastOrDefault();
                    bdc = itm.LST_BIEN_DONG.Where(c => c.TRANG_THAI == 2).LastOrDefault();//dữ liệu bên qldkts 17 02 2020 trạng thái = 2 là đã duyệt
                    if (bdc != null)
                    {
                        if (!itm.TRANG_THAI.HasValue)
                            itm.TRANG_THAI = 2;
                        bdc.IS_BIENDONG_CUOI = true;
                        itm.LST_BIEN_DONG.Remove(bdc);
                        itm.LST_BIEN_DONG.Add(bdc);
                    }
                    itm.LY_DO_BIEN_DONG_MA = bdf.LY_DO_BIEN_DONG_MA;
                    itm.LY_DO_BIEN_DONG_TEN = bdf.LY_DO_BIEN_DONG_TEN;
                    itm.NGAY_NHAP = itm.NGAY_NHAP ?? bdf.NGAY_BIEN_DONG.toDateSys(CommonHelper.DATE_FORMAT_DB);
                    itm.NGAY_SU_DUNG = itm.NGAY_SU_DUNG ?? bdf.NGAY_SU_DUNG.toDateSys(CommonHelper.DATE_FORMAT_DB);
                    //List<string> lstexists = itm.LST_BIEN_DONG.GroupBy(x => x.NGAY_BIEN_DONG.toDateVNString()).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
                    //if (lstexists.Count() > 0)
                    //{
                    //    foreach (string dateStr in lstexists)
                    //    {

                    //    }
                    //}
                }
                itm.TAI_SAN_JSON = null;
                itm.LST_BIEN_DONG_JSON = null;
                itm.LST_HIEN_TRANG_JSON = null;
                itm.LST_NGUON_VON_JSON = null;
            }
            return lst ?? new List<TaiSanDongBoModel>();
        }
        public IActionResult ExportXml(decimal pLOAI_HINH_TAI_SAN, int pNAM, string pNHOM_DON_VI, string pMA_DON_VI, int is32 = 0)
        {
            //config.ExecuteProcedureINSERT_TO_TEMP_DONG_BO(pLOAI_HINH_TAI_SAN, 0, pNAM, pNHOM_DON_VI);
            var lst = prepareTaiSanDongBo(pLOAI_HINH_TAI_SAN, pNAM, pNHOM_DON_VI, is32, pMA_DON_VI);
            string xmlString = "";
            XmlRootAttribute xmlRoot = new XmlRootAttribute("root");
            XmlSerializer xmlSerializer = new XmlSerializer(lst.GetType(), xmlRoot);
            //string fName = $"EXP_DB_TS_{((enumLOAI_HINH_TAI_SAN)pLOAI_HINH_TAI_SAN).ToString().ToUpper()}_{pMA_DON_VI}_{pNAM}_{(is32 == 1 ? 32 : 162)}_{lst.Count()}({DateTime.Now.ToString("dd-MM-yyyy hh-mm")}).xml";

            int total = lst.Count();
            int pageSize = 10000;
            int TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;
            List<string> fNames = new List<string>();
            List<string> fullpaths = new List<string>();
            string fName = "";
            string fullpath = "";
            try
            {
                for (int i = 1; i <= TotalPages; i++)
                {
                    var lstSplit = lst.Skip(i * pageSize).Take(pageSize).ToList();
                    fName = string.Format("EXP_DB_TS_{0}{1}_{2}_{3}_{4}_({5})({6}).xml", ((enumLOAI_HINH_TAI_SAN)pLOAI_HINH_TAI_SAN).ToString().ToUpper(), string.IsNullOrEmpty(pMA_DON_VI) ? "" : "_" + pMA_DON_VI, pNAM, (is32 == 1 ? "QD32" : "TT162"), lstSplit.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"), i);
                    fullpath = $"{GSDataSettingsDefaults.FolderWorkFiles}{fName}";
                    fullpath = _fileProvider.MapPath(fullpath);
                    _fileProvider.CreateFile(fullpath);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        xmlSerializer.Serialize(memoryStream, lstSplit);
                        memoryStream.Position = 0;
                        xmlString = new StreamReader(memoryStream).ReadToEnd();
                        _fileProvider.WriteAllBytes(fullpath, memoryStream.ToArray());
                        fNames.Add(fName);
                        fullpaths.Add(fullpath);
                    }
                }
                return Json(new { code = "00", filePaths = fullpaths, fileNames = fNames, fileType = "text/xml"/*, textFile = xmlString*/ });
            }
            catch
            {
                return Json(new { code = "01" });
            }

        }
        public IActionResult ExportJson(String foldersave, decimal pLOAI_HINH_TAI_SAN, int pNAM, string pNHOM_DON_VI, string pMA_DON_VI, int is32 = 0)
        {
            if (string.IsNullOrEmpty(foldersave))
            {
                return null;
            }
            int maxNam = DateTime.Now.Year, minNam = 1900;
            //string rsnam = dbContextDKTS.LayMotGiaTri("SELECT MAX(NAM) FROM GS_TEMP_DONG_BO_DU_LIEU");
            //if (!int.TryParse(rsnam, out maxNam))
            //{
            //    maxNam = DateTime.Now.Year;
            //}
            //rsnam = dbContextDKTS.LayMotGiaTri("SELECT MIN(NAM) FROM GS_TEMP_DONG_BO_DU_LIEU");
            //if (!int.TryParse(rsnam, out minNam))
            //{
            //    minNam = DateTime.Now.Year;
            //}
            //config.ExecuteProcedureINSERT_TO_TEMP_DONG_BO(pLOAI_HINH_TAI_SAN, 0, pNAM, pNHOM_DON_VI);
            List<DonVi> donVis = new List<DonVi>();
            if (!string.IsNullOrEmpty(pMA_DON_VI))
            {
                foreach (string ma in pMA_DON_VI.Split(',').ToList())
                {
                    var x = _donViService.GetDonViByMa(ma);
                    donVis.Add(x);
                }
            }

            else
            {
                donVis = _donViService.GetAllDonVis().Where(c => c.MA == c.MA_BO).ToList();
            }

            List<string> fNames = new List<string>();
            List<string> fullpaths = new List<string>();
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            serializerSettings.DateFormatString = CommonHelper.DATE_FORMAT_DB;
            serializerSettings.NullValueHandling = NullValueHandling.Ignore;
            foreach (DonVi donVi in donVis)
            {
                var lstall = prepareTaiSanDongBo(pMA_DON_VI: donVi.MA);
                if (lstall == null)
                    return null;
                maxNam = (int)lstall.Max(c => c.NAM).GetValueOrDefault(DateTime.Now.Year);
                minNam = (int)lstall.Min(c => c.NAM).GetValueOrDefault(1900);
                for (int loaiHinhTaiSanId = 1; loaiHinhTaiSanId <= 10; loaiHinhTaiSanId++)
                {
                    for (int nam = minNam; nam <= maxNam; nam++)
                    {
                        var lst = lstall.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTaiSanId && c.NAM == nam).ToList();
                        if (lst != null && lst.Count > 0)
                        {
                            List<string> param = new List<string>();
                            string _pathStore = _fileProvider.Combine(foldersave, String.Format("{0}-{1}", donVi.MA, donVi.TEN), ((enumLOAI_HINH_TAI_SAN)loaiHinhTaiSanId).ToString(), nam.ToString());
                            _fileProvider.CreateDirectory(_pathStore);

                            int total = lst.Count();
                            int pageSize = 10000;
                            int TotalPages = total / pageSize;

                            if (total % pageSize > 0)
                                TotalPages++;
                            string fName = "";
                            string fullpath = "";
                            string json = "";

                            for (int i = 0; i < TotalPages; i++)
                            {
                                var lstSplit = lst.Skip(i * pageSize).Take(pageSize).ToList();
                                param = new List<string>();
                                param.Add(donVi.MA);
                                param.Add(nam.ToString());
                                fName = string.Format("EXP_DB_TS_{0}{1}_{2}_({3})({4}).json", ((enumLOAI_HINH_TAI_SAN)loaiHinhTaiSanId).ToString().ToUpper(), param.Count > 0 ? "_" + string.Join("_", param) : "", lstSplit.Count(), DateTime.Now.ToString("dd-MM-yyyy hh-mm"), i);

                                fullpath = _fileProvider.Combine(_pathStore, fName);

                                json = JsonConvert.SerializeObject(lstSplit, serializerSettings);
                                _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                                fNames.Add(fName);
                                fullpaths.Add(fullpath);
                            }
                        }

                    }
                }
            }

            return Json(new { code = "00", filePaths = fullpaths, fileNames = fNames, fileType = MimeTypes.ApplicationJson/*, textFile = json*/ });

        }
        public IActionResult ExportJson1(decimal pLOAI_HINH_TAI_SAN, int pNAM, string pNHOM_DON_VI, string pMA_DON_VI, int is32 = 0)
        {
            //config.ExecuteProcedureINSERT_TO_TEMP_DONG_BO(pLOAI_HINH_TAI_SAN, 0, pNAM, pNHOM_DON_VI);
            var lst = prepareTaiSanDongBo(pLOAI_HINH_TAI_SAN, pNAM, pNHOM_DON_VI, is32, pMA_DON_VI);
            List<string> param = new List<string>();


            int total = lst.Count();
            int pageSize = 1000;
            int TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;
            List<string> fNames = new List<string>();
            List<string> fullpaths = new List<string>();
            string fName = "";
            string fullpath = "";
            try
            {
                string json = "";
                var serializerSettings = new JsonSerializerSettings();
                serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                //serializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializerSettings.DateFormatString = CommonHelper.DATE_FORMAT_DB;
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                string _pathStore = DateTime.Now.ToPathFolderStore();
                _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                _fileProvider.CreateDirectory(_pathStore);
                String strTime = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                for (int i = 0; i < TotalPages; i++)
                {

                    var lstSplit = lst.Skip(i * pageSize).Take(pageSize).ToList();

                    param = new List<string>();
                    if (!string.IsNullOrEmpty(pMA_DON_VI))
                        param.Add(pMA_DON_VI);
                    if (pNAM > 0)
                        param.Add(pNAM.ToString());
                    fName = string.Format("EXP_DB_TS_{0}{1}_{2}_({3})({4}).json", ((enumLOAI_HINH_TAI_SAN)pLOAI_HINH_TAI_SAN).ToString().ToUpper(), param.Count > 0 ? "_" + string.Join("_", param) : "", lstSplit.Count(), strTime, i);

                    fullpath = _fileProvider.Combine(_pathStore, fName);

                    //fullpath = _fileProvider.MapPath(fullpath);
                    //_fileProvider.CreateFile(fullpath);
                    json = JsonConvert.SerializeObject(lstSplit, serializerSettings);
                    _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                    fNames.Add(fName);
                    fullpaths.Add(fullpath);
                }
                return Json(new { code = "00", filePaths = fullpaths, fileNames = fNames, fileType = MimeTypes.ApplicationJson/*, textFile = json*/ });
            }
            catch (Exception ex)
            {
                return Json(new { code = "01", mss = ex.Message });
            }

        }

        public IActionResult ExportJsonHaoMon(decimal pLOAI_HINH_TAI_SAN, int pNAM, string pNHOM_DON_VI, string pMA_DON_VI, int is32 = 0)
        {
            //config.ExecuteProcedureINSERT_TO_TEMP_DONG_BO(pLOAI_HINH_TAI_SAN, 0, pNAM, pNHOM_DON_VI);
            // var lst = prepareTaiSanDongBo(pLOAI_HINH_TAI_SAN, pNAM, pNHOM_DON_VI, is32, pMA_DON_VI);
            #region prepare
            List<string> parameter = new List<string>();
            if (pLOAI_HINH_TAI_SAN > 0)
                parameter.Add(string.Format("LOAI_HINH_TAI_SAN_ID = {0}", pLOAI_HINH_TAI_SAN));
            if (pNAM > 0)
                parameter.Add(string.Format("NAM = {0}", pNAM));
            //if (!string.IsNullOrEmpty(pNHOM_DON_VI))
            //    parameter.Add(string.Format("MA_NHOM_DON_VI = {0}", pNHOM_DON_VI));
            if (!string.IsNullOrEmpty(pMA_DON_VI))
            {
                List<string> lsMaDonVi = pMA_DON_VI.Split(',').ToList();
                List<string> str = new List<string>();
                foreach (var mdv in lsMaDonVi)
                {
                    //var donVi = _donViService.GetDonViByMa(mdv.Trim());
                    //if (donVi != null)
                    //{
                    //    var donVis = _donViService.GetListDonViChild(donVi.ID);
                    //    if (donVis != null && donVis.Count > 0)
                    //        str = str.Concat(donVis.Select(c => string.Format("(1,'{0}')", c.MA))).ToList();
                    //}
                    str.Add($"MA_TAI_SAN LIKE '{mdv}%'");
                }
                //parameter.Add(string.Format("(1,MA_DON_VI) IN ({0})", string.Join(",", str))); 
                parameter.Add(string.Format("({0})", string.Join(" OR ", str)));
            }
            else
            {
                parameter.Add("IS_LOI = 1");
            }
            //if (is32 >= 0)
            //    parameter.Add(string.Format("IS_32 = {0}", is32));
            string sql = "SELECT MA_TAI_SAN,KT_HAO_MON_JSON FROM GS_TEMP_DONG_BO_DU_LIEU WHERE TRANG_THAI IS NOT NULL AND TAI_SAN_JSON IS NOT NULL" + (parameter.Count > 0 ? " AND " + string.Join(" AND ", parameter) : "");
            var lst = dbContextDKTS.getTable<TaiSanDongBoModel>(sql);
            #endregion
            List<string> param = new List<string>();
            List<DB_KT_HaoMonModel> lstHaoMon = new List<DB_KT_HaoMonModel>();
            if (lst != null && lst.Count > 0)
            {
                foreach (var item in lst)
                {
                    if (string.IsNullOrEmpty(item.KT_HAO_MON_JSON) || item.KT_HAO_MON_JSON == "[]")
                        continue;
                    var x = item.KT_HAO_MON_JSON.toEntities<DB_KT_HaoMonModel>();
                    if (x != null && x.Count > 0)
                        x = x.Select(c => { c.MA_TAI_SAN = item.MA_TAI_SAN; return c; }).ToList();
                    lstHaoMon = lstHaoMon.Concat(x).ToList();
                }
            }

            int total = lstHaoMon.Count();
            int pageSize = 100000;
            int TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;
            List<string> fNames = new List<string>();
            List<string> fullpaths = new List<string>();
            string fName = "";
            string fullpath = "";
            try
            {
                string json = "";
                var serializerSettings = new JsonSerializerSettings();
                //serializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                serializerSettings.Formatting = Newtonsoft.Json.Formatting.None;
                serializerSettings.DateFormatString = CommonHelper.DATE_FORMAT_DB;
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                string _pathStore = DateTime.Now.ToPathFolderStore();
                _pathStore = _fileProvider.Combine(_fileProvider.MapPath(GSDataSettingsDefaults.FolderWorkFiles), _pathStore);
                _fileProvider.CreateDirectory(_pathStore);
                String strTime = DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss");
                for (int i = 0; i < TotalPages; i++)
                {

                    var lstSplit = lstHaoMon.Skip(i * pageSize).Take(pageSize).ToList();

                    param = new List<string>();
                    if (!string.IsNullOrEmpty(pMA_DON_VI))
                        param.Add(pMA_DON_VI);
                    if (pNAM > 0)
                        param.Add(pNAM.ToString());
                    fName = string.Format("EXP_HAOMON_TS_{0}{1}_{2}_({3})({4}).json", ((enumLOAI_HINH_TAI_SAN)pLOAI_HINH_TAI_SAN).ToString().ToUpper(), param.Count > 0 ? "_" + string.Join("_", param) : "", lstSplit.Count(), strTime, i);

                    fullpath = _fileProvider.Combine(_pathStore, fName);

                    //fullpath = _fileProvider.MapPath(fullpath);
                    //_fileProvider.CreateFile(fullpath);
                    json = JsonConvert.SerializeObject(lstSplit, serializerSettings);
                    _fileProvider.WriteAllText(fullpath, json, Encoding.UTF8);
                    fNames.Add(fName);
                    fullpaths.Add(fullpath);
                }
                return Json(new { code = "00", filePaths = fullpaths, fileNames = fNames, fileType = MimeTypes.ApplicationJson/*, textFile = json*/ });
            }
            catch (Exception ex)
            {
                return Json(new { code = "01", mss = ex.Message });
            }

        }



        public virtual IActionResult ExportByUrl(string filePath, string fileName, string fileType)
        {
            byte[] bytes;
            bytes = _fileProvider.ReadAllBytes(filePath);
            _fileProvider.DeleteFile(filePath);
            return File(bytes, fileType, fileName);
        }
        public virtual IActionResult ExportByUrl1(string filePath, string fileType)
        {
            byte[] bytes;
            string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
            bytes = _fileProvider.ReadAllBytes(filePath);
            _fileProvider.DeleteFile(filePath);
            return File(bytes, fileType, fileName);
        }
    }
}
