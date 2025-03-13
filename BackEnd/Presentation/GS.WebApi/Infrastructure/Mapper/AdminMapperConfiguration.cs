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
using GS.WebApi.Models.DanhMuc;
using GS.WebApi.Models.TaiSan;
using GS.WebApi.Models.TaiSanXacLap;
using System;
using GS.Core.Domain.DB;
using GS.Core.Domain.DMDC;
using GS.WebApi.Models.DMDC;
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
            CreateTaiSanToanDanMaps();
            CreateTaiSanMaps();
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

            CreateMap<HienTrang, HienTrangModel>();
            CreateMap<HienTrangModel, HienTrang>();

            CreateMap<LoaiDonVi, LoaiDonViModel>()
                .ForMember(c => c.DB_PARENT_ID, options => options.Ignore());
            CreateMap<LoaiDonViModel, LoaiDonVi>()
                .ForMember(c => c.CHE_DO_HOACH_TOAN_ID, options => options.Ignore());

            CreateMap<NguonVon, NguonVonModel>();
            CreateMap<NguonVonModel, NguonVon>();

            CreateMap<DonVi, DonViModel>();
            CreateMap<DonViModel, DonVi>()
                .ForMember(m => m.TRANG_THAI_DONG_BO_ID, option => option.Ignore())
                .ForMember(m => m.KHONG_CHUYEN_MA, option => option.Ignore());


            CreateMap<NhanXe, NhanXeModel>();
            CreateMap<NhanXeModel, NhanXe>();

            CreateMap<PhuongAnXuLy, PhuongAnXuLyModel>();
            CreateMap<PhuongAnXuLyModel, PhuongAnXuLy>();

            CreateMap<NguonGocTaiSan, NguonGocTaiSanModel>();
            CreateMap<NguonGocTaiSanModel, NguonGocTaiSan>()
                .ForMember(m=> m.PARENT_ID, option=> option.Ignore());

            CreateMap<HinhThucXuLy, HinhThucXuLyModel>();
            CreateMap<HinhThucXuLyModel, HinhThucXuLy>();
            CreateMap<CheDoHaoMon, CheDoHaoMonModel>();
            CreateMap<CheDoHaoMonModel, CheDoHaoMon>()
                .ForMember(m=>m.TU_NGAY, option=> option.Ignore())
                .ForMember(m => m.DEN_NGAY, option => option.Ignore());
            CreateMap<DongXe, DongXeModel>();
            CreateMap<DongXeModel, DongXe>();

            CreateMap<LyDoBienDong, LyDoBienDongModel>();
            CreateMap<LyDoBienDongModel, LyDoBienDong>()
                .ForMember(c => c.LOAI_HINH_TAI_SAN_ID, options => options.Ignore())
            .ForMember(c => c.LOAI_DON_VI, options => options.Ignore());

            CreateMap<DiaBan, DiaBanModel>();
            CreateMap<DiaBanModel, DiaBan>();


            CreateMap<LoaiTaiSan, LoaiTaiSanModel>();           
            CreateMap<LoaiTaiSanModel, LoaiTaiSan>();

            CreateMap<LoaiTaiSanDonVi, LoaiTaiSanDonViModel>();
            CreateMap<LoaiTaiSanDonViModel, LoaiTaiSanDonVi>();

            CreateMap<ChucDanh, ChucDanhModel>();
            CreateMap<ChucDanhModel, ChucDanh>();


            CreateMap<DuAn, DuAnModel>();
            CreateMap<DuAnModel, DuAn>();

            CreateMap<NguoiDung, NguoiDungModel>()
                .ForMember(c => c.LIST_DON_VI, options => options.Ignore());
               
            CreateMap<NguoiDungModel, NguoiDung>()
                 .ForMember(c => c.IS_QUAN_TRI, options => options.Ignore());
            CreateMap<MucDichSuDung, MucDichSuDungModel>();
            CreateMap<MucDichSuDungModel, MucDichSuDung>();

            CreateMap<DonViBoPhan, DonViBoPhanModel>();
            CreateMap<DonViBoPhanModel, DonViBoPhan>();

            //Danh mục dùng chung
            CreateMap<DMDC_DuAn, DMDC_DuAnModel>();
            CreateMap<DMDC_DuAnModel, DMDC_DuAn>();

            CreateMap<DMDC_DiaBan, DMDC_DiaBanModel>();
            CreateMap<DMDC_DiaBanModel, DMDC_DiaBan>();

            CreateMap<DMDC_DonViDuAn, DMDC_DonViDuAnModel>();
            CreateMap<DMDC_DonViDuAnModel, DMDC_DonViDuAn>();

            CreateMap<DMDC_DonViNganSach, DMDC_DonViNganSachModel>();
            CreateMap<DMDC_DonViNganSachModel, DMDC_DonViNganSach>();

            CreateMap<DMDC_QuocGia, DMDC_QuocGiaModel>();
            CreateMap<DMDC_QuocGiaModel, DMDC_QuocGia>();

            CreateMap<DMDC_ToChucNganSach, DMDC_ToChucNganSachModel>();
            CreateMap<DMDC_ToChucNganSachModel, DMDC_ToChucNganSach>();
            #endregion
        }
        #endregion
        #region TaiSan
        protected virtual void CreateTaiSanMaps()
        {
            CreateMap<DBTaiSan, DBTaiSanModel>();
            CreateMap<DBTaiSanModel, DBTaiSan>();


            CreateMap<TaiSanNhatKy, TaiSanNhatKyModel>();
            CreateMap<TaiSanNhatKyModel, TaiSanNhatKy>();


            CreateMap<TaiSan, TaiSanModel>();
            CreateMap<TaiSanModel, TaiSan>();

            CreateMap<TaiSanDat, TaiSanDatDBModel>();
            CreateMap<TaiSanDatDBModel, TaiSanDat>();

            CreateMap<TaiSanNha, TaiSanNhaDBModel>();
            CreateMap<TaiSanNhaDBModel, TaiSanNha>();

            CreateMap<TaiSanCln, TaiSanClnDBModel>();
            CreateMap<TaiSanClnDBModel, TaiSanCln>();

            CreateMap<TaiSanVkt, TaiSanVktDBModel>();
            CreateMap<TaiSanVktDBModel, TaiSanVkt>();

            CreateMap<TaiSanOto, TaiSanOtoDBModel>();
            CreateMap<TaiSanOtoDBModel, TaiSanOto>();

            CreateMap<TaiSanMayMoc, TaiSanMayMocDBModel>();
            CreateMap<TaiSanMayMocDBModel, TaiSanMayMoc>();

            CreateMap<TaiSanVoHinh, TaiSanVoHinhDBModel>();
            CreateMap<TaiSanVoHinhDBModel, TaiSanVoHinh>();

            CreateMap<TaiSanHienTrangSuDung, TaiSanHienTrangSuDungDBModel>();
            CreateMap<TaiSanHienTrangSuDungDBModel, TaiSanHienTrangSuDung>();

            CreateMap<TaiSanNguonVon, TaiSanNguonVonDBModel>();
            CreateMap<TaiSanNguonVonDBModel, TaiSanNguonVon>();

            CreateMap<BienDong, BienDongDBModel>();
            CreateMap<BienDongDBModel, BienDong>();

            CreateMap<HaoMonDBModel, HaoMonTaiSan>();
            CreateMap<KhauHaoDBModel, KhauHaoTaiSan>();

            CreateMap<LogsDongBoCsdlqg, LogsDongBoCsdlqgModel>();
            CreateMap<LogsDongBoCsdlqgModel, LogsDongBoCsdlqg>();
        }
        #endregion
        #region Tai san toan dan
        protected virtual void CreateTaiSanToanDanMaps()
        {
            #region autoMap for tai san toan dan
            CreateMap<QuyetDinhTichThu, QuyetDinhTichThuModel>();
            CreateMap<QuyetDinhTichThuModel, QuyetDinhTichThu>();

            CreateMap<TaiSanToanDanModel, TaiSanTd>();
            CreateMap<TaiSanTd, TaiSanToanDanModel>();

            CreateMap<TaiSanTdXuLy, TSToanDanXuLyModel>();
            CreateMap<TSToanDanXuLyModel, TaiSanTdXuLy>();

            CreateMap<XuLy, XuLyModel>();
            CreateMap<XuLyModel, XuLy>();
            #endregion
        }
        #endregion

    }
}