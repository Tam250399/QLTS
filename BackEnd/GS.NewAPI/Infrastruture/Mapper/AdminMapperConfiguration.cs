using AutoMapper;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure.Mapper;
using GS.NewAPI.Models;
using GS.NewAPI.Models.BienDongs;
using GS.NewAPI.Models.DanhMuc;

namespace GS.NewAPI.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public class AdminMapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor

        public AdminMapperConfiguration()
        {
            CreateDanhMucMaps();
        }
        #endregion
        #region Properties

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order => 0;

        #endregion
        #region Danh muc
        protected virtual void CreateDanhMucMaps()
        {
            #region Automap for DanhMuc
            CreateMap<QuocGia, QuocGiaModel>();
            CreateMap<QuocGiaModel, QuocGia>();
            CreateMap<DiaBan, DiaBanModel>();
            CreateMap<DiaBanModel, DiaBan>();
            //Mục đích sử dụng
            CreateMap<MucDichSuDung, MucDichSuDungModel>();
            CreateMap<MucDichSuDungModel, MucDichSuDung>();
            CreateMap<LyDoBienDong, LyDoBienDongModel>();
            CreateMap<LyDoBienDongModel, LyDoBienDong>();
            CreateMap<DonViBoPhanModel, DonViBoPhan>();
            CreateMap<DonViBoPhan, DonViBoPhanModel>();
            #endregion
            CreateMap<TaiSanModel, TaiSan>();
            CreateMap<TaiSan, TaiSanModel>();
            CreateMap<TaiSanLichSuModel, TaiSanLichSu>();
            CreateMap<TaiSanLichSu, TaiSanLichSuModel>();
            CreateMap<TaiSanModel, TaiSanDat>();
            CreateMap<TaiSanDat, TaiSanModel>();
            CreateMap<BienDongModel, BienDong>();
            CreateMap<BienDong, BienDongModel>();
            CreateMap<BienDongChiTietModel, BienDongChiTiet>();
            CreateMap<BienDongChiTiet, BienDongChiTietModel>();
        }
        #endregion
        
        

    }
}