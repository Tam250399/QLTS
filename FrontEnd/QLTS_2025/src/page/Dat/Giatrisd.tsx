import { Box, Grid, TextField, Typography } from "@mui/material";
import {
  FieldErrors,
  UseFormClearErrors,
  UseFormGetValues,
  UseFormRegister,
  UseFormSetError,
  UseFormSetValue,
} from "react-hook-form";
import { Thongtinchung } from "../../validateform/thongtinchung";
import { useEffect, useState } from "react";
import formatCurrencyVND from "../../components/Format/FormatVND";
import {
  handleChangeGiaTriQSD,
  handleChangeNguonKhac,
  handleChangeNguyenGia,
} from "../../components/form/HandleChageDat";

interface GiaTriSuDungDatProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
  setValue: UseFormSetValue<Thongtinchung>;
  setError: UseFormSetError<Thongtinchung>;
  clearErrors: UseFormClearErrors<Thongtinchung>;
  getValues: UseFormGetValues<Thongtinchung>;
}

const Giatrisd = ({
  register,
  errors,
  setValue,
  getValues,
}: GiaTriSuDungDatProps) => {
  const [displayValues, setDisplayValues] = useState<Record<string, string>>(
    {}
  );
  const { NGUON_KHAC, GIA_TRI_QUYEN_SD_DAT, NGUON_NGAN_SACH } =
    getValues("GIA_TRI_SU_DUNG_DAT") || {};
  const NGUYEN_GIA = getValues("NGUYEN_GIA");
  const [nguonKhacError, setNguonKhacError] = useState<string | undefined>(
    undefined
  );
  const [qsdError, setQsdError] = useState<string | undefined>(undefined);
  const [nguonKhac, setNguonKhac] = useState(0);
  const [nguyenGia, setNguyenGia] = useState(0);
  const [qsdDat, setQsdDat] = useState(0);

  useEffect(() => {
    const fields = {
      NGUYEN_GIA,
      GIA_TRI_QUYEN_SD_DAT,
      NGUON_KHAC,
      NGUON_NGAN_SACH,
    };

    const newDisplayValues: Record<string, string> = {};
    Object.entries(fields).forEach(([fieldName, value]) => {
      if (value !== undefined) {
        newDisplayValues[fieldName] = formatCurrencyVND(value, "đ̲");
      }
    });

    setDisplayValues((prev) => ({ ...prev, ...newDisplayValues }));
  }, [NGUYEN_GIA, GIA_TRI_QUYEN_SD_DAT, NGUON_KHAC, NGUON_NGAN_SACH]);

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
            Giá trị quyền sử dụng đất
          </Typography>

          {/* Giá trị QSD đất */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Giá trị QSD đất <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="đ̲"
            value={displayValues.GIA_TRI_QUYEN_SD_DAT || ""}
            {...register("GIA_TRI_SU_DUNG_DAT.GIA_TRI_QUYEN_SD_DAT", {
              required: "Bạn phải nhập quyền sử dụng đất",
              validate: (value) => {
                setQsdDat(value);
                if (!value) {
                  return "Bạn phải nhập quyền sử dụng đất";
                }
                if (
                  !isNaN(nguyenGia) &&
                  Number(value) > nguyenGia &&
                  nguyenGia > 0
                ) {
                  setQsdError(
                    "Quyền sử dụng đất không được lớn hơn nguyên giá"
                  );
                  return true;
                }
                setQsdError(undefined);
                return true;
              },
            })}
            onChange={handleChangeGiaTriQSD(setValue, (value) =>
              setDisplayValues((prev) => ({
                ...prev,
                GIA_TRI_QUYEN_SD_DAT: value,
              }))
            )}
            error={
              !!errors.GIA_TRI_SU_DUNG_DAT?.GIA_TRI_QUYEN_SD_DAT || !!qsdError
            }
            helperText={
              errors.GIA_TRI_SU_DUNG_DAT?.GIA_TRI_QUYEN_SD_DAT?.message ||
              qsdError
            }
          />
          {/* Nguyên giá */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Nguyên giá <span style={{ color: "red" }}>*</span>
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="đ̲"
            value={displayValues.NGUYEN_GIA || ""}
            {...register("NGUYEN_GIA", {
              required: "Bạn phải nhập nguyên giá",
              validate: (value) => {
                setNguyenGia(value);
                if (!value) {
                  return "Bạn phải nhập nguyên giá";
                }
                if (!isNaN(qsdDat) && qsdDat > value) {
                  setQsdError(
                    "Quyền sử dụng đất không được lớn hơn nguyên giá"
                  );
                  return true;
                }
                if (!isNaN(nguonKhac) && nguonKhac > value) {
                  setNguonKhacError(
                    "Tổng nguồn vốn không được lớn hơn nguyên giá"
                  );
                  setValue("GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH", 0, {
                    shouldValidate: true,
                  });
                  return true;
                }
                setValue(
                  "GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH",
                  value - nguonKhac,
                  {
                    shouldValidate: true,
                  }
                );
                setNguonKhacError(undefined);
                setQsdError(undefined);
                return true;
              },
            })}
            onChange={handleChangeNguyenGia(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, NGUYEN_GIA: value }))
            )}
            error={!!errors.NGUYEN_GIA}
            helperText={errors.NGUYEN_GIA?.message}
          />

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
            type="text"
            placeholder="đ̲"
            value={displayValues.NGUON_NGAN_SACH || ""}
            {...register("GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH")}
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
            type="text"
            placeholder="đ̲"
            value={displayValues.NGUON_KHAC || ""}
            {...register("GIA_TRI_SU_DUNG_DAT.NGUON_KHAC", {
              validate: (value) => {
                setNguonKhac(value);
                if (
                  !isNaN(nguyenGia) &&
                  Number(value) > nguyenGia &&
                  nguyenGia > 0
                ) {
                  setNguonKhacError(
                    "Tổng nguồn vốn không được lớn hơn nguyên giá"
                  );
                  setValue("GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH", 0, {
                    shouldValidate: true,
                  });
                  return true;
                }
                setValue(
                  "GIA_TRI_SU_DUNG_DAT.NGUON_NGAN_SACH",
                  isNaN(nguyenGia) ? 0 : nguyenGia - value,
                  {
                    shouldValidate: true,
                  }
                );
                setNguonKhacError(undefined);
                return true;
              },
            })}
            onChange={handleChangeNguonKhac(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, NGUON_KHAC: value }))
            )}
            error={!!nguonKhacError}
            helperText={nguonKhacError}
          />
        </Grid>
      </Grid>
    </Box>
  );
};

export default Giatrisd;
