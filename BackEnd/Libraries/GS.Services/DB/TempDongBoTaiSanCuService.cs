using GS.Core.Data;
using GS.Core.Domain.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GS.Services.DB
{
    public class TempDongBoTaiSanCuService : ITempDongBoTaiSanCuService
    {
        #region Field       
        private readonly IRepository<TempDongBoTaiSanCu> _itemRepository;
        #endregion

        #region Ctor

        public TempDongBoTaiSanCuService(
            IRepository<TempDongBoTaiSanCu> itemRepository
            )
        {

            this._itemRepository = itemRepository;
        }

        #endregion

        public IList<TempDongBoTaiSanCu> GetAllTempDongBoTaiSanCus()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public IList<TempDongBoTaiSanCu> GetTempDongBoTaiSanCus(decimal? TrangThaiId = null, DateTime? NgayTao = null, decimal LoaiHinhTaiSanId = 0, decimal LoaiBienDong = 0,decimal DonViId = 0)
        {
            var query = _itemRepository.Table;
            if(TrangThaiId!= null && TrangThaiId >= 0)
            {
                query.Where(c => c.TRANG_THAI_ID == TrangThaiId);
            }
            if (NgayTao != null)
            {
                query.Where(c => c.NGAY_TAO == NgayTao);
            }
            if (LoaiHinhTaiSanId >0)
            {
                query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSanId);
            }
            if (LoaiBienDong > 0)
            {
                query.Where(c => c.LOAI_BIEN_DONG_ID == LoaiBienDong);
            }
            if (DonViId > 0)
            {
                query.Where(c => c.DON_VI_ID == DonViId);
            }
            return query.ToList();
        }
        public void InsertTempDongBoTaiSanCu(TempDongBoTaiSanCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
        }

        public void UpdateTempDongBoTaiSanCu(TempDongBoTaiSanCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
        }
        public void DeleteTempDongBoTaiSanCu(TempDongBoTaiSanCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
        }
    }
}
