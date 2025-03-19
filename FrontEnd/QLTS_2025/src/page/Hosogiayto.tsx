import {
  Box,
  Checkbox,
  FormControlLabel,
  Grid,
  TextareaAutosize,
  TextField,
  Typography,
} from "@mui/material";
import React, { useState } from "react";

import { UseFormRegister, UseFormSetValue } from "react-hook-form";

import { Thongtinchung } from "../validateform/thongtinchung";

interface ThongtintaisanProps {
  register: UseFormRegister<Thongtinchung>;
  setValue: UseFormSetValue<Thongtinchung>;
}

const HO_SO_GIAY_TO = ({ register, setValue }: ThongtintaisanProps) => {
  const [disableInputs, setDisableInputs] = useState(false);

  // Xử lý sự kiện khi chọn checkbox
  const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setDisableInputs(event.target.checked);
    console.log(disableInputs);
    if (!disableInputs) {
      const fields = [
        "CHUNG_NHAN_QUYEN_SD_DAT",
        "QD_GIAO_DAT",
        "QD_CHO_THUE_DAT",
        "HOP_DONG_CHO_THUE_DAT",
        "GIAY_TO_KHAC",
      ] as const;

      fields.forEach((field) => {
        setValue(`HO_SO_GIAY_TO.${field}` as keyof Thongtinchung, "");
        setValue(`HO_SO_GIAY_TO.NGAY_CAP.${field}` as keyof Thongtinchung, "");
      });
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
      {/* Tiêu đề "Hồ sơ,giấy tờ" */}
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
      <Grid container sx={{ justifyContent: "space-between" }}>
        {/* Các trường hiện trạng sử dụng */}
        <Grid item xs={2} sx={{ display: "flex", alignItems: "center" }}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Giấy CHUNG_NHAN_QUYEN_SD_DAT đất
          </Typography>
        </Grid>

        <Grid container xs={6}>
          <Grid item xs={0.8}>
            <TextField
              fullWidth
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: "#e3e3e3",
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
              disabled={disableInputs}
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: disableInputs ? "#e3e3e3" : "#fff",
                  borderTopLeftRadius: 0, // Bỏ góc bo tròn bên trái
                  borderBottomLeftRadius: 0,
                  "& fieldset": {
                    borderTopLeftRadius: 0,
                    borderBottomLeftRadius: 0,
                  },
                },
              }}
              {...register("HO_SO_GIAY_TO.CHUNG_NHAN_QUYEN_SD_DAT")}
            />
          </Grid>
        </Grid>

        <Grid item xs={3}>
          <TextField
            fullWidth
            disabled={disableInputs}
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("HO_SO_GIAY_TO.NGAY_CAP.CHUNG_NHAN_QUYEN_SD_DAT")}
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
                  backgroundColor: "#e3e3e3",
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
              disabled={disableInputs}
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: disableInputs ? "#e3e3e3" : "#fff",
                  borderTopLeftRadius: 0, // Bỏ góc bo tròn bên trái
                  borderBottomLeftRadius: 0,
                  "& fieldset": {
                    borderTopLeftRadius: 0,
                    borderBottomLeftRadius: 0,
                  },
                },
              }}
              {...register("HO_SO_GIAY_TO.QD_GIAO_DAT")}
            />
          </Grid>
        </Grid>

        <Grid item xs={3}>
          <TextField
            fullWidth
            disabled={disableInputs}
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("HO_SO_GIAY_TO.NGAY_CAP.QD_GIAO_DAT")}
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
                  backgroundColor: "#e3e3e3",
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
              disabled={disableInputs}
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: disableInputs ? "#e3e3e3" : "#fff",
                  borderTopLeftRadius: 0, // Bỏ góc bo tròn bên trái
                  borderBottomLeftRadius: 0,
                  "& fieldset": {
                    borderTopLeftRadius: 0,
                    borderBottomLeftRadius: 0,
                  },
                },
              }}
              {...register("HO_SO_GIAY_TO.QD_CHO_THUE_DAT")}
            />
          </Grid>
        </Grid>

        <Grid item xs={3}>
          <TextField
            fullWidth
            disabled={disableInputs}
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("HO_SO_GIAY_TO.NGAY_CAP.QD_CHO_THUE_DAT")}
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
                  backgroundColor: "#e3e3e3",
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
              disabled={disableInputs}
              size="small"
              margin="dense"
              InputProps={{
                sx: {
                  fontSize: "14px",
                  backgroundColor: disableInputs ? "#e3e3e3" : "#fff",
                  borderTopLeftRadius: 0, // Bỏ góc bo tròn bên trái
                  borderBottomLeftRadius: 0,
                  "& fieldset": {
                    borderTopLeftRadius: 0,
                    borderBottomLeftRadius: 0,
                  },
                },
              }}
              {...register("HO_SO_GIAY_TO.HOP_DONG_CHO_THUE_DAT")}
            />
          </Grid>
        </Grid>

        <Grid item xs={3}>
          <TextField
            fullWidth
            disabled={disableInputs}
            type="date"
            size="small"
            margin="dense"
            InputLabelProps={{ shrink: true }}
            InputProps={{ sx: { fontSize: "14px" } }}
            {...register("HO_SO_GIAY_TO.NGAY_CAP.HOP_DONG_CHO_THUE_DAT")}
          />
        </Grid>

        <Grid item xs={2} sx={{ display: "flex", alignItems: "center" }}>
          <Typography variant="subtitle2" sx={{ fontSize: "14px" }}>
            Giấy tờ pháp lý khác
          </Typography>
        </Grid>

        <Grid container xs={6} spacing={0}>
          <Grid item xs={11.8} sx={{ mt: 1 }}>
            <TextareaAutosize
              {...register("HO_SO_GIAY_TO.GIAY_TO_KHAC")}
              disabled={disableInputs}
              minRows={1} // Số dòng tối thiểu
              style={{
                width: "100%", // Đảm bảo full-width theo cha
                fontSize: "14px",
                backgroundColor: disableInputs ? "#e3e3e3" : "#fff",
                padding: "8px",
                borderRadius: "4px",
                border: "1px solid #ccc",
                resize: "vertical", // Giới hạn resize theo chiều dọc
                boxSizing: "border-box", // Đảm bảo kích thước đúng
              }}
              onFocus={(e) => {
                e.target.style.border = "1px solid #1976d2";
                e.target.style.outline = "none"; // Bỏ viền mặc định của trình duyệt
              }}
              onBlur={(e) => (e.target.style.border = "1px solid #ccc")} // Trở về màu cũ khi mất focus
            />
          </Grid>
        </Grid>

        <Grid item xs={3}></Grid>
        {/* Checkbox "Không có giấy tờ" */}
        <Grid container justifyContent="center" sx={{ mt: 1 }}>
          <Grid item xs={7}>
            <FormControlLabel
              control={
                <Checkbox onChange={handleCheckboxChange} sx={{ p: 0.5 }} />
              }
              label={
                <Typography sx={{ fontSize: "14px", fontWeight: "bold" }}>
                  Không có giấy tờ
                </Typography>
              }
              sx={{ display: "flex", alignItems: "center", width: "100%" }}
            />
          </Grid>
          {!disableInputs && (
            <Grid item xs={7}>
              <Typography sx={{ color: "red", fontSize: "14px" }}>
                Cần phải nhập ít nhất 1 loại giấy tờ
              </Typography>
            </Grid>
          )}
        </Grid>
      </Grid>
    </Box>
  );
};

export default HO_SO_GIAY_TO;
