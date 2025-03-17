import {
  Box,
  Checkbox,
  FormControlLabel,
  Grid,
  TextField,
  Typography,
} from "@mui/material";
import React, { useState } from "react";
import { FieldErrors, UseFormRegister } from "react-hook-form";
import { Thongtinchung } from "../validateform/thongtinchung";

interface ThongtintaisanProps {
  register: UseFormRegister<Thongtinchung>;
  errors: FieldErrors<Thongtinchung>;
}
const Hosogiayto = ({ register, errors }: ThongtintaisanProps) => {
  const [disableInputs, setDisableInputs] = useState(false);

  // Xử lý sự kiện khi chọn checkbox
  const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setDisableInputs(event.target.checked);
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
        Hồ sơ,giấy tờ
      </Typography>
      <Grid container spacing={1} sx={{ justifyContent: "space-between" }}>
        {/* Các trường hiện trạng sử dụng */}
        <Grid item xs={2} sx={{ display: "flex", alignItems: "center" }}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Giấy CNQSD đất
          </Typography>
        </Grid>

        <Grid container xs={6} spacing={0}>
          <Grid item xs={0.8}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#f5f5f5",
                  textAlign: "center",
                  borderTopRightRadius: 0, // Bỏ góc bo tròn bên phải
                  borderBottomRightRadius: 0,
                  "& fieldset": {
                    borderTopRightRadius: 0,
                    borderBottomRightRadius: 0,
                  },
                },
                disabled: true,
              }}
              value="Số"
            />
          </Grid>
          <Grid item xs={11}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#fff",
                  borderTopLeftRadius: 0, // Bỏ góc bo tròn bên trái
                  borderBottomLeftRadius: 0,
                  "& fieldset": {
                    borderTopLeftRadius: 0,
                    borderBottomLeftRadius: 0,
                  },
                },
              }}
              {...register("hoSoGiayTo.CNQSD")}
            />
          </Grid>
        </Grid>

        <Grid item xs={3}>
          <TextField
            fullWidth
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("hoSoGiayTo.ngayCap.CNQSD")}
          />
        </Grid>

        <Grid item xs={2} sx={{ display: "flex", alignItems: "center" }}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Quyết định giao đất
          </Typography>
        </Grid>

        <Grid container xs={6} spacing={0}>
          <Grid item xs={0.8}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#f5f5f5",
                  textAlign: "center",
                  borderTopRightRadius: 0, // Bỏ góc bo tròn bên phải
                  borderBottomRightRadius: 0,
                  "& fieldset": {
                    borderTopRightRadius: 0,
                    borderBottomRightRadius: 0,
                  },
                },
                disabled: true,
              }}
              value="Số"
            />
          </Grid>
          <Grid item xs={11}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#fff",
                  borderTopLeftRadius: 0, // Bỏ góc bo tròn bên trái
                  borderBottomLeftRadius: 0,
                  "& fieldset": {
                    borderTopLeftRadius: 0,
                    borderBottomLeftRadius: 0,
                  },
                },
              }}
              {...register("hoSoGiayTo.quyetDinhGiaoDat")}
            />
          </Grid>
        </Grid>

        <Grid item xs={3}>
          <TextField
            fullWidth
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("hoSoGiayTo.ngayCap.quyetDinhGiaoDat")}
          />
        </Grid>

        <Grid item xs={2} sx={{ display: "flex", alignItems: "center" }}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Quyết định cho thuê đất
          </Typography>
        </Grid>

        <Grid container xs={6} spacing={0}>
          <Grid item xs={0.8}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#f5f5f5",
                  textAlign: "center",
                  borderTopRightRadius: 0, // Bỏ góc bo tròn bên phải
                  borderBottomRightRadius: 0,
                  "& fieldset": {
                    borderTopRightRadius: 0,
                    borderBottomRightRadius: 0,
                  },
                },
                disabled: true,
              }}
              value="Số"
            />
          </Grid>
          <Grid item xs={11}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#fff",
                  borderTopLeftRadius: 0, // Bỏ góc bo tròn bên trái
                  borderBottomLeftRadius: 0,
                  "& fieldset": {
                    borderTopLeftRadius: 0,
                    borderBottomLeftRadius: 0,
                  },
                },
              }}
              {...register("hoSoGiayTo.quyetDinhChoThueDat")}
            />
          </Grid>
        </Grid>

        <Grid item xs={3}>
          <TextField
            fullWidth
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("hoSoGiayTo.ngayCap.quyetDinhChoThueDat")}
          />
        </Grid>

        <Grid item xs={2} sx={{ display: "flex", alignItems: "center" }}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Hợp đồng cho thuê đất
          </Typography>
        </Grid>

        <Grid container xs={6} spacing={0}>
          <Grid item xs={0.8}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#f5f5f5",
                  textAlign: "center",
                  borderTopRightRadius: 0, // Bỏ góc bo tròn bên phải
                  borderBottomRightRadius: 0,
                  "& fieldset": {
                    borderTopRightRadius: 0,
                    borderBottomRightRadius: 0,
                  },
                },
                disabled: true,
              }}
              value="Số"
            />
          </Grid>
          <Grid item xs={11}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#fff",
                  borderTopLeftRadius: 0, // Bỏ góc bo tròn bên trái
                  borderBottomLeftRadius: 0,
                  "& fieldset": {
                    borderTopLeftRadius: 0,
                    borderBottomLeftRadius: 0,
                  },
                },
              }}
              {...register("hoSoGiayTo.hopDongChoThueDat")}
            />
          </Grid>
        </Grid>

        <Grid item xs={3}>
          <TextField
            fullWidth
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("hoSoGiayTo.ngayCap.hopDongChoThueDat")}
          />
        </Grid>

        <Grid item xs={2} sx={{ display: "flex", alignItems: "center" }}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Giấy tờ pháp lý khác
          </Typography>
        </Grid>

        <Grid container xs={6}>
          <TextField
            fullWidth
            size="small"
            margin="dense"
            InputProps={{
              sx: {
                fontSize: "14px",
                backgroundColor: "#fff",
              },
            }}
            {...register("hoSoGiayTo.giayToKhac")}
          />
        </Grid>

        <Grid item xs={3}></Grid>
        {/* Checkbox "Không có giấy tờ" */}
        <Grid container spacing={1} alignItems="center" sx={{ mt: 1 }}>
          <Grid item xs={2}></Grid>
          <Grid item xs={7}>
            <FormControlLabel
              control={<Checkbox onChange={handleCheckboxChange} />}
              label={
                <Typography sx={{ fontSize: "14px", fontWeight: "bold" }}>
                  Không có giấy tờ
                </Typography>
              }
            />
          </Grid>
        </Grid>

        {/* Cảnh báo nếu chưa nhập giấy tờ */}
        {!disableInputs && errors.hoSoGiayTo && (
          <Typography
            sx={{ color: "red", fontSize: "14px", mt: 1, textAlign: "center" }}
          >
            Cần phải nhập ít nhất 1 loại giấy tờ
          </Typography>
        )}
      </Grid>
    </Box>
  );
};

export default Hosogiayto;
