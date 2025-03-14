using AutoMapper;
using GS.Core.Domain.BaoCaos.CCDC;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.ThuocTinhs;
using GS.Core.Infrastructure.Mapper;
using GS.Web.Framework.Models;
using GS.NewAPI.Models.DanhMuc;
using System;
using GS.Core.Domain.DB;
using GS.Core.Domain.DMDC;
using GS.Core.Domain.KT;

namespace GS.WebApi.Infrastructure.Mapper
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

            #endregion
        }
        #endregion
        
        

    }
}