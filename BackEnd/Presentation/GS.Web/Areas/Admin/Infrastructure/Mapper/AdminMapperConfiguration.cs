using AutoMapper;
using GS.Core.Domain.Api.TaiSanDBApi;
using GS.Core.Domain.BaoCaoDienTu;
using GS.Core.Domain.BaoCaoDoiChieus;
using GS.Core.Domain.BaoCaos;
using GS.Core.Domain.BaoCaos.CCDC;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.CCDC;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DB;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.KT;
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.SHTD;
using GS.Core.Domain.TaiSans;
using GS.Core.Domain.ThuocTinhs;
using GS.Core.Infrastructure.Mapper;
using GS.Web.Framework.Models;
using GS.Web.Models.BaoCao;
using GS.Web.Models.BaoCaoDienTu;
using GS.Web.Models.BaoCaos;
using GS.Web.Models.BaoCaos.CCDC;
using GS.Web.Models.BienDongs;
using GS.Web.Models.CauHinh;
using GS.Web.Models.CCDC;
using GS.Web.Models.DanhMuc;
using GS.Web.Models.DB;
using GS.Web.Models.DongBoTaiSan;
using GS.Web.Models.HeThong;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.KeToan;
using GS.Web.Models.NghiepVu;
using GS.Web.Models.SHTD;
using GS.Web.Models.TaiSans;
using GS.Web.Models.ThuocTinh;
using System;

