using GS.Core;
using GS.Core.Domain.BaoCaos;
using System;
using System.Collections.Generic;

namespace GS.Services.BaoCaos
{
    public partial interface IQueueProcessService
    {
        #region QueueProcess

        IList<QueueProcess> GetAllQueueProcesss();

        IList<QueueProcess> GetQueueProcesss(decimal? DonViId = 0);

        void InsertQueueProcess(QueueProcess entity);

        IPagedList<QueueProcessSearch> SearchQueueProcesss(int pageIndex = 0, int pageSize = int.MaxValue, Decimal? DonViId = 0, string KeySearch = null, DateTime? FromDate = null, DateTime? ToDate = null, Decimal? NguoiTaoId = 0, decimal TrangThaiId = -1, bool? isDongBo = null, Guid? guid = null, String guidResponse = null);

        QueueProcess GetQueueProcessById(decimal Id);

        QueueProcess GetQueueProcessByGuid(Guid guid);

        IList<QueueProcess> GetQueueProcessByIds(IList<decimal> newsIds);

        //decimal InsertQueueProcess(string p_MaBaoCao, params object[] parameters);

        void UpdateQueueProcess(QueueProcess entity);
        void UpdateQueueProcessTrangThaiAndGUIDFILE(QueueProcess entity);

        void DeleteQueueProcess(QueueProcess entity);

        //string GetDuLieuBaoCaoJson(decimal id);
        string GenerateStatment(string sql, params object[] parameters);
        QueueProcess GetQueueTheoNguoiDung(Guid NguoiDungGuid);

        bool CheckCanCreateThread(decimal NguoiDungId);

        void UpdateStartLayDuLieu(decimal id);

        void UpdateStopLayDuLieu(decimal id);

        int CountQueueTrangThai(enumTRANG_THAI_QUEUE_BAO_CAO TrangThai, decimal NguoiTaoId = 0);

        QueueProcess GetQueueProcessGanNhat(decimal NguoiTaoId, decimal DonVi, string MaBaoCao);
        QueueProcess GetQueueProcessCanXuatFileBaoCao();
        #endregion QueueProcess
        void InsertQueueProcessDongBo(QueueProcess entity);
        String GetDataByStatement<T>(String Statement) where T : BaseViewEntity;
        QueueProcess GetQueueProcessBaoCao(bool isDongBo = false);
        List<T> GetListDataByStatement<T>(string Statement) where T : BaseViewEntity;
        QueueProcess GetQueueProcessBaoCaoCanRender(bool isDongBo = false);
        //IPagedList<QueueProcessSearch> SearchQueueProcesss2(int pageIndex = 0, int pageSize = int.MaxValue, decimal? DonViId = 0, string KeySearch = null, DateTime? FromDate = null, DateTime? ToDate = null, decimal? NguoiTaoId = 0, decimal TrangThaiId = -1, bool? isDongBo = null, Guid? guid = null, string guidResponse = null);
    }
}