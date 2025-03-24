import { Box, Grid, TextField, Typography } from "@mui/material";

import {
  FieldErrors,
  UseFormGetValues,
  UseFormRegister,
  UseFormSetValue,
} from "react-hook-form";
import { Thongtinchung } from "../../validateform/thongtinchung";
import { useEffect, useState } from "react";
import formatCurrencyVND from "../../components/Format/FormatVND";
import {
  handleChangeBiLanChiem,
  handleChangeBoTrong,
  handleChangeDeo,
  handleChangeDienTichs,
  handleChangeSuDungHonHop,
  handleChangeSuDungKhac,
  handleChangeTruSoLamViec,
} from "../../components/form/HandleChageDat";
interface ThongtintaisanProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
  getValues: UseFormGetValues<Thongtinchung>;
  setValue: UseFormSetValue<Thongtinchung>;
}

const Dientichhientrang = ({
  register,
  getValues,
  setValue,
}: ThongtintaisanProps) => {
  const dienTich = getValues("dienTich");

  const [displayValues, setDisplayValues] = useState<Record<string, string>>(
    {}
  );

  const [areaError, setAreaError] = useState<string | undefined>(undefined);
  const {
    TRU_SO_LAM_VIEC,
    deO,
    BO_TRONG,
    BI_LAN_CHIEM,
    SU_DUNG_HON_HOP,
    SU_DUNG_KHAC,
  } = getValues("HIEN_TRANG_SU_DUNG") || {};

  const totalRelevantFields = [
    TRU_SO_LAM_VIEC,
    deO,
    BO_TRONG,
    BI_LAN_CHIEM,
    SU_DUNG_HON_HOP,
    SU_DUNG_KHAC,
  ].reduce<number>((sum, value) => sum + (Number(value) || 0), 0);

  useEffect(() => {
    if (dienTich && totalRelevantFields !== Number(dienTich)) {
      setAreaError("Diện tích đất phải bằng tổng hiện trạng sử dụng.");
    } else {
      setAreaError(undefined);
    }
  }, [dienTich, totalRelevantFields]);

  useEffect(() => {
    const fields = {
      dienTich,
      TRU_SO_LAM_VIEC,
      deO,
      BO_TRONG,
      BI_LAN_CHIEM,
      SU_DUNG_HON_HOP,
      SU_DUNG_KHAC,
    };

    const newDisplayValues: Record<string, string> = {};
    Object.entries(fields).forEach(([fieldName, value]) => {
      if (value !== undefined) {
        newDisplayValues[fieldName] = formatCurrencyVND(value, "m²");
      }
    });

    setDisplayValues((prev) => ({ ...prev, ...newDisplayValues }));
  }, [
    dienTich,
    TRU_SO_LAM_VIEC,
    deO,
    BO_TRONG,
    BI_LAN_CHIEM,
    SU_DUNG_HON_HOP,
    SU_DUNG_KHAC,
  ]);

  //check onchange khi nhập dữ liệu số

  return (
    <Box
      sx={{
        border: "1px solid #007bff",
        borderRadius: 2,
        p: 3,
        bgcolor: "white",
        boxShadow: 2,
        position: "relative",
      }}
    >
      {/* Tiêu đề "Thông tin tài sản đất" */}
      <Typography
        sx={{
          position: "absolute",
          top: "-12px",
          left: "15px",
          backgroundColor: "white",
          padding: "0 8px",
          color: "#007bff",
          fontSize: "14px",
          fontWeight: "bold",
        }}
      >
        Diện tích khuôn viên, hiện trạng sử dụng
      </Typography>
      <Grid container spacing={2}>
        {/* Diện tích - Bắt buộc */}
        <Grid item xs={12} sm={6}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Diện tích <span style={{ color: "red" }}>*</span>
          </Typography>

          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="m²"
            value={displayValues.dienTich || ""}
            onChange={handleChangeDienTichs(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, dienTich: value }))
            )}
            error={!!areaError}
            helperText={areaError}
          />
        </Grid>

        {/* Hiện trạng sử dụng - Tiêu đề */}
        <Grid item xs={12}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Hiện trạng sử dụng <span style={{ color: "red" }}>*</span>
          </Typography>
        </Grid>

        {/* Các trường hiện trạng sử dụng */}
        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Trụ sở làm việc
          </Typography>

          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="m²"
            value={displayValues.TRU_SO_LAM_VIEC || ""}
            onChange={handleChangeTruSoLamViec(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, TRU_SO_LAM_VIEC: value }))
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            HDSN-Không KD
          </Typography>
          <TextField
            fullWidth
            size="small"
            type="number"
            margin="dense"
            placeholder="m²"
            disabled
            className="bg-slate-200"
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("HIEN_TRANG_SU_DUNG.hdSnKhongKd")}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            HDSN-Kinh doanh
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            placeholder="m²"
            type="number"
            disabled
            className="bg-slate-200"
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("HIEN_TRANG_SU_DUNG.HD_SD_KINH_DOANH")}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            HDSN-Cho thuê
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            placeholder="m²"
            type="number"
            disabled
            className="bg-slate-200"
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("HIEN_TRANG_SU_DUNG.HD_SD_CHO_THUE")}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            HDSN-LDLK
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            placeholder="m²"
            type="number"
            disabled
            className="bg-slate-200"
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("HIEN_TRANG_SU_DUNG.HD_SD_KINH_DOANH_LK")}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Để ở
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="m²"
            value={displayValues.deO || ""}
            onChange={handleChangeDeo(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, deO: value }))
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Bỏ trống
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="m²"
            value={displayValues.BO_TRONG || ""}
            onChange={handleChangeBoTrong(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, BO_TRONG: value }))
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Bị lấn chiếm
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="m²"
            value={displayValues.BI_LAN_CHIEM || ""}
            onChange={handleChangeBiLanChiem(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, BI_LAN_CHIEM: value }))
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Sử dụng hỗn hợp
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="m²"
            value={displayValues.SU_DUNG_HON_HOP || ""}
            onChange={handleChangeSuDungHonHop(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, SU_DUNG_HON_HOP: value }))
            )}
          />
        </Grid>

        <Grid item xs={12} sm={6} md={3}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Sử dụng khác
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="m²"
            value={displayValues.SU_DUNG_KHAC || ""}
            onChange={handleChangeSuDungKhac(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, SU_DUNG_KHAC: value }))
            )}
          />
        </Grid>
      </Grid>
    </Box>
  );
};

export default Dientichhientrang;
