using DevExpress.DataProcessing.InMemoryDataProcessor;
using DevExpress.XtraPrinting.Native;
using GS.Core;
using GS.Core.Data;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Data;
using GS.Services.DB;
using GS.Services.HeThong;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace GS.Services.BaoCaos
{
    public partial class QueueProcessService : IQueueProcessService
    {
        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IWorkContext _workContext;
        private readonly IRepository<QueueProcess> _itemRepository;
        //private readonly IRepository<QueueProcessSearch> _itemSearchRepository;
        private readonly ICauHinhService _cauHinhService;
        private readonly INguoiDungService _nguoiDungService;
        private readonly IDB_QueueProcessService _dB_QueueProcessService;

        #endregion Fields

        #region Ctor

        public QueueProcessService(
            IDbContext dbContext,
            IRepository<QueueProcess> itemRepository,
            //IRepository<QueueProcessSearch> itemSearchRepository,
            IWorkContext workContext,
            ICauHinhService cauHinhService,
            INguoiDungService nguoiDungService,
            IDB_QueueProcessService dB_QueueProcessService
            )
        {
            this._dbContext = dbContext;
            this._itemRepository = itemRepository;
            //this._itemSearchRepository = itemSearchRepository;
            this._workContext = workContext;
            this._cauHinhService = cauHinhService;
            this._nguoiDungService = nguoiDungService;
            this._dB_QueueProcessService = dB_QueueProcessService;
        }

        #endregion Ctor

        #region Methods

        public void DeleteQueueProcess(QueueProcess entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
        }

        public IList<QueueProcess> GetAllQueueProcesss()
        {
            return _itemRepository.Table.ToList();
        }

        public QueueProcess GetQueueProcessById(decimal Id)
        {
            if (Id == 0)
                return new QueueProcess();
            return _itemRepository.GetById(Id);
        }

        public QueueProcess GetQueueProcessByGuid(Guid guid)
        {
            var query = _itemRepository.Table.Where(c => c.GUID == guid);
            return query.FirstOrDefault();
        }

        public IList<QueueProcess> GetQueueProcessByIds(IList<decimal> newsIds)
        {
            if (newsIds == null || newsIds.Count == 0)
                return new List<QueueProcess>();
            return _itemRepository.Table.Where(p => newsIds.Contains((int)p.ID)).ToList();
        }

        public IList<QueueProcess> GetQueueProcesss(decimal? DonViId = 0)
        {
            return _itemRepository.Table.Where(p => p.DON_VI_ID == DonViId).ToList();
        }

        public void InsertQueueProcessDongBo(QueueProcess entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.GUID = Guid.NewGuid();
            entity.ID = 0;
            entity.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_TRANG_THAI_CHO;
            entity.NGAY_TAO = DateTime.Now;
            _itemRepository.Insert(entity);
        }

        public void InsertQueueProcess(QueueProcess entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.GUID = Guid.NewGuid();
            entity.ID = 0;
            entity.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.TRANG_THAI_CHO;
            entity.NGAY_TAO = DateTime.Now;
            _itemRepository.Insert(entity);
        }

        public IPagedList<QueueProcessSearch> SearchQueueProcesss(int pageIndex = 0, int pageSize = int.MaxValue, decimal? DonViId = 0, string KeySearch = null, DateTime? FromDate = null, DateTime? ToDate = null, Decimal? NguoiTaoId = 0, decimal TrangThaiId = -1, bool? isDongBo = null, Guid? guid = null, String guidResponse = null)
        {
            var query = _itemRepository.Table.Select(c => new QueueProcessSearch() {
                ID = c.ID,
                MA_BAO_CAO = c.MA_BAO_CAO,
                DON_VI_ID = c.DON_VI_ID,
                TYPE_QUEUE_PROCESS_ID = c.TYPE_QUEUE_PROCESS_ID,
                TRANG_THAI_ID = c.TRANG_THAI_ID,
                NGAY_TAO = c.NGAY_TAO,
                NGUOI_TAO_ID = c.NGUOI_TAO_ID,
                GUID = c.GUID,
                FILE_TYPE = c.FILE_TYPE,
                GUID_FILE = c.GUID_FILE,
                TIME_START_GET_DATA = c.TIME_START_GET_DATA,
                TIME_END_GET_DATA = c.TIME_END_GET_DATA,
                STATEMENT = c.STATEMENT,
                SEARCH_MODEL_JSON = c.SEARCH_MODEL_JSON,
                SEARCH_MODEL_CLASS = c.SEARCH_MODEL_CLASS,
                REPORT_CLASS = c.REPORT_CLASS,
                MODEL_DATA_TYPE = c.MODEL_DATA_TYPE,
                RESPONSE = c.RESPONSE,
                DB_QUEUE_PROCESS_ID = c.DB_QUEUE_PROCESS_ID
            });
            if (DonViId > 0)
                query = query.Where(p => p.DON_VI_ID == DonViId);
            if (!string.IsNullOrEmpty(KeySearch))
                query = query.Where(p => p.MA_BAO_CAO.ToUpper().Contains(KeySearch.ToUpper()));
            if (FromDate != null)
                query = query.Where(p => p.NGAY_TAO.Date >= FromDate);
            if (ToDate != null)
                query = query.Where(p => p.NGAY_TAO.Date <= ToDate);
            if (NguoiTaoId > 0)
                query = query.Where(p => p.NGUOI_TAO_ID == NguoiTaoId || p.NGUOI_TAO_ID == null);
            if (TrangThaiId >= 0)
                query = query.Where(p => p.TRANG_THAI_ID == TrangThaiId);

            //if (isDongBo == true)
            //{
            //    query = query.Where(c => c.TRANG_THAI_ID >= (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_TRANG_THAI_CHO);
            //}
            //else if (isDongBo == false)
            //{
            //    query = query.Where(c => c.TRANG_THAI_ID < (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_TRANG_THAI_CHO);
            //}

            if (guid == null || (guid != null && guid != new Guid() && guid != Guid.Empty))
                query = query.Where(p => p.GUID == guid);
            if (!String.IsNullOrEmpty(guidResponse))
            {
                query = query.Where(p => p.RESPONSE.Contains(guidResponse));
                //DB_QueueProcess dB_QueueProcess = _dB_QueueProcessService.GetAllDB_QueueProcesss(guidResponse: guidResponse).First();
                //query = query.Where(p => p.ID == dB_QueueProcess.DOI_TUONG_ID);
            }

            return new PagedList<QueueProcessSearch>(query.OrderByDescending(p => p.NGAY_TAO).ToList(), pageIndex, pageSize);
        }

        //public IPagedList<QueueProcessSearch> SearchQueueProcesss2(int pageIndex = 0, int pageSize = int.MaxValue, decimal? DonViId = 0, string KeySearch = null, DateTime? FromDate = null, DateTime? ToDate = null, Decimal? NguoiTaoId = 0, decimal TrangThaiId = -1, bool? isDongBo = null, Guid? guid = null, String guidResponse = null)
        //{
        //    var query = _itemSearchRepository.Table;
        //    if (DonViId > 0)
        //        query = query.Where(p => p.DON_VI_ID == DonViId);
        //    if (!string.IsNullOrEmpty(KeySearch))
        //        query = query.Where(p => p.MA_BAO_CAO.ToUpper().Contains(KeySearch.ToUpper()));
        //    if (FromDate != null)
        //        query = query.Where(p => p.NGAY_TAO.Date >= FromDate);
        //    if (ToDate != null)
        //        query = query.Where(p => p.NGAY_TAO.Date <= ToDate);
        //    if (NguoiTaoId > 0)
        //        query = query.Where(p => p.NGUOI_TAO_ID == NguoiTaoId || p.NGUOI_TAO_ID == null);
        //    if (TrangThaiId >= 0)
        //        query = query.Where(p => p.TRANG_THAI_ID == TrangThaiId);
        //    if (isDongBo == true)
        //        query = query.Where(p => p.TYPE_QUEUE_PROCESS_ID == (int)enumTypeQueue_Process.CSDLQGTSC);
        //    if (guid == null || (guid != null && guid != new Guid() && guid != Guid.Empty))
        //        query = query.Where(p => p.GUID == guid);
        //    if (!String.IsNullOrEmpty(guidResponse))
        //    {
        //        query = query.Where(p => p.RESPONSE.Contains(guidResponse));
        //        //DB_QueueProcess dB_QueueProcess = _dB_QueueProcessService.GetAllDB_QueueProcesss(guidResponse: guidResponse).First();
        //        //query = query.Where(p => p.ID == dB_QueueProcess.DOI_TUONG_ID);
        //    }
        //    return new PagedList<QueueProcessSearch>(query.OrderByDescending(p => p.NGAY_TAO), pageIndex, pageSize);
        //}

        public void UpdateQueueProcess(QueueProcess entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
        }

        public void UpdateQueueProcessTrangThaiAndGUIDFILE(QueueProcess entity)
        {
            if (entity == null)
                throw new ArgumentException(nameof(entity));
            OracleParameter p_ID = new OracleParameter("p_ID", OracleDbType.Int32, entity.ID, ParameterDirection.Input);
            OracleParameter p_TRANG_THAI_ID = new OracleParameter("p_TRANG_THAI_ID", OracleDbType.Clob, entity.TRANG_THAI_ID, ParameterDirection.Input);
            OracleParameter p_GUID_FILE = new OracleParameter("p_GUID_FILE", OracleDbType.Raw, entity.GUID_FILE, ParameterDirection.Input);
            string sql = "UPDATE bc_queue_process q SET q.trang_thai_id = :p_TRANG_THAI_ID , q.guid_file = :p_GUID_FILE where q.id = :p_ID ;";
            _dbContext.ExecuteSqlCommand(sql, false, null, p_ID, p_TRANG_THAI_ID, p_GUID_FILE);
        }

        //public decimal InsertQueueProcess(string p_MaBaoCao, params object[] parameters)
        //{
        //	var cauHinhDonVi = _cauHinhService.LoadCauHinh<DonViCauHinh>(_workContext.CurrentDonVi.ID);
        //	if (cauHinhDonVi.BaoCao == null)
        //		return 0;
        //	var cauHinhModel = cauHinhDonVi.BaoCao.toEntities<CauHinhBaoCao>().Where(c => c.MaBaoCao == p_MaBaoCao).FirstOrDefault();
        //	if (cauHinhModel == null || cauHinhModel.TenProcedure == null)
        //		return 0;
        //	string sql = GenerateStatment(cauHinhModel.TenProcedure, parameters);
        //	QueueProcess queue = new QueueProcess()
        //	{
        //		MA_BAO_CAO = p_MaBaoCao,
        //		STATEMENT = sql
        //	};
        //	InsertQueueProcess(queue);
        //	return queue.ID;
        //}

        public string GenerateStatment(string sql, params object[] parameters)
        {
            string _null = "NULL";
            for (var i = 0; i <= (parameters?.Length ?? 0) - 1; i++)
            {
                if (!(parameters[i] is DbParameter parameter) || parameter.Direction == ParameterDirection.Output || parameter.ParameterName == "TBL_OUT")
                    continue;
                else if (parameter.DbType == DbType.String || parameter.DbType == DbType.String)
                    sql = $"{sql}{(i > 0 ? "," : "(")} {parameter.ParameterName} => '{parameter.Value}'";
                else if (parameter.DbType == DbType.Date)
                    sql = $"{sql}{(i > 0 ? "," : "(")} {parameter.ParameterName} => TO_DATE('{(parameter.Value as DateTime?).toDateVNString()}','DD/MM/YYYY')";
                else
                {
                    var val = parameter.Value != null ? parameter.Value.ToString().Replace(',', '.') : _null;

                    sql = $"{sql}{(i > 0 ? "," : "(")} {parameter.ParameterName} => {val}";
                }
            }
            sql = $"BEGIN {sql}, TBL_OUT => :l_TBL_OUT); END;";
            return sql;
        }

        //public string GetDuLieuBaoCaoJson(decimal id)
        //{
        //	OracleParameter p_id_bc_queue = new OracleParameter("p_id_bc_queue", OracleDbType.Decimal, id, ParameterDirection.Input);
        //	//var a =  _dbContext.ExecuteSqlCommand("BEGIN SP_GET_DATA_REPORT(:p_id_bc_queue); END;", true, null, p_id_bc_queue);
        //	var a = _dbContext.DoWork("BEGIN SP_GET_DATA_REPORT(:p_id_bc_queue); END;", p_id_bc_queue);
        //	var res = GetQueueProcessById(id);
        //	if (res != null)
        //		return res.DATA_JSON;
        //	else
        //		return "";
        //}

        public QueueProcess GetQueueTheoNguoiDung(Guid NguoiDungGuid)
        {
            NguoiDung nguoiDung = _nguoiDungService.GetNguoiDungByGuid(NguoiDungGuid);
            if (nguoiDung != null)
            {
                var item = _itemRepository.Table.Where(p => p.NGUOI_TAO_ID == nguoiDung.ID).OrderByDescending(p => p.ID).FirstOrDefault();
                return item;
            }
            return null;
        }

        public bool CheckCanCreateThread(decimal NguoiDungId)
        {
            var cauHinhThreadBaoCao = _cauHinhService.LoadCauHinh<CauHinhThreadBaoCao>();
            // trạng thái chờ không được lớn hơn max
            // người dùng không được có tiến trình đang chạy
            if (CountQueueTrangThai(enumTRANG_THAI_QUEUE_BAO_CAO.TRANG_THAI_CHO) >= cauHinhThreadBaoCao.ThreadBaoCao_MaxThreadWaiting ||
                CountQueueTrangThai(enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU, NguoiDungId) > 0 ||
                CountQueueTrangThai(enumTRANG_THAI_QUEUE_BAO_CAO.TRANG_THAI_CHO, NguoiDungId) > 0)
                return false;
            return true;
        }

        public int CountQueueTrangThai(enumTRANG_THAI_QUEUE_BAO_CAO TrangThai, decimal NguoiTaoId = 0)
        {
            var q = _itemRepository.Table.Where(p => p.TRANG_THAI_ID == (int)TrangThai);
            if (NguoiTaoId > 0)
                q = q.Where(p => p.NGUOI_TAO_ID == NguoiTaoId);
            return q.Count();
        }

        public void UpdateStartLayDuLieu(decimal id)
        {
            var item = GetQueueProcessById(id);
            item.TIME_START_GET_DATA = DateTime.Now;
            item.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_LAY_DU_LIEU;
            UpdateQueueProcess(item);
        }

        public void UpdateStopLayDuLieu(decimal id)
        {
            var item = GetQueueProcessById(id);
            item.TIME_END_GET_DATA = DateTime.Now;
            item.TRANG_THAI_ID = (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_XUAT_FILE;
            UpdateQueueProcess(item);
        }

        public QueueProcess GetQueueProcessGanNhat(decimal NguoiTaoId, decimal DonVi, string MaBaoCao)
        {
            return _itemRepository.Table.Where(p =>
            p.NGUOI_TAO_ID == NguoiTaoId &&
            p.DON_VI_ID == DonVi &&
            p.MA_BAO_CAO == MaBaoCao).OrderByDescending(p => p.NGAY_TAO).FirstOrDefault();
        }

        public QueueProcess GetQueueProcessCanXuatFileBaoCao()
        {
            var res = _itemRepository.Table.Where(p => p.TRANG_THAI_ID == (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_CHO_XUAT_FILE).OrderBy(p => p.NGAY_TAO).FirstOrDefault();
            return res;
        }

        /// <summary>
        /// get QueueProcess đồng bộ báo cáo gần nhất
        /// </summary>
        /// <returns></returns>
        public QueueProcess GetQueueProcessBaoCao(bool isDongBo = false)
        {
            int trangThai = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_TRANG_THAI_CHO : (int)enumTRANG_THAI_QUEUE_BAO_CAO.TRANG_THAI_CHO;
            var res = _itemRepository.Table.Where(p => p.TRANG_THAI_ID == trangThai).OrderBy(p => p.NGAY_TAO).FirstOrDefault();
            return res;
        }
        /// <summary>
        /// get QueueProcess đồng bộ báo cáo gần nhất cần render
        /// </summary>
        /// <returns></returns>
        public QueueProcess GetQueueProcessBaoCaoCanRender(bool isDongBo = false)
        {
            int trangThai = isDongBo ? (int)enumTRANG_THAI_QUEUE_BAO_CAO.DB_DANG_CHO_XUAT_FILE : (int)enumTRANG_THAI_QUEUE_BAO_CAO.DANG_CHO_XUAT_FILE;
            var res = _itemRepository.Table.Where(p => p.TRANG_THAI_ID == trangThai).OrderBy(p => p.NGAY_TAO).FirstOrDefault();
            return res;
        }
        public String GetDataByStatement<T>(String Statement) where T : BaseViewEntity
        {
            OracleParameter P_STATEMENT = new OracleParameter("P_STATEMENT", OracleDbType.Varchar2, Statement, ParameterDirection.Input);
            OracleParameter l_TBL_OUT = new OracleParameter("l_TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityViewFromSql<T>("SP_GET_REPORT", P_STATEMENT, l_TBL_OUT).ToList();
                return result.toStringJson(isIgnoreNull: true);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<T> GetListDataByStatement<T>(String Statement) where T : BaseViewEntity
        {
            OracleParameter P_STATEMENT = new OracleParameter("P_STATEMENT", OracleDbType.Varchar2, Statement, ParameterDirection.Input);
            OracleParameter l_TBL_OUT = new OracleParameter("l_TBL_OUT", OracleDbType.RefCursor, ParameterDirection.Output);
            try
            {
                var result = _dbContext.EntityViewFromSql<T>("SP_GET_REPORT", P_STATEMENT, l_TBL_OUT).ToList();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion Methods
    }
}