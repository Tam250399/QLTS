using DevExpress.DataProcessing;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.NewAPI.Models;
using GS.NewAPI.Models.DanhMuc;
using GS.Services.DanhMuc;
///using GS.Web.Models.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.NewAPI.Factories
{
    public class DanhMucModelFactory : IDanhMucModelFactory
    {
        #region Ctor
        private readonly IQuocGiaService _quocGiaService;
        private readonly ILyDoBienDongService _lyDoBienDongService;
        /// <summary>
        private readonly IMucDichSuDungService _mucDichSuDungService;
        private readonly IDonViBoPhanService _donViBoPhanService;
        /// </summary>
        private readonly IDiaBanService _diaBanService;

        public DanhMucModelFactory(
            IQuocGiaService quocGiaService,
            ILyDoBienDongService lyDoBienDongService,
            IDonViBoPhanService donViBoPhanService,
            IMucDichSuDungService mucDichSuDungService,
            IDiaBanService diaBanService
            )
        {
            this._quocGiaService = quocGiaService;
            this._lyDoBienDongService = lyDoBienDongService;
            this._mucDichSuDungService = mucDichSuDungService;
            this._donViBoPhanService = donViBoPhanService;
            this._diaBanService = diaBanService;
        }
        #endregion
        #region quốc gia
        public IList<QuocGiaModel> GetAllQuocGias()
        {
            var query = _quocGiaService.GetAllQuocGias();
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        public IList<QuocGiaModel> SearchQuocGiasByName(string tenQuocGia)
        {
            var query = _quocGiaService.SearchQuocGias(Keysearch: tenQuocGia);
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        public MessageReturn DeleteQuocGia(decimal ID = 0)
        {
            QuocGia quocGia = _quocGiaService.GetQuocGiaById(Id: ID);
            try
            {
                if (quocGia.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                quocGia.DB_ID = null;
                _quocGiaService.UpdateQuocGia(quocGia);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID invalid");
            }

        }


        #endregion

        #region Địa bàn
        public IList<DiaBanModel> GetTinhThanhPhosByQuocGiaId(int quocGiaId)
        {
            var query = _diaBanService.GetDiaBans(CapDiaban:1, QuocGiaId: quocGiaId);
            return query.Select(m => m.ToModel<DiaBanModel>()).ToList();
        }

        public IList<DiaBanModel> GetDiaBansByMaCha(string maCha)
        {
            var query = _diaBanService.GetDiaBansByMaCha(maCha);
            return query.Select(m => m.ToModel<DiaBanModel>()).ToList();
        }
        public IList<MucDichSuDungModel> GetMucDichSuDungsByLoaiHinhTSId(decimal? loaiHinhTaiSanId)
        {
            var query = _mucDichSuDungService.GetMucDichSuDungsByLoaiHinhTSId(loaiHinhTaiSanId);
            return query.Select(m => m.ToModel<MucDichSuDungModel>()).ToList();
        }
        #endregion

        public IList<LyDoBienDongModel> GetLyDoTangGiams(decimal? loaiLyDoBienDongId = 0, decimal? loaiHinhTaiSanId = 0, Boolean isTangMoi = false)
        {

            var query = _lyDoBienDongService.GetLyDoTangGiams(loaiLyDoBienDongId: loaiLyDoBienDongId, loaiHinhTaiSanId: loaiHinhTaiSanId, isTangMoi: isTangMoi);
            Boolean a = false;
            return query.Select(m => m.ToModel<LyDoBienDongModel>()).ToList();
        }

        public IList<DonViBoPhanModel> GetDonViBoPhans(decimal donViId)
        {

            var query = _donViBoPhanService.GetDonViBoPhans(donViId);
            Boolean a = false;
            return query.Select(m => m.ToModel<DonViBoPhanModel>()).ToList();
        }
    }
}
