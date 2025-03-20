import {
  Box,
  Checkbox,
  FormControlLabel,
  FormGroup,
  Grid,
  TextField,
  Typography,
} from "@mui/material";
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
  const [nguonKhac, setNguonKhac] = useState<number>(0);
  const [nguyenGia, setNguyenGia] = useState<number>(0);
  const [nguonNganSach, setNguonNganSach] = useState<number>(0);
  const [isCalculateKH, setIsCalculateKH] = useState(false);

  const handleNguyenGiaChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = parseFloat(event.target.value) || 0;

    setNguyenGia(value);
    setValue("GIA_TRI_HAO_MON.NGUYEN_GIA", value, { shouldValidate: true });
    const updatedNGUON_NGAN_SACH = Math.max(0, value - nguonKhac);
    setNguonNganSach(updatedNGUON_NGAN_SACH);
    setValue("GIA_TRI_HAO_MON.NGUON_NGAN_SACH", updatedNGUON_NGAN_SACH);
  };

  const handleNguyenGiaBlur = () => {
    if (nguonNganSach < 0) {
      setError("GIA_TRI_HAO_MON.NGUON_KHAC", {
        type: "manual",
        message: "Tổng các nguồn vốn phải bằng nguyên giá!",
      });
    } else {
      clearErrors("GIA_TRI_HAO_MON.NGUON_KHAC");
    }
  };

  // Khi nhập Nguồn khác, cập nhật Nguồn ngân sách ngay
  const handleNguonKhacChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = parseFloat(event.target.value) || 0;

    // Cập nhật nguồn ngân sách
    const updatedNGUON_NGAN_SACH = Math.max(-1, nguyenGia - value);
    setNguonKhac(value);
    setValue("GIA_TRI_HAO_MON.NGUON_KHAC", Number(value));
    setNguonNganSach(updatedNGUON_NGAN_SACH);
    setValue("GIA_TRI_HAO_MON.NGUON_NGAN_SACH", updatedNGUON_NGAN_SACH);
  };

  const handleNguonKhacBlur = () => {
    if (nguonNganSach < 0) {
      setError("GIA_TRI_HAO_MON.NGUON_KHAC", {
        type: "manual",
        message: "Tổng các nguồn vốn phải bằng nguyên giá!",
      });
    } else {
      clearErrors("GIA_TRI_HAO_MON.NGUON_KHAC");
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
            type="number"
            placeholder="đ̲"
            {...register("GIA_TRI_HAO_MON.NGUYEN_GIA", {
              required: "Bạn phải nhập giá trị nguyên giá",
            })}
            value={nguyenGia || ""}
            onChange={handleNguyenGiaChange}
            onBlur={handleNguyenGiaBlur}
          />
          {errors?.GIA_TRI_HAO_MON?.NGUYEN_GIA && (
            <span className="text-red-500 text-xs">
              {errors?.GIA_TRI_HAO_MON?.NGUYEN_GIA.message}
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
            {...register("GIA_TRI_HAO_MON.NGUON_NGAN_SACH")}
            value={nguonNganSach || ""}
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
            {...register("GIA_TRI_HAO_MON.NGUON_KHAC")}
            value={nguonKhac || ""}
            onChange={handleNguonKhacChange}
            onBlur={handleNguonKhacBlur}
          />
          {errors?.GIA_TRI_HAO_MON?.NGUON_KHAC && (
            <span className="text-red-500 text-xs">
              {errors?.GIA_TRI_HAO_MON?.NGUON_KHAC.message}
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
            {...register("GIA_TRI_HAO_MON.GIA_TRI_CON_LAI")}
          />
          {errors?.GIA_TRI_HAO_MON?.GIA_TRI_CON_LAI && (
            <span className="text-red-500 text-xs">
              {errors?.GIA_TRI_HAO_MON?.GIA_TRI_CON_LAI.message}
            </span>
          )}
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
          <FormGroup>
            <FormControlLabel
              control={
                <Checkbox
                  checked={isCalculateKH}
                  onChange={(e) => setIsCalculateKH(e.target.checked)}
                />
              }
              label={
                <Typography sx={{ fontWeight: 600 }}>Tính khấu hao</Typography>
              }
            />
          </FormGroup>

          {isCalculateKH && (
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                  Tỷ lệ KH theo QĐ (%) <span style={{ color: "red" }}>*</span>
                </Typography>
                <TextField
                  fullWidth
                  size="small"
                  margin="dense"
                  type="number"
                />
              </Grid>

              <Grid item xs={12}>
                <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                  KH tháng theo QĐ <span style={{ color: "red" }}>*</span>
                </Typography>
                <TextField
                  fullWidth
                  size="small"
                  margin="dense"
                  type="number"
                />
              </Grid>

              <Grid item xs={12}>
                <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                  Tỷ lệ % NG tính KH <span style={{ color: "red" }}>*</span>
                </Typography>
                <TextField
                  fullWidth
                  size="small"
                  margin="dense"
                  type="number"
                />
              </Grid>

              <Grid item xs={12}>
                <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
                  Ngày bắt đầu tính KH <span style={{ color: "red" }}>*</span>
                </Typography>
                <TextField
                  fullWidth
                  size="small"
                  margin="dense"
                  type="date"
                  InputLabelProps={{ shrink: true }}
                />
              </Grid>
            </Grid>
          )}
        </Grid>
      </Grid>
    </Box>
  );
};

export default GiaTriHaoMon;
