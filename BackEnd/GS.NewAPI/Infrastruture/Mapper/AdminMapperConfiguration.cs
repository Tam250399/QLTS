using AutoMapper;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.TaiSans;
using GS.Core.Infrastructure.Mapper;
using GS.NewAPI.Models;
using GS.NewAPI.Models.DanhMuc;
using System.Collections.Generic;

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
            CreateTaiSan();
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

        }
        #endregion

        protected virtual void CreateTaiSan()
        {
            CreateMap<TaiSanModel, TaiSan>().ReverseMap();
            CreateMap<List<TaiSanModel>, List<TaiSan>>().ReverseMap();
            CreateMap<TaiSan, TaiSanModel>();
            CreateMap<LoaiTaiSan, LoaiTaiSanModel>();
            CreateMap<LoaiTaiSanModel, LoaiTaiSan>();

        }


    }
}