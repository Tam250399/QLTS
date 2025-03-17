using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.NewAPI.Models;
using GS.NewAPI.Models.DanhMuc;
using System;
using System.Collections.Generic;

namespace GS.NewAPI.Factories
{
    public partial interface IDanhMucModelFactory
    {
        #region quốc gia

        IList<QuocGiaModel> GetAllQuocGias();
        IList<DiaBanModel> GetTinhThanhPhosByQuocGiaId(int quocGiaId);
        IList<DiaBanModel> GetDiaBansByMaCha(string maCha);
        IList<QuocGiaModel> SearchQuocGiasByName(string tenQuocGia);
        IList<LyDoBienDongModel> GetLyDoTangGiams(decimal? loaiLyDoBienDongId = 0, decimal? loaiHinhTaiSanId = 0, Boolean isTangMoi = false);

        IList<DonViBoPhanModel> GetDonViBoPhans(decimal donViId);

        //MessageReturn UpdateQuocGia(QuocGiaModel model, NguoiDung currentUser);

        //MessageReturn UpDateListQuocGia(List<QuocGiaModel> ListQuocGiaModel, NguoiDung currentUser);
        MessageReturn DeleteQuocGia(decimal ID = 0);
        IList<MucDichSuDungModel> GetMucDichSuDungsByLoaiHinhTSId(decimal? loaiHinhTaiSanId);
        #endregion
    }
}
