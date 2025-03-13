//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Data;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Core.Domain.CCDC;
using System.IO;
using GS.Core.Domain.Common;
using OfficeOpenXml;
using Oracle.ManagedDataAccess.Client;
using GS.Core.Domain.Api.TaiSanDBApi;

namespace GS.Services.CCDC
{
    public partial class CongCuService : ICongCuService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<CongCu> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<XuatNhap> _xuatNhapRepository;
        private readonly IRepository<NhapXuatCongCu> _xuatNhapCongCuRepository;
        #endregion

        #region Ctor

        public CongCuService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext,
            IRepository<CongCu> itemRepository,
            IRepository<XuatNhap> xuatNhapRepository,
            IRepository<NhapXuatCongCu> xuatNhapCongCuRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._xuatNhapRepository = xuatNhapRepository;
            this._xuatNhapCongCuRepository = xuatNhapCongCuRepository;
        }

        #endregion

        #region Methods
        public virtual IList<CongCu> GetAllCongCus(String ma_db = "", decimal? donViId = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(ma_db))
                query = query.Where(c => c.MA_DB == ma_db);
            if (donViId.GetValueOrDefault(0) > 0)
                query = query.Where(c => c.DON_VI_ID == donViId);

            return query.ToList();
        }
        public virtual IList<CongCu> SearchCongCuDongBo(string keySearch = null)
        {
            var query = _itemRepository.Table;
            if (string.IsNullOrEmpty(keySearch))
                return new List<CongCu>();
            keySearch = keySearch.ToUpper().Trim();
            query = query.Where(c => (c.MA_DB != null && c.MA_DB.ToUpper().Contains(keySearch)) || c.MA.ToUpper().Contains(keySearch) || c.TEN.ToUpper().Contains(keySearch));
            return query.ToList();
        }

        public virtual CongCu GetCongCuByGuid(Guid GUID)
        {
            var query = _itemRepository.Table.Where(c => c.GUID == GUID);
            return query.FirstOrDefault();
        }

        public virtual IPagedList<CongCu> SearchCongCus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, bool isPhanBo = false, Decimal XuatNhapId = 0)
        {
            var query = _itemRepository.Table;
            if (Keysearch != null)
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower()) || c.MA.ToLower().Contains(Keysearch.ToLower()));
            if (isPhanBo)
            {
                query = from c in query
                        join m in _xuatNhapCongCuRepository.Table on c.ID equals m.CONG_CU_ID
                        join x in _xuatNhapRepository.Table on m.NHAP_XUAT_ID equals x.ID
                        where x.IS_XUAT == false && x.FROM_XUAT_NHAP_ID == null && x.DON_VI_BO_PHAN_ID == null && x.DON_VI_ID == _workContext.CurrentDonVi.ID
                        select c;
            }
            if (XuatNhapId > 0)
            {
                query = from c in query
                        join m in _xuatNhapCongCuRepository.Table on c.ID equals m.CONG_CU_ID
                        join x in _xuatNhapRepository.Table on m.NHAP_XUAT_ID equals x.ID
                        where m.NHAP_XUAT_ID == XuatNhapId && x.DON_VI_ID == _workContext.CurrentDonVi.ID
                        select c;
            }
            return new PagedList<CongCu>(query.OrderByDescending(c => c.NGAY_TAO), pageIndex, pageSize); ;
        }

        public virtual CongCu GetCongCuById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        public virtual CongCu GetCongCuByMaDB(string maDB)
        {
            if (string.IsNullOrEmpty(maDB))
                return null;
            return _itemRepository.Table.Where(c => c.MA_DB == maDB).FirstOrDefault();
        }
        public virtual CongCu GetCongCu(string ma = null, string madb = null, decimal? donViId = null)
        {
            var query = _itemRepository.Table;
            if (String.IsNullOrEmpty(ma)
                && String.IsNullOrEmpty(madb))
                return null;
            if (donViId.GetValueOrDefault(0) > 0)
                query = query.Where(c => c.DON_VI_ID == donViId);
            if (!String.IsNullOrEmpty(ma))
                query = query.Where(c => c.MA == ma);
            if (!String.IsNullOrEmpty(madb))
                query = query.Where(c => c.MA_DB == madb);
            return query.FirstOrDefault();
        }

        public virtual IList<CongCu> GetCongCuByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual IList<CongCu> GetCongCus(Decimal? NhapXuatId = 0, Decimal? NhomCongCuId = 0, Decimal DonViId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID);
            if (NhapXuatId > 0)
            {
                query = from c in query
                        join m in _xuatNhapCongCuRepository.Table on c.ID equals m.CONG_CU_ID
                        join x in _xuatNhapRepository.Table on m.NHAP_XUAT_ID equals x.ID
                        where m.NHAP_XUAT_ID == NhapXuatId
                        select c;
            }
            if (NhomCongCuId >= 0)
            {
                query = query.Where(c => c.NHOM_CONG_CU_ID == NhomCongCuId);
            }
            if (DonViId > 0)
            {
                query = query.Where(c => c.DON_VI_ID == DonViId);
            }
            return query.OrderByDescending(c => c.NGAY_TAO).ToList();
        }

        public virtual void InsertCongCu(CongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
            if (entity.DON_VI_ID == 0)
                entity.DON_VI_ID = _workContext.CurrentDonVi.ID;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }

        public virtual Decimal? GetValueIdMax()
        {
            //var idMax = _itemRepository.Table.OrderByDescending(c => c.ID).Select(c => c.ID).FirstOrDefault();
            var idMax = _itemRepository.Table.DefaultIfEmpty().Max(c => c.ID);

            return idMax;
        }

        public virtual void UpdateCongCu(CongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteCongCu(CongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

        public virtual List<MessageReturn> ImportExcel(Stream stream)
        {
            var package = new ExcelPackage(stream);
            var workSheets = package.Workbook.Worksheets;
            List<MessageReturn> ListResult = new List<MessageReturn>();
            if (workSheets.Count < 1)
            {
                //ErrorMessageJson("File không chuẩn dữ liệu");
                return null;
            }
            foreach (var item in workSheets)
            {
                for (var rowNumber = 2; rowNumber <= item.Dimension.End.Row; rowNumber++)
                {
                    //.Cells[FromRow, FromCol, ToRow, ToCol] 
                    var row = item.Cells[rowNumber, 1, rowNumber, item.Dimension.End.Column];
                    var loaiCCDC = row[rowNumber, 1].Text;
                    var maCCDC = row[rowNumber, 2].Text;
                    var tenCCDC = row[rowNumber, 3].Text;
                    var boPhanSuDung = row[rowNumber, 4];
                    var ngayGhiTang = row[rowNumber, 5];
                    var soLuong = row[rowNumber, 6];
                    var donViTinh = row[rowNumber, 7];
                    var donGiaMua = row[rowNumber, 8];
                    var thanhTien = row[rowNumber, 9];
                    var maNguonGoc = row[rowNumber, 10];
                    var maTinhTrang = row[rowNumber, 11];
                    var soChungTu = row[rowNumber, 12];

                    //Ma loại tài sản
                    //string SoLuong = row[rowNumber, 5].Text;
                    //{
                    //    foreach (var ts in SoLuong)
                    //    {
                    //        TaiSan taiSan = new TaiSan();
                    //        var LoaiTaiSanValue = row[rowNumber, 1].Text;
                    //        string MaLoaiTaiSan = LoaiTaiSanValue.Split('-')[0];
                    //        if (!string.IsNullOrEmpty(MaLoaiTaiSan))
                    //        {
                    //            LoaiTaiSan loaiTaiSan = _loaiTaiSanService.GetCacheLoaiTaiSanByMa(MaLoaiTaiSan.Trim());
                    //            if (loaiTaiSan == null)
                    //            {
                    //                ListResult.Add(new MessageReturn(MessageReturn.ERROR_CODE, "LOAI_TAI_SAN"));
                    //                break;
                    //            }
                    //            taiSan.LOAI_TAI_SAN_ID = loaiTaiSan.ID;
                    //            taiSan.LOAI_HINH_TAI_SAN_ID = loaiTaiSan.LOAI_HINH_TAI_SAN_ID;
                    //            taiSan.TEN = row[rowNumber, 2].Text;
                    //            if (string.IsNullOrEmpty(taiSan.TEN))
                    //            {
                    //                ListResult.Add(new MessageReturn(MessageReturn.ERROR_CODE, "TEN_TAI_SAN"));
                    //                break;
                    //            }
                    //            // nguyên giá
                    //            string NguyenGia = row[rowNumber, 6].Text;
                    //            if (!string.IsNullOrEmpty(NguyenGia))
                    //            {
                    //                taiSan.NGUYEN_GIA_BAN_DAU = decimal.Parse(row[rowNumber, 6].Text);
                    //            }
                    //            //ngày bắt đầu sử dụng
                    //            string NgayBatDauSuDung = row[rowNumber, 13].Text;
                    //            if (!string.IsNullOrEmpty(NgayBatDauSuDung))
                    //            {
                    //                taiSan.NGAY_SU_DUNG = DateTime.Parse(NgayBatDauSuDung);
                    //            }
                    //            //Ngay nhập
                    //            string NgayNhap = row[rowNumber, 12].Text;
                    //            if (!string.IsNullOrEmpty(NgayNhap))
                    //            {
                    //                taiSan.NGAY_NHAP = DateTime.Parse(NgayBatDauSuDung);
                    //            }
                    //            taiSan.DON_VI_ID = _workContext.CurrentDonVi.ID;
                    //            taiSan.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                    //            // đơn vị bộ phận
                    //            string TenBoPhan = row[rowNumber, 4].Text.Trim();
                    //            if (string.IsNullOrEmpty(TenBoPhan.Trim()))
                    //            {
                    //                DonViBoPhan donViBoPhan = _donViBoPhanService.GetCacheDonViBoPhanByTen(TenBoPhan);
                    //                if (donViBoPhan == null)
                    //                {
                    //                    donViBoPhan = new DonViBoPhan();
                    //                    donViBoPhan.TEN = TenBoPhan;
                    //                    donViBoPhan.DON_VI_ID = _workContext.CurrentDonVi.ID;
                    //                    _donViBoPhanService.InsertDonViBoPhan(donViBoPhan);
                    //                }
                    //                taiSan.DON_VI_BO_PHAN_ID = donViBoPhan.ID;
                    //            }
                    //            taiSan.NGAY_NHAP = taiSan.NGAY_NHAP ?? DateTime.Now;
                    //            _taiSanService.InsertTaiSan(taiSan, true);
                    //            // cập nhật mã tài sản
                    //            if (taiSan.LOAI_HINH_TAI_SAN_ID == (decimal)enumLOAI_HINH_TAI_SAN.VO_HINH)
                    //            {
                    //                // get tài sản vô hình  gốc
                    //                decimal? parentId = taiSan.LOAI_TAI_SAN_DON_VI_ID;
                    //                LoaiTaiSanVoHinh taiSanVoHinh = new LoaiTaiSanVoHinh();
                    //                do
                    //                {
                    //                    taiSanVoHinh = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhById(parentId.Value);
                    //                    parentId = taiSanVoHinh.PARENT_ID;
                    //                } while (parentId != null);
                    //                //var LoaiTaiSanVoHinhCha = _loaiTaiSanDonViServices.GetLoaiTaiSanVoHinhByMa()
                    //                taiSan.MA = CommonHelper.GenMaTaiSan(taiSan.donvi.MA, taiSanVoHinh.MA, taiSan.ID);
                    //            }
                    //            else
                    //            {
                    //                taiSan.MA = CommonHelper.GenMaTaiSan(taiSan.donvi.MA, taiSan.loaitaisan.MA, taiSan.ID);
                    //            }
                    //            // loại hình tài sản
                    //            switch (taiSan.LOAI_HINH_TAI_SAN_ID)
                    //            {
                    //                case (decimal)enumLOAI_HINH_TAI_SAN.DAC_THU:
                    //                    break;
                    //                case (decimal)enumLOAI_HINH_TAI_SAN.DAT:
                    //                    break;
                    //                case (decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC:
                    //                    break;
                    //                case (decimal)enumLOAI_HINH_TAI_SAN.NHA:
                    //                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC:
                    //                    break;
                    //                case (decimal)enumLOAI_HINH_TAI_SAN.OTO:
                    //                case (decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC:
                    //                    break;
                    //                case (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI:
                    //                    break;
                    //                default:
                    //                    break;
                    //            }
                    //            // yêu cầu
                    //            YeuCau yeuCau = new YeuCau();
                    //            yeuCau.TAI_SAN_ID = taiSan.ID;
                    //            yeuCau.TAI_SAN_MA = taiSan.MA;
                    //            yeuCau.TAI_SAN_TEN = taiSan.TEN;
                    //            yeuCau.LOAI_TAI_SAN_ID = taiSan.LOAI_TAI_SAN_ID;
                    //            yeuCau.LOAI_TAI_SAN_DON_VI_ID = taiSan.LOAI_TAI_SAN_DON_VI_ID;
                    //            yeuCau.DON_VI_BO_PHAN_ID = taiSan.DON_VI_BO_PHAN_ID;
                    //            yeuCau.NGAY_BIEN_DONG = taiSan.NGAY_NHAP;
                    //            yeuCau.NGAY_SU_DUNG = taiSan.NGAY_SU_DUNG;
                    //            yeuCau.NGUYEN_GIA = taiSan.NGUYEN_GIA_BAN_DAU;
                    //            yeuCau.TAI_SAN_MA = taiSan.MA;
                    //            yeuCau.TAI_SAN_TEN = taiSan.TEN;
                    //            yeuCau.LOAI_HINH_TAI_SAN_ID = taiSan.LOAI_HINH_TAI_SAN_ID;
                    //            yeuCau.TRANG_THAI_ID = (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET;
                    //            yeuCau.LOAI_BIEN_DONG_ID = (int)enumLOAI_LY_DO_TANG_GIAM.TANG_TOAN_BO;
                    //            //nguồn vốn
                    //            List<NguonVonJson> nguonVonJsons = new List<NguonVonJson>();
                    //            string NguonVonNganSach = row[rowNumber, 9].Text;
                    //            if (!string.IsNullOrEmpty(NguonVonNganSach))// nguồn vốn ngân sách
                    //            {
                    //                nguonVonJsons.Add(new NguonVonJson
                    //                {
                    //                    TEN = "Nguồn ngân sách",
                    //                    GiaTri = decimal.Parse(row[rowNumber, 9].Text),
                    //                    ID = 1
                    //                });
                    //            }
                    //            string NguonVonPTHDSN = row[rowNumber, 10].Text;
                    //            if (!string.IsNullOrEmpty(NguonVonPTHDSN))//Nguồn quỹ PT HĐSN
                    //            {
                    //                nguonVonJsons.Add(new NguonVonJson
                    //                {
                    //                    TEN = "Quỹ phát triển hoạt động sự nghiệp",
                    //                    GiaTri = decimal.Parse(row[rowNumber, 10].Text),
                    //                    ID = 2
                    //                });
                    //            }
                    //            string NguonVonVienTro = row[rowNumber, 11].Text;
                    //            if (!string.IsNullOrEmpty(NguonVonPTHDSN))//Nguồn quỹ PT HĐSN
                    //            {
                    //                nguonVonJsons.Add(new NguonVonJson
                    //                {
                    //                    TEN = "Nguồn viện trợ (ODA,...)",
                    //                    GiaTri = decimal.Parse(row[rowNumber, 11].Text),
                    //                    ID = 4
                    //                });
                    //            }
                    //            string NguonKhac = row[rowNumber, 11].Text;
                    //            if (!string.IsNullOrEmpty(NguonVonPTHDSN))//Nguồn quỹ PT HĐSN
                    //            {
                    //                nguonVonJsons.Add(new NguonVonJson
                    //                {
                    //                    TEN = "Nguồn viện trợ (ODA,...)",
                    //                    GiaTri = decimal.Parse(row[rowNumber, 11].Text),
                    //                    ID = 4
                    //                });
                    //            }

                    //            //yeuCau.NGUON_VON_JSON = 
                    //            //yeuCau.NGUOI_TAO_ID = _workContext.CurrentCustomer.ID;
                    //            _yeuCauService.InsertYeuCau(yeuCau);
                    //            YeuCauChiTiet yeuCauChiTiet = new YeuCauChiTiet();
                    //            string MucDichDuocGiao = row[rowNumber, 15].Text;
                    //            if (string.IsNullOrEmpty(MucDichDuocGiao))
                    //            {
                    //                ListResult.Add(new MessageReturn(MessageReturn.ERROR_CODE, "TEN_TAI_SAN"));
                    //                _yeuCauService.DeleteYeuCau(yeuCau);
                    //                _taiSanService.DeleteTaiSan(taiSan);
                    //                break;
                    //            }
                    //            else
                    //            {
                    //                MucDichSuDung mucDichSuDung = _mucDichSuDungService.GetMucDichSuDungByTen(MucDichDuocGiao);
                    //                if (mucDichSuDung == null)
                    //                {
                    //                    ListResult.Add(new MessageReturn(MessageReturn.ERROR_CODE, "TEN_TAI_SAN"));
                    //                    _yeuCauService.DeleteYeuCau(yeuCau);
                    //                    _taiSanService.DeleteTaiSan(taiSan);
                    //                }
                    //                yeuCauChiTiet.MUC_DICH_SU_DUNG_ID = mucDichSuDung.ID;
                    //                _yeuCauChiTietService.InsertYeuCauChiTiet(yeuCauChiTiet);

                    //            }

                    //        }
                    //    }
                    //}

                }
            }
            return ListResult;
        }

        public MessageReturn DelelteCongCuAndThongTinLienQuan(decimal CONG_CU_ID, decimal DEL_LO = 0)
        {
            try
            {
                OracleParameter P_CONG_CU_ID = new OracleParameter("P_CONG_CU_ID", OracleDbType.Decimal, CONG_CU_ID, ParameterDirection.Input);
                OracleParameter IS_DEL_LO = new OracleParameter("IS_DEL_LO", OracleDbType.Decimal, DEL_LO, ParameterDirection.Input);
                var result = _dbContext.ExecuteSqlCommand("BEGIN DELETE_CCDC( :P_CONG_CU_ID, :IS_DEL_LO); END;", false, null, P_CONG_CU_ID, IS_DEL_LO);
                return MessageReturn.CreateSuccessMessage("Success done");

            }
            catch (Exception ex)
            {
                //DelelteTaiSanDB(ma_db);
                return MessageReturn.CreateErrorMessage(ex.Message);
            }

        }
        public decimal GetDonGiaCCDC(decimal id)
        {
            NhapXuatCongCu nhapXuatCongCu = _xuatNhapCongCuRepository.Table.Where(c => c.CONG_CU_ID == id).OrderBy(c => c.ID).FirstOrDefault();
            if (nhapXuatCongCu != null)
                return nhapXuatCongCu.DON_GIA.GetValueOrDefault(0);
            return 0;
        }
        public virtual MessageReturn insertOrupdateCCDCByJson(string json)
        {
            try
            {
                OracleParameter P_JSON_STRING = new OracleParameter("P_JSON_STRING", OracleDbType.Clob, json, ParameterDirection.Input);
                OracleParameter MSS_OUT = new OracleParameter("MSS_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                var result = _dbContext.EntityFromStore<MssReturn>("INSERT_CCDC", P_JSON_STRING, MSS_OUT).ToList();
                MessageReturn mss = new MessageReturn();
                if (result != null && result.Count() > 0)
                    mss = result.FirstOrDefault().STRJSON.toEntity<MessageReturn>();
                return mss;
            }
            catch (Exception ex)
            {
                return new MessageReturn(MessageReturn.ERROR_CODE, ex.Message);
            }

        }
        public virtual MessageReturn insertOrupdateKiemKeByJson(string json)
        {
            try
            {
                OracleParameter P_JSON_STRING = new OracleParameter("P_JSON_STRING", OracleDbType.Clob, json, ParameterDirection.Input);
                OracleParameter MSS_OUT = new OracleParameter("MSS_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
                var result = _dbContext.EntityFromStore<MssReturn>("INSERT_KIEM_KE_CCDC", P_JSON_STRING, MSS_OUT).ToList();
                MessageReturn mss = new MessageReturn();
                if (result != null && result.Count() > 0)
                    mss = result.FirstOrDefault().STRJSON.toEntity<MessageReturn>();
                return mss;
            }
            catch (Exception ex)
            {
                return new MessageReturn(MessageReturn.ERROR_CODE, ex.Message);
            }

        }
        #endregion
    }
}

