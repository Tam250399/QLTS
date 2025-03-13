using DevExpress.DataAccess.Json;
using DevExpress.XtraReports.UI;
using GS.Core;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Services;
using GS.Services.BaoCaos;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Factories.HeThong;
using GS.Web.Framework.Models;
using GS.Web.Models.BaoCaos;
using System;
using System.IO;
using System.Linq;

namespace GS.Web.Factories.BaoCaos
{
    public partial class QueueProcessModelFactory : IQueueProcessModelFactory
    {
        private readonly IQueueProcessService _itemService;
        private readonly IWorkContext _workContext;
        private readonly IFileCongViecService _fileCongViecService;
        private readonly IFileCongViecModelFactory _fileCongViecModelFactory;
        private readonly IBaoCaoService _baoCaoService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IHoatDongService _hoatDongService;
        private readonly INhanHienThiService _nhanHienThiService;
        private readonly IDonViService _donViService;

        public QueueProcessModelFactory(
            IQueueProcessService itemService,
            IWorkContext workContext,
            IFileCongViecService fileCongViecService,
            IFileCongViecModelFactory fileCongViecModelFactory,
            IBaoCaoService baoCaoService,
            INguoiDungService nguoiDungService,
            IHoatDongService hoatDongService,
            INhanHienThiService nhanHienThiService,
            IDonViService donViService)
        {
            this._itemService = itemService;
            this._workContext = workContext;
            this._fileCongViecService = fileCongViecService;
            this._fileCongViecModelFactory = fileCongViecModelFactory;
            this._baoCaoService = baoCaoService;
            this._nguoiDungService = nguoiDungService;
            this._hoatDongService = hoatDongService;
            this._nhanHienThiService = nhanHienThiService;
            this._donViService = donViService;
        }