namespace GS.Web.Areas.Admin.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for admin area models
    /// </summary>
    public class AdminMapperConfiguration : Profile, IOrderedMapperProfile
    {
        #region Ctor

        public AdminMapperConfiguration()
        {
            CauHinhMaps();
            CreateHeThongMaps();
            CreateDanhMucMaps();
            CreateTaiSanMaps();
            CreateBaoCaoMaps();
            CreateBaoCaoDoiChieuMaps();
            CreateBaoCaoDienTuMaps();
            CreateThuocTinhMaps();
            CreateSHTDhMaps();
            CreateKeToanMaps();
            CreatDongBoTaiSanMap();
            //add some generic mapping rules
            ForAllMaps((mapConfiguration, map) =>
            {
                //exclude Form and CustomProperties from mapping BaseGSModel
                if (typeof(BaseGSModel).IsAssignableFrom(mapConfiguration.DestinationType))
                {
                    //map.ForMember(nameof(BaseGSModel.Form), options => options.Ignore());
                    map.ForMember(nameof(BaseGSModel.CustomProperties), options => options.Ignore());
                }


                ////exclude Locales from mapping ILocalizedModel
                //if (typeof(ILocalizedModel).IsAssignableFrom(mapConfiguration.DestinationType))
                //    map.ForMember(nameof(ILocalizedModel<ILocalizedModel>.Locales), options => options.Ignore());





                //if (typeof(IPluginModel).IsAssignableFrom(mapConfiguration.DestinationType))
                //{
                //    //exclude some properties from mapping plugin models
                //    map.ForMember(nameof(IPluginModel.ConfigurationUrl), options => options.Ignore());
                //    map.ForMember(nameof(IPluginModel.IsActive), options => options.Ignore());
                //    map.ForMember(nameof(IPluginModel.LogoUrl), options => options.Ignore());

                //    //define specific rules for mapping plugin models
                //    if (typeof(IPlugin).IsAssignableFrom(mapConfiguration.SourceType))
                //    {
                //        map.ForMember(nameof(IPluginModel.DisplayOrder), options => options.MapFrom(plugin => ((IPlugin)plugin).PluginDescriptor.DisplayOrder));
                //        map.ForMember(nameof(IPluginModel.FriendlyName), options => options.MapFrom(plugin => ((IPlugin)plugin).PluginDescriptor.FriendlyName));
                //        map.ForMember(nameof(IPluginModel.SystemName), options => options.MapFrom(plugin => ((IPlugin)plugin).PluginDescriptor.SystemName));
                //    }
                //}
            });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Order of this mapper implementation
        /// </summary>
        public int Order => 0;

        #endregion

        #region Appworks HeThong
        protected virtual void CreateHeThongMaps()
        {
            CreateMap<ThongTinBaoCaoValue, ThongTinBaoCaoValueModel>();
            CreateMap<ThongTinBaoCaoValueModel, ThongTinBaoCaoValue>();
            CreateMap<ThongTinBaoCaoColume, LayoutReportModel>();
            CreateMap<LayoutReportModel, ThongTinBaoCaoColume>();
            CreateMap<FileCongViec, FileCongViecModel>();
            CreateMap<FileCongViecModel, FileCongViec>();
            CreateMap<NguoiDung, NguoiDungCache>();
            CreateMap<NguoiDung, NguoiDungModel>();
            CreateMap<NguoiDungModel, NguoiDung>();
            CreateMap<NguoiDungExcelModel, NguoiDung>();
            CreateMap<NguoiDung, NguoiDungExcelModel>();
            CreateMap<NhanHienThi, NhanHienThiModel>();
            CreateMap<NhanHienThiModel, NhanHienThi>();
            CreateMap<NhatKy, NhatKyModel>();
            CreateMap<NhatKyModel, NhatKy>();
            CreateMap<Quyen, QuyenModel>();
            CreateMap<QuyenModel, Quyen>();
            CreateMap<CauHinh, CauHinhModel>();
            CreateMap<CauHinhModel, CauHinh>();
            CreateMap<VaiTro, VaiTroModel>();
            CreateMap<VaiTroModel, VaiTro>();
            CreateMap<HoatDong, HoatDongModel>()
                .ForMember(model => model.TEN_KIEU_HOAT_DONG, options => options.MapFrom(entity => entity.loaihoatdong.TEN))
                .ForMember(model => model.TEN_DANG_NHAP, options => options.MapFrom(entity => entity.nguoidung.TEN_DANG_NHAP))
                .ForMember(model => model.TEN_DAY_DU, options => options.MapFrom(entity => entity.nguoidung.TEN_DAY_DU));
            CreateMap<HoatDongModel, HoatDong>()
                .ForMember(entity => entity.loaihoatdong, options => options.Ignore())
                .ForMember(entity => entity.LOAI_HOAT_DONG_ID, options => options.Ignore())
                .ForMember(entity => entity.NGAY_TAO, options => options.Ignore())
                .ForMember(entity => entity.NGUOI_DUNG_ID, options => options.Ignore())
                .ForMember(entity => entity.DOI_TUONG_ID, options => options.Ignore())
                .ForMember(entity => entity.TEN_DOI_TUONG, options => options.Ignore());

            CreateMap<LoaiHoatDong, LoaiHoatDongModel>();
            CreateMap<LoaiHoatDongModel, LoaiHoatDong>()
                .ForMember(entity => entity.TU_KHOA_HE_THONG, options => options.Ignore());
            CreateMap<Widget, WidgetModel>();
            CreateMap<WidgetModel, Widget>();
            CreateMap<ScheduleTaskModel, ScheduleTask>();
            CreateMap<ScheduleTask, ScheduleTaskModel>();
        }


        #endregion
        #region Cau hinh he thong 
        protected virtual void CauHinhMaps()
        {
            CreateMap<CauHinhChung, CauHinhChungModel>();
            CreateMap<CauHinhChungModel, CauHinhChung>();
            CreateMap<XacThucChungThuSo, XacThucChungThuSomodel>();
            CreateMap<XacThucChungThuSomodel, XacThucChungThuSo>();
            CreateMap<KetXuatDuLieu, KetXuatDuLieuModel>();
            CreateMap<KetXuatDuLieuModel, KetXuatDuLieu>();
            CreateMap<DonViCauHinh, DonViCauHinhModel>();
            CreateMap<DonViCauHinhModel, DonViCauHinh>();
            CreateMap<DinhMucChucDanhModel, CauHinhDinhMucChucDanh>();
            CreateMap<CauHinhDinhMucChucDanh, DinhMucChucDanhModel>();
            CreateMap<CauHinhThreadBaoCao, CauHinhThreadBaoCaoModel>();
            CreateMap<CauHinhThreadBaoCaoModel, CauHinhThreadBaoCao>();
            CreateMap<DieuKienLocTaiSan, DieuKienLocTaiSanModel>();
            CreateMap<DieuKienLocTaiSanModel, DieuKienLocTaiSan>();
            CreateMap<TrangThaiNam, TrangThaiNamModel>();
            CreateMap<TrangThaiNamModel, TrangThaiNam>();
            CreateMap<CauHinhTuDongDuyet, CauHinhTuDongDuyetModel>();
            CreateMap<CauHinhTuDongDuyetModel, CauHinhTuDongDuyet>();
        }
        #endregion
        #region Danh muc
        protected virtual void CreateDanhMucMaps()
        {
            #region Automap for DanhMuc
            CreateMap<CheDoHaoMon, CheDoHaoMonModel>();
            CreateMap<CheDoHaoMonModel, CheDoHaoMon>();
            CreateMap<ChucDanh, ChucDanhModel>();
            CreateMap<ChucDanhModel, ChucDanh>();
            CreateMap<DiaBan, DiaBanModel>();
            CreateMap<DiaBanTest, DiaBanTestModel>();
            CreateMap<DoiTac, DoiTacModel>();
            CreateMap<DoiTacModel, DoiTac>();
            CreateMap<DongXe, DongXeModel>();
            CreateMap<DongXeModel, DongXe>();
            CreateMap<DonVi, DonViModel>()
                .ForMember(model => model.TenLoaiDonVi, option => option.MapFrom(map => map.LoaiDonVi != null ? map.LoaiDonVi.TEN : null));
            CreateMap<DonViModel, DonVi>();
            CreateMap<DonViBoPhan, DonViBoPhanModel>();
            CreateMap<DonViBoPhanModel, DonViBoPhan>();
            CreateMap<DonViChuyenDoi, DonViChuyenDoiModel>();
            CreateMap<DonViChuyenDoiModel, DonViChuyenDoi>();
            CreateMap<DuAn, DuAnModel>();
            CreateMap<DuAnModel, DuAn>();
            CreateMap<HienTrang, HienTrangModel>();
            CreateMap<HienTrangModel, HienTrang>();
            CreateMap<HinhThucMuaSam, HinhThucMuaSamModel>();
            CreateMap<HinhThucMuaSamModel, HinhThucMuaSam>();
            CreateMap<LoaiBienDong, LoaiBienDongModel>();
            CreateMap<LoaiBienDongModel, LoaiBienDong>();
            CreateMap<LoaiDonVi, LoaiDonViModel>();
            CreateMap<LoaiDonViModel, LoaiDonVi>();
            CreateMap<LoaiTaiSan, LoaiTaiSanModel>();

            CreateMap<LoaiTaiSanDonVi, LoaiTaiSanModel>(); 
            CreateMap<LoaiTaiSanDonVi, LoaiTaiSanVoHinhModel>();
            CreateMap<LoaiTaiSanVoHinhModel, LoaiTaiSanDonVi>();

            CreateMap<LoaiTaiSanKhauHao, LoaiTaiSanKhauHaoModel>();
            CreateMap<LoaiTaiSanKhauHaoModel, LoaiTaiSanKhauHao>();
            CreateMap<LyDoBienDong, LyDoBienDongModel>();
            CreateMap<LyDoBienDongModel, LyDoBienDong>();
            CreateMap<MucDichSuDung, MucDichSuDungModel>();
            CreateMap<MucDichSuDungModel, MucDichSuDung>();
            CreateMap<NguonVon, NguonVonModel>();
            CreateMap<NguonTaiSan, NguonTaiSanModel>();
            CreateMap<NguonTaiSanModel, NguonTaiSan>();
            CreateMap<NhanXe, NhanXeModel>();
            CreateMap<NhanXeModel, NhanXe>();
            CreateMap<QuocGia, QuocGiaModel>();
            CreateMap<QuocGiaModel, QuocGia>();
            CreateMap<NhomCongCu, NhomCongCuModel>()
            .ForMember(model => model.TEN_DON_VI, options => options.MapFrom(entity => entity.DON_VI_ID > 0 ? entity.DON_VI_ENTITY.TEN : ""));
            CreateMap<NhomCongCuModel, NhomCongCu>();
            CreateMap<PhuongAnXuLy, PhuongAnXuLyModel>();
            CreateMap<PhuongAnXuLyModel, PhuongAnXuLy>();
            CreateMap<NguonGocTaiSan, NguonGocTaiSanModel>();
            CreateMap<NguonGocTaiSanModel, NguonGocTaiSan>();
            CreateMap<HinhThucXuLy, HinhThucXuLyModel>();
            CreateMap<HinhThucXuLyModel, HinhThucXuLy>();

            CreateMap<DinhMuc, DinhMucModel>();
            CreateMap<DinhMucModel,DinhMuc>();
            CreateMap<DinhMucChiTiet, DinhMucChiTietModel>();
            CreateMap<DinhMucChiTietModel, DinhMucChiTiet>();

            #endregion
        }
        #endregion
        #region Báo cáo
        protected virtual void CreateBaoCaoMaps()
        {
            CreateMap<BaoCao, BaoCaoModel>();
            CreateMap<BaoCaoModel, BaoCao>();
            CreateMap<QueueProcess, QueueProcessModel>();
            CreateMap<QueueProcessModel, QueueProcess>();
            CreateMap<QueueProcessSearch, QueueProcessModel>();
            CreateMap<QueueProcessModel, QueueProcessSearch>();
        }
        #endregion
        #region Báo cáo đối chiếu
        protected virtual void CreateBaoCaoDoiChieuMaps()
        {
            CreateMap<BaoCaoDoiChieu, BaoCaoDoiChieuModel>();
            CreateMap<BaoCaoDoiChieuModel, BaoCaoDoiChieu>();
        }
        #endregion
        #region Báo cáo điện tử
        protected virtual void CreateBaoCaoDienTuMaps()
        {
            CreateMap<BaoCaoDienTu, BaoCaoDienTuModel>();
            CreateMap<BaoCaoDienTuModel, BaoCaoDienTu>();
        }
        #endregion
        #region Tài sản 
        protected virtual void CreateTaiSanMaps()
        {
            #region Automap for NghiepVu
            CreateMap<YeuCau, YeuCauModel>()
                .ForMember(model => model.TenLyDoBienDong, options => options.MapFrom(entity => entity.LY_DO_BIEN_DONG_ID > 0 ? entity.lydobiendong.TEN : ""))
                .ForMember(model => model.TaiSanGuid, options => options.MapFrom(entity => entity.TAI_SAN_ID > 0 ? entity.taisan.GUID : Guid.Empty));
            CreateMap<YeuCauModel, YeuCau>();
            CreateMap<YeuCauChiTiet, YeuCauChiTietModel>();
            CreateMap<YeuCauChiTietModel, YeuCauChiTiet>();
            CreateMap<YeuCauNhatKy, YeuCauNhatKyModel>();
            CreateMap<YeuCauNhatKyModel, YeuCauNhatKy>();
            #endregion
            #region Automap for TaiSans
            CreateMap<TaiSan, TaiSanModel>()
                .ForMember(model => model.TenDonVi, options => options.MapFrom(entity => entity.DON_VI_ID > 0 ? entity.donvi.TEN : ""))
                .ForMember(model => model.NguoiTaoTen, options => options.MapFrom(entity => entity.NGUOI_TAO_ID > 0 ? entity.nguoidung.TEN_DANG_NHAP : ""));
            CreateMap<TaiSanModel, TaiSan>();
            CreateMap<TaiSanCln, TaiSanClnModel>();
            CreateMap<TaiSanClnModel, TaiSanCln>();
            CreateMap<TaiSanDat, TaiSanDatModel>()
                .ForMember(model => model.DAT_DIEN_TICH, options => options.MapFrom(entity => entity.DIEN_TICH));
            CreateMap<TaiSanDatModel, TaiSanDat>();
            CreateMap<TaiSanMayMoc, TaiSanMayMocModel>();
            CreateMap<TaiSanMayMocModel, TaiSanMayMoc>();
            CreateMap<TaiSanVoHinh, TaiSanVoHinhModel>();
            CreateMap<TaiSanVoHinhModel, TaiSanVoHinh>();
            CreateMap<TaiSanNguonVon, TaiSanNguonVonModel>();
            CreateMap<TaiSanNguonVonModel, TaiSanNguonVon>();
            CreateMap<TaiSanNguonVon, NguonVonModel>()
                .ForMember(model => model.GiaTri, options => options.MapFrom(entity => entity.GIA_TRI))
                .ForMember(model => model.ID, options => options.MapFrom(entity => entity.NGUON_VON_ID))
                .ForMember(model => model.TEN, options => options.MapFrom(entity => entity.nguonvon.TEN));
            CreateMap<TaiSanNha, TaiSanNhaModel>()
                      .ForMember(model => model.NHA_DIA_CHI, options => options.MapFrom(entity => entity.DIA_CHI));
            CreateMap<TaiSanNhaModel, TaiSanNha>();
            CreateMap<TaiSanOto, TaiSanOtoModel>();
            CreateMap<TaiSanOtoModel, TaiSanOto>();
            CreateMap<TaiSanVkt, TaiSanVktModel>();
            CreateMap<TaiSanVktModel, TaiSanVkt>();
            CreateMap<TaiSanHienTrangSuDung, TaiSanHienTrangSuDungModel>();
            CreateMap<TaiSanHienTrangSuDungModel, TaiSanHienTrangSuDung>();
            CreateMap<TaiSanKiemKe, TaiSanKiemKeModel>();
            CreateMap<TaiSanKiemKeModel, TaiSanKiemKe>();
            CreateMap<TaiSanKiemKeHoiDong, TaiSanKiemKeHoiDongModel>();
            CreateMap<TaiSanKiemKeHoiDongModel, TaiSanKiemKeHoiDong>();
            CreateMap<TaiSanKiemKeTaiSan, TaiSanKiemKeTaiSanModel>();
            CreateMap<TaiSanKiemKeTaiSanModel, TaiSanKiemKeTaiSan>();
            CreateMap<TaiSanChoThue, TaiSanChoThueModel>()
                .ForMember(model => model.TenTaiSan, options => options.MapFrom(entity => entity.taisan.TEN))
                .ForMember(model => model.MaTaiSan, options => options.MapFrom(entity => entity.taisan.MA))
                .ForMember(model => model.DonViSuDungTaiSan, options => options.MapFrom(entity => entity.taisan.donvi.TEN))
                .ForMember(model => model.BoPhanSuDung, options => options.MapFrom(entity => entity.taisan.donvibophan.TEN));
            CreateMap<TaiSanChoThueModel, TaiSanChoThue>();

            CreateMap<KhaiThac, KhaiThacModel>();
            CreateMap<KhaiThacModel, KhaiThac>();
            CreateMap<KhaiThacChiTiet, KhaiThacChiTietModel>();
            CreateMap<KhaiThacChiTietModel, KhaiThacChiTiet>();
            CreateMap<KhaiThacTaiSan, KhaiThacTaiSanModel>();
            CreateMap<KhaiThacTaiSanModel, KhaiThacTaiSan>();

            CreateMap<MuaSam, MuaSamModel>();
            CreateMap<MuaSamModel, MuaSam>();
            CreateMap<MuaSamChiTiet, MuaSamChiTietModel>();
            CreateMap<MuaSamChiTietModel, MuaSamChiTiet>();
            CreateMap<DeNghiXuLy, DeNghiXuLyModel>();
            CreateMap<DeNghiXuLyModel, DeNghiXuLy>();
            CreateMap<DeNghiXuLyTaiSan, DeNghiXuLyTaiSanModel>();
            CreateMap<DeNghiXuLyTaiSanModel, DeNghiXuLyTaiSan>();

            #region Tài sản khấu hao
            CreateMap<TaiSanKhauHao, TaiSanKhauHaoModel>();
            CreateMap<TaiSanKhauHaoModel, TaiSanKhauHao>();
            #endregion
            #endregion
            #region Automap for BienDongs
            CreateMap<BienDong, BienDongModel>();
            CreateMap<BienDongModel, BienDong>();
            CreateMap<BienDongChiTiet, BienDongChiTietModel>();
            CreateMap<BienDongChiTietModel, BienDongChiTiet>();
            CreateMap<TrungGianBDYC, TrungGianBDYCModel>()
                .ForMember(c => c.TAI_SAN_TRUOC_DIEU_CHUYEN_ID, options => options.Ignore())
                .ForMember(c => c.TAI_SAN_TRANG_THAI_ID, options => options.Ignore())
                .ForMember(c => c.HINH_THUC_XU_LY_ID, options => options.Ignore());

            CreateMap<TrungGianBDYCModel, TrungGianBDYC>();
            CreateMap<TrungGianBDYCChiTiet, TrungGianBDYCChiTietModel>();
            CreateMap<TrungGianBDYCChiTietModel, TrungGianBDYCChiTiet>();
            ///<summary>
            ///Map YeuCauModel-BienDong khi duyet taisan
            ///</summary>
            CreateMap<YeuCauModel, BienDong>();
            ///<summary>
            ///Map YeuCauModel-BienDong khi duyet taisan
            ///</summary>
            CreateMap<YeuCauChiTietModel, BienDongChiTiet>();
            #endregion
            #region Automap for CCDC
            CreateMap<CongCu, CongCuModel>();
            CreateMap<CongCuModel, CongCu>();
            CreateMap<CongCuDonVi, CongCuDonViModel>();
            CreateMap<CongCuDonViModel, CongCuDonVi>();
            CreateMap<NhapXuatCongCu, NhapXuatCongCuModel>();
            CreateMap<NhapXuatCongCuModel, NhapXuatCongCu>();
            CreateMap<XuatNhap, XuatNhapModel>();
            CreateMap<XuatNhapModel, XuatNhap>();
            CreateMap<SuaChuaBaoDuongModel, SuaChuaBaoDuong>();
            CreateMap<SuaChuaBaoDuong, SuaChuaBaoDuongModel>();
            CreateMap<ChoThue, ChoThueModel>();
            CreateMap<ChoThueModel, ChoThue>();
            CreateMap<KiemKe, KiemKeModel>();
            CreateMap<KiemKeModel, KiemKe>();
            CreateMap<KiemKeCongCu, KiemKeCongCuModel>();
            CreateMap<KiemKeCongCuModel, KiemKeCongCu>();
            CreateMap<KiemKeHoiDong, KiemKeHoiDongModel>();
            CreateMap<KiemKeHoiDongModel, KiemKeHoiDong>();
            CreateMap<GiamHongmat, GiamHongmatModel>();
            CreateMap<GiamHongmatModel, GiamHongmat>();
            #endregion
        }
        #endregion
        #region Thuộc tính 
        protected virtual void CreateThuocTinhMaps()
        {
            CreateMap<ThuocTinh, ThuocTinhModel>();
            CreateMap<ThuocTinhModel, ThuocTinh>();
            CreateMap<ThuocTinhEntity, modelThuocTinh>();
            CreateMap<modelThuocTinh, ThuocTinhEntity>();
            CreateMap<ThuocTinhData, ThuocTinhDataModel>();
            CreateMap<ThuocTinhDataModel, ThuocTinhData>();
            CreateMap<ThuocTinhTaiSan, ThuocTinhTaiSanModel>();
            CreateMap<ThuocTinhTaiSanModel, ThuocTinhTaiSan>();

        }
        #endregion
        #region SHTD
        protected virtual void CreateSHTDhMaps()
        {
            CreateMap<TaiSanTd, TaiSanTdModel>().ForMember(model => model.TenLoaiTaiSan, options => options.MapFrom(entity => entity.LOAI_TAI_SAN_ID > 0 ? entity.loaitaisan.TEN : "")).ForMember(model => model.NamSuDung, options => options.MapFrom(entity => entity.NGAY_SU_DUNG !=null ? entity.NGAY_SU_DUNG.Value.Year : 0));
            CreateMap<TaiSanTdModel, TaiSanTd>().ForMember(entity => entity.quyetdinh, options => options.MapFrom(model => model.quyetdinh.ID == -1 ? model.quyetdinh : null)).ForMember(entity => entity.loaitaisan, options => options.MapFrom(model => model.quyetdinh.ID == -1 ? model.quyetdinh : null)).ForMember(entity => entity.NGAY_SU_DUNG, options => options.MapFrom(model => model.NamSuDung !=null ? new DateTime((int)model.NamSuDung,01,01) : DateTime.Now));
            CreateMap<TaiSanTdXuLy, TaiSanTdXuLyModel>();
            CreateMap<TaiSanTdXuLyModel, TaiSanTdXuLy>();
            CreateMap<XuLy, XuLyModel>();
            CreateMap<XuLyModel, XuLy>();
            CreateMap<QuyetDinhTichThu, QuyetDinhTichThuModel>();
            CreateMap<QuyetDinhTichThuModel, QuyetDinhTichThu>();
            CreateMap<XuLyKetQua, XuLyKetQuaModel>();
            CreateMap<XuLyKetQuaModel, XuLyKetQua>();
            CreateMap<KetQuaTaiSan, KetQuaTaiSanModel>();
            CreateMap<KetQuaTaiSanModel, KetQuaTaiSan>();
            CreateMap<KetQuaModel, KetQua>();
            CreateMap<NhatKyTaiSanToanDanModel, NhatKyTaiSanToanDan>();
            CreateMap<ThuChiModel, ThuChi>();
            CreateMap<KetQua, KetQuaModel>();
            CreateMap<NhatKyTaiSanToanDan, NhatKyTaiSanToanDanModel>();
            CreateMap<ThuChi, ThuChiModel>();
            //đồng bộ
            CreateMap<QuyetDinhTichThu, QuyetDinhTichThuDongBoModel>();
            CreateMap<TaiSanTd, TaiSanTdDongBoModel>();
            CreateMap<TaiSanTdXuLy, TaiSanTdXuLyDongBoModel>();
            CreateMap<ThuChi, ThuChiDongBoModel>();

        }
        #endregion
        #region Đồng bộ
        protected void CreatDongBoTaiSanMap()
        {
            CreateMap<TaiSanDatDBModel, TaiSanDatApi>();
            CreateMap<TaiSanNhaDBModel, TaiSanNhaApi>();
            CreateMap<TaiSanOtoDBModel, TaiSanOtoApi>();
            CreateMap<TaiSanVktDBModel, TaiSanVktApi>();
            CreateMap<TaiSanMayMocDBModel, TaiSanMayMocApi>();
            CreateMap<TaiSanClnDBModel, TaiSanClnApi>();
            CreateMap<TaiSanVoHinhDBModel, TaiSanVoHinhApi>();
            CreateMap<TaiSanNhatKyModel, TaiSanNhatKy>();
            CreateMap<TaiSanNhatKy, TaiSanNhatKyModel>();
            CreateMap<DBTaiSan, DBTaiSanModel>();
            CreateMap<DBTaiSanModel, DBTaiSan>();
            CreateMap<DB_QueueProcess, DB_QueueProcessModel>();
            CreateMap<DB_QueueProcessModel, DB_QueueProcess>();
            CreateMap<DB_QueueProcessHistory, DB_QueueProcessHistoryModel>();
            CreateMap<DB_QueueProcessHistoryModel, DB_QueueProcessHistory>();
            CreateMap<DBTempTaiSanCuModel, DBTempTaiSanCu>();
            CreateMap<LogsDongBoCsdlqg, LogsDongBoCsdlqgModel>();
            CreateMap<LogsDongBoCsdlqgModel, LogsDongBoCsdlqg>();
            CreateMap<DBTaiSan, DBTaiSanView>();
            CreateMap<DBTaiSanView, DBTaiSan>();
            CreateMap<ImportExcelTaiSanModel, ImportExcelTaiSan>();
            CreateMap<ImportExcelTaiSan, ImportExcelTaiSanModel>();
        }

        #endregion
        #region KeToan
        protected virtual void CreateKeToanMaps()
        {
            CreateMap<HaoMonTaiSan, HaoMonTaiSanModel>()
                 .ForMember(model => model.TAI_SAN_TEN, options => options.MapFrom(entity => entity.TAI_SAN_ID > 0 ? entity.TaiSan.TEN : ""));
            CreateMap<HaoMonTaiSanModel, HaoMonTaiSan>();
            CreateMap<HaoMonTaiSanLog, HaoMonTaiSanLogModel>();
            CreateMap<HaoMonTaiSanLogModel, HaoMonTaiSanLog>();
            CreateMap<KhauHaoTaiSan, KhauHaoTaiSanModel>();
            CreateMap<KhauHaoTaiSanModel, KhauHaoTaiSan>();
        }
        #endregion
    }
}