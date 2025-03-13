SELECT
    DA.MA_DU_AN,
    DA.TEN_DU_AN,
    DA.LOAI_DU_AN,
    DA.NGAY_BAT_DAU,
    DA.NGAY_KET_THUC,
    DA.TONG_KINH_PHI,
    DA.HINH_THUC,
    DA.SO_QUYET_DINH_PHE_DUYET,
    DA.CHU_DAU_TU,
    DA.DIA_DIEM,
    DA.NGUON_VON,
    DA.GHI_CHU,
    DA.NGUON_NS,
    DA.NGUON_ODA,
    DA.NGUON_VIEN_TRO,
    DA.NGUON_KHAC,
    DA.TRANG_THAI,
    DA.KIEU,
    DA.CO_QUAN_TAI_CHINH,
    DA.MA_LOAI_DU_AN,
    DA.MA_NHOM_DU_AN,
    DA.TEN_NHOM_DU_AN,
    DA.MA_HT,
    DA.TEN_HT,
    DA.HT_QUANLY,
    DA.HIEU_LUC,
    DA.MA_DON_VI_CU,
    DA.MA_DU_AN_CU,
    (
        SELECT
            ID
        FROM
            DPAC_QLDKTS_CORE.DM_DON_VI
        WHERE
            MA = DA.MA_DON_VI
            AND ROWNUM = 1) AS DON_VI_ID,
    (
        SELECT
            ID
        FROM
            DPAC_QLDKTS_CORE.DM_DON_VI
        WHERE
            MA = DA.MA_DON_VI_CU
            AND ROWNUM = 1) AS DON_VI_CU_ID
FROM
    DM_DUAN DA
WHERE
    ID IN (
            SELECT
                DU_AN_ID
            FROM 
                TS_TAI_SAN
            WHERE
                DU_AN_ID IS NOT NULL);