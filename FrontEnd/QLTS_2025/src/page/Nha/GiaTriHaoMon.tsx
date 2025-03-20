import { Box, Grid, TextField, Typography } from "@mui/material";
import {
  FieldErrors,
  UseFormClearErrors,
  UseFormRegister,
  UseFormSetError,
  UseFormSetValue,
} from "react-hook-form";
import { useState } from "react";
import { ThongTinNha } from "../../validateform/thongtinnha";

interface GiaTriHaoMonProps {
  register: UseFormRegister<ThongTinNha>;
  errors: FieldErrors<ThongTinNha>;
  setValue: UseFormSetValue<ThongTinNha>;
  setError: UseFormSetError<ThongTinNha>;
  clearErrors: UseFormClearErrors<ThongTinNha>;
}

const GiaTriHaoMon = ({
  register,
  errors,
  setValue,
  setError,
  clearErrors,
}: GiaTriHaoMonProps) => {
  const [GIA_TRI_QUYEN_SD_DAT, setGIA_TRI_QUYEN_SD_DAT] = useState<number>(0);
  const [NGUON_KHAC, setNGUON_KHAC] = useState<number>(0);
  const [NGUYEN_GIA, setNGUYEN_GIA] = useState<number>(0);
  const [NGUON_NGAN_SACH, setNGUON_NGAN_SACH] = useState<number>(0);

  const handleGIA_TRI_QUYEN_SD_DATChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = parseFloat(event.target.value) || 0;
    setGIA_TRI_QUYEN_SD_DAT(value);
    setValue("GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT", Number(value), {
      shouldValidate: true,
    });
  };

  const handleNGUYEN_GIAChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = parseFloat(event.target.value) || 0;

    setNGUYEN_GIA(value);
    setValue("GIA_TRI_SU_DUNG_DAT.NGUYEN_GIA", value, { shouldValidate: true });
    const updatedNGUON_NGAN_SACH = Math.max(0, value - NGUON_KHAC);
    setNGUON_NGAN_SACH(updatedNGUON_NGAN_SACH);
    setValue("GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH", updatedNGUON_NGAN_SACH);
  };

  const handleNGUYEN_GIABlur = () => {
    if (
      NGUYEN_GIA > 0 &&
      GIA_TRI_QUYEN_SD_DAT > 0 &&
      GIA_TRI_QUYEN_SD_DAT > NGUYEN_GIA
    ) {
      setError("GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT", {
        type: "manual",
        message:
          "Giá trị quyền sử dụng đất không được lớn hơn nguyên giá, đề nghị kiểm tra lại!",
      });
    } else {
      clearErrors("GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT");
    }
  };

  const handleGIA_TRI_QUYEN_SD_DATBlur = () => {
    if (
      NGUYEN_GIA > 0 &&
      GIA_TRI_QUYEN_SD_DAT > 0 &&
      GIA_TRI_QUYEN_SD_DAT > NGUYEN_GIA
    ) {
      setError("GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT", {
        type: "manual",
        message:
          "Giá trị quyền sử dụng đất không được lớn hơn nguyên giá, đề nghị kiểm tra lại!",
      });
    } else {
      clearErrors("GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT");
    }
  };

  // Khi nhập Nguồn khác, cập nhật Nguồn ngân sách ngay
  const handleNGUON_KHACChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = parseFloat(event.target.value) || 0;

    // Cập nhật nguồn ngân sách
    const updatedNGUON_NGAN_SACH = Math.max(-1, NGUYEN_GIA - value);
    setNGUON_KHAC(value);
    setValue("GIA_TRI_SU_DUNG_DAT.NGUON_KHAC", Number(value));
    setNGUON_NGAN_SACH(updatedNGUON_NGAN_SACH);
    setValue("GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH", updatedNGUON_NGAN_SACH);
  };

  const handleNGUON_KHACBlur = () => {
    if (NGUON_NGAN_SACH < 0) {
      setError("GIA_TRI_SU_DUNG_DAT.NGUON_KHAC", {
        type: "manual",
        message: "Tổng các nguồn vốn phải bằng nguyên giá!",
      });
    } else {
      clearErrors("GIA_TRI_SU_DUNG_DAT.NGUON_KHAC");
    }
  };

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
      <Grid container spacing={1}>
        <Grid item xs={12} md={6}>
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
            Giá trị hao mòn/ khấu hao
          </Typography>

          {/* Nguyên giá */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Nguyên giá <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="number"
            placeholder="đ̲"
            {...register("GIA_TRI_SU_DUNG_DAT.NGUYEN_GIA", {
              required: "Bạn phải nhập giá trị nguyên giá",
            })}
            value={NGUYEN_GIA || ""}
            onChange={handleNGUYEN_GIAChange}
            onBlur={handleNGUYEN_GIABlur}
          />
          {errors?.GIA_TRI_SU_DUNG_DAT?.NGUYEN_GIA && (
            <span className="text-red-500 text-xs">
              {errors?.GIA_TRI_SU_DUNG_DAT?.NGUYEN_GIA.message}
            </span>
          )}

          <Typography
            variant="subtitle2"
            sx={{ fontSize: "14px", fontWeight: "bold", margin: "15px 0" }}
          >
            Trong đó:
          </Typography>

          {/* Nguồn ngân sách */}
          <Typography
            variant="subtitle2"
            sx={{ fontSize: "14px", fontStyle: "italic" }}
          >
            Nguồn ngân sách
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="number"
            placeholder="đ̲"
            {...register("GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH")}
            value={NGUON_NGAN_SACH || ""}
            InputProps={{
              readOnly: true,
              sx: { fontSize: "14px", backgroundColor: "#e9ecef" },
            }}
            disabled
          />

          {/* Nguồn khác */}
          <Typography
            variant="subtitle2"
            sx={{ fontSize: "14px", fontStyle: "italic" }}
          >
            Nguồn khác
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="number"
            placeholder="đ̲"
            {...register("GIA_TRI_SU_DUNG_DAT.NGUON_KHAC")}
            value={NGUON_KHAC || ""}
            onChange={handleNGUON_KHACChange}
            onBlur={handleNGUON_KHACBlur}
          />
          {errors?.GIA_TRI_SU_DUNG_DAT?.NGUON_KHAC && (
            <span className="text-red-500 text-xs">
              {errors?.GIA_TRI_SU_DUNG_DAT?.NGUON_KHAC.message}
            </span>
          )}

          {/* Nguồn khác */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Giá trị còn lại
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="number"
            placeholder="đ̲"
            {...register("GIA_TRI_SU_DUNG_DAT.NGUON_KHAC")}
            value={NGUON_KHAC || ""}
            onChange={handleNGUON_KHACChange}
            onBlur={handleNGUON_KHACBlur}
          />
        </Grid>
        <Grid item xs={12} md={6}>
          {/* Giá trị QSD đất */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Tỷ lệ hao mòn (%)
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="number"
            placeholder="đ̲"
            {...register("GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT")}
            value={GIA_TRI_QUYEN_SD_DAT || ""}
            onChange={handleGIA_TRI_QUYEN_SD_DATChange}
            onBlur={handleGIA_TRI_QUYEN_SD_DATBlur}
            InputProps={{
              readOnly: true,
              sx: { fontSize: "14px", backgroundColor: "#e9ecef" },
            }}
            disabled
          />
          {errors.GIA_TRI_SU_DUNG_DAT?.GIA_TRI_QUYEN_SD_DAT && (
            <span className="text-red-500 text-xs">
              {errors.GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT.message}
            </span>
          )}
        </Grid>
      </Grid>
    </Box>
  );
};

export default GiaTriHaoMon;
