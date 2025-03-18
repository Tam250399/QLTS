import { Box, Grid, TextField, Typography } from "@mui/material";
import { FieldErrors, UseFormRegister, UseFormSetValue } from "react-hook-form";
import { Thongtinchung } from "../validateform/thongtinchung";
import { useState } from "react";

interface GiaTriSuDungDatProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
  setValue: UseFormSetValue<Thongtinchung>;
}
const Giatrisd = ({ register, errors, setValue }: GiaTriSuDungDatProps) => {
  const [giaTriQSD, setGiaTriQSD] = useState<number>(0);
  const [nguonKhac, setNguonKhac] = useState<number>(0);
  const [nguyenGia, setNguyenGia] = useState<number>(0);
  const [nguonNganSach, setNguonNganSach] = useState<number>(0);

  const handleGiaTriQSDChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = parseFloat(event.target.value) || 0;
    setGiaTriQSD(value);
    setValue("giaTriSdDat.giaTriQSD", value);

    const updatedNguonNganSach = value - nguonKhac;
    setNguonNganSach(updatedNguonNganSach);
    setValue("giaTriSdDat.nguonNganSach", updatedNguonNganSach);
  };

  const handleNguyenGiaChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = parseFloat(event.target.value) || 0;
    setNguyenGia(value);
    setValue("giaTriSdDat.nguyenGia", value);
  };

  // Khi mất focus, chỉ cập nhật nếu giá trị chưa có
  const handleGiaTriQSDBlur = () => {
    setNguyenGia((prev) => {
      if (prev === 0) {
        setValue("giaTriSdDat.nguyenGia", giaTriQSD);
        return giaTriQSD;
      }
      return prev;
    });
  };

  const handleNguyenGiaBlur = () => {
    setGiaTriQSD((prev) => {
      if (prev === 0) {
        setValue("giaTriSdDat.giaTriQSD", nguyenGia);
        return nguyenGia;
      }
      return prev;
    });
  };

  // Khi nhập Nguồn khác, cập nhật Nguồn ngân sách ngay
  const handleNguonKhacChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = parseFloat(event.target.value) || 0;
    setNguonKhac(value);

    const updatedNguonNganSach = giaTriQSD - value > 0 ? giaTriQSD - value : 0;
    setNguonNganSach(updatedNguonNganSach);
    setValue("giaTriSdDat.nguonNganSach", updatedNguonNganSach);
    setValue("giaTriSdDat.nguonKhac", value);
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
            {...register("giaTriSdDat.giaTriQSD", { required: true })}
            value={giaTriQSD || ""}
            onChange={handleGiaTriQSDChange}
            onBlur={handleGiaTriQSDBlur}
          />
          {errors.giaTriSdDat?.giaTriQSD && (
            <span className="text-red-500 text-xs">
              Bạn phải nhập giá trị quyền sử dụng đất
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
            {...register("giaTriSdDat.nguyenGia", { required: true })}
            value={nguyenGia || ""}
            onChange={handleNguyenGiaChange}
            onBlur={handleNguyenGiaBlur}
          />
          {errors.giaTriSdDat?.giaTriQSD && (
            <span className="text-red-500 text-xs">
              Bạn phải nhập nguyên giá
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
        </Grid>
      </Grid>
    </Box>
  );
};

export default Giatrisd;
