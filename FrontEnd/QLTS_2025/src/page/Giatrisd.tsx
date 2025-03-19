import { Box, Grid, TextField, Typography } from "@mui/material";
import {
  FieldErrors,
  UseFormClearErrors,
  UseFormRegister,
  UseFormSetError,
  UseFormSetValue,
} from "react-hook-form";
import { Thongtinchung } from "../validateform/thongtinchung";
import { useState } from "react";

interface GiaTriSuDungDatProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
  setValue: UseFormSetValue<Thongtinchung>;
  setError: UseFormSetError<Thongtinchung>;
  clearErrors: UseFormClearErrors<Thongtinchung>;
}

const Giatrisd = ({
  register,
  errors,
  setValue,
  setError,
  clearErrors,
}: GiaTriSuDungDatProps) => {
  const [giaTriQSD, setGiaTriQSD] = useState<number>(0);
  const [nguonKhac, setNguonKhac] = useState<number>(0);
  const [nguyenGia, setNguyenGia] = useState<number>(0);
  const [nguonNganSach, setNguonNganSach] = useState<number>(0);

  const handleGiaTriQSDChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    let value = parseFloat(event.target.value) || 0;

    if (value > 0 && nguyenGia > 0 && value > nguyenGia) {
      setError("giaTriSdDat.giaTriQSD", {
        type: "manual",
        message:
          "Giá trị quyền sử dụng đất không được lớn hơn nguyên giá, đề nghị kiểm tra lại!",
      });
    } else {
      setGiaTriQSD(value);
      setValue("giaTriSdDat.giaTriQSD", value, { shouldValidate: true });
    }
  };

  const handleNguyenGiaChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    let value = parseFloat(event.target.value) || 0;

    setNguyenGia(value);
    setValue("giaTriSdDat.nguyenGia", value, { shouldValidate: true });
    const updatedNguonNganSach = Math.max(0, value - nguonKhac);
    setNguonNganSach(updatedNguonNganSach);
    setValue("giaTriSdDat.nguonNganSach", updatedNguonNganSach);
  };

  const handleNguyenGiaBlur = () => {
    if (nguyenGia > 0 && giaTriQSD > 0 && giaTriQSD > nguyenGia) {
      setError("giaTriSdDat.giaTriQSD", {
        type: "manual",
        message:
          "Giá trị quyền sử dụng đất không được lớn hơn nguyên giá, đề nghị kiểm tra lại!",
      });
    } else {
      clearErrors("giaTriSdDat.giaTriQSD");
    }
  };

  // Khi nhập Nguồn khác, cập nhật Nguồn ngân sách ngay
  const handleNguonKhacChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    let value = parseFloat(event.target.value) || 0;

    // Cập nhật nguồn ngân sách
    const updatedNguonNganSach = Math.max(-1, nguyenGia - value);
    if (updatedNguonNganSach < 0) {
      setError("giaTriSdDat.nguonKhac", {
        type: "manual",
        message: "Tổng các nguồn vốn phải bằng nguyên giá!",
      });
    } else {
      setNguonKhac(value);
      setValue("giaTriSdDat.nguonKhac", value);
      setNguonNganSach(updatedNguonNganSach);
      setValue("giaTriSdDat.nguonNganSach", updatedNguonNganSach);
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
            type="number"
            placeholder="đ̲"
            {...register("giaTriSdDat.giaTriQSD", {
              required: "Bạn phải nhập giá trị quyền sử dụng đất",
            })}
            value={giaTriQSD || ""}
            onChange={handleGiaTriQSDChange}
          />
          {errors.giaTriSdDat?.giaTriQSD && (
            <span className="text-red-500 text-xs">
              {errors.giaTriSdDat.giaTriQSD.message}
            </span>
          )}

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
            {...register("giaTriSdDat.nguyenGia", {
              required: "Bạn phải nhập giá trị nguyên giá",
            })}
            value={nguyenGia || ""}
            onChange={handleNguyenGiaChange}
            onBlur={handleNguyenGiaBlur}
          />
          {errors?.giaTriSdDat?.nguyenGia && (
            <span className="text-red-500 text-xs">
              {errors?.giaTriSdDat?.nguyenGia.message}
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
            {...register("giaTriSdDat.nguonNganSach")}
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
            {...register("giaTriSdDat.nguonKhac")}
            value={nguonKhac || ""}
            onChange={handleNguonKhacChange}
          />
          {errors?.giaTriSdDat?.nguonKhac && (
            <span className="text-red-500 text-xs">
              {errors?.giaTriSdDat?.nguonKhac.message}
            </span>
          )}
        </Grid>
      </Grid>
    </Box>
  );
};

export default Giatrisd;
