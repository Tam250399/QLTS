import { Box, Grid, TextField, Typography } from "@mui/material";
import {
  FieldErrors,
  UseFormClearErrors,
  UseFormGetValues,
  UseFormRegister,
  UseFormSetError,
  UseFormSetValue,
} from "react-hook-form";
import { useEffect, useState } from "react";
import { ThongTinNha } from "../../validateform/thongtinnha";
import formatCurrencyVND from "../../components/Format/FormatVND";
import {
  handleChangeGiaTriConLai,
  handleChangeNguonKhac,
  handleChangeNguyenGia,
} from "../../components/form/HandleChangeNha";

interface GiaTriHaoMonProps {
  register: UseFormRegister<ThongTinNha>;
  errors: FieldErrors<ThongTinNha>;
  setValue: UseFormSetValue<ThongTinNha>;
  setError: UseFormSetError<ThongTinNha>;
  clearErrors: UseFormClearErrors<ThongTinNha>;
  getValues: UseFormGetValues<ThongTinNha>;
}

const GiaTriHaoMon = ({
  register,
  errors,
  setValue,
  getValues,
}: GiaTriHaoMonProps) => {
  const [displayValues, setDisplayValues] = useState<Record<string, string>>(
    {}
  );
  const { NGUON_KHAC, GIA_TRI_CON_LAI, NGUON_NGAN_SACH } =
    getValues("GIA_TRI_HAO_MON") || {};
  const NGUYEN_GIA = getValues("GIA_TRI_HAO_MON.NGUYEN_GIA");
  const [nguonKhacError, setNguonKhacError] = useState<string | undefined>(
    undefined
  );
  const [nguonKhac, setNguonKhac] = useState(0);
  const [nguyenGia, setNguyenGia] = useState(0);

  useEffect(() => {
    const fields = {
      NGUYEN_GIA,
      GIA_TRI_CON_LAI,
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
  }, [NGUYEN_GIA, GIA_TRI_CON_LAI, NGUON_KHAC, NGUON_NGAN_SACH]);

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
      <Grid container spacing={4}>
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
            type="text"
            placeholder="đ̲"
            value={displayValues.NGUYEN_GIA || ""}
            {...register("GIA_TRI_HAO_MON.NGUYEN_GIA", {
              required: "Bạn phải nhập nguyên giá",
              validate: (value) => {
                setNguyenGia(value);
                if (!value) {
                  return "Bạn phải nhập nguyên giá";
                }
                if (!isNaN(nguonKhac) && nguonKhac > value) {
                  setNguonKhacError(
                    "Tổng nguồn vốn không được lớn hơn nguyên giá"
                  );
                  setValue("GIA_TRI_HAO_MON.NGUON_NGAN_SACH", 0, {
                    shouldValidate: true,
                  });
                  return true;
                }
                setValue("GIA_TRI_HAO_MON.NGUON_NGAN_SACH", value - nguonKhac, {
                  shouldValidate: true,
                });
                setNguonKhacError(undefined);
                return true;
              },
            })}
            onChange={handleChangeNguyenGia(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, NGUYEN_GIA: value }))
            )}
            error={!!errors.GIA_TRI_HAO_MON?.NGUYEN_GIA}
            helperText={errors.GIA_TRI_HAO_MON?.NGUYEN_GIA?.message}
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
            {...register("GIA_TRI_HAO_MON.NGUON_NGAN_SACH")}
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
            {...register("GIA_TRI_HAO_MON.NGUON_KHAC", {
              validate: (value) => {
                setNguonKhac(value);
                if (!isNaN(nguyenGia) && Number(value) > nguyenGia) {
                  setNguonKhacError(
                    "Tổng nguồn vốn không được lớn hơn nguyên giá"
                  );
                  setValue("GIA_TRI_HAO_MON.NGUON_NGAN_SACH", 0, {
                    shouldValidate: true,
                  });
                  return true;
                }
                setValue(
                  "GIA_TRI_HAO_MON.NGUON_NGAN_SACH",
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

          {/* Gía trị còn lại */}
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Giá trị còn lại
          </Typography>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            type="text"
            placeholder="đ̲"
            value={displayValues.GIA_TRI_CON_LAI || ""}
            onChange={handleChangeGiaTriConLai(setValue, (value) =>
              setDisplayValues((prev) => ({ ...prev, GIA_TRI_CON_LAI: value }))
            )}
          />
        </Grid>
        <Grid item xs={12} md={3}>
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
            {...register("GIA_TRI_HAO_MON.TY_LE_HAO_MON")}
            InputProps={{
              readOnly: true,
              sx: { fontSize: "14px", backgroundColor: "#e9ecef" },
            }}
            disabled
          />
          {errors.GIA_TRI_HAO_MON?.TY_LE_HAO_MON && (
            <span className="text-red-500 text-xs">
              {errors.GIA_TRI_HAO_MON?.TY_LE_HAO_MON?.message}
            </span>
          )}
        </Grid>
      </Grid>
    </Box>
  );
};

export default GiaTriHaoMon;