        public void SaveFileBaoCao(XtraReport report, string MaBaoCao, decimal idQueue, string data = null)
        {
            if (data != null)
                report.DataSource = CreateDataSourceFromText(data);
            var queue = _itemService.GetQueueProcessById((int)idQueue);
            string contentType = "";
            string fileExtension = "";
            var strTitok = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            var nameFile = $"{MaBaoCao}-{strTitok}";
            if (queue.FILE_TYPE != (int)enumEXPORT_FILE_TYPE.JSON)
            {
                switch (queue.FILE_TYPE)
                {
                    case (int)enumEXPORT_FILE_TYPE.WORD_DOCX:
                        contentType = MimeTypes.TextDocx;
                        fileExtension = ".docx";
                        break;

                    case (int)enumEXPORT_FILE_TYPE.EXCEL_XLSX:
                        contentType = MimeTypes.TextXlsx;
                        fileExtension = ".xlsx";
                        break;

                    //case (int)enumEXPORT_FILE_TYPE.WORD_DOC:
                    //    contentType = MimeTypes.TextDoc;
                    //    fileExtension = ".doc";
                    //    break;

                    case (int)enumEXPORT_FILE_TYPE.EXCEL_XLS:
                        contentType = MimeTypes.TextXls;
                        fileExtension = ".xls";
                        break;

                    case (int)enumEXPORT_FILE_TYPE.PDF:
                        contentType = MimeTypes.ApplicationPdf;
                        fileExtension = ".pdf";
                        break;

                    default:
                        contentType = MimeTypes.ApplicationPdf;
                        fileExtension = ".pdf";
                        break;
                }
                //lưu báo cáo

                using (MemoryStream ms = new MemoryStream())
                {
                    switch (queue.FILE_TYPE)
                    {
                        case (int)enumEXPORT_FILE_TYPE.WORD_DOCX:
                            report.ExportToDocx(ms);
                            break;

                        case (int)enumEXPORT_FILE_TYPE.EXCEL_XLSX:
                            report.ExportToXlsx(ms);
                            break;

                        case (int)enumEXPORT_FILE_TYPE.PDF:
                            report.ExportToPdf(ms);
                            break;
                        case (int)enumEXPORT_FILE_TYPE.EXCEL_XLS:
                            report.ExportToXls(ms);
                            break;
                        default:
                            report.ExportToPdf(ms);
                            break;
                    }
                    byte[] fileBinary = ms.ToArray();
                    var fwork = new FileCongViec
                    {
                        GUID = Guid.NewGuid(),
                        NOI_DUNG_FILE = fileBinary,
                        LOAI_FILE = contentType,
                        //we store filename without extension for downloads
                        TEN_FILE = nameFile,
                        DUOI_FILE = fileExtension,
                        NGAY_TAO = DateTime.Now,
                        NGUOI_TAO = queue.NGUOI_TAO_ID ?? 0,
                        KICH_THUOC = Convert.ToInt32(fileBinary.LongLength / 1024) //luu thanh kb,
                    };
                    _fileCongViecService.InsertFileCongViec(fwork);
                    //luu file content ra ngoai
                    //_fileCongViecModelFactory.SaveWorkFileOnDisk(fwork, fileBinary);
                    queue.GUID_FILE = fwork.GUID;
                }
            }
            //queue.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DA_EXPORT_FILE;
            if (queue.TRANG_THAI_ID >= (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_TRANG_THAI_CHO)
                queue.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DA_EXPORT_FILE;
            else
                queue.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DA_EXPORT_FILE;
            _itemService.UpdateQueueProcessTrangThaiAndGUIDFILE(queue);
        }

        /// <summary>
        /// HungNT- Từ ExtraReport xuất luôn ra file dạng Byte[]
        /// </summary>
        /// <param name="report"></param>
        /// <param name="MaBaoCao"></param>
        /// <param name="data"></param>
        /// <param name="FileType"></param>
        /// <returns></returns>
        public byte[] ExportFileBaoCao(XtraReport report, string MaBaoCao, string data = null, enumEXPORT_FILE_TYPE FileType = enumEXPORT_FILE_TYPE.EXCEL_XLSX)
        {
            if (data != null)
                report.DataSource = CreateDataSourceFromText(data);
            var options = new DevExpress.XtraPrinting.XlsxExportOptions();
            options.IgnoreErrors = DevExpress.XtraPrinting.XlIgnoreErrors.NumberStoredAsText;
                using (MemoryStream ms = new MemoryStream())
                {
                    switch (FileType)
                    {
                        case enumEXPORT_FILE_TYPE.WORD_DOCX:
                            report.ExportToDocx(ms);
                            break;

                        case enumEXPORT_FILE_TYPE.EXCEL_XLSX:
                        report.ExportToXlsx(ms, options);
                            break;

                        case enumEXPORT_FILE_TYPE.PDF:
                            report.ExportToPdf(ms);
                            break;
                        case enumEXPORT_FILE_TYPE.EXCEL_XLS:
                            report.ExportToXls(ms);
                            break;
                        default:
                            report.ExportToPdf(ms);
                            break;
                    }
                    byte[] fileBinary = ms.ToArray();
                    return fileBinary;
                }
        }

        private JsonDataSource CreateDataSourceFromText(string json)
        {
            var jsonDataSource = new JsonDataSource();
            json = json ?? "{}";
            // Specify a string with JSON content
            // Specify the object that retrieves JSON data
            jsonDataSource.JsonSource = new CustomJsonSource(json);
            // Retrieve data from the JSON data source to the Report Designer's Field List
            jsonDataSource.Fill();
            return jsonDataSource;
        }

        public QueueProcessSearchModel PrepareQueueProcessSearchModel(QueueProcessSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.ddlTrangThai = (new enumTRANG_THAI_QUEUE_BAO_CAO()).ToSelectList().ToList();


            searchModel.SetGridPageSize();
            return searchModel;
        }

        public QueueProcessListModel PrepareQueueProcessListModel(QueueProcessSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //var idDonVi = _workContext.CurrentDonVi.ID;
            var idNguoiTao = _workContext.CurrentCustomer.ID;
            //get items
            Guid guid = Guid.Empty;
            if (!String.IsNullOrEmpty(searchModel.guid))
                guid = Guid.Parse(searchModel.guid);
            //Guid guidResponse = Guid.Empty;
            //if (!String.IsNullOrEmpty(searchModel.guidResponse))
            //    guidResponse = Guid.Parse(searchModel.guidResponse);
            var items = _itemService.SearchQueueProcesss(KeySearch: searchModel.KeySearch, pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, FromDate: searchModel.FromDate, ToDate: searchModel.ToDate, NguoiTaoId: idNguoiTao, TrangThaiId: searchModel.TrangThaiId, guid: guid, isDongBo:searchModel.isDongBo);

            //prepare list model
            var model = new QueueProcessListModel
            {
                //fill in model values from the entity
                Data = items.Select(c =>
                {
                    var res = c.ToModel<QueueProcessModel>();
                    res.DB_QUEUE_PROCESS_ID = c.DB_QUEUE_PROCESS_ID;
                    if (res.DB_QUEUE_PROCESS_ID > 0)
                    {
                        res.IsShowSendRequest = true;
                    }
                    res.strTrangThai = _nhanHienThiService.GetGiaTriEnum((enumTRANG_THAI_QUEUE_BAO_CAO)res.TRANG_THAI_ID);
                    var l_cauHinhBaoCao = _baoCaoService.GetCauHinhBaoCaoByMa(res.MA_BAO_CAO, res.DON_VI_ID);
                    if (l_cauHinhBaoCao != null)
                        res.TenBaoCao = l_cauHinhBaoCao.TenBaoCao;
                    if (searchModel.isDongBo == true && !String.IsNullOrEmpty(c.RESPONSE))
                    {
                        ResponseApi response = c.RESPONSE.toEntity<ResponseApi>();
                        if (response != null && response.Data != null)
                        {
                            string jsonData = Convert.ToString(response.Data);
                            ResponseApi data = jsonData.toEntity<ResponseApi>();
                            if (data != null)
                                res.guidResponse = Convert.ToString(data.Data);
                        }

                    }
                    var donVi = _donViService.GetDonViById(c.DON_VI_ID);
                    if (donVi != null)
                        res.TenAndMaDonVi = String.Format(@"{0} - {1}", donVi.MA, donVi.TEN);
                    //res.strThoiGianLayDuLieu = $"Ngày tạo:\n\t{c.NGAY_TAO.toDateVNString()}\nThời gian lấy dữ liệu từ:\n\t{c.TIME_START_GET_DATA.toDateVNString(true)}\nĐến:\n\t{c.TIME_END_GET_DATA.toDateVNString(true)}";
                    return res;
                }),
                Total = items.TotalCount
            };
            return model;
        }

        public QueueProcessModel PrepareQueueProcessModel(QueueProcessModel model, QueueProcess item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<QueueProcessModel>();
                model.strTrangThai = _nhanHienThiService.GetGiaTriEnum((enumTRANG_THAI_QUEUE_BAO_CAO)model.TRANG_THAI_ID);
                var l_cauHinhBaoCao = _baoCaoService.GetCauHinhBaoCaoByMa(model.MA_BAO_CAO, model.DON_VI_ID);
                if (l_cauHinhBaoCao != null)
                    model.TenBaoCao = l_cauHinhBaoCao.TenBaoCao;
                var nguoiDung = _nguoiDungService.GetNguoiDungById(model.NGUOI_TAO_ID);
                if (nguoiDung != null)
                    model.TenNguoiTao = nguoiDung.TEN_DAY_DU;
            }
            //more
            return model;
        }
        public QueueProcess InsertQueueForSpecificReport(string maBaoCao, BaseSearchModel searchModel, int FileType, string ClassReportFullName, string ModelReturnFullName)
        {
            //đẩy tham số
            var queue = new QueueProcess()
            {
                MA_BAO_CAO = maBaoCao,
                NGUOI_TAO_ID = (int)_workContext.CurrentCustomer.ID,
                DON_VI_ID = (int)_workContext.CurrentDonVi.ID,
                FILE_TYPE = FileType,
                SEARCH_MODEL_JSON = searchModel.toStringJson(),
                SEARCH_MODEL_CLASS = searchModel.GetType().FullName,
                REPORT_CLASS = ClassReportFullName,
                MODEL_DATA_TYPE = ModelReturnFullName,
                TYPE_QUEUE_PROCESS_ID = (int)enumTypeQueue_Process.QLTSC
            };
            _itemService.InsertQueueProcess(queue);
            _hoatDongService.InsertHoatDong(enumHoatDong.TaoMoi, "Tạo file báo cáo", queue, "QueueProcess");
            return queue;
        }
    }
}